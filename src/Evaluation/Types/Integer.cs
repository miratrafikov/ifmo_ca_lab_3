using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IElement, IAtom
    {
        public string Head { get; }
        public int Value { get; }

        public Integer(int value)
        {
            Head = "Integer";
            Value = value;
        }

        public static implicit operator Integer(int value) => new Integer(value);

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
