using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms
{
    public class Symbol : IAtom, IElement
    {
        public string Value { get; }

        public Symbol(string value)
        {
            Value = value;
        }

        public static implicit operator Symbol(string value) => new Symbol(value);

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
