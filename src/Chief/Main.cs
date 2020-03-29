using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    static class Main
    {
        private static string s_inputString;
        public static string InputString
        {
            get { return s_inputString; }
            set
            {
                s_inputString = NormalizeString(value);
            }
        }

        private static List<Token> s_tokens;
        private static IElement s_tree;

        public static void GetTokensFromString()
        {
            s_tokens = Lexer.Tokenize(InputString);
        }

        public static void GetTreeFromTokens()
        {
            s_tree = Parser.Parse(s_tokens);
        }

        public static void GetTreeFromString()
        {
            s_tree = Parser.Parse(Lexer.Tokenize(InputString));
        }

        private static string NormalizeString(string str)
        {
            return str.Replace(" ", string.Empty).ToLower();
        }
    }
}
