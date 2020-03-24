using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Expression : IElement
    {

        #region Constructors

        public Expression()
        {
            Operands = new List<IElement>();
            Apributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(Head head)
        {
            Head = head;
            Operands = new List<IElement>();
            Apributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(Head head, List<IElement> operands)
        {
            Head = head;
            Operands = operands;
            Apributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        #endregion

        public List<IElement> Operands;
        public Head Head { get; set; }
        public List<IAttribute> Apributes { get; set; }
    }
}
