using System;
using System.Collections.Generic;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

using Terminal = ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington.TokenType;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public static class Parser
    {
        private static List<Token> _tokens;

        public static IExpression Parse(List<Token> tokens)
        {
            int i = 0;
            _tokens = tokens;
            NT_Root(ref i);
            return default;
        }

        private static bool NT_Root(ref int i)
        {
            if (!NT_Element(ref i)) return false;
            if (!T_EOF(ref i)) return false;
            return true;
        }

        private static bool NT_Element(ref int i)
        {
            if (!NT_Expression(ref i) && !T_Symbol(ref i) && !T_Number(ref i)) return false;
            return true;
        }

        private static bool NT_Expression(ref int i)
        {
            if (!T_Symbol(ref i)) return false;
            if (!T_LeftBracket(ref i)) return false;
            if (!NT_Operand(ref i)) return false;
            if (!T_RightBracket(ref i)) return false;
            return true;
        }

        private static bool NT_Operand(ref int i)
        {
            if (!NT_Element(ref i)) return false;
            if (T_Comma(ref i))
            {
                if (!NT_Operand(ref i)) return false;
            }

            return true;
        }

        private static bool T_EOF(ref int i)
        {
            if (_tokens[i].Type == TokenType.EOF) return true;
            return false;
        }

        private static bool T_Number(ref int i)
        {
            if (_tokens[i].Type != TokenType.Number) return false;
            return true;
        }

        private static bool T_Symbol(ref int i)
        {
            if (_tokens[i].Type != TokenType.Symbol) return false;
            return true;
        }

        private static bool T_LeftBracket(ref int i)
        {
            if (_tokens[i].Type != TokenType.LeftBracket) return false;
            return true;
        }

        private static bool T_RightBracket(ref int i)
        {
            if (_tokens[i].Type != TokenType.RightBracket) return false;
            return true;
        }

        private static bool T_Comma(ref int i)
        {
            if (_tokens[i].Type != TokenType.Comma) return false;
            return true;
        }
    }
}
