using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tSiteCondition and is intended
    ///to contain all tSiteCondition Unit Tests
    ///</summary>
    [TestClass()]
    public class tSiteCondition
    {
        
        /// <summary>
        ///A test for SiteCondition Constructor
        ///</summary>
        [TestMethod()]
        public void t_SiteConditionConstructor()
        {
            SiteCondition target = new SiteCondition();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewConditions
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewConditions()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Guid siteVisitId = LookupConstants.LocationTypePoint;

            SiteCondition actual= SiteCondition.CreateNewConditions(siteVisitId);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
            Assert.AreEqual(siteVisitId, actual.SiteVisitId, "SiteVisitId not assigned");
        }

        /// <summary>
        ///A test for SiteVisitId
        ///</summary>
        [TestMethod()]
        public void t_SiteVisitId()
        {
            SiteCondition target = new SiteCondition();
            Guid expected = LookupConstants.LocationTypeSite;

            Guid actual;
            target.SiteVisitId = expected;
            actual = target.SiteVisitId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Sky
        ///</summary>
        [TestMethod()]
        public void t_Sky()
        {
            SiteCondition target = new SiteCondition();
            byte expected = 3;

            byte actual;
            target.Sky = expected;
            actual = target.Sky;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SkyText
        ///</summary>
        [TestMethod()]
        public void t_SkyText()
        {
            SiteCondition target = new SiteCondition();
            target.Sky = 4;
            string expected = "Drizzle";

            string actual;
            actual = target.SkyText;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Temperature
        ///</summary>
        [TestMethod()]
        public void t_Temperature()
        {
            SiteCondition target = new SiteCondition();
            Temperature expected = new Temperature() { Units = "F", Value = 234 };

            Temperature actual;
            target.Temperature = expected;
            actual = target.Temperature;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Wind
        ///</summary>
        [TestMethod()]
        public void t_Wind()
        {
            SiteCondition target = new SiteCondition();
            byte expected = 2;
            byte actual;
            target.Wind = expected;
            actual = target.Wind;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WindText
        ///</summary>
        [TestMethod()]
        public void t_WindText()
        {
            SiteCondition target = new SiteCondition();
            target.Wind = 2;
            string expected = "Wind felt on face";

            string actual = target.WindText;
            Assert.AreEqual(expected, actual);
        }
    }
}
