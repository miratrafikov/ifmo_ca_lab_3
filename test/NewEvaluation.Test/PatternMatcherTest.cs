using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Core;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types;
using ShiftCo.ifmo_ca_lab_3.NewEvaluation.Types.Atoms;

namespace ShiftCo.ifmo_ca_lab_3.NewEvaluation.Test
{
    [TestClass]
    public class PatternMatcherTest
    {
        [TestMethod]
        public void TryMatch_GivenSymbolRuleAndSymbolElement_Matches()
        {
            var rule = new Symbol("symbolX");
            var element = new Symbol("symbolX");

            var result = PatternMatcher.TryMatch(element, rule);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TryMatch_GivenExpressionWithPatternRuleAndExpressionElement_Matches()
        {
            var rule = new Expression(new Symbol("Fib"), new List<IElement>()
            {
                new Symbol("symbolX"),
                new Pattern(new Symbol("storeOneInteger"), typeof(Integer), false)
            });
            var element = new Expression(new Symbol("Fib"), new List<IElement>()
            {
                new Symbol("symbolX"),
                new Integer(127)
            });

            var result = PatternMatcher.TryMatch(element, rule);

            Assert.IsTrue(result.Success);
        }
    }
}
