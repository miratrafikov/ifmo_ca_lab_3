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
            var expr = new Expression(new Symbol(nameof(sum)), new List<IElement>()
            {
                 new Number(2),
                 new Number(3)
            });
            IElement rule = new Expression(new Symbol(nameof(sum)), new List<IElement>()
            {
                new NumberPattern("a"),
                new NullableSequencePattern("b")
            });
            var matches = PatternMatcher.Matches(rule, expr);
            Assert.AreEqual(true, matches.Success);
        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression(new Symbol(nameof(sum)), new List<IElement>()
            {
                new Symbol("x"),
                new Expression(new Symbol(nameof(mul)), new List<IElement>()
                {
                    new Number(3),
                    new Symbol("x")
                })
            });
            IElement rule = new Expression(new Symbol(nameof(sum)), new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("c"),
                new Expression(new Symbol(nameof(mul)), new List<IElement>()
                {
                    new NumberPattern("d"),
                    new ElementPattern("x")
                }),
                new NullableSequencePattern("e")
            });
            var matches = PatternMatcher.Matches(rule, expr);
            Assert.AreEqual(true, matches.Success);
        }

        [TestMethod]
        public void Test3()
        {
            var pattern = new Expression(new SymbolPattern("f"), new NumberPattern("x"));
            var expr = new Expression("fact", new Number(5));
            var matches = PatternMatcher.Matches(pattern, expr);
            Assert.AreEqual(true, matches.Success);
        }
    }
}
