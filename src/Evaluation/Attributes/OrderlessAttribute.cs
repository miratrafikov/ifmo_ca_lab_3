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
        public List<IExpression> Apply(List<IExpression> operands)
        {
            var comparer = new ExpressionComparer();
            operands.Sort(comparer);
            return operands;
        }
    }
}
