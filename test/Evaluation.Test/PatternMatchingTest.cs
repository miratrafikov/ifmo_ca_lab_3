using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Util;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Mirat.Core;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class PatternMatchingTest
    {
        [TestMethod]
        public void Test1()
        {
            var expr = new Expression(Head.Sum, new List<IElement>()
            { 
                 new Integer(2), 
                 new Integer(3)
            });
            IElement rule = new Expression(Head.Pattern, new List<IElement>()
            {
                new IntegerPattern("a"),
                new NullableSequencePattern("b")
            });
            var matches = PatternMatcher.Matches(ref rule, expr);
            Assert.AreEqual(true, matches);
        }

        [TestMethod]
        public void Test2()
        {
            var expr = new Expression(Head.Sum, new List<IElement>()
            {
                new Symbol("x"),
                new Expression(Head.Mul, new List<IElement>()
                {
                    new Integer(3),
                    new Symbol("x")
                })
            });
            var times = new Expression(Head.Mul, new List<IElement>()
            {
                new IntegerPattern("d"),
                new ElementPattern("x")
            });
            IElement rule = new Expression(Head.Sum, new List<IElement>()
            {
                new NullableSequencePattern("a"),
                new ElementPattern("x"),
                new NullableSequencePattern("c"),
                times,
                new NullableSequencePattern("e")
            });
            var matches = PatternMatcher.Matches(ref rule, expr);
            Assert.AreEqual(true, matches);
        }
    }
}
