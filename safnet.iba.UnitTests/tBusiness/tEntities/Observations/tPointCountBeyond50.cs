using safnet.iba.Business.Entities.Observations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tPointCountBeyond50 and is intended
    ///to contain all tPointCountBeyond50 Unit Tests
    ///</summary>
    [TestClass()]
    public class tPointCountBeyond50
    {

        /// <summary>
        ///A test for PointCountBeyond50 Constructor
        ///</summary>
        [TestMethod()]
        public void t_PointCountBeyond50Constructor()
        {
            PointCountBeyond50 target = new PointCountBeyond50();
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

            PointCountBeyond50 actual = PointCountBeyond50.CreateNewPointCountObservation(surveyId);
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
            PointCountBeyond50 target = new PointCountBeyond50(); 

            Guid actual;
            actual = target.ObservationTypeId;
            Assert.AreEqual(LookupConstants.ObservationTypePointBeyond50m, actual);
        }
    }
}
