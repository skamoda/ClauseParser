using System;

namespace ClauseParser.Models.Exceptions
{
    public class InvalidChildIndexException : Exception
    {
        public InvalidChildIndexException() { }

        public InvalidChildIndexException(string message) : base(message) { }

        public InvalidChildIndexException(string message, Exception inner) : base(message, inner) { }
    }
}
