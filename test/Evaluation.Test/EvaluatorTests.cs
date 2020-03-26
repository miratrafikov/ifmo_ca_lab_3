using System;
using System.Collections.Generic;
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
        [TestMethod]
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
            var sets = Evaluator.Run(new Expression(nameof(set), new List<IElement>() 
            { 
                lhs, rhs
            }));
            var res = Evaluator.Run(add);

        }
    }
}
