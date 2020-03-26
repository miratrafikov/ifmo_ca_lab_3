using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Util
{
    public class MatchingResult
    {
        public bool IsSuccess { get; }
        public Dictionary<string, List<IElement>> RetrievedVariables { get; }

        public MatchingResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public MatchingResult(bool isSuccess, Dictionary<string, List<IElement>> retrievedVariables) : this(isSuccess)
        {
            RetrievedVariables = retrievedVariables;
        }
    }
}
