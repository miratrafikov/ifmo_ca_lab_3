using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Symbol : IElement, IAtom
    {
        public string Head { get; }
        public string Value { get; }

        public Symbol(string value)
        {
            Head = "Symbol";
            Value = value;
        }

        public override int GetHashCode()
        {
            var hashCode = 17 * Head.GetHashCode();
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

        public object Clone()
        {
            return MemberwiseClone();
        }

        public static implicit operator Symbol(string value) => new Symbol(value);
    }
}
