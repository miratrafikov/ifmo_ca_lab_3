using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class SymbolPattern : IPattern
    {
        public SymbolPattern(string name)
        {
            Name = new Symbol(name);
        }

        public Symbol Name { get; set; }

        public IElement GetHead()
        {
            return new Symbol("pattern");
        }

        public Symbol Element { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SymbolPattern p && p.Name.Equals(Name) && p.Element.Equals(Element))
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
