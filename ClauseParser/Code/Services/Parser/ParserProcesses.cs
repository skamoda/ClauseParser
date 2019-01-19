using ClauseParser.Models;
using ClauseParser.Models.Symbol;
using System;
using System.Collections.Generic;

namespace ClauseParser.Code.Services.Parser
{
    public static class ParserProcesses
    {
        // 1st step
        public static Step EliminateEqualsStep(Step step)
        {
            List<Symbol> symbols = step.Top.ReverseBFS();
            step.Title = "TODO: TITLE";

            foreach (Symbol symbol in symbols)
            {
                if (symbol is Operator && symbol.Name == "EQUALS")
                {
                    Symbol parent = symbol.Parent;

                    Symbol left = symbol.Children[0];
                    Symbol right = symbol.Children[1];

                    Symbol newOperator = new Operator(name: "AND");
                    Symbol newLeft = new Operator(name: "IMPLIES");
                    Symbol newRight = new Operator(name: "IMPLIES");

                    newLeft.SetChild(0, left.Clone());
                    newLeft.SetChild(1, right.Clone());

                    newRight.SetChild(0, right.Clone());
                    newRight.SetChild(1, left.Clone());

                    newOperator.SetChild(0, newLeft);
                    newLeft.Parent = newOperator;
                    newOperator.SetChild(1, newRight);
                    newRight.Parent = newOperator;

                    if (parent == null)
                    {
                        step.Top = newOperator;
                    }
                    else
                    {
                        parent.SetChild(symbol.IndexInParent, newOperator);
                    }
                }
            }

            return step;
        }

        // 2nd step
        public static Step EliminateImpliesStep(Step step)
        {
            List<Symbol> symbols = step.Top.ReverseBFS();
            step.Title = "TODO: TITLE";

            foreach (Symbol symbol in symbols)
            {
                if (symbol is Operator && symbol.Name == "IMPLIES")
                {

                    Symbol parent = symbol.Parent;

                    Symbol left = symbol.Children[0];
                    Symbol right = symbol.Children[1];

                    Symbol newOperator = new Operator(name: "OR");
                    Symbol newLeft = new Negation();

                    newLeft.SetChild(0, left.Clone());

                    newOperator.SetChild(0, newLeft);
                    newOperator.SetChild(1, right.Clone());

                    if (parent == null)
                    {
                        step.Top = newOperator;
                    }
                    else
                    {
                        parent.SetChild(symbol.IndexInParent, newOperator);
                    }

                }
            }

            return step;
        }

        // 3rd step
        public static Step MoveNegationInwardsStep(Step step)
        {
            throw new NotImplementedException("TODO");
        }

        // 4th step
        // each existential variable is replaced by a Skolem constant 
        // or Skolem function of the enclosing universally quantified variables.
        public static Step SkolemizeStep(Step step)
        {
            throw new NotImplementedException("TODO");
        }

        // 5th step
        public static Step DropForAllQuantifierStep(Step step)
        {
            throw new NotImplementedException("TODO");
        }

        // 6th step
        public static Step DistributeConjuctsStep(Step step)
        {
            throw new NotImplementedException("TODO");
        }
        
    }
}
















































































