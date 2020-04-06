using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms
{
    public class Symbol : IAtom, IElement
    {
        public readonly string Value;

        public Symbol(string value)
        {
            Value = value;
        }
    }
}
