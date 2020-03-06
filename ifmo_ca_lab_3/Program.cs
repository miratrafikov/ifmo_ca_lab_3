using System;
using System.Collections.Generic;

using ifmo_ca_lab_3.Lexical;
using ifmo_ca_lab_3.Base;
using ifmo_ca_lab_3.Base.Interfaces;
using ifmo_ca_lab_3.Base.Expressions;
using System.Linq;

namespace ifmo_ca_lab_3
{
    class Program
    {
        static string str = "sum(1, sum(54, 5))";

        // Список найденных лексером токенов
        static List<Token> Tokens;

        // Объект, сформированный парсером из списка токенов
        static object RetrievedObject;

        static void Main()
        {
            // Приведение строки к нормальному виду
            NormalizeString(ref str);
            try
            {
                // Работа лексера
                Tokens = Lexer.Tokenize(str);

                // Вывод списка токенов
                OutputTokens();

                // Работа парсера
                //RetrievedObject = Parser.ParseTokenList(Tokens);

                // Вывод дерева объектов
                //OutputObjectTree(RetrievedObject, 0);
            }
            catch (Exception ex)
            {
                // Вывод информации об ошибке
                Console.WriteLine(ex.Message);
            }
        }

        private static void NormalizeString(ref string str)
        {
            str = str.ToLower().Replace(" ", String.Empty);
        }

        private static void OutputTokens()
        {
            var table = new ConsoleTables.ConsoleTable("Type ID", "Description", "Content", "Starts at");
            foreach (Token Token in Tokens)
            {
                table.AddRow(Token.typeId, Token.typeDescription, Token.content, Token.startPos);
            }
            table.Write(ConsoleTables.Format.Alternative);
        }

        private static int OutputObjectTree(object Node, int layer)
        {
            Console.Write($"{string.Concat(Enumerable.Repeat("-", layer*2))}{Node.GetType().Name}");
            if (Node.GetType() == typeof(SumExpression))
            {
                foreach (IOperand Op in ((Expression)Node).Operands)
                {
                    Console.WriteLine();
                    layer++;
                    layer = OutputObjectTree(Op, layer);
                }
            }
            if (Node.GetType() == typeof(Value))
            {
                Console.Write($" {((Value)Node).Key}");
            }
            return --layer;
        }
    }
}
