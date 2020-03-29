namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public struct Result
    {
        public bool Success { get; }
        public object Value { get; }

        public Result(bool success, object value = null)
        {
            Success = success;
            Value = value;
        }
    }
}
