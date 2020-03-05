using System;
using System.Collections.Generic;

using ifmo_ca_lab_3.Lexical;

namespace ifmo_ca_lab_3
{
    class Program
    {
        static string str = "add(1, pow(54, x))";

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
                RetrievedObject = Parser.ParseTokenList(Tokens);
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
            table.Write();
        }
    }
}
