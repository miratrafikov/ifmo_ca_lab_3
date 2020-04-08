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
            Operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new ComplexAttribute(),
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head)
        {
            Head = head;
            Operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new ComplexAttribute(),
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, List<IElement> operands)
        {
            Head = head;
            Operands = operands;
            Attributes = new List<IAttribute>()
            {
                new ComplexAttribute(),
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, params IElement[] operands)
        {
            Head = head;
            Operands = new List<IElement>();
            foreach (var operand in operands)
            {
                Operands.Add(operand);
            }
            Attributes = new List<IAttribute>()
            {
                new ComplexAttribute(),
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        #endregion Constructors

        public List<IElement> Operands;
        public string Head { get; set; }
        public List<IAttribute> Attributes { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
