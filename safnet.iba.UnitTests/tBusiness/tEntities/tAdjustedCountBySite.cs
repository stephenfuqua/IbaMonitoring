using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tAdjustedCountBySite and is intended
    ///to contain all tAdjustedCountBySite Unit Tests
    ///</summary>
    [TestClass()]
    public class tAdjustedCountBySite
    {


        /// <summary>
        ///A test for AdjustedCount
        ///</summary>
        [TestMethod()]
        public void t_AdjustedCount()
        {
            AdjustedCountBySite target = new AdjustedCountBySite();
            Decimal expected = 22.353M;

            Decimal actual;
            target.AdjustedCount = expected;
            actual = target.AdjustedCount;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommonName
        ///</summary>
        [TestMethod()]
        public void t_CommonName()
        {
            AdjustedCountBySite target = new AdjustedCountBySite();
            string expected = "Common Name";

            string actual;
            target.CommonName = expected;
            actual = target.CommonName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SiteName
        ///</summary>
        [TestMethod()]
        public void t_SiteName()
        {
            AdjustedCountBySite target = new AdjustedCountBySite();
            string expected = "Site nasaas";

            string actual;
            target.SiteName = expected;
            actual = target.SiteName;
            Assert.AreEqual(expected, actual);
        }
    }
}
