using ClauseParser.Models.Exceptions;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class ClosingBracket : Symbol
    {
        protected override List<Symbol> GetChildren() => new List<Symbol>();
        public override void SetChild(int index, Symbol value) => throw new InvalidChildIndexException("Index: " + index);
    }
}
