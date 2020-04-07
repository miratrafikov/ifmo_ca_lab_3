using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Commons;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class PatternMatcher
    {
        private static Dictionary<string, IPattern> Patterns = new Dictionary<string, IPattern>();
        private static ElementComparer Comparer = new ElementComparer();

        public static Result Matches(IElement lhs, IElement obj)
        {
            if (lhs == null || obj == null)
            {
                return new Result(false);
            }

            // Pattern is either Atom or Expression
            if (obj.Head != lhs.Head && lhs.Head != nameof(pattern))
            {
                return new Result(false);
            }

            // Pattern is kind of '_Integer'
            if (obj is Integer integer && lhs is IntegerPattern)
            {
                ((IntegerPattern)lhs).Element = integer;
                return new Result(true, lhs);
            }

            // Pattern is kind of '_'
            if (lhs is ElementPattern)
            {
                ((ElementPattern)lhs).Element = obj;
                return new Result(true, lhs);
            }

            // No pattern kinds required
            // Example: Symbol x and Symbol x
            if (Comparer.Compare(lhs, obj) == 0)
            {
                return new Result(true, lhs);
            }

            if (lhs is Expression p && obj is Expression o)
            {
                int j = 0;
                for (int i = 0; i < o._operands.Count; i++)
                {
                    // Skip all nullable sequences in pattern
                    while (j < p._operands.Count && p._operands[j] is NullableSequencePattern) j++;
                    IElement tempPattern = null;
                    if (j < p._operands.Count) tempPattern = p._operands[j];
                    var matchResult = Matches(tempPattern, o._operands[i]);
                    if (matchResult.Success == true)
                    {
                        ((Expression)lhs)._operands[j] = (IElement)matchResult.Value;
                        j++;
                    }
                    // If does not matches but previous is nullable sequence
                    else if (j > 0 && p._operands[j - 1] is NullableSequencePattern)
                    {
                        ((NullableSequencePattern)((Expression)lhs)._operands[j - 1]).Operands.Add(o._operands[i]);
                    }
                    else
                    {
                        return new Result(false);
                    }
                }

                while (j < ((Expression)lhs)._operands.Count &&
                    ((Expression)lhs)._operands[j] is NullableSequencePattern) j++;
                if (j == ((Expression)lhs)._operands.Count)
                {
                    Patterns = new Dictionary<string, IPattern>();
                    if (ArePatternsSame(lhs)) return new Result(true, lhs);
                }
                else
                {
                    return new Result(false);
                }
            }
            return new Result(false);
        }

        // To see if all patterns with name 'x' are contain the same data.
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
                foreach (var o in expr._operands)
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
