using System.Collections.Generic;

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
    public class ContextTests
    {
        public void Test1()
        {
            var lhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("b"),
                new ElementPattern("x"),
                new NullableSequencePattern("c")
            });
            var times = new Expression(nameof(mul), new List<IElement>()
            {
                new Integer(2),
                new ElementPattern("x")
            });
            var rhs = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new NullableSequencePattern("b"),
                times,
                new NullableSequencePattern("c"),
            });
            Context.AddRule(lhs, rhs);

            var expr = new Expression(nameof(sum), new List<IElement>()
            {
                new Symbol("x"),
                new Symbol("x")
            });
            var alteredExpr = Context.GetElement(expr);
            Assert.AreEqual(alteredExpr, expr);
        }

        [TestMethod]
        public void Test3()
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
            var rhs = new Expression(nameof(mul), new List<IElement>
            {
                new Integer(default)
            });

            Context.AddRule(lhs, rhs);
            var e = Context.GetElement(add);
            Assert.AreEqual(add, new Integer(4));
        }
    }
}
