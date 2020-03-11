using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ifmo_ca_lab_3.Evaluation.Interfaces;
using static ifmo_ca_lab_3.Evaluation.HeadsEnum;
using ifmo_ca_lab_3.Evaluation.Commons;

namespace ifmo_ca_lab_3.Evaluation.Attributes
{
    class OrderlessAttribute: IAttribute
    {
        public List<IExpression> Apply(List<IExpression> operands)
        {
            var comparer = new ExpressionComparer();
            operands.Sort(comparer);
            return operands;
        }
    }
}
