using System;
using System.Collections.Generic;

using ifmo_ca_lab_3.Base;
using ifmo_ca_lab_3.Base.Interfaces;
using ifmo_ca_lab_3.Base.Expressions;

namespace ifmo_ca_lab_3.Lexical
{
    static class Parser
    {
        // Список токенов и его итератор
        static List<Token> Tokens;
        static int itToken = 0;

        public static object ParseTokenList(List<Token> Tokens)
        {
            Parser.Tokens = Tokens;
            return ParseToken();
        }

        private static object ParseToken()
        {
            if (Tokens[itToken].typeId == (int)TokenTypes.Number)
            {
                object result = ParseNumber();
                itToken++;
                return result;
            }
            if (Tokens[itToken].typeId == (int)TokenTypes.Variable)
            {
                object result = ParseVariable();
                itToken++;
                return result;
            }
            if (Tokens[itToken].typeId == (int)TokenTypes.Sum || Tokens[itToken].typeId == (int)TokenTypes.Mul ||
                Tokens[itToken].typeId == (int)TokenTypes.Pow)
            {
                object result = ParseExpression();
                itToken++;
                return result;
            }
            throw new Exception("Token error");
        }

        private static Value ParseNumber()
        {
            return new Value(Convert.ToInt32(Tokens[itToken].content));
        }

        private static Symbol ParseVariable()
        {
            return new Symbol(Tokens[itToken].content);
        }

        private static Expression ParseExpression()
        {
            switch (Tokens[itToken].typeId)
            {
                case (int)TokenTypes.Sum:
                    return ParseSumExpression();
                case (int)TokenTypes.Mul:
                    return ParseMulExpression();
                case (int)TokenTypes.Pow:
                    return ParsePowExpression();
            }
            return new SumExpression();
        }

        private static Expression ParseSumExpression()
        {
            List<IOperand> Operands = new List<IOperand>();
            itToken++;
            if (Tokens[itToken].typeId != (int)TokenTypes.LeftBracket)
            {
                throw new Exception($"Expected bracket token, token #{itToken}");
            }
            while (true)
            {
                itToken++;
                if (Tokens[itToken].typeId != (int)TokenTypes.Number && Tokens[itToken].typeId != (int)TokenTypes.Variable &&
                    Tokens[itToken].typeId != (int)TokenTypes.Sum && Tokens[itToken].typeId != (int)TokenTypes.Mul &&
                    Tokens[itToken].typeId != (int)TokenTypes.Pow)
                {
                    throw new Exception($"Expected number, variable or expression token, token #{itToken}");
                }
                switch (Tokens[itToken].typeId)
                {
                    case (int)TokenTypes.Number:
                        Operands.Add(ParseNumber());
                        break;
                    case (int)TokenTypes.Variable:
                        Operands.Add(ParseVariable());
                        break;
                    default:
                        Operands.Add(ParseExpression());
                        break;
                }
                itToken++;
                if (Tokens[itToken].typeId == (int)TokenTypes.RightBracket)
                {
                    break;
                }
                if (Tokens[itToken].typeId != (int)TokenTypes.Comma)
                {
                    throw new Exception($"Expected comma token, token #{itToken}");
                }
            }
            return new SumExpression(Operands);
        }

        private static Expression ParseMulExpression()
        {
            return new SumExpression();
        }
        private static Expression ParsePowExpression()
        {
            return new SumExpression();
        }
    }
}
