using System;
using System.Collections.Generic;
using System.Text;
using ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ifmo_ca_lab_3.Evaluation.Attributes
{
    class OrderlessAttribute: IAttribute
    {
        public string Key { get; set; }
        public IExpression Apply(IExpression expr)
        {
            return expr;
        }
    }
}
