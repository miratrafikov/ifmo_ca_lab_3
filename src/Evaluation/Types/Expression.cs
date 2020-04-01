using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Expression : IElement
    {
        public IElement Head { get; set; }
        public List<IElement> Elements { get; set; }
        public List<IAttribute> Attributes { get; set; }

        public Expression()
        {
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(IElement head) : this()
        {
            Head = head;
        }

        public Expression(IElement head, List<IElement> elements) : this(head)
        {
            Elements = elements;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
