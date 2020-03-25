using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

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

        public static bool operator ==(Symbol left, Symbol right) => 
            string.Compare(left.Value, right.Value) == 0;

        public static bool operator !=(Symbol left, Symbol right) =>
            string.Compare(left.Value, right.Value) != 0;
    }
}
