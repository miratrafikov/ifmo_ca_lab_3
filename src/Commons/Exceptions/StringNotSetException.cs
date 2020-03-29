using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class StringNotSetException : Exception
    {
        private static readonly string s_message = "No defined string exists yet";

        public StringNotSetException() : base(s_message) { }
    }
}
