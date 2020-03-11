using System;
using System.Collections.Generic;
using System.Text;
using ifmo_ca_lab_3.Base.Interfaces;

namespace ifmo_ca_lab_3.Base.Attributes
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
