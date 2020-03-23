using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types
{
    public class Symbol : IAtom<string>
    {
        public string Head { get; set; }
        public string Value { get; set; }
    }
}
