using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using iba;
using safnet.iba.

namespace iba.UnitTest
{
    /// <summary>
    /// Validations for the <see cref="FiftyMeterDataEntryMapper"/> class.
    /// </summary>
    [TestClass]
    public class tFiftyMeterDataEntryMapper
    {

        #region Public Methods
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.LoadSpecies();
        }



        /// <summary>
        /// Validates that the Insert method properly loads <see cref="Observation"/> records
        /// </summary>
        [TestMethod]
        public void t_Insert()
        {
            FiftyMeterDataEntry entry = new FiftyMeterDataEntry()
            {
                Comments = "comments",
                CountBeyond50 = 1,
                CountWithin50 = 2,
                PointSurveyId = DbTestHelper.TestGuid4,
                SpeciesCode = DbTestHelper.SPECIES_2_CODE
            };

            // Todo: fix this
            FiftyMeterDataEntryFacade.Insert(entry);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var observationQuery = from observations in iba.Observation_ado select observations;
                Assert.IsNotNull(observationQuery, "observationQuery is null");
                Assert.AreEqual(3, observationQuery.Count(), "observationQuery has wrong count");

                Observation_ado beyond50 = observationQuery.First(x => x.ObservationTypeId.Equals(PointCountBeyond50.ObservationTypeGuid));
                Assert.IsNotNull(beyond50, "no object added for Beyond 50 count");
                Assert.AreEqual(entry.Comments, beyond50.Comments, "comments beyond");
                Assert.AreEqual(entry.PointSurveyId, beyond50.EventId, "event id beyond");
                Assert.AreEqual(new Guid(DbTestHelper.SPECIES_2_ID), beyond50.SpeciesId, "species ID beyond");

                List<Observation_ado> withinList = observationQuery.Where(x => x.ObservationTypeId.Equals(PointCountWithin50.ObservationTypeGuid)).ToList();
                Assert.IsNotNull(withinList, "withinList is null");

                Assert.AreEqual(entry.Comments, withinList[0].Comments, "within 1 comments");
                Assert.AreEqual(entry.PointSurveyId, withinList[0].EventId, "within 1 event id");
                Assert.AreEqual(new Guid(DbTestHelper.SPECIES_2_ID), withinList[0].SpeciesId, "within 1 species id");

                Assert.AreEqual(entry.Comments, withinList[1].Comments, "within 2 comments");
                Assert.AreEqual(entry.PointSurveyId, withinList[1].EventId, "within 2 event id");
                Assert.AreEqual(new Guid(DbTestHelper.SPECIES_2_ID), withinList[1].SpeciesId, "within 2 species id");
            }
        }

        [TestMethod]
        public void t_Update()
        {
        }

        /// <summary>
        /// Validates that the Delete function properly deletes all related Observations.
        /// </summary>
        [TestMethod]
        public void t_Delete()
        {
            Guid eventId = DbTestHelper.TestGuid1;
            Guid speciesId1 = new Guid(DbTestHelper.SPECIES_1_ID);
            Guid speciesId2 = new Guid(DbTestHelper.SPECIES_2_ID);
            string speciesCode2 = DbTestHelper.SPECIES_2_CODE;
            string comments1 = string.Empty;
            string comments2 = "comments 2";

            multiBackdoorSetup(eventId, speciesId1, speciesId2, comments1, comments2);

            // TODO: fix this
            FiftyMeterDataEntryMapper.Delete(new FiftyMeterDataEntry()
            {
                Comments = comments2,
                SpeciesCode = speciesCode2,
                PointSurveyId = eventId,
                CountBeyond50 = 2,
                CountWithin50 = 1
            }, new Business.Entities.FiftyMeterPointSurvey());

            // There should be one object left for species 1
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var observationQuery = from observations in iba.Observation_ado select observations;
                Assert.IsNotNull(observationQuery, "observationQuery is null");
                Assert.AreEqual(1, observationQuery.Count(), "observationQuery has wrong count");
                Assert.IsTrue(observationQuery.First().SpeciesId.Equals(speciesId1), "the remaining observation isn't for Species 1");
            }
        }

        /// <summary>
        /// Validates the List&lt;<see cref="FiftyMeterDataEntry"/>&gt with two entries is returned, the first
        /// having a species with count within 50 and the second having a species with count within 50 and two 
        /// beyond 50 meters.
        /// </summary>
        [TestMethod]
        public void t_SelectAllForEvent_Survey()
        {
            Guid eventId = DbTestHelper.TestGuid1;
            DbTestHelper.LoadSinglePointSurvey(eventId);
            Guid speciesId1 = new Guid(DbTestHelper.SPECIES_1_ID);
            Guid speciesId2 = new Guid(DbTestHelper.SPECIES_2_ID);
            string speciesCode1 = DbTestHelper.SPECIES_1_CODE;
            string speciesCode2 = DbTestHelper.SPECIES_2_CODE;
            string comments1 = string.Empty;
            string comments2 = "comments 2";

            multiBackdoorSetup(eventId, speciesId1, speciesId2, comments1, comments2);


            // Run the system
            List<FiftyMeterDataEntry> entryList = FiftyMeterDataEntryMapper.SelectAllForEvent(eventId);

            // Validate results
            Assert.IsNotNull(entryList, "entrylist is null");
            Assert.AreEqual(2, entryList.Count(), "entrylist does not contain 2 objects");

            ValidateFiftyMeterDataEntryObject(eventId, comments1, entryList, "1", 0, 1, speciesCode1);
            ValidateFiftyMeterDataEntryObject(eventId, comments2, entryList, "2", 2, 1, speciesCode2);
        }


        /// <summary>
        /// Validates the List&lt;<see cref="FiftyMeterDataEntry"/>&gt with four entries are returned, the first
        /// having a species with count within 50 and the second having a species with count within 50 and two 
        /// beyond 50 meters, and repeat with at a different SamplingPoint.
        /// </summary>
        [TestMethod]
        public void t_SelectAllForEvent_SiteVisit()
        {
            Guid siteVisitid = DbTestHelper.TestGuid3;
            DbTestHelper.LoadSingleSiteVisit(siteVisitid);
            Guid eventId = DbTestHelper.TestGuid1;
            Guid eventId2 = DbTestHelper.TestGuid2;
            Guid speciesId1 = new Guid(DbTestHelper.SPECIES_1_ID);
            Guid speciesId2 = new Guid(DbTestHelper.SPECIES_2_ID);
            string speciesCode1 = DbTestHelper.SPECIES_1_CODE;
            string speciesCode2 = DbTestHelper.SPECIES_2_CODE;
            string comments1 = string.Empty;
            string comments2 = "comments 2";

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    PointSurvey_ado survey1 = PointSurvey_ado.CreatePointSurvey_ado(eventId, true, DbTestHelper.TestGuid2, DateTime.Now, DateTime.Now.AddHours(2));
                    survey1.SiteVisitId = siteVisitid;
                    iba.AddToPointSurvey_ado1(survey1);
                    PointSurvey_ado survey2 = PointSurvey_ado.CreatePointSurvey_ado(eventId2, true, DbTestHelper.TestGuid2, DateTime.Now, DateTime.Now.AddHours(2));
                    survey2.SiteVisitId = siteVisitid;
                    iba.AddToPointSurvey_ado1(survey2);
                });


            multiBackdoorSetup(eventId, speciesId1, speciesId2, comments1, comments2);
            multiBackdoorSetup(eventId2, speciesId1, speciesId2, comments1, comments2);


            // Run the system
            List<FiftyMeterDataEntry> entryList = FiftyMeterDataEntryMapper.SelectAllForEvent(siteVisitid);

            // Validate results
            Assert.IsNotNull(entryList, "entrylist is null");
            Assert.AreEqual(4, entryList.Count(), "entrylist does not contain 4 objects");

            // TODO: need to make sure the grouping handles different PointSurveyId values, separating them appropriately.

            ValidateFiftyMeterDataEntryObject(eventId, comments1, entryList, "1", 0, 1, speciesCode1);
            ValidateFiftyMeterDataEntryObject(eventId, comments2, entryList, "2", 2, 1, speciesCode2);
            ValidateFiftyMeterDataEntryObject(eventId2, comments1, entryList, "1", 0, 1, speciesCode1);
            ValidateFiftyMeterDataEntryObject(eventId2, comments2, entryList, "2", 2, 1, speciesCode2);
        }


        #endregion

        #region Private Methods

        public static void ValidateFiftyMeterDataEntryObject(Guid pointSurveyId, string comments1, List<FiftyMeterDataEntry> entryList, string whichSpecies, int expectedCount, int expectedCountWithin50, string speciesCode)
        {
            FiftyMeterDataEntry species1 = entryList.Single(x => x.SpeciesCode.Equals(speciesCode) && x.PointSurveyId.Equals(pointSurveyId));
            Assert.IsNotNull(species1, "species " + whichSpecies + " isn't in the list");
            Assert.AreEqual(expectedCount, species1.CountBeyond50, "Count beyond 50 is wrong for species " + whichSpecies);
            Assert.AreEqual(expectedCountWithin50, species1.CountWithin50, "count within 50 is wrong for species " + whichSpecies);
            Assert.AreEqual(comments1, species1.Comments, "wrong count for species " + whichSpecies);
            Assert.AreEqual(pointSurveyId, species1.PointSurveyId, "wrong point survey id for species " + whichSpecies);
        }

        private static void multiBackdoorSetup(Guid eventId, Guid speciesId1, Guid speciesId2, string comments1, string comments2)
        {
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                Observation_ado obsSpecies1 = Observation_ado.CreateObservation_ado(0, eventId, speciesId1, PointCountWithin50.ObservationTypeGuid);
                obsSpecies1.Comments = comments1;
                iba.AddToObservation_ado(obsSpecies1);

                Observation_ado obsSpecies2 = Observation_ado.CreateObservation_ado(0, eventId, speciesId2, PointCountWithin50.ObservationTypeGuid);
                obsSpecies2.Comments = comments2;
                iba.AddToObservation_ado(obsSpecies2);

                Observation_ado obsSpecies3 = Observation_ado.CreateObservation_ado(0, eventId, speciesId2, PointCountBeyond50.ObservationTypeGuid);
                obsSpecies3.Comments = comments2;
                iba.AddToObservation_ado(obsSpecies3);

                Observation_ado obsSpecies4 = Observation_ado.CreateObservation_ado(0, eventId, speciesId2, PointCountBeyond50.ObservationTypeGuid);
                obsSpecies4.Comments = comments2;
                iba.AddToObservation_ado(obsSpecies4);
            });
        }

        #endregion

    }
}
