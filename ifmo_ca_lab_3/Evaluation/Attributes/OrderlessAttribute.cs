using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ifmo_ca_lab_3.Evaluation.Interfaces;
<<<<<<< HEAD
=======
using static ifmo_ca_lab_3.Evaluation.Heads;
>>>>>>> 6fa822710a13c48ed7275eb45d304ca10c713733
using ifmo_ca_lab_3.Evaluation.Commons;

namespace ifmo_ca_lab_3.Evaluation.Attributes
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
