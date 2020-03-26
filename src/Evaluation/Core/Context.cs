using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.PatternMatcher;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Context
    {
        private static Dictionary<string, IPattern> Patterns = new Dictionary<string, IPattern>();
        private static List<(IElement, IElement)> _context = new List<(IElement, IElement)>();

        public static void AddRule(IElement lhs, IElement rhs)
        {
            _context.Add((lhs, rhs));
        }

        public static void AddRules(List<(IElement, IElement)> rules)
        {
            foreach (var rule in rules)
            {
                _context.Add((rule.Item1, rule.Item2));
            }
        }

        public static IElement GetElement(IElement element)
        {
            foreach (var rule in _context)
            {
                var (lhs, rhs) = rule;
                if (Matches(ref lhs, element))
                {
                    return GetRhs(lhs, rhs);
                }
            }
            return element;
        }

        private static IElement GetRhs(IElement lhs, IElement rhs)
        {
            Patterns = new Dictionary<string, IPattern>();
            PatternsSetUp(lhs);
            // Hardcode
            if (lhs is Expression expr &&
                expr.Operands.Count == 5 &&
                expr.Operands[0] is NullableSequencePattern seq1 &&
                expr.Operands[1] is IntegerPattern int1 &&
                expr.Operands[2] is NullableSequencePattern seq2 &&
                expr.Operands[3] is IntegerPattern int2 &&
                expr.Operands[4] is NullableSequencePattern seq3)
            {
                Integer val;
                switch (expr.Head)
                {
                    case nameof(sum):
                        val = new Integer(int1.Element.Value + int2.Element.Value);
                        break;

                    case nameof(mul):
                        val = new Integer(int1.Element.Value * int2.Element.Value);
                        break;

                    case nameof(pow):
                        val = new Integer((int)Math.Pow(int1.Element.Value, int2.Element.Value));
                        break;

                    default:
                        val = null;
                        break;
                }
                if (!(val is null))
                {
                    var exp = new Expression(expr.Head, new List<IElement>() { seq1, seq2, val, seq3 });
                    exp.Operands.RemoveAll(o => o is NullableSequencePattern n &&
                                                n.Operands.Count == 0);
                    return exp;
                }
            }
            return ApplyPatterns(rhs);
        }

        private static void PatternsSetUp(IElement lhs)
        {
            switch (lhs)
            {
                case IPattern p:
                    if (!Patterns.ContainsKey(p.Name.Value))
                    {
                        Patterns.Add(p.Name.Value, p);
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
            IPattern pattern = null;
            switch (rhs)
            {
                case IPattern p when p.GetType() != Patterns[p.Name.Value].GetType():
                    throw new Exception("Pattern types does not matches");
                case IntegerPattern integer:
                    pattern = Patterns[integer.Name.Value];
                    return ((IntegerPattern)pattern).Element;

                case ElementPattern element:
                    pattern = Patterns[element.Name.Value];
                    return ((ElementPattern)pattern).Element;

                case Expression exp:
                    for (int i = 0; i < exp.Operands.Count; i++)
                    {
                        if (exp.Operands[i] is NullableSequencePattern seq)
                        {
                            pattern = Patterns[seq.Name.Value];
                            if (!(pattern is NullableSequencePattern))
                            {
                                throw new Exception("Pattern types does not matches");
                            }
                            if (((NullableSequencePattern)pattern).Operands.Count > 0)
                            {
                                exp.Operands.InsertRange(i, ((NullableSequencePattern)pattern).Operands);
                            }
                        }
                        else
                        {
                            exp.Operands[i] = ApplyPatterns(exp.Operands[i]);
                        }
                    }
                    exp.Operands.RemoveAll(o => o is NullableSequencePattern n &&
                                                n.Operands.Count == 0);
                    return exp;

                default:
                    return rhs;
            }
        }
    }
}
