using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types
{
    public class Expression : IElement
    {
        public List<IElement> operands;
        public string Head { get; set; }
    }
}
