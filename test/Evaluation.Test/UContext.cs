using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShiftCo.ifmo_ca_lab_3.Evaluation;

namespace ShiftCo.ifmo_ca_lab_3.Evaluation.Test
{
    [TestClass]
    public class UContext
    {
        [TestMethod]
        public void Random()
        {
            var obj_1 = new Value(1);
            var obj_2 = new Value(2);
            Context.AddEntry(obj_1, obj_2);
            Debug.WriteLine($"Key ({Context.GetSubstitute(new Value(1)).Key}).");
        }

        [TestMethod]
        public void AddEntry_NewPair_KeyAsNumber_ValueAsNumber()
        {
            var obj_1 = new Value(1);
            var obj_2 = new Value(2);
            Context.AddEntry(obj_1, obj_2);
        }

        [TestMethod]
        public void AddEntry_Overwrite_KeyAsNumber_ValueAsNumber()
        {

        }
    }
}
