﻿using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.PatternMatcher;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.ContextInitializer;
using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public static class Context
    {
        private static Dictionary<string, IPattern> s_patterns = new Dictionary<string, IPattern>();
        private static readonly List<(IElement, IElement)> s_context = GetInitialContext();

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
                if (!(matchResult is null))
                {
                    return GetRhs(matchResult, rhs);
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
                    foreach (var o in exp._operands)
                    {
                        operands.Add(ClearPatterns(o));
                    }
                    exp._operands = operands;
                    return exp;
                default:
                    return lhs;
            }
        }

        private static IElement GetRhs(IElement lhs, IElement rhs)
        {
            // Hardcode
            if (lhs is Expression expr &&
                expr._operands.Count == 5 &&
                expr._operands[0] is NullableSequencePattern seq1 &&
                expr._operands[1] is IntegerPattern int1 &&
                expr._operands[2] is NullableSequencePattern seq2 &&
                expr._operands[3] is IntegerPattern int2 &&
                expr._operands[4] is NullableSequencePattern seq3)
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

                    case nameof(Util.Head.pow):
                        val = new Integer((int)Math.Pow(int1.Element.Value, int2.Element.Value));
                        break;

                    default:
                        val = null;
                        break;
                }
                if (!(val is null))
                {
                    var operands = seq1.Operands;
                    operands = operands.Concat(seq2.Operands).ToList();
                    operands.Add(val);
                    operands = operands.Concat(seq3.Operands).ToList();
                    var exp = new Expression(expr.Head, operands); ;
                    exp._operands.RemoveAll(o => o is NullableSequencePattern n &&
                                                n.Operands.Count == 0);
                    return exp;
                }
            }
            if (lhs is Expression pow &&
                pow._operands.Count == 5 &&
                pow._operands[0] is NullableSequencePattern seq4 &&
                pow._operands[1] is ElementPattern el &&
                pow._operands[2] is NullableSequencePattern seq5 &&
                pow._operands[3] is IntegerPattern int3 &&
                pow._operands[4] is NullableSequencePattern seq6) 
            {
                return new Expression(nameof(mul), Enumerable.Repeat(el.Element, int3.Element.Value).ToList());
            }

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
                    foreach (var o in e._operands)
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
                case IPattern p when (!s_patterns.ContainsKey(p.Name.Value)):
                    return rhs;
                case IPattern p when p.GetType() != s_patterns[p.Name.Value].GetType():
                    throw new PatternsDontMatchException();
                case IntegerPattern integer:
                    pattern = s_patterns[integer.Name.Value];
                    return ((IntegerPattern)pattern).Element;

                case ElementPattern element:
                    pattern = s_patterns[element.Name.Value];
                    return ((ElementPattern)pattern).Element;

                case Expression exp:
                    var i = 0;
                    while (i < exp._operands.Count)
                    {
                        if (exp._operands[i] is NullableSequencePattern seq)
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
                                    exp._operands.InsertRange(i, operands);
                                    exp._operands.RemoveAt(i + operands.Count);
                                }
                                else
                                {
                                    exp._operands.RemoveAt(i);
                                }
                            }
                        }
                        else
                        {
                            exp._operands[i] = ApplyPatterns(exp._operands[i]);
                        }
                        i++;
                    }
                    exp._operands.RemoveAll(o => o is NullableSequencePattern n &&
                                                n.Operands.Count == 0);
                    return exp;

                default:
                    return rhs;
            }
        }
    }
}
