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
    public class EvaluatorTest
    {
        [TestMethod]
        public void Run_GivenPattern_ReturnsSame()
        {
            var patternElement = new Pattern(new Symbol("x"), typeof(IElement), true);

            var evaluationResult = Evaluator.Run(patternElement);

            Assert.IsTrue(patternElement.Equals(evaluationResult));
        }
    }
}
