using ClauseParser.Code;
using ClauseParser.Models.Exceptions;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class OpeningBracket : Symbol
    {
        public OpeningBracket()
        {
            Priority = (int) Consts.Priorities.BRACKET;
        }
        public override void SetChild(int index, Symbol symbol) => throw new InvalidChildIndexException("Index: " + index);
        public override List<Symbol> GetChildren() => new List<Symbol>();
    }
}
