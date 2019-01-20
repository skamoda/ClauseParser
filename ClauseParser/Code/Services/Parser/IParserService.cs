using System.Collections.Generic;
using ClauseParser.Models;
using ClauseParser.Models.Symbol;

namespace ClauseParser.Code.Services.Parser
{
    public interface IParserService
    {
        List<Step> Parse(string text);
        List<Symbol> Collect(string text);
        List<Symbol> ConvertToPostfix(List<Symbol> list);
    }
}
