using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tTemperature and is intended
    ///to contain all tTemperature Unit Tests
    ///</summary>
    [TestClass()]
    public class tTemperature
    {
        /// <summary>
        ///A test for Temperature Constructor
        ///</summary>
        [TestMethod()]
        public void t_TemperatureConstructor()
        {
            Temperature target = new Temperature();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void t_ToString()
        {
            string unit = "Celsius";
            int temp = 32;
            Temperature target = new Temperature() { Value = temp, Units = unit };
            string expected = "32&deg; Celsius";

            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Units
        ///</summary>
        [TestMethod()]
        public void t_Units()
        {
            Temperature target = new Temperature();
            string expected = "asdfad";
            string actual;
            target.Units = expected;
            actual = target.Units;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void t_Value()
        {
            Temperature target = new Temperature();
            int expected = -21; 
            int actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
