﻿using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    internal interface IPattern : IElement
    {
        public Symbol Name { get; set; }
    }
}
