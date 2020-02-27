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
        private static readonly string[] Functions = { "add", "sub", "mul", "pow" };

        // Список найденных токенов
        private static List<Token> Tokens = new List<Token>();

        public static string Tokenize(string str, ref List<Token> Tokens)
        {
            Lexer.Tokens = Tokens;
            foreach(char ch in str)
            {
                State.currPos++;
                if (!alphabet.Contains(ch))
                {
                    return string.Format("Error: Unrecognized character \"{0}\" at position {1}.", ch, State.currPos);
                }
                switch (State.tokenExpected)
                {
                    case (int)State.TokenExpectations.No:
                        State.tokenStartPos = State.currPos;
                        if (letters.Contains(ch))
                        {
                            State.tokenExpected = (int)State.TokenExpectations.Letter;
                            continue;
                        }
                        if (numbers.Contains(ch))
                        {
                            State.tokenExpected = (int)State.TokenExpectations.Number;
                            continue;
                        }
                        MakeToken(str);
                        break;
                    case (int)State.TokenExpectations.Letter:
                        if (CharDisallowed(ch))
                        {
                            return string.Format("Error: Forbidden character \"{0}\" at position {1}.", ch, State.currPos);
                        }
                        if (!letters.Contains(ch))
                        {
                            MakeToken(str);
                            State.tokenStartPos = State.currPos;
                            MakeToken(str);
                        }
                        break;
                    case (int)State.TokenExpectations.Number:
                        if (CharDisallowed(ch))
                        {
                            return string.Format("Error: Forbidden character \"{0}\" at position {1}.", ch, State.currPos);
                        }
                        if (!numbers.Contains(ch))
                        {
                            MakeToken(str);
                            State.tokenStartPos = State.currPos;
                            MakeToken(str);
                        }
                        break;
                }
            }
            return TokensToString();
        }

        private static bool CharDisallowed(char ch)
        {
            switch (State.tokenExpected)
            {
                case (int)State.TokenExpectations.Letter:
                    if (numbers.Contains(ch) || space.Contains(ch))
                    {
                        return true;
                    }
                    break;
                case (int)State.TokenExpectations.Number:
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
            int contentLength = Convert.ToBoolean(State.tokenExpected) ? State.currPos - State.tokenStartPos : 1;
            string tokenContent = str.Substring(State.tokenStartPos, contentLength);
            int tokenType = DetermineTokenType(tokenContent);
            Tokens.Add(new Token(tokenType, tokenContent, State.tokenStartPos));

            // Сброс состояния ожидания
            State.tokenExpected = (int)State.TokenExpectations.No;
        }

        private static int DetermineTokenType(string tokenContent)
        {
            switch (State.tokenExpected)
            {
                case (int)State.TokenExpectations.Letter:
                    if (Array.Exists(Functions, el => el == tokenContent))
                    {
                        switch (tokenContent)
                        {
                            case "add":
                                return (int)TokenTypes.Add;
                            case "sub":
                                return (int)TokenTypes.Sub;
                            case "mul":
                                return (int)TokenTypes.Mul;
                            case "pow":
                                return (int)TokenTypes.Pow;
                        }
                    }
                    return (int)TokenTypes.Variable;
                case (int)State.TokenExpectations.Number:
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

        private static string TokensToString()
        {
            string str = "";
            foreach (Token Token in Tokens)
            {
                str += $"{Token.type:00} {Token.typeDescription.ToUpper().PadRight(12)} {Token.content.PadRight(3)} {Token.startPos:00}\n";
            }
            return str;
        }
    }
}
