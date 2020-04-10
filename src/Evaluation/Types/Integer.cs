using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IAtom<int>
    {
        public Integer(int value)
        {
            Value = value;
        }

        public IElement GetHead()
        {
            return new Symbol("integer");
        }
        public int Value { get; set; }

        public static implicit operator Integer(int value) => new Integer(value);

        public static bool operator ==(Integer left, Integer right) =>
            left.Value - right.Value == 0;

        public static bool operator !=(Integer left, Integer right) =>
            left.Value - right.Value != 0;
    }
}
