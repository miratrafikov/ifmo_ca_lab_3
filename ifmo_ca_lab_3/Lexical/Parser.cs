using System;
using System.Collections.Generic;

using ifmo_ca_lab_3.Base;
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
            return new SumExpression();
        }
    }
}
