﻿using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns
{
    public class IntegerPattern : IPattern
    {
        public IntegerPattern(string name)
        {
            Head = nameof(pattern);
            Name = name;
        }

        public string Head { get; set; }
        public Symbol Name { get; set; }
        public Integer Element { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
