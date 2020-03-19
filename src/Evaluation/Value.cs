using ShiftCo.ifmo_ca_lab_3.Evaluation.Commons;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
{
    public class Value : IExpression
    {
        public Value(int val)
        {
            Head = "value";
            Key = val;
        }

        public object Key { get; set; }

        public string Head { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is IExpression)) return false;
            var comparer = new ExpressionComparer();
            return comparer.Compare(this, (IExpression)obj) == 0;
        }

        public void Evaluate() { }

        public bool IsAlike(IExpression iexpr)
        {
            return iexpr is Value;
        }
    }
}
