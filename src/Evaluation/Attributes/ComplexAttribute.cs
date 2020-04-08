using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes
{
    class ComplexAttribute : IAttribute
    {
        public Expression Apply(Expression expr)
        {
            var operands = new List<IElement>();
            foreach (var operand in expr.Operands)
            {
                if (operand is Expression e)
                {
                    if (e.Operands.Count == 1)
                    {
                        operands.Add(e.Operands.First());
                    }
                    else
                    {
                        operands.Add(Apply(e));
                    }
                }
                else
                {
                    operands.Add(operand);
                }
            }
            expr.Operands = operands;
            return expr;
        }
    }
}
