using System;
using System.Collections.Generic;
using CommandLine;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.Talk;
using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using Parser = ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue.Parser;

namespace ShiftCo.ifmo_ca_lab_3
{
    class Program
    {
        public class CliOptions
        {
            [Option('h', "help", Required = false, HelpText = "See help message")]
            public bool Help { get; set; }
        }

        static List<Token> _tokens;
        static IExpression _objects;

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CliOptions>(args).WithParsed<CliOptions>(cli =>
                {
                    if (cli.Help)
                    {
                       //Console.WriteLine("");
                    }
                }
            );
            Console.WriteLine("q 2 Xit | test 2 test");
            string input = "";
            while (true)
            {
                var str = Console.ReadLine();
                if (str == "test")
                {
                    str = "mul(pow(2,3), mul(x))";
                }
                NormalizeString(ref str);
                try
                {
                    _tokens = Lexer.Tokenize(str);
                    Speaker.TalkTokens(ref _tokens);
                    _objects = Parser.Parse(_tokens);
                    Speaker.PrintElementsTree(_objects);
                    _objects.Evaluate();
                    Speaker.PrintElementsTree(_objects);

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
