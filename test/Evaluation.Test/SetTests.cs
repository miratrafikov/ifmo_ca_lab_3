using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class SetTests
    {
        private static ElementComparer Comparer = new ElementComparer();
        [TestMethod]
        public void Test1()
        {
            var expr = new Expression(nameof(set),
                new Symbol("summa"),
                new Symbol("sum")
            );
            var s = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(s, new Symbol("sum")));
            expr = new Expression("summa",
                new Number(2),
                new Number(2));
            var altered = new Number(4);
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test2()
        {
            var s = new Expression(nameof(sum),
                    new Number(2),
                    new Number(2)
            );
            var expr = new Expression(nameof(delayed),
                new Symbol("x"),
                s
            );
            var del = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(s, del));
            var evaluated = Evaluator.Run(new Symbol("x"));
            Assert.AreEqual(0, Comparer.Compare(evaluated, new Number(4)));
        }

        [TestMethod]
        public void Test3()
        {
            IElement lhs = new Expression("Fac", new Number(1));
            IElement rhs = new Number(1);
            Context.AddRule(lhs, rhs);

            lhs = new Expression("Fac", new NumberPattern("x"));
            rhs = new Expression("mul",
                    new NumberPattern("x"),
                    new Expression("Fac",
                        new Expression("sum",
                            new Number(-1),
                            new NumberPattern("x")
                        )
                    )
            );
            Context.AddRule(lhs, rhs);
            var fac5 = new Expression("Fac", new Number(5));
            var altered = new Number(120);
            var evaluated = Evaluator.Run(fac5);
            Assert.AreEqual(0, Comparer.Compare(altered, evaluated));
        }
    }
}
