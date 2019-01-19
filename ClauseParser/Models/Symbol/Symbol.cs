using ClauseParser.Code;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ClauseParser.Models.Symbol
{
    public abstract class Symbol : IEnumerable
    {
        public bool SubtreeReplaced { get; set; } = false;
        public string Name { get; set; } = String.Empty;
        public int Priority { get; set; } = 0;
        public int IndexInParent { get; set; }
        public Symbol Parent { get; set; }
        public abstract List<Symbol> GetChildren();
        public List<Symbol> Children => GetChildren();
        public IEnumerator GetEnumerator() => GetChildren().GetEnumerator();
        //protected abstract void SetChild(int index, Symbol value);
        public abstract void SetChild(int index, Symbol value);
        public int ChildrenCount => GetChildren().Count;
        public bool IsFinal => ChildrenCount == 0;
        public bool Precedes(Symbol other) => Priority > other.Priority;

        internal Symbol SetParent(Symbol symbol)
        {
            Parent = symbol;
            return this;
        }

        public Symbol this[int i]
        {
            get { return GetChildren()[i]; }
            set { SetChild(i, value); }
        }

        public List<Symbol> BFS()
        {
            List<Symbol> result = new List<Symbol>();
            Queue<Symbol> Q = new Queue<Symbol>();

            Q.Enqueue(this);

            while (Q.Count > 0)
            {
                Symbol u = Q.Dequeue();
                foreach (Symbol v in u.GetChildren())
                {
                    Q.Enqueue(v);
                }
                result.Add(u);
            }
            return result;
        
        }

        public List<Symbol> ReverseBFS()
        {
            return Extensions.Reverse(BFS());
        }
        
        public virtual Symbol FillChildren(ref Stack<Symbol> stack)
        {
            if (ChildrenCount == 0)
                return this;

            for(int i = ChildrenCount - 1; i>=0; --i)
            {
                var symbol = stack.Pop();
                symbol.Parent = this;

                this[i] = symbol;
            }
            return this;
        }

        public override string ToString()
        {
            return GetType().Name + ": " + Name;
        }

        public Symbol Clone()
        {
            Symbol n;
            if (this is Operator)
            {
                n = new Operator(Parent, Name);
            }
            else if (this is Quantifier)
            {
                n = new Quantifier(Parent, Name);
            }
            else if(this is Function)
            {
                n = new Function(Parent, Name, ChildrenCount);
            }
            else
            {
                n = (Symbol)Activator.CreateInstance(GetType());
                n.Name = Name;
                n.Priority = Priority;
                n.Parent = Parent;
            }
            n.IndexInParent = IndexInParent;
            for (int i = 0; i < ChildrenCount; ++i)
            {
                n[i] = this[i];
            }
            return n;
        }

        
    }
}
