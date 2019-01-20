using System;
using ClauseParser.Models.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    public class Variable : Symbol
    {
        public Variable(Symbol parent = null, string name = "")
        {
            Parent = parent;
            Name = name;
        }

        public override void SetChild(int index, Symbol symbol) => throw new InvalidChildIndexException("Index: " + index);

        public override string ToString()
        {
            return "Variable: " + Name;
        }

        public override string Serialize()
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append(Name);


            return stringBuilder.ToString();
        }
        protected override List<Symbol> GetChildren() => new List<Symbol>();
    }
}
