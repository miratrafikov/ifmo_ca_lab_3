using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Core
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

        private static bool ArePatternsSame(IElement element)
        {
            if (element is IPattern p)
            {
                if (Patterns.ContainsKey(p.Name.Value))
                {
                    return (Equals(p, Patterns[p.Name.Value]));
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
