using ClauseParser.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    public class Quantifier : Symbol
    {
        public Variable Variable;
        public Symbol Expression;

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

        public override string Serialize()
        {
            var stringBuilder = new StringBuilder();

            if (ChildrenCount == 2)
            {
                stringBuilder.Append(Name);
                stringBuilder.Append(Children[0].Serialize());
                stringBuilder.Append($"({Children[1].Serialize()})");
            }
            else
            {
                throw new AggregateException();
            }


            return stringBuilder.ToString();
        }

        protected override List<Symbol> GetChildren() => new List<Symbol>() { Variable, Expression };
    }
}
