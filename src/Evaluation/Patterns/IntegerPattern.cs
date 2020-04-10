using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class IntegerPattern : IPattern
    {
        public IntegerPattern(string name)
        {
            Name = name;
        }

        public IElement GetHead()
        {
            return new Symbol("pattern");
        }

        public Symbol Name { get; set; }
        public Integer Element { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is IntegerPattern p && p.Name == Name && p.Element == Element)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
