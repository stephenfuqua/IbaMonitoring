using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.DataTypes;
using System;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tDegree and is intended
    ///to contain all tDegree Unit Tests
    ///</summary>
    [TestClass()]
    public class tDegree
    {
        /// <summary>
        ///A test for Degree Constructor
        ///</summary>
        [TestMethod()]
        public void t_DegreeConstructor()
        {
            Degree target = new Degree();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void t_ToString()
        {
            decimal value = -24.234M;
            Degree target = new Degree() { Value = value };
            string expected = "-24.234°";

            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void t_Value()
        {
            decimal expected = -24.234M;
            Degree target = new Degree() { Value = expected };

            decimal actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void t_Value_180()
        {
            decimal expected = 180.0001M;
            Degree target = new Degree() { Value = expected };

            decimal actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void t_Value_Minus180()
        {
            decimal expected = -180.0001M;
            Degree target = new Degree() { Value = expected };

            decimal actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
