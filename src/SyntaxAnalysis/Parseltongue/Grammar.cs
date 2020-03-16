using System;
using System.Collections.Generic;
using System.Data;
using Terminal = ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington.TokenType;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    static class Grammar
    {
        static Dictionary<NonTerminal, List<List<Object>>> RuleNT = new Dictionary<NonTerminal, List<List<object>>>();

        static Grammar()
        {
            RuleNT.Add(NonTerminal.Root, new List<List<object>>
            {
                new List<object>{NonTerminal.Element, Terminal.EOF}
            });
            RuleNT.Add(NonTerminal.Element, new List<List<object>>
            {
                new List<object>{NonTerminal.Expression},
                new List<object>{Terminal.Symbol},
                new List<object>{Terminal.Number}
            });
            RuleNT.Add(NonTerminal.Expression, new List<List<object>>
            {
                new List<object>{Terminal.Symbol, Terminal.LeftBracket, NonTerminal.Operand, Terminal.RightBracket}
            });
            RuleNT.Add(NonTerminal.Operand, new List<List<object>>
            {
                new List<object>{NonTerminal.Element, NonTerminal.NextOperand}
            });
            RuleNT.Add(NonTerminal.NextOperand, new List<List<object>>
            {
                new List<object>{Terminal.Comma, NonTerminal.Operand},
                new List<object>{NonTerminal.Idle}
            });
            
            
        }
    }
}
