using System;
using System.Collections.Generic;
using System.Text;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    public interface IExpression
    {
        public string Head { get; set; }
        public object Key { get; set; }
        public void Evaluate();
        public bool IsAlike(IExpression iexpr);
    }
}
