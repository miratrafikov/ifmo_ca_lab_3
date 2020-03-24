using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Expression : IElement
    {
        public List<IElement> Operands;
        public Head Head { get; set; }
    }
}
