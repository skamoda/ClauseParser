using System;

namespace ClauseParser.Models.Exceptions
{
    class BracketsNotMatchingException : Exception
    {
        public BracketsNotMatchingException() { }

        public BracketsNotMatchingException(string message) : base(message) { }

        public BracketsNotMatchingException(string message, Exception inner) : base(message, inner) { }
    }
}
