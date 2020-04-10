using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Commons;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Utils;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Core
{
    public static class Context
    {
        private static readonly List<(IElement, IElement)> s_rules;

        public static void AddRule(IElement lhs, IElement rhs)
        {
            var result = TryFindEntry(lhs);
            if (result.Success)
            {
                s_rules[(int)result.Value] = (lhs, rhs);
            }
            else
            {
                s_rules.Add((lhs, rhs));
            }
        }

        private static Result TryFindEntry(IElement lhs)
        {
            for (var i = 0; i < s_rules.Count; i++)
            {
                if (lhs.Equals(s_rules[i].Item1))
                {
                    return new Result(true, i);
                }
            }

            return new Result(false);
        }

        public static IElement Transform(IElement element)
        {
            foreach (var (lhs, rhs) in s_rules)
            {
                var result = TryMatch(element, lhs);
                if (result.Success)
                {
                    var retrievedVariables = (Dictionary<Symbol, List<IElement>>)result.Value;
                    return ApplyTransformation(rhs, retrievedVariables);
                }
            }

            return element;
        }

        private static Result TryMatch(IElement element, IElement lhs)
        {
            throw new NotImplementedException();
        }

        private static IElement ApplyTransformation(IElement rhs, Dictionary<Symbol, List<IElement>> retrievedVariables)
        {
            throw new NotImplementedException();
        }
    }
}
