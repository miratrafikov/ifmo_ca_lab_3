using ifmo_ca_lab_3.Evaluation.Base.Interfaces;

namespace ifmo_ca_lab_3.Evaluation.Base
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
