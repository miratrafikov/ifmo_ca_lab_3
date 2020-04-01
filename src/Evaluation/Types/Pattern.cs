using System;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Types
{
    public class Pattern : IElement
    {
        public Symbol Name { get; }
        public Type Type { get; }
        public bool IsSequence { get; }

        public Pattern(Symbol name, Type type, bool isSequence)
        {
            Name = name;
            Type = type;
            IsSequence = isSequence;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
