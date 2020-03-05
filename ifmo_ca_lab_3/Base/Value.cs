using ifmo_ca_lab_3.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Base
{
    public class Value : IOperand
    {
        public int Key { get; set; }
        public Value(int val) 
        {
            Key = val;
        }
    }
}
