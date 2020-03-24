using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Apributes
{
    public class OrderlessApribute : IAttribute
    {
        public OrderlessApribute()
        {
            Name = "Orderless";
        }

        public Symbol Name { get; set; }
    }
}
