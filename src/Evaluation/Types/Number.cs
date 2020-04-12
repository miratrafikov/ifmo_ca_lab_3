using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Number : IAtom<decimal>
    {
        public Number(int value)
        {
            Value = value;
        }

        public Number(decimal value)
        {
            Value = value;
        }

        public IElement GetHead()
        {
            return new Symbol("integer");
        }

        public decimal Value { get; set; }

        public static implicit operator Number(int value) => new Number(value);

        public static bool operator ==(Number left, Number right) =>
            left.Value - right.Value == 0;

        public static bool operator !=(Number left, Number right) =>
            left.Value - right.Value != 0;

        public override string ToString()
        {
            return $"Number: {Value}";
        }
    }
}
