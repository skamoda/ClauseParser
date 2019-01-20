using ClauseParser.Code.Services.Parser;
using ClauseParser.Models.Symbol;
using System;
using System.Collections.Generic;

namespace ClauseParser.Models
{
    public class Step
    {
        public Symbol.Symbol Top { get; set; }

        public string Title { get; set; }

        public Step()
        {

        }

        public Step(List<Symbol.Symbol> postfixList)
        {
            Stack<Symbol.Symbol> symbolStack = new Stack<Symbol.Symbol>();

            foreach (Symbol.Symbol t in postfixList)
            {
                // If symbol must have children fill it
                // Regardless of presence of children, put it on stack
                symbolStack.Push(t.FillChildren(ref symbolStack));
            }

            // Only thing left on the stack should be the root node of the tree
            if (symbolStack.Count != 1)
                throw new Exception("Undexpected stack size: " + symbolStack.Count);

            Top = symbolStack.Peek();
        }

        public void Simplify()
        {
            bool actionTaken;
            do
            {
                actionTaken = false;
                List<Symbol.Symbol> symbolList = Top.ReverseBFS();
                foreach (var symbol in symbolList)
                {
                    if (symbol is Operator)
                    {
                        Symbol.Symbol left = symbol.GetChildren()[0];
                        Symbol.Symbol right = symbol.GetChildren()[1];

                        if (left is Variable && right is Variable)
                        {
                            if (left.Name == right.Name)
                            {
                                if (symbol.Name == "AND" || symbol.Name == "OR" || symbol.Name == "IMPLIES")
                                {
                                    actionTaken = true;
                                    Symbol.Symbol newSymbol = new Variable(name: left.Name);

                                    if (symbol.Parent == null)
                                    {
                                        Top = newSymbol;
                                    }
                                    else
                                    {
                                        symbol.Parent.SetChild(symbol.IndexInParent, newSymbol);
                                    }
                                }

                            }
                        }

                    }
                }
            } while (actionTaken);
        }

        public List<Symbol.Symbol> GetSymbolsSeparated()
        {
            if (Top is Operator && Top.Name == "AND")
            {
                return ParserProcesses.AddChild(Top);
            }

            return new List<Symbol.Symbol> { Top };
        }

        public Step CloneStep()
        {
            var clonedStep = (Step) Activator.CreateInstance(GetType());
            clonedStep.Top = Top.Clone();
            clonedStep.Title = Title;
            return clonedStep;

        }

        
    }
}
