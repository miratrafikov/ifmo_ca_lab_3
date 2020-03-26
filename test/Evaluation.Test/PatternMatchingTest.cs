using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces.Markers;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class PatternMatchingTest
    {
        [TestMethod]
        public void Matches_Sum_True()
        {
            var testExpression = new Expression("Sum", new List<IElement>()
            {
                 new Integer(215),
                 new Integer(39)
            });
            IElement testRule = new Expression("Pattern", new List<IElement>()
            {
                new IntegerPattern("a"),
                new NullableSequencePattern("b")
            });
            var matches = PatternMatcher.TryMatch(testExpression, testRule);
            Assert.AreEqual(true, matches);
        }

        [TestMethod]
        public void Matches_SumComposite_True()
        {
            var testExpression = new Expression("Sum", new List<IElement>()
            {
                new Symbol("x"),
                new Expression("Mul", new List<IElement>()
                {
                    new Integer(3),
                    new Symbol("x")
                })
            });
            var timesRule = new Expression("Mul", new List<IElement>()
            {
                new IntegerPattern("d"),
                new ElementPattern("x")
            });
            IElement testRule = new Expression("Sum", new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("c"),
                timesRule,
                new NullableSequencePattern("e")
            });
            var matches = PatternMatcher.TryMatch(testExpression, testRule);
            Assert.AreEqual(true, matches);
        }
    }
}
