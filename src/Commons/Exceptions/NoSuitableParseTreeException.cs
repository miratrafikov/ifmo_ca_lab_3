using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class NoSuitableParseTreeException : Exception
    {
        public NoSuitableParseTreeException() : base("No suitable parse tree exists") { }
    }
}
