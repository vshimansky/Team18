using System;

namespace UniversalAcceptanceLibrary.Exceptions
{
    public class InvalidUnicodeDomainException : UniversalAcceptanceException
    {
        public InvalidUnicodeDomainException(string message): base(message)
        {
        }
    }
}
