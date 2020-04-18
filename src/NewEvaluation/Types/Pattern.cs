using System;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Utils;

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

        public override int GetHashCode()
        {
            var hashCode = 17 * (int)Element.Pattern;
            hashCode += 23 * Name.GetHashCode();
            hashCode += 23 * Type.GetHashCode();
            hashCode += 23 * IsSequence.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object? obj)
        {
            return (obj is Pattern objAsPattern && objAsPattern.Name == Name && 
                    objAsPattern.Type == Type && objAsPattern.IsSequence == IsSequence);
        }

        public override string ToString()
        {
            return Name + (IsSequence ? "___" : "_") + (Type == typeof(IElement) ? "" : Type.Name);
        }
    }
}
