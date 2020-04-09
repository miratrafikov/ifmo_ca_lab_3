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
                new Integer(2),
                new Integer(2));
            var altered = new Integer(4);
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test2()
        {
            var s = new Expression(nameof(sum),
                    new Integer(2),
                    new Integer(2)
            );
            var expr = new Expression(nameof(delayed),
                new Symbol("x"),
                s
            );
            var del = Evaluator.Run(expr);
            Assert.AreEqual(0, Comparer.Compare(s, del));
            var evaluated = Evaluator.Run(new Symbol("x"));
            Assert.AreEqual(0, Comparer.Compare(evaluated, new Integer(4)));
        }

        [TestMethod]
        public void Test3()
        {
            IElement lhs = new Expression("Fac", new Integer(1));
            IElement rhs = new Integer(1);
            Context.AddRule(lhs, rhs);

            lhs = new Expression("Fac", new IntegerPattern("x"));
            rhs = new Expression("mul",
                    new IntegerPattern("x"),
                    new Expression("Fac",
                        new Expression("sum",
                            new Integer(-1),
                            new IntegerPattern("x")
                        )
                    )
            );
            Context.AddRule(lhs, rhs);
            var fac5 = new Expression("Fac", new Integer(5));
            var altered = new Integer(120);
            var evaluated = Evaluator.Run(fac5);
            Assert.AreEqual(0, Comparer.Compare(altered, evaluated));
        }
    }
}
