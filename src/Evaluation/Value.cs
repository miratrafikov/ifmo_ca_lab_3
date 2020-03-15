using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
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
