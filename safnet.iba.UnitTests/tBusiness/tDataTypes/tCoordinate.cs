using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tCoordinate and is intended
    ///to contain all tCoordinate Unit Tests
    ///</summary>
    [TestClass()]
    public class tCoordinate
    {
        

        /// <summary>
        ///A test for Coordinate Constructor
        ///</summary>
        [TestMethod()]
        public void t_CoordinateConstructor()
        {
            Coordinate target = new Coordinate();

            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Latitude
        ///</summary>
        [TestMethod()]
        public void t_Latitude()
        {
            Coordinate target = new Coordinate(); 
            Degree expected = new Degree() { Value = 23.2340M };

            Degree actual;
            target.Latitude = expected;
            actual = target.Latitude;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Longitude
        ///</summary>
        [TestMethod()]
        public void t_Longitude()
        {
            Coordinate target = new Coordinate(); 
            Degree expected = new Degree() { Value = 23.2340M };
            Degree actual;

            target.Longitude = expected;
            actual = target.Longitude;
            Assert.AreEqual(expected, actual);
        }
    }
}
