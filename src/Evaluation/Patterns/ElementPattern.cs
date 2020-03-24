using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    class ElementPattern : IPattern
    {
        public ElementPattern(string name)
        {
            Head = Head.Pattern;
            Name = name;
        }

        public Head Head { get; set; }
        public Symbol Name { get; set; }
        public IElement Element { get; set; }
    }
}
