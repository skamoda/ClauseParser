using System;
using System.Collections.Generic;

namespace ClauseParser.Models
{
    public class Parser
    {
        public List<Symbol> LoadText(string text)
        {
            // for future use - it can be be bracket, operator, negation, quantifier etc.
            List<Symbol> loadedtext = new List<Symbol>();

            for (int i = 0; i < text.Length; ++i)
            {
                if (Char.IsWhiteSpace(text[i]))
                {
                    continue;
                }
                
                // get symbols and character check
                // insert into list
                
            }

            return loadedtext;
        }
    }
}
