using System.Collections.Generic;

namespace ClauseParser.Models
{
    public static class Global
    {
        //Unicode codes for operators
        public const int FORALL = 8704;
        public const int EXISTS = 8707;
        public const int AND = 8743;
        public const int OR = 8744;
        public const int IMPLIES = 8658;
        public const int EQUALS = 8660;
        public const int NOT = 172;
        public const int COMMA = 44;


        public static Dictionary<int, string> NamesDictionary = new Dictionary<int, string>()
        {
            { 8704, "FORALL" },
            { 8707, "EXISTS" },
            { 8743, "AND" },
            { 8744, "OR" },
            { 8658, "IMPLIES" },
            { 8660, "EQUALS" },
            { 172, "NOT" },
            { 44, "COMMA" },
        };

    }
}
