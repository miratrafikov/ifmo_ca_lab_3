using System;
using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class PatternMatcher
    {
        private static Dictionary<string, IPattern> Patterns;

        public static bool Matches(ref IElement pattern, IElement obj)
        {
            if (pattern == null || obj == null)
            {
                return false;
            }

            // Pattern is either Atom or Expression
            if (obj.Head != pattern.Head && pattern.Head != Head.Pattern)
            {
                return false;
            }

            // No pattern kinds required
            // Example: Symbol x and Symbol x
            if (ReferenceEquals(pattern, obj))
            {
                return true;
            }

            // Pattern is kind of '_Integer'
            if (obj is Integer integer && pattern is IntegerPattern)
            {
                ((IntegerPattern)pattern).Element = integer;
                return true;
            }

            // Pattern is kind of '_'
            if (pattern is ElementPattern)
            {
                ((ElementPattern)pattern).Element = obj;
                return true;
            }

            if (pattern is Expression p && obj is Expression o)
            {
                int j = 0;
                for (int i = 0; i < o.Operands.Count; i++)
                {
                    // Skip all nullable sequences in pattern
                    while (j < p.Operands.Count && p.Operands[j] is NullableSequencePattern) j++;
                    IElement tempPattern = null;
                    if (j < p.Operands.Count) tempPattern = p.Operands[j];
                    if (Matches(ref tempPattern, o.Operands[i]))
                    {
                        ((Expression)pattern).Operands[j] = tempPattern;
                        j++;
                    }
                    // If does not matches but previous
                    else if (j > 0 && p.Operands[j - 1] is NullableSequencePattern)
                    {
                        ((NullableSequencePattern)((Expression)pattern).Operands[j - 1]).Operands.Add(o.Operands[i]);
                    }
                    else
                    {
                        return false;
                    }
                }

                while (j < ((Expression)pattern).Operands.Count &&
                    ((Expression)pattern).Operands[j] is NullableSequencePattern) j++;
                if (j == ((Expression)pattern).Operands.Count)
                {
                    // TODO:
                    Patterns = new Dictionary<string, IPattern>();
                    return ArePatternsSame(pattern);
                }
                else
                {
                    return false;
                }
            }
            throw new Exception("Unexpected type of pattern and/or object");
        }

        // To see if all patterns with name 'x' are contains the same data.
        private static bool ArePatternsSame(IElement element)
        {
            if (element is IPattern p)
            {
                if (Patterns.ContainsKey(p.Name.Value))
                {
                    var comparer = new ElementComparer();
                    var dp = Patterns[p.Name.Value];
                    if (p is NullableSequencePattern l && dp is NullableSequencePattern r)
                    {
                        if (l.Operands.Count != r.Operands.Count) return false;
                        foreach (var o in l.Operands.Zip(r.Operands))
                        {
                            if (comparer.Compare(o.First, o.Second) != 0) return false;
                        }
                        return true;
                    }
                    else if (p is ElementPattern && dp is ElementPattern)
                    {
                        return comparer.Compare(((ElementPattern)p).Element, ((ElementPattern)dp).Element) == 0;
                    }
                    else if (p is IntegerPattern && dp is IntegerPattern)
                    {
                        return (((IntegerPattern)p).Element.Value - ((IntegerPattern)dp).Element.Value == 0);
                    }
                    return false;
                }
                else
                {
                    Patterns.Add(p.Name.Value, p);
                    return true;
                }
            }
            else if (element is Expression expr)
            {
                foreach (var o in expr.Operands)
                {
                    if (!ArePatternsSame(o)) return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
