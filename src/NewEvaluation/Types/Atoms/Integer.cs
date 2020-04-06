using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Utils;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms
{
    public class Integer : IAtom, IElement
    {
        public readonly int Value;

        public Integer(int value)
        {
            Value = value;
        }

        public override int GetHashCode()
        {
            var hashCode = 17 * (int)Element.Integer;
            hashCode += 23 * Value.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return (obj is Integer objAsInteger && objAsInteger.Value == Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
