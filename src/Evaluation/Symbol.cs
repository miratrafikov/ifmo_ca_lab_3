using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation
{
    public class Symbol : IOperand
    {
        public string Key { get; set; }
        public Symbol(string key)
        {
            Key = key;
        }
    }
}
