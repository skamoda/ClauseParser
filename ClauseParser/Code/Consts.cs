using System.Collections.Generic;

namespace ClauseParser.Code
{
    public static class Consts
    {
        public enum Priorities
        {
            BRACKET,
            COMMA,
            IMPLIES,
            EQUALS,
            OR,
            AND,
            NOT,
            FORALL,
            EXISTS,
            FUNCTION
        };
        // Add dictionaries for consts
        //Unicode codes for operators
        public const int FORALL = 8704;
        public const int EXISTS = 8707;
        public const int AND = 8743;
        public const int OR = 8744;
        public const int IMPLIES = 8658;
        public const int EQUALS = 8660;
        public const int NOT = 172;
        public const int COMMA = 44;


        public static Dictionary<string, int> CodesDictionary = new Dictionary<string, int>()
        {
            {"FORALL", FORALL  },
            {"EXISTS", EXISTS  },
            {"AND", AND  },
            {"OR", OR  },
            {"IMPLIES", IMPLIES  },
            {"EQUALS", EQUALS  },
            {"NOT", NOT }
        };

        public static Dictionary<int, string> NamesDictionary = new Dictionary<int, string>()
        {
            { FORALL, "FORALL" },
            { EXISTS, "EXISTS" },
            { AND, "AND" },
            { OR, "OR" },
            { IMPLIES, "IMPLIES" },
            { EQUALS, "EQUALS" },
            { NOT, "NOT" },
            { COMMA, "COMMA" },
        };
    }
}
