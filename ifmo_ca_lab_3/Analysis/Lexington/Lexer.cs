using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ifmo_ca_lab_3.Analysis.Lexington
{
    static class Lexer
    {
        // Алфавит лексера
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string numbers = "0123456789";
        private static readonly string brackets = "()";
        private static readonly string comma = ",";
        private static readonly string alphabet = letters + numbers + brackets + comma;

        // Список найденных токенов
        private static List<Token> Tokens = new List<Token>();

        public static List<Token> Tokenize(string str)
        {
            return new List<Token>();
        }

        private static bool CharDisallowed(char ch)
        {
            switch (LexerState.tokenExpected)
            {
                case (int)TokenExpectations.Symbol:
                    if (numbers.Contains(ch))
                    {
                        return true;
                    }
                    break;
                case (int)TokenExpectations.Number:
                    if (letters.Contains(ch))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private static void MakeToken(string str)
        {
            // Сбор информации и добавление нового токена в коллекцию
            int contentLength = Convert.ToBoolean(LexerState.tokenExpected) ? LexerState.currPos - LexerState.tokenStartPos : 1;
            string tokenContent = str.Substring(LexerState.tokenStartPos, contentLength);
            int tokenType = DetermineTokenType(tokenContent);
            Tokens.Add(new Token(tokenType, tokenContent));

            // Сброс состояния ожидания
            LexerState.tokenExpected = (int)TokenExpectations.No;
        }

        private static int DetermineTokenType(string tokenContent)
        {
            return 1;
        }
    }
}
