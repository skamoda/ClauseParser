using System;
using ClauseParser.Code;
using ClauseParser.Models.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    public class Negation : Symbol
    {

        public Symbol Child;
        public Negation()
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

            this.Child = symbol;
            symbol.IndexInParent = index;
            symbol.Parent = this;
        }

        public override string Serialize()
        {
            var stringBuilder = new StringBuilder();

            if (ChildrenCount == 1)
            {
                stringBuilder.Append(Name);
                stringBuilder.Append($"({Child.Serialize()})");
            }
            else
            {
                throw new AggregateException();
            }


            return stringBuilder.ToString();
        }

        protected override List<Symbol> GetChildren() => new List<Symbol> { Child };
    }
}
