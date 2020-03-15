using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
{
    public class Value : IExpression
    {
        public Value(int val)
        {
            Head = "Value";
            Key = val;
        }

        public object Key { get; set; }
        public string Head { get; set; }
        public void Evaluate() { }
        public bool IsAlike(IExpression iexpr)
        {
            return iexpr is Value;
        }
    }
}
