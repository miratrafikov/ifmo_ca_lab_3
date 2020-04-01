using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms
{
    public class Symbol : IAtom, IElement
    {
        public string Name { get; }

        public Symbol(string name)
        {
            Name = name;
        }

        public static implicit operator Symbol(string name) => new Symbol(name);

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
