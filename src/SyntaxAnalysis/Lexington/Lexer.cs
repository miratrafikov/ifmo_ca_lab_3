using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington
{
    public static class Lexer
    {
        // Алфавит лексера
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string numbers = "0123456789";
        private static readonly string brackets = "()";
        private static readonly string comma = ",";
        private static readonly string alphabet = letters + numbers + brackets + comma;

        // Список найденных токенов
        private static readonly List<Token> Tokens = new List<Token>();

        public static List<Token> Tokenize(string str)
        {
            AlphabetCheck(str);
            while (!string.IsNullOrEmpty(str))
            {
                var token = GetToken(str);
                if (!token.Equals(default))
                {
                    Tokens.Add(token);
                    str = str.Substring(token.Content.Length);
                }
            }
            return Tokens;
        }

        private static void AlphabetCheck(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!alphabet.Contains(str[i]))
                {
                    throw new Exception($"Col. #{i}: Non-alphabetical character \"{str[i]}\".");
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
