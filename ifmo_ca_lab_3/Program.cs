using System;
using System.Collections.Generic;
using System.Linq;

using ifmo_ca_lab_3.Analysis.Lexington;
using ifmo_ca_lab_3.Talk;
using ifmo_ca_lab_3.Evaluation;
using ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ifmo_ca_lab_3
{
    class Program
    {
        // АХТУНГ
        // Раскаменчивай нужный регион, каменчивай ненужный
        // Можно свернуть регион и закомментить, но лудше тогда начинать с нижнего камента
        // Или забить хуй и тупа юзать CTRL+K CTRL+F, чтобы идэйе исправила пошедшие по пизде отступы и проч.
        // Ващета я это сделал т.к. произвел слияние вместо


        #region Код Патоха 🤮
        static void Main()
        {
            Expression expr = new Expression("Sum");
            var pow = new Expression("Pow");
            pow.Operands = new List<IExpression>() { new Expression("Symbol", "a"), new Value(2) };
            expr.Operands = new List<IExpression> { new Value(2), new Expression("Symbol", "x"), pow, new Value(5) };
            expr.Evaluate();
        }
        #endregion

        /*
        #region Код Мирата 😎
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
                Speaker.TalkTokens(ref Tokens);

                // Работа парсера
                //RetrievedObject = Parser.ParseTokenList(Tokens);

                // Вывод дерева объектов
                //Speaker.TalkObjectTrees(RetrievedObject, 0);
            }
            catch (Exception ex)
            {
                // Вывод информации об ошибке
                Console.WriteLine(ex.Message);
            }
        }

        private static void NormalizeString(ref string str)
        {
            str = str.Replace(" ", String.Empty).ToLower();
        }
        #endregion*/
    }
}
