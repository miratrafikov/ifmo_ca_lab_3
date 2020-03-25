namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public class Result
    {
        public bool success;
        public object value;

        public Result(bool success, object value = null)
        {
            this.success = success;
            this.value = value;
        }
    }
}
