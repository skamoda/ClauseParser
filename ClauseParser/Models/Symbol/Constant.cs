using ClauseParser.Models.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    public class Constant : Symbol
    {
        public Constant() : base() { }
        public Constant(Symbol parent = null, string name = "")
        {
            Parent = parent;
            Name = name;
        }
        public override void SetChild(int index, Symbol symbol) => throw new InvalidChildIndexException("Index: " + index);

        public virtual string Serialize()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Name);


            return stringBuilder.ToString();
        }
        public override List<Symbol> GetChildren() => new List<Models.Symbol.Symbol>();
    }
}
