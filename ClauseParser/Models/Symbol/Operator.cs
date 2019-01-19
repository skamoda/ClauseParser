using ClauseParser.Code;
using ClauseParser.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class Operator : Symbol
    {

        public Symbol Left { get; set; }
        public Symbol Right { get; set; }
        //public Operator() : base() { }
        public Operator(Symbol parent = null, String name = "")
        {
            Parent = parent;
            Name = name;
            Priority = (int)Enum.Parse(typeof(Consts.Priorities), this.Name);
        }

        public override List<Symbol> GetChildren() => new List<Symbol> { Left, Right };

        public override void SetChild(int index, Symbol value)
        {
            switch (index)
            {
                case 0:
                    Left = value;
                    break;
                case 1:
                    Right = value;
                    break;
                default:
                    throw new InvalidChildIndexException("Index: " + index);
            }
            value.IndexInParent = index;
            value.Parent = this;
        }
    }
}
