using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;

using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class PatternMatchingTest
    {
        [TestMethod]
        public void Test1()
        {
            var expr = new Expression(nameof(sum), new List<IElement>()
            {
                 new Integer(2),
                 new Integer(3)
            });
            IElement rule = new Expression(nameof(pattern), new List<IElement>()
            {
                new IntegerPattern("a"),
                new NullableSequencePattern("b")
            });
            var matches = PatternMatcher.Matches(rule, expr);
            Assert.AreEqual(true, matches);
        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression(nameof(sum), new List<IElement>()
            {
                new Symbol("x"),
                new Expression(nameof(mul), new List<IElement>()
                {
                    new Integer(3),
                    new Symbol("x")
                })
            });
            var times = new Expression(nameof(sum), new List<IElement>()
            {
                new IntegerPattern("d"),
                new ElementPattern("x")
            });
            IElement rule = new Expression(nameof(sum), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("c"),
                times,
                new NullableSequencePattern("e")
            });
            var matches = PatternMatcher.Matches(rule, expr);
            Assert.AreEqual(true, matches);
        }
    }
}
