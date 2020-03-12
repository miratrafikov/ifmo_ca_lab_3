using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Expressions;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    /*
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
            if (Tokens[itToken].type == (int)TokenType.Number)
            {
                object result = ParseNumber();
                itToken++;
                return result;
            }
            if (Tokens[itToken].type == (int)TokenType.Variable)
            {
                object result = ParseVariable();
                itToken++;
                return result;
            }
            if (Tokens[itToken].type == (int)TokenType.Sum || Tokens[itToken].type == (int)TokenType.Mul ||
                Tokens[itToken].type == (int)TokenType.Pow)
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
            switch (Tokens[itToken].type)
            {
                case (int)TokenType.Sum:
                    return ParseSumExpression();
                case (int)TokenType.Mul:
                    return ParseMulExpression();
                case (int)TokenType.Pow:
                    return ParsePowExpression();
            }
            return new SumExpression();
        }

        private static Expression ParseSumExpression()
        {
            List<IOperand> Operands = new List<IOperand>();
            itToken++;
            if (Tokens[itToken].type != (int)TokenType.LeftBracket)
            {
                throw new Exception($"Expected bracket token, token #{itToken}");
            }
            while (true)
            {
                itToken++;
                if (Tokens[itToken].type != (int)TokenType.Number && Tokens[itToken].type != (int)TokenType.Variable &&
                    Tokens[itToken].type != (int)TokenType.Sum && Tokens[itToken].type != (int)TokenType.Mul &&
                    Tokens[itToken].type != (int)TokenType.Pow)
                {
                    throw new Exception($"Expected number, variable or expression token, token #{itToken}");
                }
                switch (Tokens[itToken].type)
                {
                    case (int)TokenType.Number:
                        Operands.Add(ParseNumber());
                        break;
                    case (int)TokenType.Variable:
                        Operands.Add(ParseVariable());
                        break;
                    default:
                        Operands.Add(ParseExpression());
                        break;
                }
                itToken++;
                if (Tokens[itToken].type == (int)TokenType.RightBracket)
                {
                    break;
                }
                if (Tokens[itToken].type != (int)TokenType.Comma)
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
    */
}
