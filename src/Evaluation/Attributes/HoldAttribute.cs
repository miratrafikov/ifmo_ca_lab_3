using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes
{
    class HoldAttribute : IAttribute
    {
        public HoldAttribute(string type)
        {
            Type = type;
        }

        public string Type { get; }

        public Expression Apply(Expression expr)
        {
            return expr;
        }

        public override bool Equals(object obj)
        {
            if (obj is HoldAttribute h)
            {
                return h.Type == Type;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}
