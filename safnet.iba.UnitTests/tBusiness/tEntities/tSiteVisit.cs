using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.Entities.Observations;
using System.Collections.Generic;

namespace safnet.iba.UnitTest
{


    /// <summary>
    ///This is a test class for tSiteVisit and is intended
    ///to contain all tSiteVisit Unit Tests
    ///</summary>
    [TestClass()]
    public class tSiteVisit
    {

        /// <summary>
        ///A test for SiteVisit Constructor
        ///</summary>
        [TestMethod()]
        public void t_SiteVisitConstructor()
        {
            SiteVisit target = new SiteVisit();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewSiteVisit
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewSiteVisit()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Guid siteId = LookupConstants.LocationTypePoint;

            SiteVisit actual= SiteVisit.CreateNewSiteVisit(siteId);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
            Assert.AreEqual(siteId, actual.LocationId, "LocationId not assigned");
        }

        /// <summary>
        ///A test for Comments
        ///</summary>
        [TestMethod()]
        public void t_Comments()
        {
            SiteVisit target = new SiteVisit();
            string expected = "these are some comments";
            string actual;
            target.Comments = expected;
            actual = target.Comments;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for EndConditions
        ///</summary>
        [TestMethod()]
        public void t_EndConditions()
        {
            SiteVisit target = new SiteVisit();
            SiteCondition expected = new SiteCondition() { Sky = 0 };

            SiteCondition actual;
            target.EndConditions = expected;
            actual = target.EndConditions;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FlattenedDataEntryList
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_FlattenedDataEntryList()
        {
            SiteVisit target = new SiteVisit();

            List<FiftyMeterPointObservation> observations = new List<FiftyMeterPointObservation>() 
            { 
                new PointCountBeyond50() { SpeciesCode = DbTestHelper.SPECIES_1_CODE },
                new PointCountWithin50() { SpeciesCode = DbTestHelper.SPECIES_2_CODE }
            };
            safnet.iba.Business.Entities.Moles.MFiftyMeterPointSurvey.AllInstances.ObservationsGet = (FiftyMeterPointSurvey survey) => 
                {
                    return observations;
                };
            target.PointSurveys.Add(new FiftyMeterPointSurvey());


            List<FiftyMeterDataEntry> actual;
            actual = target.FlattenedDataEntryList;

            Assert.AreEqual(1, actual.Count, "wrong count");
        }

        /// <summary>
        ///A test for ObserverId
        ///</summary>
        [TestMethod()]
        public void t_ObserverId()
        {
            SiteVisit target = new SiteVisit();
            Guid expected = LookupConstants.ObservationTypeParent;

            Guid actual;
            target.ObserverId = expected;
            actual = target.ObserverId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PointSurveys
        ///</summary>
        [TestMethod()]
        [DeploymentItem("safnet.iba.dll")]
        public void t_PointSurveys()
        {
            SiteVisit_Accessor target = new SiteVisit_Accessor();
            List<FiftyMeterPointSurvey> expected = new List<FiftyMeterPointSurvey>() { new FiftyMeterPointSurvey() { NoiseCode = 2 } };

            List<FiftyMeterPointSurvey> actual;
            target.PointSurveys = expected;
            actual = target.PointSurveys;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RecorderId
        ///</summary>
        [TestMethod()]
        public void t_RecorderId()
        {
            SiteVisit target = new SiteVisit();
            Guid expected = LookupConstants.LocationTypeSite;

            Guid actual;
            target.RecorderId = expected;
            actual = target.RecorderId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for StartConditions
        ///</summary>
        [TestMethod()]
        public void t_StartConditions()
        {
            SiteVisit target = new SiteVisit();
            SiteCondition expected = new SiteCondition() { Sky = 1 };

            SiteCondition actual;
            target.StartConditions = expected;
            actual = target.StartConditions;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SupplementalObservations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("safnet.iba.dll")]
        public void t_SupplementalObservations()
        {
            SiteVisit_Accessor target = new SiteVisit_Accessor();
            List<SupplementalObservation> expected = new List<SupplementalObservation>() { new SupplementalObservation() { Comments = "asdf" } };

            List<SupplementalObservation> actual;
            target.SupplementalObservations = expected;
            actual = target.SupplementalObservations;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WeekNumber on January 1, 2010
        ///</summary>
        [TestMethod()]
        public void t_WeekNumber_Jan1()
        {
            SiteVisit target = new SiteVisit();
            target.StartTimeStamp = new DateTime(2010, 01, 01);

            int expected = 1;
            int actual = target.WeekNumber;
            Assert.AreEqual(expected, actual, "Week number not accurate");
        }

        /// <summary>
        ///A test for WeekNumber on January 7
        ///</summary>
        [TestMethod()]
        public void t_WeekNumber_Jan7()
        {
            SiteVisit target = new SiteVisit();
            target.StartTimeStamp = new DateTime(2010, 01, 07);

            int expected = 2;
            int actual = target.WeekNumber;
            Assert.AreEqual(expected, actual, "Week number not accurate");
        }

        /// <summary>
        ///A test for WeekNumber on January 9
        ///</summary>
        [TestMethod()]
        public void t_WeekNumber_Jan9()
        {
            SiteVisit target = new SiteVisit();
            target.StartTimeStamp = new DateTime(2010, 01, 09);

            int expected = 2;
            int actual = target.WeekNumber;
            Assert.AreEqual(expected, actual, "Week number not accurate");
        }


        /// <summary>
        ///A test for WeekNumber on December 31
        ///</summary>
        [TestMethod()]
        public void t_WeekNumber_Dec31()
        {
            SiteVisit target = new SiteVisit();
            target.StartTimeStamp = new DateTime(2010, 12, 31);

            int expected = 53;
            int actual = target.WeekNumber;
            Assert.AreEqual(expected, actual, "Week number not accurate");
        }
    }
}
