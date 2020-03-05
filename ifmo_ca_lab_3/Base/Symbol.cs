using ifmo_ca_lab_3.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Base
{
    public class Symbol : IOperand
    {
        public string Key { get; set; }
        public Symbol(string key)
        {
            this.Key = key;
        }
    }
}
