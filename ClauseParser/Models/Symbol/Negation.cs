using ClauseParser.Code;
using ClauseParser.Models.Exceptions;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class Negation : Symbol
    {

        public Symbol child;
        public Negation() : base()
        {
            Priority = (int) Consts.Priorities.NOT;
            Name = "NOT";
        }

        public Negation(Symbol parent = null, string name = "")
        {
            Parent = parent;
            Name = "NOT";
            Priority = (int) Consts.Priorities.NOT;
        }


        public override void SetChild(int index, Symbol symbol)
        {
            if (index != 0)
                throw new InvalidChildIndexException("Index: " + index);

            this.child = symbol;
            symbol.IndexInParent = index;
            symbol.Parent = this;
        }

        public override List<Symbol> GetChildren() => new List<Symbol> { child };
    }
}
