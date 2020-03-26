using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Expression : IElement
    {
        public string Head { get; }
        public List<IElement> Operands { get; set; }
        public List<IAttribute> Attributes { get; }

        public Expression(string head, List<IElement> operands, List<IAttribute> attributes = null)
        {
            Head = head;
            Operands = operands;
            Attributes = attributes ?? new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }
    }
}
