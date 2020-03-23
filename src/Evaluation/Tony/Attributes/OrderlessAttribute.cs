using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Commons;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Heads;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.Attributes
{
    class OrderlessAttribute : IAttribute
    {
        public List<IExpression> Apply(Expression expr)
        {
            if (expr.Head == nameof(pow)) return expr.Operands;
            var operands = expr.Operands;
            if (operands != null)
                operands.Sort(new ExpressionComparer());
            return operands;
        }
    }
}
