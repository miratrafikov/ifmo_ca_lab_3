using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces
{
    interface IPattern : IElement
    {
        public Symbol Name { get; set; }
    }
}
