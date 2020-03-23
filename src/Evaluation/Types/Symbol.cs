using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Symbol : IAtom<string>
    {
        public string Head { get; set; }
        public string Value { get; set; }
    }
}
