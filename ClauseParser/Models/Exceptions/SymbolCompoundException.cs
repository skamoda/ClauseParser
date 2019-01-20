using System;

namespace ClauseParser.Models.Exceptions
{
    public class SymbolCompoundException : Exception
    {
        public SymbolCompoundException() { }

        public SymbolCompoundException(string message) : base(message) { }

        public SymbolCompoundException(string message, Exception inner) : base(message, inner) { }
    }
}
