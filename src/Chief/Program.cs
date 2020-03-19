using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;
using ShiftCo.ifmo_ca_lab_3.Talk;
using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;

namespace ShiftCo.ifmo_ca_lab_3
{
    class Program
    {
        static List<Token> _tokens;
        static IExpression _objects;

        static void Main()
        {
            
            var minusY = new Expression()
            {
                Head = "Mul",
                Operands = new List<IExpression>() { new Value(-1), new Expression("Symbol", "y") }          
            };
            var expr1 = new Expression("Add");
            expr1.Operands = new List<IExpression>{ new Expression("Symbol", "x"), minusY };
            // (-xy)^3
            //expr1.Operands = new List<IExpression> { new Value(5), new Value(6) };
            //expr1.Evaluate();
            var expr2 = new Expression("Pow");
            expr2.Operands = new List<IExpression> { expr1, new Value(2) };
            expr2.Evaluate();
            expr2.ToNormalForm();
        }

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
            
            var str = "sum(x, 2,3, PoW(y, 15), 5)";

            NormalizeString(ref str);
            try
            {
                _tokens = Lexer.Tokenize(str);
                Speaker.TalkTokens(ref _tokens);
                _objects = Parser.Parse(_tokens);
                Speaker.TalkObjectTrees(_objects, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void NormalizeString(ref string str)
        {
            str = str.Replace(" ", string.Empty).ToLower();
        }*/
    }
}
