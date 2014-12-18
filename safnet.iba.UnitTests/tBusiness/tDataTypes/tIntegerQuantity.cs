using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tIntegerQuantity and is intended
    ///to contain all tIntegerQuantity Unit Tests
    ///</summary>
    [TestClass()]
    public class tIntegerQuantity
    {




        /// <summary>
        ///A test for IntegerQuantity Constructor
        ///</summary>
        [TestMethod()]
        public void t_IntegerQuantityConstructor()
        {
            IntegerQuantity target = new IntegerQuantity();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Unit
        ///</summary>
        [TestMethod()]
        public void t_Unit()
        {
            IntegerQuantity target = new IntegerQuantity();
            string expected = "meters";
            string actual;

            target.Unit = expected;
            actual = target.Unit;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Value
        ///</summary>
        [TestMethod()]
        public void t_Value()
        {
            IntegerQuantity target = new IntegerQuantity();
            int expected = 34;

            int actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
        }
    }
}
