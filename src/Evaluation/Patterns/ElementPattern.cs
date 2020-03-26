using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class ElementPattern : IPattern, IElement
    {
        public string Head { get; }
        public string Name { get; }

        public ElementPattern(string name)
        {
            Head = "Pattern";
            Name = name;
        }
    }
}
