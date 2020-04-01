using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms
{
    public class Integer : IAtom, IElement
    {
        public int Value { get; set; }

        public Integer(int value)
        {
            Value = value;
        }

        public static implicit operator Integer(int value) => new Integer(value);

        public static bool operator ==(Integer left, Integer right) =>
            left.Value - right.Value == 0;

        public static bool operator !=(Integer left, Integer right) =>
            left.Value - right.Value != 0;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
