using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Integer : IAtom<int>
    {
        public string Head { get; set; }
        public int Value { get; set; }
    }
}
