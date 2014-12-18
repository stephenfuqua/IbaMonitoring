using safnet.iba.Business.Entities.Observations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tSupplementalObservation and is intended
    ///to contain all tSupplementalObservation Unit Tests
    ///</summary>
    [TestClass()]
    public class tSupplementalObservation
    {

        /// <summary>
        ///A test for SupplementalObservation Constructor
        ///</summary>
        [TestMethod()]
        public void t_SupplementalObservationConstructor()
        {
            SupplementalObservation target = new SupplementalObservation();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewSupplementalObservation
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewSupplementalObservation()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Guid surveyId = LookupConstants.LocationTypePoint;

            SupplementalObservation actual = SupplementalObservation.CreateNewSupplementalObservation(surveyId);
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
            SupplementalObservation target = new SupplementalObservation(); 
            Guid actual;
            actual = target.ObservationTypeId;
            Assert.AreEqual(LookupConstants.ObservationTypeSupplemental, actual);
        }
    }
}
