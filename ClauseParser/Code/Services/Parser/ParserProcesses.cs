using ClauseParser.Models;
using ClauseParser.Models.Symbol;
using System.Collections.Generic;

namespace ClauseParser.Code.Services.Parser
{
    public static class ParserProcesses
    {
        // 1st step
        public static Step EliminateEqualsStep(Step step)
        {
            List<Symbol> symbols = step.Top.ReverseBFS();
            step.Title = "Eliminating Equals";

            foreach (Symbol symbol in symbols)
            {
                if (symbol is Operator && symbol.Name == "EQUALS")
                {
                    var parentSymbol = symbol.Parent;

                    var leftChildSymbol = symbol.Children[0];
                    var rightChildSymbol = symbol.Children[1];

                    Symbol newSymbolOperator = new Operator(name: "AND");
                    Symbol newChildLeft = new Operator(name: "IMPLIES");
                    Symbol newChildRight = new Operator(name: "IMPLIES");

                    // 1st set (copy) children for new left child of AND statement
                    newChildLeft.SetChild(0, leftChildSymbol.Clone());
                    newChildLeft.SetChild(1, rightChildSymbol.Clone());

                    // 2nd set (copy) children for new right child of AND statement
                    newChildRight.SetChild(0, rightChildSymbol.Clone());
                    newChildRight.SetChild(1, leftChildSymbol.Clone());

                    // 3rd asign children for AND statement
                    // Left one
                    newSymbolOperator.SetChild(0, newChildLeft);
                    newChildLeft.Parent = newSymbolOperator;

                    // and right one
                    newSymbolOperator.SetChild(1, newChildRight);
                    newChildRight.Parent = newSymbolOperator;


                    if (parentSymbol == null)
                    {
                        step.Top = newSymbolOperator;
                    }
                    else
                    {
                        parentSymbol.SetChild(symbol.IndexInParent, newSymbolOperator);
                    }
                }
            }

            return step;
        }

        // 2nd step
        public static Step EliminateImpliesStep(Step step)
        {
            List<Symbol> symbols = step.Top.ReverseBFS();
            step.Title = "Eliminating Implies";

            foreach (Symbol symbol in symbols)
            {
                if (symbol is Operator && symbol.Name == "IMPLIES")
                {

                    var parentSymbol = symbol.Parent;

                    var leftChildSymbol = symbol.Children[0];
                    var rightChildSymbol = symbol.Children[1];

                    Symbol newSymbolOperator = new Operator(name: "OR");
                    Symbol newChildLeft = new Negation();

                    // 1st copy left child of IMPLIES statement
                    newChildLeft.SetChild(0, leftChildSymbol.Clone());

                    // 2nd asign Negation before copied left child of IMPLIES
                    newSymbolOperator.SetChild(0, newChildLeft);

                    // We can only clone because right child if free of change
                    newSymbolOperator.SetChild(1, rightChildSymbol.Clone());

                    if (parentSymbol == null)
                    {
                        step.Top = newSymbolOperator;
                    }
                    else
                    {
                        parentSymbol.SetChild(symbol.IndexInParent, newSymbolOperator);
                    }

                }
            }

            return step;
        }

