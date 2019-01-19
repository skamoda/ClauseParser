using System;

namespace ClauseParser.Models.Exceptions
{
    public class symbolCompoundException : Exception
    {
        public symbolCompoundException() { }

        public symbolCompoundException(string message) : base(message) { }

        public symbolCompoundException(string message, Exception inner) : base(message, inner) { }
    }
}
