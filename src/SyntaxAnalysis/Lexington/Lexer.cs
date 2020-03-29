using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    public static class Lexer
    {
        // Алфавит лексера
        private static readonly string s_letters = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string s_numbers = "0123456789";
        private static readonly string s_brackets = "()";
        private static readonly string s_comma = ",";
        private static readonly string s_modificators = "-+";
        private static readonly string s_underline = "_";
        private static readonly string s_alphabet = s_letters + s_numbers + s_brackets + s_comma + s_modificators + s_underline;

        // Список найденных токенов
        private static List<Token> s_tokens;

        public static List<Token> Tokenize(string str)
        {
            AlphabetCheck(str);
            s_tokens = new List<Token>();
            for (var i = 0; i < str.Length; i++)
            {
                var token = GetToken(str.Substring(i));
                if (token.Content != null)
                {
                    s_tokens.Add(token);
                    i += token.Content.Length - 1;
                }
                else
                {
                    throw new NoSuitableTokenException(str.Substring(i));
                }
            }
            s_tokens.Add(new Token(TokenType.EOF, ""));
            return s_tokens;
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

        private static Token GetToken(string str)
        {
            foreach (var (tokenType, tokenDefinition) in Grammar.TokenDefinitions)
            {
                var match = (new Regex(tokenDefinition)).Match(str);
                if (match.Success)
                {
                    return new Token(tokenType, match.Value);
                }
            }
            return default;
        }
    }
}
