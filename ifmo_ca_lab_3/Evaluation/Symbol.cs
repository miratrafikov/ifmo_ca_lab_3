using ifmo_ca_lab_3.Evaluation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Evaluation
{
    public class Symbol : IExpression
    {
        public Symbol(string key)
        {
            Head = "Symbol";
            Key = key;
        }

        public string Head { get; set; }
        public object Key { get; set; }
        public void Evaluate() { }
    }
}
