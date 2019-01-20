using ClauseParser.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ClauseParser.Models.Symbol
{
    /// <summary>
    /// Symbol base model
    /// </summary>
    public abstract class Symbol : IEnumerable
    {
        /// <summary>
        /// Flag indicating if subtree has been replaced
        /// </summary>
        public bool SubtreeReplaced { get; set; } = false;

        /// <summary>
        /// Symbol's name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Symbol's priority
        /// </summary>
        public int Priority { get; set; } = 0;

        /// <summary>
        /// Symbol's index in parent symbol
        /// </summary>
        public int IndexInParent { get; set; }
        
        /// <summary>
        /// Parent reference
        /// </summary>
        public Symbol Parent { get; set; }

        /// <summary>
        /// Internal method for getting list of children
        /// </summary>
        /// <returns></returns>
        protected abstract List<Symbol> GetChildren();

        /// <summary>
        /// Method for getting list of children
        /// </summary>
        /// <returns></returns>
        public List<Symbol> Children => GetChildren();

        /// <summary>
        /// Enumerator implementation
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => GetChildren().GetEnumerator();

        //protected abstract void SetChild(int index, Symbol value);
        /// <summary>
        /// Child setter
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public abstract void SetChild(int index, Symbol value);

        /// <summary>
        /// Children count
        /// </summary>
        public int ChildrenCount => GetChildren().Count;

        /// <summary>
        /// Flag indicating if is final
        /// </summary>
        public bool IsFinal => ChildrenCount == 0;

        /// <summary>
        /// Method for checking if this symbol precedes another symbol
        /// </summary>
        /// <param name="other">Symbol for comparison</param>
        /// <returns></returns>
        public bool Precedes(Symbol other) => Priority > other.Priority;

        /// <summary>
        /// Parent setter
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        internal Symbol SetParent(Symbol symbol)
        {
            Parent = symbol;
            return this;
        }

        /// <summary>
        /// Array operator
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Symbol this[int i]
        {
            get { return GetChildren()[i]; }
            set { SetChild(i, value); }
        }

        /// <summary>
        /// Get BFS symbol list
        /// </summary>
        /// <returns>BFS list</returns>
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

        /// <summary>
        /// Get reversed BFS list
        /// </summary>
        /// <returns>Reversed BFS list</returns>
        public List<Symbol> ReverseBFS()
        {
            return Extensions.Reverse(BFS());
        }
        
        /// <summary>
        /// Fill children
        /// </summary>
        /// <param name="stack">Stack of children</param>
        /// <returns>Symbol</returns>
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

        /// <summary>
        /// ToString() override method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GetType().Name + ": " + Name;
        }

        /// <summary>
        /// Serializer method
        /// </summary>
        /// <returns>Serialized object as string</returns>
        public virtual string Serialize()
        {
            var stringBuilder = new StringBuilder();

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Clone method
        /// </summary>
        /// <returns>Cloned symbol</returns>
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
