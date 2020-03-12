using ShiftCo.ifmo_ca_lab_3.Evaluation.Base.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Base
{
    public class Value : IOperand
    {
        public int Key { get; set; }
        public Value(int val)
        {
            Key = val;
        }
    }
}
