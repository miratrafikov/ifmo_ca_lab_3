using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Expression : IElement
    {
        #region Constructors

        public Expression()
        {
            _operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head)
        {
            Head = head;
            _operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, List<IElement> operands)
        {
            Head = head;
            _operands = operands;
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, params IElement[] operands)
        {
            Head = head;
            _operands = new List<IElement>();
            foreach (var operand in operands)
            {
                _operands.Add(operand);
            }
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        #endregion Constructors

        public List<IElement> _operands;
        public string Head { get; set; }
        public List<IAttribute> Attributes { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
