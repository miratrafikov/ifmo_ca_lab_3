using System.Diagnostics;
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
            foreach (var token in tokens)
            {
                Debug.WriteLine($"{token.Type} : {token.Content}");
            }
        }
    }
}