        // 3rd step
        public static Step MoveNegationInwardsStep(Step step)
        {
            step.Title = "Moving  Negation Inwards";
            bool endConditional;
            do
            {
                endConditional = false;
                List<Symbol> symbols = step.Top.ReverseBFS();
                foreach (var symbol in symbols)
                {
                    if (symbol is Negation && symbol.Children[0].IsFinal == false)
                    {
                        var parentSymbol = symbol.Parent;
                        var childSymbol = symbol.Children[0];

                        if (childSymbol is Negation) // Negation case
                        {
                            endConditional = true;
                            if (parentSymbol == null)
                            {
                                step.Top = childSymbol.Children[0];
                            }
                            else
                            {
                                parentSymbol.SetChild(symbol.IndexInParent, childSymbol.Children[0]);
                            }
                        }
                        else if (childSymbol is Operator) // AND and OR case
                        {
                            // setting end conditional to true
                            endConditional = true;

                            if (childSymbol.Name == "AND")
                            {
                                Symbol newOperatorSymbol = new Operator(name: "OR");

                                var leftChildSymbol = childSymbol.Children[0];
                                var rightChildSymbol = childSymbol.Children[1];

                                Symbol newLeftChild = new Negation();
                                Symbol newRightChild = new Negation();

                                newLeftChild.SetChild(0, leftChildSymbol.Clone());
                                newRightChild.SetChild(1, rightChildSymbol.Clone());

                                newOperatorSymbol.SetChild(0, newLeftChild);
                                newOperatorSymbol.SetChild(1, newRightChild);

                                if (parentSymbol == null)
                                {
                                    step.Top = newOperatorSymbol;
                                }
                                else
                                {
                                    parentSymbol.SetChild(symbol.IndexInParent, newOperatorSymbol);
                                }

                            }
                            else if (childSymbol.Name == "OR")
                            {
                                Symbol newOperatorSymbol = new Operator(name: "AND");

                                var leftChildSymbol = childSymbol.Children[0];
                                var rightChildSymbol = childSymbol.Children[1];

                                Symbol newLeftChild = new Negation();
                                Symbol newRightChild = new Negation();

                                newLeftChild.SetChild(0, leftChildSymbol.Clone());
                                newRightChild.SetChild(1, rightChildSymbol.Clone());

                                newOperatorSymbol.SetChild(0, newLeftChild);
                                newOperatorSymbol.SetChild(1, newRightChild);

                                if (parentSymbol == null)
                                {
                                    step.Top = newOperatorSymbol;
                                }
                                else
                                {
                                    parentSymbol.SetChild(symbol.IndexInParent, newOperatorSymbol);
                                }

                            }
                        }
                        else if (childSymbol is Quantifier) // FORALL and EXISTS case
                        {
                            // setting end conditional to true
                            endConditional = true;
                            if (childSymbol.Name == "FORALL")
                            {
                                Symbol newOperatorSymbol = new Operator(name: "EXISTS");

                                var leftChildSymbol = childSymbol.Children[0];
                                var rightChildSymbol = childSymbol.Children[1];

                                Symbol newRightChild = new Negation();

                                newRightChild.SetChild(0, rightChildSymbol.Clone());

                                newOperatorSymbol.SetChild(0, leftChildSymbol);
                                newOperatorSymbol.SetChild(1, newRightChild);

                                if (parentSymbol == null)
                                {
                                    step.Top = newOperatorSymbol;
                                }
                                else
                                {
                                    parentSymbol.SetChild(symbol.IndexInParent, newOperatorSymbol);
                                }

                            }
                            else if (childSymbol.Name == "EXISTS")
                            {
                                Symbol newOperatorSymbol = new Operator(name: "FORALL");

                                var leftChildSymbol = childSymbol.Children[0];
                                var rightChildSymbol = childSymbol.Children[1];

                                Symbol newRightChild = new Negation();

                                newRightChild.SetChild(0, rightChildSymbol.Clone());

                                newOperatorSymbol.SetChild(0, leftChildSymbol);
                                newOperatorSymbol.SetChild(1, newRightChild);

                                if (parentSymbol == null)
                                {
                                    step.Top = newOperatorSymbol;
                                }
                                else
                                {
                                    parentSymbol.SetChild(symbol.IndexInParent, newOperatorSymbol);
                                }
                            }
                        }
                    }
                }

            } while (endConditional);

            return step;
        }

