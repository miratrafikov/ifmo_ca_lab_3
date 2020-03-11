using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Base.Interfaces
{
    public interface IExpression
    {
        public string Head { get; set; }
        public object Value { get; set; }
        public void Evaluate();
    }
}
