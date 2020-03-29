using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class StrangePatternOrObjectException : Exception
    {
        private static readonly string s_message = "Unexpected pattern or object";

        public StrangePatternOrObjectException() : base(s_message) { }
    }
}
