using safnet.iba.Business.Entities.Observations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tObservation and is intended
    ///to contain all tObservation Unit Tests
    ///</summary>
    [TestClass()]
    public class tObservation
    {



        internal virtual Observation CreateObservation()
        {
            Observation target = new safnet.iba.Business.Entities.Observations.Moles.SObservation();
            return target;
        }

        /// <summary>
        ///A test for Comments
        ///</summary>
        [TestMethod()]
        public void t_Comments()
        {
            Observation target = CreateObservation();
            string expected = "these are comments";

            string actual;
            target.Comments = expected;
            actual = target.Comments;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EventId
        ///</summary>
        [TestMethod()]
        public void t_EventId()
        {
            Observation target = CreateObservation();
            Guid expected = LookupConstants.LocationTypePoint;

            Guid actual;
            target.EventId = expected;
            actual = target.EventId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        public void t_Id()
        {
            Observation target = CreateObservation();
            long expected = 12390907234;

            long actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }

                /// <summary>
        ///A test for SpeciesCode
        ///</summary>
        [TestMethod()]
        public void t_SpeciesCode()
        {
            Observation target = CreateObservation();
            string expected = DbTestHelper.SPECIES_2_CODE;

            string actual;
            target.SpeciesCode = expected;
            actual = target.SpeciesCode;
            Assert.AreEqual(expected, actual);
        }
    }
}
