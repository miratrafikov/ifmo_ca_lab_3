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
        [TestMethod]
        public void Parse_OnePattern()
        {
            var tokens = Lexer.Tokenize("a_integer");
            var tree = Parser.Parse(tokens);
            Assert.IsTrue(tree is IPattern);
            tokens = Lexer.Tokenize("as12___");
            tree = Parser.Parse(tokens);
            Assert.IsTrue(tree is NullableSequencePattern);
        }

        [TestMethod]
        public void Parse_ExpressionWithPattern()
        {
            var tokens = Lexer.Tokenize("set(fib(n_integer),mul(n,fib(sum(n,mul(1)))))");
            var tree = Parser.Parse(tokens);
            Assert.IsTrue(tree is Expression);
            Expression firstChild = (Expression)((Expression)tree)._operands[0];
            var pattern = firstChild._operands[0];
            Assert.IsTrue(pattern is IPattern);
        }
    }
}
