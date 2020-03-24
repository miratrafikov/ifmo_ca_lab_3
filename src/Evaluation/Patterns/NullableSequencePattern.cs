using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
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
