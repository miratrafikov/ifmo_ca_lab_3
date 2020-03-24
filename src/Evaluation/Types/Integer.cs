using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IAtom<int>
    {
        public Integer(int value)
        {
            Head = Head.Integer;
            Value = value;
        }
        public Head Head { get; set; }
        public int Value { get; set; }

        public static implicit operator Integer(int value) => new Integer(value);
    }
}
