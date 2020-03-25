using System;
using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Core.PatternMatcher;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    internal static class Context
    {
        private static Dictionary<string, IPattern> Patterns = new Dictionary<string, IPattern>();
        private static List<(IElement,IElement)> _context = new List<(IElement, IElement)>();

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
            PatternsSetUp(lhs);
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
                    for (int i =0; i < exp.Operands.Count; i++)
                    {
                        if (exp.Operands[i] is NullableSequencePattern seq)
                        {
                            pattern = Patterns[seq.Name.Value];
                            if (!(pattern is NullableSequencePattern))
                            {
                                throw new Exception("Pattern types does not matches");
                            }
                            exp.Operands.InsertRange(i, ((NullableSequencePattern)pattern).Operands);
                        }
                        else 
                        {
                            exp.Operands[i] = ApplyPatterns(exp.Operands[i]);
                        }
                    }
                    return exp;
                default:
                    return rhs;
            }
        }
    }
}
