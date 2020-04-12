using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class PlotTests
    {
        private static ElementComparer Comparer = new ElementComparer();

        [TestMethod]
        public void Test1()
        {
            var setF = new Expression(nameof(delayed),
                new Expression("f",
                    new NumberPattern("x")
                ),
                new Expression(nameof(sum),
                    new Number(1),
                    new NumberPattern("x")
                )
            );
            var setResult = Evaluator.Run(setF);
            var expr = new Expression(nameof(plot),
                new Symbol("f"),
                new Number(1),
                new Number(3),
                new Number(1)
            );
            var evaluated = Evaluator.Run(expr);
        }

        [TestMethod]
        public void Test2()
        {
            var sin = new Expression("sin", new Number(0));
            var evaluated = Evaluator.Run(sin);
            Assert.AreEqual(0, Comparer.Compare(new Number(0), evaluated));
        }

        [TestMethod]
        public void Test3()
        {
            var sin = new Expression("sin", new Number((decimal)Math.PI / 2));
            var evaluated = Evaluator.Run(sin);
            Assert.IsTrue( ((Number)evaluated).Value - 1 < 0.0001m);
        }

        [TestMethod]
        public void Test4()
        {
            var sin = new Expression("sin", new Number(2));
            var evaluated = Evaluator.Run(sin);
            Assert.IsTrue(((Number)evaluated).Value - 0.9092m < 0.0001m);
        }
    }
}
