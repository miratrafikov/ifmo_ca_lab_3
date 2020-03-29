using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class PatternsDontMatchException : Exception
    {
        private static readonly string s_message = "Patterns don't match";
        public PatternsDontMatchException() : base(s_message) { }
    }
}
