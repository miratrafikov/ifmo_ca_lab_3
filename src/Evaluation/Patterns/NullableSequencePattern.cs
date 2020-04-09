using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class NullableSequencePattern : IPattern
    {
        public NullableSequencePattern(string name)
        {
            Head = nameof(pattern);
            Name = name;
            Operands = new List<IElement>();
        }

        public string Head { get; set; }
        public Symbol Name { get; set; }
        public List<IElement> Operands { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
