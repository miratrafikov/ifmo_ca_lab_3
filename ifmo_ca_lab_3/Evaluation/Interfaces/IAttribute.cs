using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Evaluation.Interfaces
{
    public interface IAttribute
    {
        public List<IExpression> Apply(List<IExpression> operands);
    }
}
