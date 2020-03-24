using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Attributes
{
    public class OrderlessAttribute : IAttribute
    {
        public Expression Apply(Expression expr)
        {
            if (expr.Head == Head.Pow) return expr;
            var operands = expr.Operands;
            if (operands != null)
                operands.Sort(new ElementComparer());
            return new Expression(expr.Head, operands);
        }
    }
}
