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
            foreach (var operand in expr._operands)
            {
                if (operand is Expression e)
                {
                    if (e._operands.Count == 1)
                    {
                        operands.Add(e._operands.First());
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
            expr._operands = operands;
            return expr;
        }
    }
}