        // 4th step
        // each existential variable is replaced by a Skolem constant 
        // or Skolem function of the enclosing universally quantified variables.
        public static Step SkolemizeStep(Step step)
        {
            step.Title = "Skolemizing";
            bool endConditional;

            do
            {
                endConditional = false;
                List<Symbol> symbols = step.Top.ReverseBFS();
                foreach (var symbol in symbols)
                {
                    if (symbol is Quantifier && symbol.Name == "EXISTS" && symbol.SubtreeReplaced == false)
                    {
                        endConditional = true;
                        var currentSymbol = symbol.Parent;
                        List<Quantifier> parentQuantifierList = new List<Quantifier>();
                        while (currentSymbol != null)
                        {
                            if (currentSymbol is Quantifier && currentSymbol.Name == "FORALL")
                            {
                                parentQuantifierList.Add(currentSymbol as Quantifier);
                            }

                            currentSymbol = currentSymbol.Parent;
                        }

                        if (parentQuantifierList.Count > 0)
                        {
                            List<Symbol> subTreeSymbols = symbol.Children[1].BFS();

                            foreach (var treeSymbol in subTreeSymbols)
                            {
                                if (treeSymbol is Variable)
                                {
                                    if (treeSymbol.Name == symbol.Children[0].Name)
                                    {
                                        Symbol newFunctionSymbol = new Function(name: "F" + symbol.Children[0].Name.ToUpper(), argumentCount: parentQuantifierList.Count);
                                        for (int i = parentQuantifierList.Count - 1; i >= 0; --i)
                                        {
                                            newFunctionSymbol.SetChild(i, new Variable(name: parentQuantifierList[i].Children[0].Name));
                                        }
                                        treeSymbol.Parent.SetChild(treeSymbol.IndexInParent, newFunctionSymbol);
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<Symbol> subTreeSymbols = symbol.Children[1].BFS();
                            var variableName = symbol.Children[0].Name.ToUpper();

                            foreach (var treeSymbol in subTreeSymbols)
                            {
                                if (treeSymbol is Variable && treeSymbol.Name == symbol.Children[0].Name)
                                {
                                    Symbol newConstant = new Constant(name: variableName);
                                    treeSymbol.Parent.SetChild(treeSymbol.IndexInParent, newConstant);
                                }
                            }
                        }

                        if (symbol.Parent == null)
                        {
                            step.Top = symbol.Children[1];
                        }
                        else
                        {
                            symbol.Parent.SetChild(symbol.IndexInParent, symbol.Children[1]);
                        }
                    }
                }

            } while (endConditional);


            return step;
        }

        // 5th step
        public static Step DropForAllQuantifierStep(Step step)
        {
            step.Title = "Droping ForAll Quantifiers";
            bool endConditional;
            do
            {
                endConditional = false;
                List<Symbol> symbols = step.Top.ReverseBFS();
                foreach (var symbol in symbols)
                {
                    if (symbol is Quantifier && symbol.Name == "FORALL")
                    {
                        endConditional = true;
                        var oldSymbol = symbol.Parent;
                        var statement = symbol.Children[1];
                        if (oldSymbol == null)
                        {
                            step.Top = statement;
                            statement.Parent = null;
                        }
                        else
                        {
                            oldSymbol.SetChild(symbol.IndexInParent, statement);
                        }
                    }
                }

            } while (endConditional);

            return step;
        }

        // 6th step
        public static Step DistributeConjuctsStep(Step step)
        {
            step.Title = "Distributing Conjucts";
            bool endConditional;

            do
            {
                endConditional = false;

                List<Symbol> symbols = step.Top.ReverseBFS();
                foreach (var symbol in symbols)
                {
                    if (symbol is Operator && symbol.Name == "OR")
                    {
                        var leftSymbolList = new List<Symbol>();
                        var rightSymbolList = new List<Symbol>();

                        if (symbol.Children[0] is Operator && symbol.Children[0].Name == "AND")
                        {
                            leftSymbolList.AddRange(AddChild(symbol.Children[0]));
                        }
                        else
                        {
                            leftSymbolList.Add(symbol.Children[0]);
                        }

                        if (symbol.Children[1] is Operator && symbol.Children[1].Name == "AND")
                        {
                            rightSymbolList.AddRange(AddChild(symbol.Children[1]));
                        }
                        else
                        {
                            rightSymbolList.Add(symbol.Children[1]);
                        }

                        if (leftSymbolList.Count == 1 && rightSymbolList.Count == 1)
                        {
                            continue;
                        }

                        endConditional = true;

                        var orSymbolList = new List<Operator>();

                        foreach (var leftSymbol in leftSymbolList)
                        {
                            foreach (var rightSymbol in rightSymbolList)
                            {
                                var newOperator = new Operator(name: "OR");
                                newOperator.SetChild(0, leftSymbol);
                                newOperator.SetChild(1, rightSymbol);
                                orSymbolList.Add(newOperator);
                            }
                        }

                        Symbol middleMainSymbol = new Operator(name: "AND");
                        var currentSymbol = middleMainSymbol;

                        for (int i = 0; i < orSymbolList.Count - 2; i++)
                        {
                            currentSymbol.SetChild(0, orSymbolList[i]);
                            currentSymbol.SetChild(1, new Operator(name: "AND"));
                            currentSymbol = currentSymbol.Children[1];
                        }
                        currentSymbol.SetChild(0, orSymbolList[orSymbolList.Count - 2]);
                        currentSymbol.SetChild(1, orSymbolList[orSymbolList.Count - 1]);

                        if (symbol.Parent == null)
                        {
                            step.Top = middleMainSymbol;
                        }
                        else
                        {
                            symbol.Parent.SetChild(symbol.IndexInParent, middleMainSymbol);
                        }

                    }

                }

            } while (endConditional);

            return step;
        }

        public static List<Symbol> AddChild(Symbol symbol)
        {
            var symbolList = new List<Symbol>();

            if (symbol.Children[0] is Operator && symbol.Children[0].Name == "AND")
            {
                symbolList.AddRange(AddChild(symbol.Children[0]));
            }
            else
            {
                symbolList.Add(symbol.Children[0]);
            }

            if (symbol.Children[1] is Operator && symbol.Children[1].Name == "AND")
            {
                symbolList.AddRange(AddChild(symbol.Children[1]));
            }
            else
            {
                symbolList.Add(symbol.Children[1]);
            }

            return symbolList;
        }

        public static List<Symbol> GetSymbolsSeparated(Step step)
        {
            if (step.Top is Operator && step.Top.Name == "AND")
            {
                return AddChild(step.Top);
            }

            return new List<Symbol> { step.Top };
        }
    }
}
















































































