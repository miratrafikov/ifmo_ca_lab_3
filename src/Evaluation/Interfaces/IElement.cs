using System;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces
{
    public interface IElement : ICloneable
    {
        public string Head { get; set; }
    }
}
