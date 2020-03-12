﻿using System;
using System.Collections.Generic;
using System.Linq;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Expressions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3.Talk
{
    static class Speaker
    {
        public static int TalkObjectTrees(object Node, int layer)
        {
            Console.Write($"{string.Concat(Enumerable.Repeat("-", layer * 2))}{Node.GetType().Name}");
            if (Node.GetType() == typeof(SumExpression))
            {
                foreach (IOperand Op in ((Expression)Node).Operands)
                {
                    Console.WriteLine();
                    layer++;
                    layer = TalkObjectTrees(Op, layer);
                }
            }
            if (Node.GetType() == typeof(Value))
            {
                Console.Write($" {((Value)Node).Key}");
            }
            return --layer;
        }

        public static void TalkTokens(ref List<Token> Tokens)
        {
            var table = new ConsoleTables.ConsoleTable("Type ID", "Content");
            foreach (Token Token in Tokens)
            {
                table.AddRow(Token.type, Token.content);
            }
            table.Write(ConsoleTables.Format.Alternative);
        }
    }
}
