using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class StrangePatternFormException : Exception
    {
        public StrangePatternFormException() : base("Wrong pattern composition") { }
    }
}
