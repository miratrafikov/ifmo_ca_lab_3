using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class EvaluatorTests
    {
        private static readonly ElementComparer s_comparer = new ElementComparer();

        [TestMethod]
        public void Test1()
        {
            var add = new Expression(nameof(sum), new List<IElement>()
            {
                new Number(2),
                new Number(3),
                new Number(3)
            });
            var evaluated = Evaluator.Run(add);
            Assert.AreEqual(s_comparer.Compare(new Number(8), evaluated), 0);
        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression(nameof(sum),
                new Symbol("x"),
                new Expression(nameof(mul),
                    new Number(3),
                    new Symbol("x")
                ),
                new Symbol("x")
            );
            var alteredExpr = new Expression(nameof(mul),
                new Number(5),
                new Symbol("x")
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(s_comparer.Compare(alteredExpr, evaluated), 0);
        }

        [TestMethod]
        public void Test3()
        {
            var expr = new Expression(nameof(mul),
                new Expression(nameof(sum),
                    new Number(3),
                    new Symbol("x")
                ),
                new Number(2)
            );
            var altered = new Expression(nameof(sum),
                new Number(6),
                new Expression(nameof(mul),
                    new Number(2),
                    new Symbol("x")
                )
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, s_comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test4()
        {
            var expr = new Expression(nameof(mul),
                new Expression(nameof(sum),
                    new Number(2),
                    new Symbol("x")
                ),
                new Expression(nameof(sum),
                    new Number(2),
                    new Symbol("x")
                )
            );
            var altered = new Expression(nameof(sum),
                new Number(4),
                new Expression(nameof(mul),
                    new Number(4),
                    new Symbol("x")
                ),
                new Expression(nameof(mul),
                    new Symbol("x"),
                    new Symbol("x")
                )
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(s_comparer.Compare(evaluated, altered), 0);
        }

        [TestMethod]
        public void Test5()
        {
            var expr = new Expression(nameof(pow),
                new Expression(nameof(sum),
                    new Number(2),
                    new Symbol("x")
                ),
                new Number(2)
            );
            var altered = new Expression(nameof(sum),
                new Number(4),
                new Expression(nameof(mul),
                    new Number(4),
                    new Symbol("x")
                ),
                new Expression(nameof(mul),
                    new Symbol("x"),
                    new Symbol("x")
                )
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, s_comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test6()
        {
            var expr = new Expression(nameof(pow),
                new Expression(nameof(sum),
                    new Number(-2),
                    new Symbol("x")
                ),
                new Number(2)
            );
            var altered = new Expression(nameof(sum),
                new Number(4),
                new Expression(nameof(mul),
                    new Number(-4),
                    new Symbol("x")
                ),
                new Expression(nameof(mul),
                    new Symbol("x"),
                    new Symbol("x")
                )
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, s_comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test7()
        {
            var expr = new Expression(nameof(pow),
                new Expression(nameof(sum),
                    new Expression(nameof(mul),
                        new Number(-1),
                        new Symbol("y")
                    ),
                    new Symbol("x")
                ),
                new Number(2)
            );
            var altered = new Expression(nameof(sum),
                new Expression(nameof(mul),
                    new Symbol("x"),
                    new Symbol("x")
                ),
                new Expression(nameof(mul),
                    new Symbol("y"),
                    new Symbol("y")
                ),
                new Expression(nameof(mul),
                    new Number(-2),
                    new Symbol("x"),
                    new Symbol("y")
                )
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(0, s_comparer.Compare(evaluated, altered));
        }

        [TestMethod]
        public void Test8()
        {
            var expr = new Expression(nameof(mul),
                new Expression(nameof(sum),
                    new Symbol("x"),
                    new Expression(nameof(mul),
                        new Number(-1),
                        new Symbol("y")
                    )
                ),
                new Expression(nameof(sum),
                    new Symbol("x"),
                    new Symbol("y")
                )
            );
            var altered = new Expression("sum",
                new Expression("mul",
                    new Symbol("x"),
                    new Symbol("x")
                ),
                new Expression("mul",
                    new Number(-1),
                    new Symbol("y"),
                    new Symbol("y")
                )
            );
            var eavluated = Evaluator.Run(expr);
            Assert.AreEqual(0, s_comparer.Compare(eavluated, altered));
        }

        [TestMethod]
        public void Test9()
        {
            var expr = new Expression(nameof(pow),
                new Expression(nameof(sum),
                    new Symbol("y"),
                    new Symbol("x")
                ),
                new Number(10)
            );
            var evaluated = Evaluator.Run(expr);
        }
    }
}
