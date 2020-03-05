using System;
using System.Collections.Generic;
using System.Text;

namespace ifmo_ca_lab_3.Lexical
{
    static class Parser
    {
        static string[] regulars = new string[10];
        static int[] opCodes = { (int)TokenTypes.Add, (int)TokenTypes.Sub, (int)TokenTypes.Mul, (int)TokenTypes.Pow };

        static int currToken = 0;

        public static void ParseExpression(List<Token> Tokens)
        {
            switch (Tokens[currToken].type)
            {
                case (int)TokenTypes.Number:
                    return ParseNumber(List < Token > Tokens);
                    break;
                case (int)TokenTypes.Variable:

                    break;
                default:
                    if (Tokens[currToken].type == (int)TokenTypes.Add || Tokens[currToken].type == (int)TokenTypes.Mul ||
                        Tokens[currToken].type == (int)TokenTypes.Pow)
                    {

                    }
                    else
                    {

                    }
                    break;
            }
        }





        public static string Validate(List<Token> Tokens)
        {
            InitializeRegulars();
            Console.WriteLine(regulars[(int)TokenTypes.Add]);

            string str = "";
            foreach (Token Token in Tokens)
            {
                str += Token.type.ToString();
            }
            return str;
        }

        private static void InitializeRegulars()
        {
            regulars[(int)TokenTypes.Add] = BuildRule((int)TokenTypes.Add, false);
        }

        private static string BuildRule(int opCode, bool second)
        {
            string rule = $"_{opCode}_{(int)TokenTypes.LeftBracket}";
            rule += "(";
            foreach(int code in opCodes)
            {
                rule += $"_{code}";
            }
            rule += $"_{(int)TokenTypes.Variable}_{(int)TokenTypes.Number}";
            rule += ")";
            rule += $"_{(int)TokenTypes.Comma}";
            rule += $"_{(int)TokenTypes.RightBracket}";
            return rule;
        }
    }
}
