using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Attributes
{
    public class FlatAttribute : IAttribute
    {
        public Expression Apply(Expression expr)
        {
            var head = expr.Head;
            var operands = new List<IElement>();
            foreach (var operand in expr.Operands)
            {
                if (operand.Head == head)
                {
                    operands = operands.Concat(((Expression)operand).Operands).ToList();
                }
                else
                {
                    operands.Add(operand);
                }
            }
            return new Expression(head, operands);
        }
    }
}
