using System;

namespace ShiftCo.ifmo_ca_lab_3.Commons.Exceptions
{
    [Serializable]
    public class StrangeCharacterException : Exception
    {
        public StrangeCharacterException(char character) : base($"Non-alphabetical character '{character}' met") { }
    }
}
