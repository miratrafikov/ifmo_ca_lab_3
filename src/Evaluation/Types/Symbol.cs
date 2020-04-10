using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Symbol : IAtom<string>
    {
        public Symbol(string value)
        {
            Value = value;
        }

        public IElement GetHead()
        {
            return new Symbol("symbol");
        }

        public string Value { get; set; }

        public static implicit operator Symbol(string value) => new Symbol(value);

        public override bool Equals(object obj)
        {
            var comparer = new ElementComparer();
            if (obj is  IElement element)
            {
                return 0 == comparer.Compare(element, this);
            }
            return false;
        }
    }
}
