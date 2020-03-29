using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class NoSuitableTokenException : Exception
    {
        public NoSuitableTokenException(string substring) : base($"No suitable token exists for substring {substring}") { }
    }
}
