using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class TooManyIterationsException : Exception
    {
        private static readonly string s_message = "Too many evaluation iterations occured";

        public TooManyIterationsException() : base(s_message) { }
    }
}
