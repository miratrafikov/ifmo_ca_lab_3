using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.ContextInitializer;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.PatternMatcher;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Context
    {
        private static Dictionary<string, IPattern> s_patterns = new Dictionary<string, IPattern>();
        public static readonly List<(IElement, IElement)> s_context = GetInitialContext();

        public static void AddRule(IElement lhs, IElement rhs)
        {
            s_context.Add((lhs, rhs));
        }

        public static void AddRules(List<(IElement, IElement)> rules)
        {
            foreach (var rule in rules)
            {
                s_context.Add((rule.Item1, rule.Item2));
            }
        }

        public static IElement GetElement(IElement element)
        {
            ClearPatterns();
            foreach (var rule in s_context)
            {
                var (lhs, rhs) = rule;
                var matchResult = Matches(lhs, element);
                if (matchResult.Success == true)
                {
                    return GetRhs((IElement)matchResult.Value, rhs);
                }
            }
            return element;
        }


        private static void ClearPatterns()
        {
            foreach (var rule in s_context)
            {
                var (lhs, rhs) = rule;
                lhs = ClearPatterns(lhs);
                rhs = ClearPatterns(rhs);
            }
        }

        private static IElement ClearPatterns(IElement lhs)
        {
            switch (lhs)
            {
                case IntegerPattern integer:
                    return new IntegerPattern(integer.Name.Value);
                case ElementPattern element:
                    return new ElementPattern(element.Name.Value);
                case NullableSequencePattern seq:
                    return new NullableSequencePattern(seq.Name.Value);
                case Expression exp:
                    var operands = new List<IElement>();
                    foreach (var o in exp.Operands)
                    {
                        operands.Add(ClearPatterns(o));
                    }
                    exp.Operands = operands;
                    return exp;
                default:
                    return lhs;
            }
        }

        private static IElement GetRhs(IElement lhs, IElement rhs)
        {
            IntegerPattern left = null, right = null;

            #region Conditions

            if (lhs.GetHead().Equals(new Symbol("equals")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element == right.Element ? new Symbol("true") : new Symbol("false");
            }

            if (lhs.GetHead().Equals(new Symbol("nequals")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element != right.Element ? new Symbol("true") : new Symbol("false");
            }

            if (lhs.GetHead().Equals(new Symbol("greater")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element.Value > right.Element.Value ? new Symbol("true") : new Symbol("false");
            }
            
            if (lhs.GetHead().Equals(new Symbol("greatere")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element.Value >= right.Element.Value ? new Symbol("true") : new Symbol("false");
            }

            if (lhs.GetHead().Equals(new Symbol("less")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element.Value < right.Element.Value ? new Symbol("true") : new Symbol("false");
            }

            if (lhs.GetHead().Equals(new Symbol("lesse")))
            {
                left = (IntegerPattern)((Expression)lhs).Operands[0];
                right = (IntegerPattern)((Expression)lhs).Operands[1];
                return left.Element.Value <= right.Element.Value ? new Symbol("true") : new Symbol("false");
            }

            #endregion

            #region Hardcode

            if (lhs is Expression expr &&
                expr.Operands.Count == 5 &&
                expr.Operands[0] is NullableSequencePattern seq1 &&
                expr.Operands[1] is IntegerPattern int1 &&
                expr.Operands[2] is NullableSequencePattern seq2 &&
                expr.Operands[3] is IntegerPattern int2 &&
                expr.Operands[4] is NullableSequencePattern seq3)
            {
                Integer val;
                if (expr.Head.Equals(new Symbol(nameof(sum))))
                {
                    val = new Integer(int1.Element.Value + int2.Element.Value);
                }
                else
                if (expr.Head.Equals(new Symbol(nameof(mul))))
                {
                    val = new Integer(int1.Element.Value * int2.Element.Value);
                }
                else
                if (expr.Head.Equals(new Symbol("pow")))
                {
                    val = new Integer((int)Math.Pow(int1.Element.Value, int2.Element.Value));
                }
                else
                {
                    val = null;
                }

                if (!(val is null))
                {
                    var operands = seq1.Operands;
                    operands = operands.Concat(seq2.Operands).ToList();
                    operands.Add(val);
                    operands = operands.Concat(seq3.Operands).ToList();
                    var exp = new Expression(expr.Head, operands); ;
                    exp.Operands.RemoveAll(o => o is NullableSequencePattern n &&
                                                n.Operands.Count == 0);
                    return exp;
                }
                else
                {
                    return new Expression();
                }
            }
            if (lhs is Expression pow &&
                pow.Operands.Count == 5 &&
                pow.Operands[0] is NullableSequencePattern seq4 &&
                pow.Operands[1] is ElementPattern el &&
                pow.Operands[2] is NullableSequencePattern seq5 &&
                pow.Operands[3] is IntegerPattern int3 &&
                pow.Operands[4] is NullableSequencePattern seq6)
            {
                return new Expression(nameof(mul), Enumerable.Repeat(el.Element, int3.Element.Value).ToList());
            }

            #endregion

            s_patterns = new Dictionary<string, IPattern>();
            PatternsSetUp(lhs);
            return ApplyPatterns(rhs);
        }

        private static void PatternsSetUp(IElement lhs)
        {
            switch (lhs)
            {
                case IPattern p:
                    if (!s_patterns.ContainsKey(p.Name.Value))
                    {
                        s_patterns.Add(p.Name.Value, p);
                    }
                    break;

                case Expression e:
                    foreach (var o in e.Operands)
                    {
                        PatternsSetUp(o);
                    }
                    break;

                default:
                    break;
            }
        }

        private static IElement ApplyPatterns(IElement rhs)
        {
            IElement result = null;
            IPattern pattern = null;
            switch (rhs)
            {
                case IPattern p when (!s_patterns.ContainsKey(p.Name.Value)):
                    result = rhs;
                    break;
                case IPattern p when p.GetType() != s_patterns[p.Name.Value].GetType():
                    throw new PatternsDontMatchException();
                case IntegerPattern integer:
                    pattern = s_patterns[integer.Name.Value];
                    result = ((IntegerPattern)pattern).Element;
                    break;

                case ElementPattern element:
                    pattern = s_patterns[element.Name.Value];
                    result = ((ElementPattern)pattern).Element;
                    break;

                case Expression expr:
                    result = new Expression(expr.Head);
                    foreach (var operand in expr.Operands)
                    {
                        if (operand is NullableSequencePattern seq)
                        {
                            if (s_patterns.ContainsKey(seq.Name.Value))
                            {
                                pattern = s_patterns[seq.Name.Value];
                                if (!(pattern is NullableSequencePattern))
                                {
                                    throw new PatternsDontMatchException();
                                }
                                if (((NullableSequencePattern)pattern).Operands.Count > 0)
                                {
                                    var operands = ((NullableSequencePattern)pattern).Operands;
                                    ((Expression)result).Operands.AddRange(operands);
                                }
                            }
                        }
                        else
                        {
                            ((Expression)result).Operands.Add(ApplyPatterns(operand));
                        }
                    }
                    break;

                default:
                    result = rhs;
                    break;
            }
            return result;
        }
    }
}
