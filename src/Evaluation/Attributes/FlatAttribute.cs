using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes
{
    public class FlatAttribute : IAttribute
    {
        public Expression Apply(Expression expr)
        {
            var head = expr.Head;
            var operands = new List<IElement>();
            foreach (var operand in expr.Operands)
            {
                if (operand is Expression e)
                {
                    if (e.Operands.Count == 1)
                    {
                        operands.Add(e.Operands.First());
                    }
                    else if (e.Head == head)
                    {
                        operands = operands.Concat(((Expression)operand).Operands).ToList();
                    }
                    else
                    {
                        operands.Add(operand);
                    }
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
