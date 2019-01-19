using System;

namespace ClauseParser.Models.Exceptions
{
    class SyntaxErrorException : Exception
    {
        public SyntaxErrorException() { }

        public SyntaxErrorException(string message) : base(message) { }

        public SyntaxErrorException(string message, Exception inner) : base(message, inner) { }
    }
}
