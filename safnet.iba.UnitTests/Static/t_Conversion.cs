using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Static;

namespace safnet.iba.UnitTests.Static
{
    [TestClass]
    public class t_Conversion
    {
        [TestMethod]
        public void t_GetDateForWeekNumber_1()
        {
            string expected = "12/27";
            string actual = Conversion.GetDateForWeekNumber(1);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void t_GetDateForWeekNumber_16()
        {
            string expected = "4/11";
            string actual = Conversion.GetDateForWeekNumber(16);
            Assert.AreEqual(expected, actual);
        }
    }
}
