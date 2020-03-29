using System;
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
            if (string.IsNullOrEmpty(s_inputString))
            {
                throw new Exception("String is not provided for processing.");
            }
            s_tokens = Lexer.Tokenize(InputString);
        }

        public static void GetTokensFromString(string inputString)
        {
            InputString = inputString;
            s_tokens = Lexer.Tokenize(InputString);
        }

        public static void GetTreeFromTokens()
        {
            if (s_tokens == null)
            {
                throw new Exception("No generated tokens list exists.");
            }
            s_tree = Parser.Parse(s_tokens);
        }

        public static void GetTreeFromString()
        {
            if (string.IsNullOrEmpty(s_inputString))
            {
                throw new Exception("String is not provided for processing.");
            }
            s_tree = Parser.Parse(Lexer.Tokenize(InputString));
        }
        
        public static void GetTreeFromString(string inputString)
        {
            InputString = inputString;
            s_tree = Parser.Parse(Lexer.Tokenize(InputString));
        }

        private static string NormalizeString(string str)
        {
            return str.Replace(" ", string.Empty).ToLower();
        }
    }
}
