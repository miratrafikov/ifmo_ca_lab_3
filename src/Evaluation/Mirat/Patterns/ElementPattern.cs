using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Patterns
{
    public class ElementPattern : IPattern
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
