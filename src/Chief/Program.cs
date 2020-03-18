﻿using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.Talk;
using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3
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
            var minusY = new Expression()
            {
                Head = "Mul",
                Operands = new List<IExpression>() { new Value(-1), new Expression("Symbol", "y") }          
            };
            var expr1 = new Expression("Add");
            expr1.Operands = new List<IExpression>{ new Expression("Symbol", "x"), minusY };
            var expr2 = new Expression("Pow");
            expr2.Operands = new List<IExpression> { expr1, new Value(3) };
            expr2.Evaluate();
            expr2.ToNormalForm();
          }
        #endregion

        /*
        #region Код Мирата 😎
        static string str = "sum(1, sum(54, -5))";

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
