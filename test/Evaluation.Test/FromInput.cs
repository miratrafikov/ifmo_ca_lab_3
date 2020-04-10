using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    public class FromInput
    {
        [TestMethod]
        public void Custom()
        {
            string input = "set(x,5)";
            var tokens = Lexer.Tokenize(input);
            var tree = Parser.Parse(tokens);
            Debug.WriteLine(tree.GetHead());

            Expression result = (Expression)Evaluation.Core.Evaluator.Run(tree);
            Debug.WriteLine(result.Head);
        }
    }
}
