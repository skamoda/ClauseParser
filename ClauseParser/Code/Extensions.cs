using System;
using System.Collections.Generic;
using System.Linq;

namespace ClauseParser.Code
{
    public static class Extensions
    {
        public static void Print<T>(this List<T> list)
        {
            list.ForEach(t =>
            {
                Console.WriteLine(t.ToString());
            });
        }

        public static bool IsSmallLetter(this char c)
        {

            return c >= 'a' && c <= 'z';
        }

        public static bool IsCapitalLetter(this char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public static bool IsDigit(this char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsIdentifier(this char c)
        {
            return IsSmallLetter(c) || IsCapitalLetter(c) || IsDigit(c);
        }

        public static bool IsOperator(this char c)
        {
            int code = (int)c;
            switch (code)
            {
                case Consts.AND:
                case Consts.OR:
                case Consts.IMPLIES:
                case Consts.EQUALS:
                case Consts.COMMA:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsQuantifier(this char c)
        {
            int code = (int)c;
            switch (code)
            {
                case Consts.FORALL:
                case Consts.EXISTS:
                    return true;
                default:
                    return false;
            }

        }

        public static bool IsNegation(this char c)
        {
            return c == Consts.NOT;
        }

        public static List<T> Reverse<T>(this List<T> list)
        {
            list.Reverse();
            return list;
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static string Replace(this string s)
        {
            s = s.Replace("FORALL", "∀");
            s = s.Replace("EXISTS", "∃");
            s = s.Replace("AND", "∧");
            s = s.Replace("OR", "∨");
            s = s.Replace("IMPLIES", "⇒");
            s = s.Replace("EQUALS", "⇔");
            s = s.Replace("NOT", "¬");
            return s;
        }
    }
}
