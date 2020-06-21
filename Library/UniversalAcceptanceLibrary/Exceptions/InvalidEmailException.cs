using System;

namespace UniversalAcceptanceLibrary.Exceptions
{
    public class InvalidEmailException : UniversalAcceptanceException
    {
        public InvalidEmailException(string message): base(message)
        {
        }
    }
}
