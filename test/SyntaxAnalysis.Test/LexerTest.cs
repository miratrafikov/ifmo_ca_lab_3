using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysisTest
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void Tokenize_GivenRandomValidString_ReturnsExpected()
        {
            var str = "func)((,323_";
            var tokenList = new List<Token>
            {
                new Token(TokenType.Symbol, "func"),
                new Token(TokenType.RightBracket, ")"),
                new Token(TokenType.LeftBracket, "("),
                new Token(TokenType.LeftBracket, "("),
                new Token(TokenType.Comma, ","),
                new Token(TokenType.Number, "323"),
                new Token(TokenType.Underscores, "_"),
                new Token(TokenType.EOF, "")
            };

            var result = Lexer.Tokenize(str);

            Assert.IsTrue(result.SequenceEqual(tokenList));
        }

        [TestMethod]
        public void Tokenize_GivenSimpleSymbolString_ReturnsExpected()
        {
            var str = "simple";
            var tokenList = new List<Token>
            {
                new Token(TokenType.Symbol, "simple"),
                new Token(TokenType.EOF, "")
            };

            var result = Lexer.Tokenize(str);

            Assert.IsTrue(result.SequenceEqual(tokenList));
        }
        
        [TestMethod]
        public void Tokenize_GivenNotSoSimpleSymbolString_ReturnsExpected()
        {
            var str = "krut1k777";
            var tokenList = new List<Token>
            {
                new Token(TokenType.Symbol, "krut1k777"),
                new Token(TokenType.EOF, "")
            };

            var result = Lexer.Tokenize(str);

            Assert.IsTrue(result.SequenceEqual(tokenList));
        }

        [TestMethod]
        public void Tokenize_GivenElementPattern_ReturnsExpected()
        {
            var str = "iamname_iamtype";
            var tokenList = new List<Token>
            {
                new Token(TokenType.Symbol, "iamname"),
                new Token(TokenType.Underscores, "_"),
                new Token(TokenType.Symbol, "iamtype"),
                new Token(TokenType.EOF, "")
            };

            var result = Lexer.Tokenize(str);

            Assert.IsTrue(result.SequenceEqual(tokenList));
        }
    }
}
