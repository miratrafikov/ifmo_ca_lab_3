using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class DebugException : Exception
    {
        public DebugException() : base("Debug exception") { }
    }
}
