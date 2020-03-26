using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IElement, IAtom
    {
        public string Head { get; }
        public int Value { get; set; }

        public Integer(int value)
        {
            Head = "Integer";
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
            return (obj is Integer && ((Integer)obj).Value == Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator ==(Integer left, Integer right)
        {
            return (left.Value == right.Value);
        }

        public static bool operator !=(Integer left, Integer right)
        {
            return (left.Value != right.Value);
        }
    }
}
