using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tLocation and is intended
    ///to contain all tLocation Unit Tests
    ///</summary>
    [TestClass()]
    public class tLocation
    {



        internal virtual Location CreateLocation()
        {
            Location target = new safnet.iba.Business.Entities.Moles.SLocation();
            return target;
        }

        /// <summary>
        ///A test for GeoCoordinate
        ///</summary>
        [TestMethod()]
        public void t_GeoCoordinate()
        {
            Location target = CreateLocation();
            Coordinate expected = new Coordinate();

            Coordinate actual;
            target.GeoCoordinate = expected;
            actual = target.GeoCoordinate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void t_Name()
        {
            Location target = CreateLocation();
            string expected = "a name";

            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }
    }
}
