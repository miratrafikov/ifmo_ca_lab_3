using ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ifmo_ca_lab_3.Evaluation.Base
{
    public class Symbol : IExpression
    {
        public string Key { get; set; }
        public Symbol(string key)
        {
            Key = key;
        }
    }
}
