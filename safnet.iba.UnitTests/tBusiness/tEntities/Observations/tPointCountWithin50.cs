using safnet.iba.Business.Entities.Observations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tPointCountWithin50 and is intended
    ///to contain all tPointCountWithin50 Unit Tests
    ///</summary>
    [TestClass()]
    public class tPointCountWithin50
    {

        /// <summary>
        ///A test for PointCountWithin50 Constructor
        ///</summary>
        [TestMethod()]
        public void t_PointCountWithin50Constructor()
        {
            PointCountWithin50 target = new PointCountWithin50();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewPointCountObservation
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewPointCountObservation()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Guid surveyId = LookupConstants.LocationTypePoint;

            PointCountWithin50 actual = PointCountWithin50.CreateNewPointCountObservation(surveyId);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(0, actual.Id, "ID not assigned");
            Assert.AreEqual(surveyId, actual.EventId, "EventId not assigned");
        }

        /// <summary>
        ///A test for ObservationTypeId
        ///</summary>
        [TestMethod()]
        public void t_ObservationTypeId()
        {
            PointCountWithin50 target = new PointCountWithin50();
            Guid actual;
            actual = target.ObservationTypeId;
            Assert.AreEqual(LookupConstants.ObservationTypePointLess50m, actual);
        }
    }
}
