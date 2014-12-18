using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tSamplingPoint and is intended
    ///to contain all tSamplingPoint Unit Tests
    ///</summary>
    [TestClass()]
    public class tSamplingPoint
    {



        /// <summary>
        ///A test for SamplingPoint Constructor
        ///</summary>
        [TestMethod()]
        public void t_SamplingPointConstructor()
        {
            SamplingPoint target = new SamplingPoint();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewSamplingPoint
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewSamplingPoint()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            string name = "site name";

            SamplingPoint actual = SamplingPoint.CreateNewSamplingPoint(name);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
            Assert.AreEqual(name, actual.Name, "Name not assigned");
        }

        /// <summary>
        ///A test for SiteId
        ///</summary>
        [TestMethod()]
        public void t_SiteId()
        {
            SamplingPoint target = new SamplingPoint();
            Guid expected = LookupConstants.LocationTypePoint;

            Guid actual;
            target.SiteId = expected;
            actual = target.SiteId;
            Assert.AreEqual(expected, actual);
        }
    }
}
