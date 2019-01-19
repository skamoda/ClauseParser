using ClauseParser.Models;
using ClauseParser.Models.Exceptions;
using ClauseParser.Models.Symbol;

namespace ClauseParser.Code
{
    public static class SymbolFactory
    {
        public static Models.Symbol.Symbol GetSymbol(char c)
        {
            if (c == '(')
            {
                return new OpeningBracket();
            }

            if (c == ')')
            {
                return new ClosingBracket();
            }

            if (c.IsOperator())
            {
                return new Operator(name: Global.NamesDictionary[(int)c]);
            }

            if (c.IsNegation())
            {
                return new Negation();
            }

            if (c.IsQuantifier())
            {
                return new Quantifier(name: Global.NamesDictionary[(int)c]);
            }

            return null;
        }

        public static Models.Symbol.Symbol GetSymbol(string text, ref int i)
        {
            if (i >= text.Length || i < 0)
                return null;

            char c = text[i];
            string identifier = "";
            if (c.IsSmallLetter())
            {
                identifier = "";
                while (true)
                {
                    identifier += text[i];
                    if (i + 1 >= text.Length || text[i + 1].IsIdentifier() == false)
                        break;
                    ++i;
                }
                return new Variable(name: identifier);
            }

            if (c.IsCapitalLetter())
            {
                identifier = "";
                while (true)
                {
                    identifier += text[i];
                    if (i + 1 >= text.Length || text[i + 1].IsIdentifier() == false)
                        break;
                    ++i;
                }
                if (i + 1 < text.Length && text[i + 1] == '(')
                {
                    int argumentCount = 0;
                    int bracketCount = 0;
                    for(int j = i + 1; ;++j)
                    {
                        if (text[j] == '(')
                        {
                            ++bracketCount;
                        }
                        else if (text[j] == ')')
                        {
                            --bracketCount;
                            if (bracketCount == 0)
                                break;
                        }
                        else if (bracketCount == 1 && text[j] == ',')
                        {
                            ++argumentCount;
                        }

                        if (j + 1 >= text.Length)
                            throw new SyntaxErrorException();
                    }
                    return new Function(name: identifier, argumentCount: argumentCount + 1);
                }                 
                else
                    return new Constant(name: identifier);
            }
            
            return null;
        }

        
    }
}
