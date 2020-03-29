using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Core
{
    public class PatternMatcher
    {
        private Dictionary<string, IPattern> _patterns = new Dictionary<string, IPattern>();

        public IElement Matches(IElement lhs, IElement obj)
        {
            if (lhs == null || obj == null)
            {
                return null;
            }

            // Pattern is either Atom or Expression
            if (obj.Head != lhs.Head && lhs.Head != nameof(pattern))
            {
                return null;
            }

            // No pattern kinds required
            // Example: Symbol x and Symbol x
            if (ReferenceEquals(lhs, obj))
            {
                return lhs;
            }

            // Pattern is kind of '_Integer'
            if (obj is Integer integer && lhs is IntegerPattern)
            {
                ((IntegerPattern)lhs).Element = integer;
                return lhs;
            }

            // Pattern is kind of '_'
            if (lhs is ElementPattern)
            {
                ((ElementPattern)lhs).Element = obj;
                return lhs;
            }

            if (lhs is Expression p && obj is Expression o)
            {
                var j = 0;
                for (var i = 0; i < o._operands.Count; i++)
                {
                    // Skip all nullable sequences in pattern
                    while (j < p._operands.Count && p._operands[j] is NullableSequencePattern) j++;
                    IElement tempPattern = null;
                    if (j < p._operands.Count) tempPattern = p._operands[j];
                    if (!(Matches(tempPattern, o._operands[i]) is null))
                    {
                        ((Expression)lhs)._operands[j] = tempPattern;
                        j++;
                    }
                    // If does not matches but previous
                    else if (j > 0 && p._operands[j - 1] is NullableSequencePattern)
                    {
                        ((NullableSequencePattern)((Expression)lhs)._operands[j - 1]).Operands.Add(o._operands[i]);
                    }
                    else
                    {
                        return null;
                    }
                }

                while (j < ((Expression)lhs)._operands.Count &&
                    ((Expression)lhs)._operands[j] is NullableSequencePattern) j++;
                if (j == ((Expression)lhs)._operands.Count)
                {
                    _patterns = new Dictionary<string, IPattern>();
                    if (ArePatternsSame(lhs)) return lhs;
                }
                else
                {
                    return null;
                }
            }
            throw new StrangePatternOrObjectException();
        }

        // To see if all patterns with name 'x' are contains the same data.
        private bool ArePatternsSame(IElement element)
        {
            if (element is IPattern p)
            {
                if (_patterns.ContainsKey(p.Name.Value))
                {
                    var comparer = new ElementComparer();
                    var dp = _patterns[p.Name.Value];
                    if (p is NullableSequencePattern l && dp is NullableSequencePattern r)
                    {
                        if (l.Operands.Count != r.Operands.Count) return false;
                        foreach (var (first, second) in l.Operands.Zip(r.Operands))
                        {
                            if (comparer.Compare(first, second) != 0) return false;
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
                    _patterns.Add(p.Name.Value, p);
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
