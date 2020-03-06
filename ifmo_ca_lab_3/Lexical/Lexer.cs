using System;
using System.Collections.Generic;

namespace ifmo_ca_lab_3.Lexical
{
    static class Lexer
    {
        // Алфавит лексера
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string numbers = "0123456789";
        private static readonly string brackets = "()";
        private static readonly string comma = ",";
        private static readonly string space = " ";
        private static readonly string alphabet = letters + numbers + brackets + comma + space;

        // Возможные наименования функций
        private static readonly string[] Functions = { "sum", "mul", "pow" };

        // Список найденных токенов
        private static List<Token> Tokens = new List<Token>();

        public static List<Token> Tokenize(string str)
        {
            for (LexerState.currPos = 0; LexerState.currPos < str.Length; LexerState.currPos++)
            {
                char ch = str[LexerState.currPos];
                if (!alphabet.Contains(ch))
                {
                    throw new Exception($"Non-alphabetical character \"{ch}\" at position {LexerState.currPos}.");
                }
                switch (LexerState.tokenExpected)
                {
                    case (int)TokenExpectations.No:
                        LexerState.tokenStartPos = LexerState.currPos;
                        if (letters.Contains(ch))
                        {
                            LexerState.tokenExpected = (int)TokenExpectations.Letter;
                            continue;
                        }
                        if (numbers.Contains(ch))
                        {
                            LexerState.tokenExpected = (int)TokenExpectations.Number;
                            continue;
                        }
                        MakeToken(str);
                        break;
                    case (int)TokenExpectations.Letter:
                        if (CharDisallowed(ch))
                        {
                            throw new Exception($"Error: Forbidden character \"{ch}\" at position {LexerState.currPos}.");
                        }
                        if (!letters.Contains(ch))
                        {
                            MakeToken(str);
                            LexerState.tokenStartPos = LexerState.currPos;
                            MakeToken(str);
                        }
                        break;
                    case (int)TokenExpectations.Number:
                        if (CharDisallowed(ch))
                        {
                            throw new Exception($"Error: Forbidden character \"{ch}\" at position {LexerState.currPos}.");
                        }
                        if (!numbers.Contains(ch))
                        {
                            MakeToken(str);
                            LexerState.tokenStartPos = LexerState.currPos;
                            MakeToken(str);
                        }
                        break;
                }
            }
            if (LexerState.tokenExpected != (int)TokenExpectations.No)
            {
                LexerState.currPos++;
                MakeToken(str);
            }
            return Tokens;
        }

        private static bool CharDisallowed(char ch)
        {
            switch (LexerState.tokenExpected)
            {
                case (int)TokenExpectations.Letter:
                    if (numbers.Contains(ch) || space.Contains(ch))
                    {
                        return true;
                    }
                    break;
                case (int)TokenExpectations.Number:
                    if (letters.Contains(ch) || space.Contains(ch))
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
            Tokens.Add(new Token(tokenType, tokenContent, LexerState.tokenStartPos));

            // Сброс состояния ожидания
            LexerState.tokenExpected = (int)TokenExpectations.No;
        }

        private static int DetermineTokenType(string tokenContent)
        {
            switch (LexerState.tokenExpected)
            {
                case (int)TokenExpectations.Letter:
                    if (Array.Exists(Functions, el => el == tokenContent))
                    {
                        switch (tokenContent)
                        {
                            case "sum":
                                return (int)TokenTypes.Sum;
                            case "mul":
                                return (int)TokenTypes.Mul;
                            case "pow":
                                return (int)TokenTypes.Pow;
                        }
                    }
                    return (int)TokenTypes.Variable;
                case (int)TokenExpectations.Number:
                    return (int)TokenTypes.Number;
                default:
                    if (tokenContent == "(")
                    {
                        return (int)TokenTypes.LeftBracket;
                    }
                    if (tokenContent == ")")
                    {
                        return (int)TokenTypes.RightBracket;
                    }
                    if (tokenContent == ",")
                    {
                        return (int)TokenTypes.Comma;
                    }
                    return (int)TokenTypes.Space;
            }
        }
    }
}
