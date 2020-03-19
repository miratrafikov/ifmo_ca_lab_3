using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

using ConsoleTables;

namespace ShiftCo.ifmo_ca_lab_3.Talk
{
    internal static class Speaker
    {
        public static void PrintElementsTree(object element, string indent = "", bool last = true)
        {
            var head = ((IExpression)element).Head.ToLower();
            string? value;
            switch (element)
            {
                case Value number:
                    value = number.Key.ToString();
                    Console.WriteLine($"{indent}+- {head.ToLower()} {value}");
                    break;
                case Expression expression:
                    value = (head == "symbol") ? expression.Key.ToString() : "";
                    Console.WriteLine($"{indent}+- {head.ToLower()} {value}");
                    indent += last ? "   " : "|  ";
                    if (expression.Operands != null)
                    {
                        for (var i = 0; i < expression.Operands.Count; i++)
                        {
                            PrintElementsTree(expression.Operands[i], indent, i == expression.Operands.Count - 1);
                        }
                    }
                    break;
            }
        }

        public static void TalkTokens(ref List<Token> tokens)
        {
            var table = new ConsoleTable("Type ID", "Content");
            foreach (var Token in tokens)
            {
                table.AddRow(Token.Type, Token.Content);
            }
            table.Write(Format.Alternative);
        }
    }
}
