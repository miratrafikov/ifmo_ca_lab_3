using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class NumberPattern : IPattern
    {
        public NumberPattern(string name)
        {
            Name = name;
        }

        public IElement GetHead()
        {
            return new Symbol("pattern");
        }

        public Symbol Name { get; set; }
        public Number Element { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is NumberPattern p && p.Name.Equals(Name) && p.Element.Equals(Element))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"NumberPattern: {Name.Value}";
        }
    }
}
