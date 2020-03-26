using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;

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
            var hashCode = Head.GetHashCode();
            hashCode += 23 * Value.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return (obj is Symbol && ((Symbol)obj).Value == Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(Symbol left, Symbol right)
        {
            return (left.Value == right.Value);
        }

        public static bool operator !=(Symbol left, Symbol right)
        {
            return (left.Value != right.Value);
        }
    }
}
