using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysisTest
{
    [TestClass]
    public class ParserTest
    {
        //[TestMethod]
        //public void GetParseTreeFromString_GivenData_StoresExpectedValue()
        //{
        //    var tokenList = new List<Token>
        //    { 
        //        new Token(TokenType.Symbol, "foo"),
        //        new Token(TokenType.LeftBracket, "("),
        //        new Token(TokenType.Symbol, "bar"),
        //        new Token(TokenType.LeftBracket, "("),
        //        new Token(TokenType.Number, "1"),
        //        new Token(TokenType.Comma, ","),
        //        new Token(TokenType.Number, "2"),
        //        new Token(TokenType.RightBracket, ")"),
        //        new Token(TokenType.Comma, ","),
        //        new Token(TokenType.Number, "53"),
        //        new Token(TokenType.RightBracket, ")"),
        //        new Token(TokenType.EOF, "")
        //    };
        //    tokenList = Lexer.Tokenize("foo(bar(1,2),53)");
        //    var tree = new Expression("foo", new List<IElement>
        //    {
        //        new Expression("bar", new List<IElement>
        //        {
        //            new Integer(1),
        //            new Integer(2)
        //        }),
        //        new Integer(53)
        //    });

        //    var result = (Expression)Parser.Parse(tokenList);

        //    Assert.IsTrue(result.Equals(tree));
        //}
    }
}
