using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Base.Interfaces
{
    public interface IAttribute
    {
        public string Key { get; set; }
        public IExpression Apply(IExpression expr);
    }
}
