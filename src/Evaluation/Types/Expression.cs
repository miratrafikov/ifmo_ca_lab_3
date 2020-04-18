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
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(IElement head)
        {
            Head = head;
            Operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head)
        {
            Head = new Symbol(head);
            Operands = new List<IElement>();
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(IElement head, List<IElement> operands)
        {
            Head = head;
            Operands = operands;
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, List<IElement> operands)
        {
            Head = new Symbol(head);
            Operands = operands;
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(IElement head, params IElement[] operands)
        {
            Head = head;
            Operands = new List<IElement>();
            foreach (var operand in operands)
            {
                Operands.Add(operand);
            }
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        public Expression(string head, params IElement[] operands)
        {
            Head = new Symbol(head);
            Operands = new List<IElement>();
            foreach (var operand in operands)
            {
                Operands.Add(operand);
            }
            Attributes = new List<IAttribute>()
            {
                new FlatAttribute(),
                new OrderlessAttribute()
            };
        }

        #endregion Constructors

        public IElement GetHead()
        {
            return Head;
        }

        public List<IElement> Operands;
        public IElement Head { get; set; }
        public List<IAttribute> Attributes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Expression e)
            {
                var comparer = new ElementComparer();
                return comparer.Compare(this, e) == 0 ? true : false;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Expression: {Head}";
        }
    }
}
