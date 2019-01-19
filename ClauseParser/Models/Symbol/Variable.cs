﻿using ClauseParser.Models.Exceptions;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public class Variable : Symbol
    {

        public Variable() : base() { }

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
        public override List<Symbol> GetChildren() => new List<Symbol>();
    }
}
