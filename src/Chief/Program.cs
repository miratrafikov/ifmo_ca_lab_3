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
            Console.WriteLine("Q 2 Xit");
            string input = "";
            while (input != "q")
            {
                var str = Console.ReadLine();
                NormalizeString(ref str);
                try
                {
                    _tokens = Lexer.Tokenize(str);
                    Speaker.TalkTokens(ref _tokens);
                    _objects = Parser.Parse(_tokens);
                    Speaker.TalkObjectTrees(_objects, 0);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void NormalizeString(ref string str)
        {
            str = str.Replace(" ", string.Empty).ToLower();
        }
    }
}
