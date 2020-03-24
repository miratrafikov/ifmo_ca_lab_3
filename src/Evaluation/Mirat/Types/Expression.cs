using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types
{
    public class Expression : IElement
    {
        public List<IElement> Operands;
        public Head Head { get; set; }
    }
}
