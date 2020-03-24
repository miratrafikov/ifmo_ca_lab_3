using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Patterns
{
    public class NullableSequencePattern : IPattern
    {
        public NullableSequencePattern(string name)
        {
            Head = Head.Pattern;
            Name = name;
            Operands = new List<IElement>();
        }

        public Head Head { get; set; }
        public Symbol Name { get; set; }
        public List<IElement> Operands { get; set; }
    }
}
