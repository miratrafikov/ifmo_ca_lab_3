using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IAtom<int>
    {
        public Integer(int value)
        {
            Head = nameof(integer);
            Value = value;
        }

        public string Head { get; set; }
        public int Value { get; set; }

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
