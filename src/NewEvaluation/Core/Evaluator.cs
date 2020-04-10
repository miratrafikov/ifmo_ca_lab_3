using System;
using ShiftCo.ifmo_ca_lab_3.Commons;
using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Core
{
    public static class Evaluator
    {
        private const int EvaluationRunsLimit = 100000;
        private static int s_evaluationRuns;

        public static IElement Run(IElement element)
        {
            s_evaluationRuns = 0;
            var result = Evaluate(element);
            if (result.Success)
            {
                return (IElement)result.Value;
            }
            throw new TooManyIterationsException();
        }

        private static Result Evaluate(IElement element)
        {
            throw new NotImplementedException();
        }
    }
}
