using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    [TestClass]
    public class tGetSpeciesCount
    {
        private List<Location_ado> _siteList;

        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.ClearTable("Location");
            DbTestHelper.LoadSpecies();
            _siteList = DbTestHelper.LoadSites();
        }


        /// <summary>
        /// Validates the result structure, and that there are no rows, when there are no species loaded into the database.
        /// </summary>
        [TestMethod]
        public void tGetSpeciesCount_NoData()
        {
            Guid siteVisitId1 = TestHelper.TestGuid1;
            Guid siteVisitId2 = TestHelper.TestGuid2;
            Guid siteVisitId3 = TestHelper.TestGuid3;
            Guid surveyId1 = TestHelper.TestGuid4;
            Guid surveyId2 = TestHelper.TestGuid5;
            Guid surveyId3 = TestHelper.TestGuid6;
            DbTestHelper.LoadSingleSiteVisit(siteVisitId1, _siteList[0].LocationId, DateTime.Now);
            DbTestHelper.LoadSingleSiteVisit(siteVisitId2, _siteList[1].LocationId, DateTime.Now);
            DbTestHelper.LoadSingleSiteVisit(siteVisitId3, _siteList[2].LocationId, DateTime.Now);
            DbTestHelper.LoadSamplingPoint(surveyId1, _siteList[0].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId2, _siteList[1].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId3, _siteList[2].LocationId);
            DbTestHelper.LoadSinglePointSurvey(surveyId1, siteVisitId1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId2, siteVisitId2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId3, siteVisitId3, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            // No species loaded

            // Finally, can exercise the system under test
            DataSet set = ResultsMapper.GetSpeciesCount(DateTime.Now.Year);
            Assert.IsNotNull(set, "DataSet is null");
            Assert.AreEqual(1, set.Tables.Count, "There is not 1 table in the DataSet");

            DataTable table = set.Tables[0];

            Assert.IsTrue(table.Columns.Contains("CommonName"), "Does not contain CommonName column");
            Assert.IsTrue(table.Columns.Contains("ScientificName"), "Does not contain ScientificName column");
            Assert.IsTrue(table.Columns.Contains("SpeciesCount"), "Does not contain SpeciesCount column");
            Assert.IsTrue(table.Columns.Contains("SpeciesId"), "Does not contain SpeciesId column");

            Assert.AreEqual(0, table.Rows.Count, "There are not zero rows in the results");
        }

        /// <summary>
        /// Validates that the count per species is returned correctly for following situation:
        /// * Species 1 - 1 at &lt; 50m in Site 1, 1 at &gt; 50m in Site 2 --> 2
        /// * Species 2 - 0 at &lt; 50 m, 3 at &gt; 50m at Site 1, supplemental at Site 2 --> 4
        /// * Species 3 - 1 at &lt; 50 m in Site 1, Site 2, and Site 3 --> 3
        /// Add an extra observation in a different year to ensure it is not returned in the results.
        /// </summary>
        [TestMethod]
        public void tGetSpeciesCount_Full()
        {
            Guid siteVisitId1 = TestHelper.TestGuid1;
            Guid siteVisitId2 = TestHelper.TestGuid2;
            Guid siteVisitId3 = TestHelper.TestGuid3;
            Guid surveyId1 = TestHelper.TestGuid4;
            Guid surveyId2 = TestHelper.TestGuid5;
            Guid surveyId3 = TestHelper.TestGuid6;
            DbTestHelper.LoadSingleSiteVisit(siteVisitId1, _siteList[0].LocationId, DateTime.Now);
            DbTestHelper.LoadSingleSiteVisit(siteVisitId2, _siteList[1].LocationId, DateTime.Now);
            DbTestHelper.LoadSingleSiteVisit(siteVisitId3, _siteList[2].LocationId, DateTime.Now);
            DbTestHelper.LoadSingleSiteVisit(surveyId1, _siteList[2].LocationId, DateTime.Now.AddYears(-1)); // extra
            DbTestHelper.LoadSamplingPoint(surveyId1, _siteList[0].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId2, _siteList[1].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId3, _siteList[2].LocationId);
            DbTestHelper.LoadSinglePointSurvey(surveyId1, siteVisitId1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId2, siteVisitId2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId3, siteVisitId3, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(siteVisitId1, surveyId1, TestHelper.TestGuid1, DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-1).AddHours(1)); // extra

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    // Load species 1 into the database
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_1_ID), PointCountWithin50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid));

                    // species 2
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_2_ID), PointCountBeyond50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_2_ID), PointCountBeyond50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_2_ID), PointCountBeyond50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_2_ID), PointCountWithin50.ObservationTypeGuid));

                    // species 3
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                        new Guid(TestHelper.SPECIES_3_ID), PointCountWithin50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId2,
                        new Guid(TestHelper.SPECIES_3_ID), PointCountWithin50.ObservationTypeGuid));
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId3,
                        new Guid(TestHelper.SPECIES_3_ID), PointCountWithin50.ObservationTypeGuid));   
                 
                    // extra
                    iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, siteVisitId1,
                        new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid));
                });

            // Finally, can exercise the system under test
            DataSet set = ResultsMapper.GetSpeciesCount(DateTime.Now.Year);
            Assert.IsNotNull(set, "DataSet is null");
            Assert.AreEqual(1, set.Tables.Count, "There is not 1 table in the DataSet");

            DataTable table = set.Tables[0];

            Assert.IsTrue(table.Columns.Contains("CommonName"), "Does not contain CommonName column");
            Assert.IsTrue(table.Columns.Contains("ScientificName"), "Does not contain ScientificName column");
            Assert.IsTrue(table.Columns.Contains("SpeciesCount"), "Does not contain SpeciesCount column");
            Assert.IsTrue(table.Columns.Contains("SpeciesId"), "Does not contain SpeciesId column");

            Assert.AreEqual(3, table.Rows.Count, "There are not three rows in the results");

            // Results are supposed to be sorted by common name. That means order of Species1, Species3, Species2.
            DataRow species1 = table.Rows[0];
            Assert.AreEqual(TestHelper.SPECIES_1_COMMON, species1["CommonName"], "CommonName wrong row 1");
            Assert.AreEqual(TestHelper.SPECIES_1_SCIENTIFIC, species1["ScientificName"], "ScientificName wrong row 1");
            Assert.AreEqual("2", species1["SpeciesCount"].ToString(), "SpeciesCount wrong row 1");
            Assert.AreEqual(TestHelper.SPECIES_1_ID, species1["SpeciesId"].ToString(), "SpeciesId wrong row 1");

            species1 = table.Rows[1];
            Assert.AreEqual(TestHelper.SPECIES_3_COMMON, species1["CommonName"], "CommonName wrong row 2");
            Assert.AreEqual(TestHelper.SPECIES_3_SCIENTIFIC, species1["ScientificName"], "ScientificName wrong row 2");
            Assert.AreEqual("3", species1["SpeciesCount"].ToString(), "SpeciesCount wrong row 2");
            Assert.AreEqual(TestHelper.SPECIES_3_ID, species1["SpeciesId"].ToString(), "SpeciesId wrong row 2");

            species1 = table.Rows[2];
            Assert.AreEqual(TestHelper.SPECIES_2_COMMON, species1["CommonName"], "CommonName wrong row 3");
            Assert.AreEqual(TestHelper.SPECIES_2_SCIENTIFIC, species1["ScientificName"], "ScientificName wrong row 3");
            Assert.AreEqual("4", species1["SpeciesCount"].ToString(), "SpeciesCount wrong row 3");
            Assert.AreEqual(TestHelper.SPECIES_2_ID, species1["SpeciesId"].ToString(), "SpeciesId wrong row 3");
        }


        /// <summary>
        /// Validates that supplemental results are not included.
        /// </summary>
        [TestMethod]
        public void tGetSpeciesCount_Supplemental()
        {
            Guid siteVisitId1 = TestHelper.TestGuid1;
            Guid surveyId1 = TestHelper.TestGuid4;
            DbTestHelper.LoadSingleSiteVisit(siteVisitId1, _siteList[0].LocationId, DateTime.Now);
            DbTestHelper.LoadSamplingPoint(surveyId1, _siteList[0].LocationId);
            DbTestHelper.LoadSinglePointSurvey(surveyId1, siteVisitId1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                // Load species 1 into the database
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, siteVisitId1,
                    new Guid(TestHelper.SPECIES_1_ID), SupplementalObservation.ObservationTypeGuid));
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                    new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid));
            });

            // Finally, can exercise the system under test
            DataSet set = ResultsMapper.GetSpeciesCount(DateTime.Now.Year);
            Assert.IsNotNull(set, "DataSet is null");
            Assert.AreEqual(1, set.Tables.Count, "There is not 1 table in the DataSet");

            DataTable table = set.Tables[0];


            Assert.AreEqual(1, table.Rows.Count, "There are not three rows in the results");

            // Results are supposed to be sorted by common name. That means order of Species1, Species3, Species2.
            DataRow species1 = table.Rows[0];
            Assert.AreEqual(TestHelper.SPECIES_1_COMMON, species1["CommonName"], "CommonName wrong row 1");
            Assert.AreEqual(TestHelper.SPECIES_1_SCIENTIFIC, species1["ScientificName"], "ScientificName wrong row 1");
            Assert.AreEqual("1", species1["SpeciesCount"].ToString(), "SpeciesCount wrong row 1");
            Assert.AreEqual(TestHelper.SPECIES_1_ID, species1["SpeciesId"].ToString(), "SpeciesId wrong row 1");
        }
    }
}
