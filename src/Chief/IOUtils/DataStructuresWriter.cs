using System;
using System.Collections.Generic;

using ConsoleTables;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

namespace ShiftCo.ifmo_ca_lab_3.Chief.IOUtils
{
    public static class DataStructuresWriter
    {
        public static void PrintTree(object element, string indent = "", bool last = true)
        {
            var head = ((IElement)element).GetHead();
            switch (element)
            {
                case Number integer:
                    var value = integer.Value.ToString();
                    Console.WriteLine($"{indent}+- {head} {value}");
                    break;
                case Symbol symbol:
                    value = symbol.Value;
                    Console.WriteLine($"{indent}+- {head} {value}");
                    break;
                case Expression expression:
                    Console.WriteLine($"{indent}+- {head}");
                    indent += last ? "   " : "|  ";
                    for (var i = 0; i < expression.Operands.Count; i++)
                    {
                        PrintTree(expression.Operands[i], indent, i == expression.Operands.Count - 1);
                    }
                    break;
            }
        }

        public static void PrintTokens(ref List<Token> tokens)
        {
            var table = new ConsoleTable("Type ID", "Content");
            foreach (var token in tokens)
            {
                table.AddRow(token.Type, token.Content);
            }
            table.Write(Format.Alternative);
        }
    }
}
