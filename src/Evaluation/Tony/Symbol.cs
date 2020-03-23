using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Commons;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Commons.Converter;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Tony
{
    public class Symbol : IExpression
    {
        public Symbol(string key)
        {
            Head = "symbol";
            Key = key;
        }

        public string Head { get; set; }

        public object Key { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is IExpression)) return false;
            var comparer = new ExpressionComparer();
            return comparer.Compare(this, (IExpression)obj) == 0;
        }

        public void Evaluate() { }

        public bool IsAlike(IExpression iexpr)
        {
            if (iexpr is Symbol) return Key.Equals(iexpr.Key);
            if (iexpr is Expression && ToExpression(iexpr).Head == nameof(Heads.mul))
            {
                if (ToExpression(iexpr).Operands.Count == 2 &&
                    ToExpression(iexpr).Operands.First().Head == nameof(Heads.value))
                    return IsAlike(ToExpression(iexpr).Operands[1]);
            }
            if (iexpr is Expression)
            {
                if (ToExpression(iexpr).Operands.Count > 0) return false;
                return Key.Equals(iexpr.Key);
            }
            return false;
        }
    }
}
