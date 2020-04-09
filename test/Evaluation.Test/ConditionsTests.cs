using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class ConditionsTests
    {
        private static ElementComparer Comparer = new ElementComparer();
        [TestMethod]
        public void Test1()
        {
            var expr = new Expression("if",
                new Symbol("true"),
                new Integer(4),
                new Symbol("x"));
            var eval1 = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(new Integer(4), eval1));
            expr = new Expression("if",
                new Symbol("false"),
                new Integer(4),
                new Symbol("x"));
            var eval2 = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(new Symbol("x"), eval2));
        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression("less",
                new Integer(2),
                new Integer(3));
            var altered = new Symbol("true");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test3()
        {
            var expr = new Expression("lesse",
                new Integer(2),
                new Integer(2));
            var altered = new Symbol("true");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test4()
        {
            var expr = new Expression("equals",
                new Integer(2),
                new Integer(3));
            var altered = new Symbol("false");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test5()
        {
            var expr = new Expression("and",
                new Symbol("false"),
                new Symbol("true"),
                new Symbol("true"));
            var altered = new Symbol("false");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test6()
        {
            var expr = new Expression("and",
                new Symbol("true"),
                new Symbol("true"),
                new Symbol("true"));
            var altered = new Symbol("true");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test7()
        {
            var expr = new Expression("or",
                new Symbol("false"),
                new Symbol("false"),
                new Symbol("false"));
            var altered = new Symbol("false");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test8()
        {
            var expr = new Expression("or",
                new Symbol("false"),
                new Symbol("true"),
                new Symbol("false"));
            var altered = new Symbol("true");
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }
    }
}
