using ifmo_ca_lab_3.Evaluation.Base.Interfaces;

namespace ifmo_ca_lab_3.Evaluation.Base
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
