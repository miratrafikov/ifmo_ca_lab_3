using System;
using System.Collections.Generic;
using System.Text;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    static class Main
    {
        private static string inputString;
        public static string InputString
        {
            get { return inputString; }
            set
            {
                inputString = NormalizeString(value);
            }
        }

        private static List<Token> tokens;
        private static IExpression element;

        public static void AcquireTokens()
        {
            tokens = Lexer.Tokenize(InputString);
        }

        public static void ParseTokens()
        {
            element = Parser.Parse(tokens);
        }

        public static string NormalizeString(string str)
        {
            return str.Replace(" ", string.Empty).ToLower();
        }
    }
}
