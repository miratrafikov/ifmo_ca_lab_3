using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public class Result
    {
        public bool success;
        public object value;

        public Result(bool success, object value = null)
        {
            this.success = success;
            this.value = value;
        }
    }
}
