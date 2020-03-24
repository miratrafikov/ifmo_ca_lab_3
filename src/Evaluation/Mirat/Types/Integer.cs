using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types
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
