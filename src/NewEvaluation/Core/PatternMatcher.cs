using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Commons;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Core
{
    public static class PatternMatcher
    {
        public static Result TryMatch(IElement element, IElement lhs)
        {
            var elementAsList = new List<IElement> { element };
            var lhsAsList = new List<IElement> { lhs };
            return TryMatch(elementAsList, lhsAsList);
        }

        private static Result TryMatch(List<IElement> elements, List<IElement> patterns)
        {
            throw new NotImplementedException();
        }
    }
}
