using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Core
{
    class PatternMatcher
    {
        public static bool Matches(ref IElement pattern, IElement obj)
        {
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

            if (pattern is Expression exp && obj is Expression o)
            {
                int j = 0;
                for (int i = 0; i < o.Operands.Count; i++)
                {
                    while ()
                }
            }

            throw new Exception("Unexpected type of pattern and/or object");
        }
    }
}
