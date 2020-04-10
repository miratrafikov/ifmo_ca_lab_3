using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Core;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;

namespace ShiftCo.ifmo_ca_lab_3.EvaluationTest
{
    [TestClass]
    public class PlotTests
    {
        [TestMethod]
        public void Test1()
        {
            var setF = new Expression(nameof(delayed),
                new Expression("f",
                    new IntegerPattern("x")
                ),
                new Expression(nameof(sum),
                    new Integer(1),
                    new IntegerPattern("x")
                )
            );
            var setResult = Evaluator.Run(setF);
            var expr = new Expression(nameof(plot),
                new Symbol("f"),
                new Integer(1),
                new Integer(3),
                new Integer(1)
            );
            var evaluated = Evaluator.Run(expr);
        }
    }
}
