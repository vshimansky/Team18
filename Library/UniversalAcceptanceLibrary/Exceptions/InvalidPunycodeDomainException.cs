using System;

namespace UniversalAcceptanceLibrary.Exceptions
{
    public class InvalidPunycodeDomainException : UniversalAcceptanceException
    {
        public InvalidPunycodeDomainException(string message): base(message)
        {
        }
    }
}
