namespace ShiftCo.ifmo_ca_lab_3.Commons
{
    public struct Result
    {
        public bool Success { get; }
        public object Value { get; }

        public Result(bool success)
        {
            Success = success;
            Value = null;
        }

        public Result(bool success, object value)
        {
            Success = success;
            Value = value;
        }

        public override string ToString()
        {
            return Success + ", " + (Value ?? "null");
        }
    }
}
