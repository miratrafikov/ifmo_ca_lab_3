using System;
using System.Collections.Generic;
using System.Text;
using ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ifmo_ca_lab_3.Evaluation.Attributes
{
    class FlatAttribute : IAttribute
    {
        public List<IExpression> Apply(List<IExpression> operands)
        {
            return operands;
        }
    }
}
