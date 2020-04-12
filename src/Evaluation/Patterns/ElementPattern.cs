using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class ElementPattern : IPattern
    {
        public ElementPattern(string name)
        {
            Name = name;
        }

        public IElement GetHead()
        {
            return new Symbol("pattern");
        }

        public Symbol Name { get; set; }
        public IElement Element { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ElementPattern p && p.Name.Equals(Name) && p.Element.Equals(Element))
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
            return $"ElementPattern: {Name.Value}";
        }
    }
}
