using ShiftCo.ifmo_ca_lab_3.Evaluation.Base.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Base
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
