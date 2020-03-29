using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;

namespace ShiftCo.ifmo_ca_lab_3.Chief
{
    public static class Main
    {
        private static readonly string s_alphabet = "abcdefghijklmnopqrstuvwxyz" + "0123456789" + "()" + "," + "-+" + "_";
        private static string s_inputString;
        public static List<Token> Tokens { get; private set; }
        public static IElement Tree { get; private set; }

        public static string InputString
        {
            get { return s_inputString; }
            set
            {
                NormalizeString(ref value);
                AlphabetCheck(value);
                s_inputString = value;
            }
        }

        private static void NormalizeString(ref string str)
        {
            str = str.Replace(" ", string.Empty).ToLower();
        }

        private static void AlphabetCheck(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (!s_alphabet.Contains(str[i]))
                {
                    throw new StrangeCharacterException(str[i]);
                }
            }
        }

        public static void GetTokensFromString()
        {
            if (string.IsNullOrEmpty(s_inputString))
            {
                throw new StringNotSetException();
            }
            Tokens = Lexer.Tokenize(InputString);
        }

        public static void GetTokensFromString(string inputString)
        {
            InputString = inputString;
            Tokens = Lexer.Tokenize(InputString);
        }

        public static void GetTreeFromTokens()
        {
            if (Tokens == null)
            {
                throw new TokenListNotSetException();
            }
            Tree = Parser.Parse(Tokens);
        }

        public static void GetTreeFromString()
        {
            if (string.IsNullOrEmpty(s_inputString))
            {
                throw new StringNotSetException();
            }
            Tree = Parser.Parse(Lexer.Tokenize(InputString));
        }

        public static void GetTreeFromString(string inputString)
        {
            InputString = inputString;
            Tree = Parser.Parse(Lexer.Tokenize(InputString));
        }
    }
}
