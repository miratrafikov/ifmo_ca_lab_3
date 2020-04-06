using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms
{
    public class Integer : IAtom, IElement
    {
        public readonly int Value;

        public Integer(int value)
        {
            Value = value;
        }
    }
}
