using ClauseParser.Code;
using System;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class Quantifier : Symbol
    {
        public Variable Variable;
        public Symbol Expression;
        //public Quantifier() : base() { }
        public Quantifier(Symbol parent = null, string name = "")
        {
            Parent = parent;
            Name = name;
            Priority = (int) Enum.Parse(typeof(Consts.Priorities), this.Name);
        }

        public override void SetChild(int index, Symbol symbol) {
            switch (index)
            {
                case 0:
                    Variable = symbol as Variable;
                    break;
                case 1:
                    Expression = symbol;
                    break;
                default:
                    throw new Exception("Invalid child index: " + index);

            }
            symbol.IndexInParent = index;
            symbol.Parent = this;
        }

        public override List<Symbol> GetChildren() => new List<Symbol>() { Variable, Expression };
    }
}
