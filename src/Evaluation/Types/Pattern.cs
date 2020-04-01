using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Pattern : IElement
    {
        public Symbol Name { get; }
        public IElement Type { get; }

        public Pattern(Symbol name, IElement type)
        {
            Name = name;
            Type = type;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
