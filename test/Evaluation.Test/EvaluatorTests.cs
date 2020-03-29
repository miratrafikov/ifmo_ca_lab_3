using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
    public class EvaluatorTests
    {
        private static readonly ElementComparer s_comparer = new ElementComparer();

        public void Test1()
        {
            var add = new Expression(nameof(sum), new List<IElement>()
            {
                new Integer(2),
                new Integer(3),
                new Integer(3)
            });
            var lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new IntegerPattern("x"),
                new NullableSequencePattern("b"),
                new IntegerPattern("y"),
                new NullableSequencePattern("c")
            });

            var rhs = new Expression(nameof(sum));
            _ = Evaluator.Run(new Expression(nameof(set), new List<IElement>()
            {
                lhs, rhs
            }));
            _ = Evaluator.Run(add);

        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression(nameof(sum),
                new Symbol("x"),
                new Symbol("x")
            );
            //var alteredExpr = Evaluator.Run(expr);
            var alteredExpr = new Expression(nameof(mul),
                new Integer(2),
                new Symbol("x")
            );
            Assert.AreEqual(s_comparer.Compare(alteredExpr, Evaluator.Run(expr)), 0);
        }

        [TestMethod]
        public void Test3()
        {
            var expr = new Expression(nameof(sum),
                new Symbol("x"),
                new Expression(nameof(mul),
                    new Integer(3),
                    new Symbol("x")
                ),
                new Symbol("x")
            );
            var alteredExpr = new Expression(nameof(mul),
                new Integer(5),
                new Symbol("x")
            );
            var evaluated = Evaluator.Run(expr);
            Assert.AreEqual(s_comparer.Compare(alteredExpr, evaluated), 0);
        }
    }
}
