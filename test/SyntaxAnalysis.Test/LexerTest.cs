using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysisTest
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void Tokenize_OnePattern()
        {
            var tokens = Lexer.Tokenize("abc_");
            Assert.AreEqual("abc", tokens[0].Content);
            Assert.AreEqual("_", tokens[1].Content);
        }

        [TestMethod]
        public void Tokenize_OnePatternInExpression()
        {
            var tokens = Lexer.Tokenize("expr(a_b,69)");
            Assert.AreEqual("expr", tokens[0].Content);
            Assert.AreEqual("(", tokens[1].Content);
            Assert.AreEqual("a", tokens[2].Content);
            Assert.AreEqual("_", tokens[3].Content);
            Assert.AreEqual("b", tokens[4].Content);
            Assert.AreEqual(",", tokens[5].Content);
            Assert.AreEqual("69", tokens[6].Content);
            Assert.AreEqual(")", tokens[7].Content);
        }
    }
}
