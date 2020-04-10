using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class NullableSequencePattern : IPattern
    {
        public NullableSequencePattern(string name)
        {
            Name = name;
            Operands = new List<IElement>();
        }

        public IElement GetHead()
        {
            return new Symbol("pattern");
        }

        public Symbol Name { get; set; }
        public List<IElement> Operands { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is NullableSequencePattern p && p.Name == Name && p.Operands.Count == Operands.Count)
            {
                var ops = Operands.Zip(p.Operands);
                foreach (var op in ops)
                {
                    if (!op.First.Equals(op.Second))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
