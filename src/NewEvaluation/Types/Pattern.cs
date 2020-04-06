using System;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types
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
    }
}
