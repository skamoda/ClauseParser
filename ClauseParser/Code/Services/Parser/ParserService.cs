using ClauseParser.Models;
using ClauseParser.Models.Exceptions;
using ClauseParser.Models.Symbol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClauseParser.Code.Services.Parser
{
    public class ParserService
    {
        //List of processes to process each step and return a new one
        private List<Func<Step,Step>> Processes { get; } = new List<Func<Step,Step>>();

        //Configure parser tasks
        public ParserService()
        {
            Processes.Add(ParserProcesses.EliminateEqualsStep);
            Processes.Add(ParserProcesses.EliminateImpliesStep);
            Processes.Add(ParserProcesses.MoveNegationInwardsStep);
            Processes.Add(ParserProcesses.SkolemizeStep);
            Processes.Add(ParserProcesses.DropForAllQuantifierStep);
            Processes.Add(ParserProcesses.DistributeConjuctsStep);  
        }

        private List<Step> Process(Step rawStep)
        {
            List<Step> stepsAfterProcess = new List<Step>();
            Step step = rawStep.CloneStep() as Step;

            stepsAfterProcess.Add(rawStep);
            foreach (var process in Processes)
            {
                
                //var newStepClone = step.CloneStep();
                step.Simplify();

                step = process(step);
                
                //add a clone of `step`, not reference (to the list) ???
                                
                stepsAfterProcess.Add(step);
                //stepsAfterProcess.Add(step);


            }

            return stepsAfterProcess;
        }

        public List<Step> Parse(string text)
        {
            // for dev purposes - hard coded text
            //text = new string("∀x(A(x)⇒(∃y(B(x,y)∧C(x,y))))");

            List<Symbol> parseText = Collect(text);


            List<Symbol> postfixSymbols = ConvertToPostfix(parseText);

            // Create a raw hierarchy
            Step rawStep = new Step(postfixSymbols);

            // List of steps
            var steps = Process(rawStep);
         
            return steps;
        }

        public List<Symbol> Collect(string text)
        {
            List<Symbol> symbols = new List<Symbol>();
            
            for (int i = 0; i < text.Length; ++i)
            {
                if (Char.IsWhiteSpace(text[i]))
                    continue;
                Symbol v = null;
                Console.WriteLine(text[i] + " " + (int)text[i]);
                Symbol t = SymbolFactory.GetSymbol(text[i]);

                if (t == null)
                {
                    t = SymbolFactory.GetSymbol(text, ref i);
                    
                    if (t == null)
                        throw new Exception("Could not produce a symbol from string starting at position " + (i + 1));
                }
                
                if (t is Quantifier)
                {
                    ++i;

                    v = SymbolFactory.GetSymbol(text, ref i);
                    if (v == null)
                        throw new Exception("Invalid syntax after quantifier at position " + (i + 1));
                    
                    if (v is Variable == false)
                        throw new QuantifierRefersToConstantException();
                    
                    symbols.Add(v);

                }
                symbols.Add(t);

            }
            return symbols;
        }

        public List<Symbol> ConvertToPostfix(List<Symbol> list)
        {
            // This will be the final list of symbols
            List<Symbol> result = new List<Symbol>();

            // This will be a temporary stack for operators
            Stack<Symbol> stack = new Stack<Symbol>();

            foreach (Symbol t in list)
            {
                // Variable and Constant cant have children
                if (t is Variable || t is Constant)
                    result.Add(t);
                else if (t is OpeningBracket)
                    stack.Push(t);
                else if (t is ClosingBracket)
                {
                    try
                    {
                        Symbol arrival = stack.Pop();
                        while (arrival is OpeningBracket == false)
                        {
                            result.Add(arrival);
                            arrival = stack.Pop();
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        throw new BracketsNotMatchingException();
                    }
                }
                else
                {
                    while (stack.Count > 0)
                    {
                        if (t.Precedes(stack.Peek()))
                            break;

                        result.Add(stack.Pop());
                    }

                    stack.Push(t);

                }

            }

            result.AddRange(stack.ToList());

            return result;
        }
    }
}
