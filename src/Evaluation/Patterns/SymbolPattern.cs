using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    class SymbolPattern : IPattern
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
    }
}
