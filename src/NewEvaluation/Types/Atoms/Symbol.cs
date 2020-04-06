using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms
{
    public class Symbol : IAtom, IElement
    {
        public readonly string Value;

        public Symbol(string value)
        {
            Value = value;
        }

        public override int GetHashCode()
        {
            var hashCode = 17;
            hashCode += 23 * Value.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return (obj is Symbol objAsSymbol && objAsSymbol.Value == Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
