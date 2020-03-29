using ShiftCo.ifmo_ca_lab_3.Chief;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShiftCo.ifmo_ca_lab_3.ChiefTest
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        [ExpectedException(typeof(Commons.Exceptions.StrangeCharacterException))]
        public void InputStringSet_GivenBadValue_ThrowsException()
        {
            var str = "!@#$%";

            Main.InputString = str;
        }

        [TestMethod]
        public void InputStringSet_CanNormalize_StoresNormalized()
        {
            var str = "aB  CD e";

            Main.InputString = str;

            Assert.IsTrue(Main.InputString == "abcde");
        }
    }
}
