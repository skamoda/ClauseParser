using System;

namespace ClauseParser.Models.Exceptions
{
    public class QuantifierRefersToConstantException : Exception
    {
        public QuantifierRefersToConstantException() { }

        public QuantifierRefersToConstantException(string message) : base(message) { }

        public QuantifierRefersToConstantException(string message, Exception inner) : base(message, inner) { }
    }
}
