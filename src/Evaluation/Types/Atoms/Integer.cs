using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms
{
    public class Integer : IAtom, IElement
    {
        public int Value { get; }

        public Integer(int value)
        {
            Value = value;
        }

        public static implicit operator Integer(int value) => new Integer(value);

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
