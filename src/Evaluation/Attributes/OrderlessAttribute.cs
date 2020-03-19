using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Heads;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Commons;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Attributes
{
    class OrderlessAttribute : IAttribute
    {
        public List<IExpression> Apply(Expression expr)
        {
            if (expr.Head == nameof(Pow)) return expr.Operands;
            var operands = expr.Operands;
            if (operands != null) 
                operands.Sort(new ExpressionComparer());
            return operands;
        }
    }
}
