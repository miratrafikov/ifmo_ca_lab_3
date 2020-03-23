using System.Collections.Generic;
using System.Linq;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Commons.Converter;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Attributes
{
    class FlatAttribute : IAttribute
    {
        // mul(mul (x, y), 2) -> mul(2,x,y)
        public List<IExpression> Apply(Expression expr)
        {
            var head = expr.Head;
            var operands = new List<IExpression>();
            foreach (var operand in expr.Operands)
            {
                if (operand.Head == head)
                {
                    operands = operands.Concat(ToExpression(operand).Operands).ToList();
                }
                else
                {// mul(3, mul(0,0)) mul(3,0,0))
                    operands.Add(operand);
                }
            }
            return operands;
        }
    }
}
