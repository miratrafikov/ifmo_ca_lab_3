using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Symbol : IAtom<string>
    {
        public Symbol(string value)
        {
            Head = Head.Symbol;
            Value = value;
        }

        public Head Head { get; set; }
        public string Value { get; set; }

        public static implicit operator Symbol(string value) => new Symbol(value);
    }
}
