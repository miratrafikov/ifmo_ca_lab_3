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
        }
    }
}
