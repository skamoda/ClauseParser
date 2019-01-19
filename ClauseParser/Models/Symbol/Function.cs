using ClauseParser.Code;
using ClauseParser.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    public class Function : Symbol
    {
        private readonly Symbol[] Arguments;
        public Function() : base() { }
        public Function(Symbol parent = null, String name = "", int argumentCount = 0)
        {
            Parent = parent;
            Name = name;
            Priority = (int) Consts.Priorities.FUNCTION;
            Arguments = new Symbol[argumentCount];
        }
        
        public override Symbol FillChildren(ref Stack<Symbol> stack)
        {
            if (ChildrenCount == 0)
                return this;

            Symbol current = stack.Pop();
            for (int alreadyPut = 0; alreadyPut < ChildrenCount;)
            {
                if(current is Operator && current.Name == "COMMA")
                {
                    if (current[1] is Operator && current[1].Name == "COMMA")
                        throw new Exception("Unexpected right child of comma being comma");
                    else
                    {
                        this[ChildrenCount - alreadyPut - 1] = current[1].SetParent(this);
                        ++alreadyPut;
                        if (current[0] is Operator && current[0].Name == "COMMA")
                        {
                            current = current[0];
                            //++alreadyPut;
                            continue;
                        }
                        else
                        {
                            this[ChildrenCount - alreadyPut - 1] = current[0].SetParent(this);
                            ++alreadyPut;
                        }
                    }

                }
                else if(ChildrenCount != 1)
                {
                    throw new Exception("Expected a comma");
                }
                else
                {
                    this[0] = current;
                    ++alreadyPut;
                }
                
            }

            return this;
        }

        public override void SetChild(int index, Symbol symbol)
        {
            try
            {
                Arguments[index] = symbol;
            }
            catch (Exception)
            {
                throw new InvalidChildIndexException("Index: " + index);
            }
            
            symbol.IndexInParent = index;
            symbol.Parent = this;
        }

        public override string Serialize()
        {
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append(Name);
            stringBuilder.Append("(");

            foreach (var argument in Arguments)
            {
                stringBuilder.Append($"{argument.Serialize()}");
                if (Arguments.LastOrDefault() != argument)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append(")");
            


            return stringBuilder.ToString();
        }

        public override List<Symbol> GetChildren() => new List<Symbol>(Arguments);
    }
}
