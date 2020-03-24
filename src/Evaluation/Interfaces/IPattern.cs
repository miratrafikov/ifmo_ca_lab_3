using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    interface IPattern : IElement
    {
        public Symbol Name { get; set; }
    }
}
