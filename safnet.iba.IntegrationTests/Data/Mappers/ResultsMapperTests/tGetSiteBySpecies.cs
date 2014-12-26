using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    [TestClass]
    public class tGetSiteBySpecies : DbTest
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
        /// Validate database results for single species, observed at two different sites in two different weeks:
        /// 1. Location 1 will have 1 count in week 1.
        /// 2. Location 1 will have 2 count in week 2.
        /// 2. Location 2 will have 1 count in week 2.
        /// Add one more observation from a different year, to ensure it is ignored.
        /// </summary>
        [TestMethod]
        public void tGetSiteBySpecies_Full()
        {
            Guid siteVisitId1 = TestHelper.TestGuid1;
            Guid siteVisitId2 = TestHelper.TestGuid2;
            Guid siteVisitId3 = TestHelper.TestGuid3;
            Guid surveyId1 = TestHelper.TestGuid4;
            Guid surveyId2 = TestHelper.TestGuid5;
            Guid surveyId3 = TestHelper.TestGuid6;

            // Week 1 site 1, visit 1
            DbTestHelper.LoadSingleSiteVisit(siteVisitId1, _siteList[0].LocationId, DateTime.Parse("2010-01-01"));
            // Week 2 site 1, visit 2
            DbTestHelper.LoadSingleSiteVisit(siteVisitId2, _siteList[0].LocationId, DateTime.Parse("2010-01-09"));
            // Week 2 site 2, visit 1
            DbTestHelper.LoadSingleSiteVisit(siteVisitId3, _siteList[1].LocationId, DateTime.Parse("2010-01-08"));
            DbTestHelper.LoadSamplingPoint(surveyId1, _siteList[0].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId2, _siteList[0].LocationId);
            DbTestHelper.LoadSamplingPoint(surveyId3, _siteList[1].LocationId);
            DbTestHelper.LoadSinglePointSurvey(surveyId1, siteVisitId1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId2, siteVisitId2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadSinglePointSurvey(surveyId3, siteVisitId3, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            // Different year
            DbTestHelper.LoadSingleSiteVisit(surveyId1, _siteList[0].LocationId, DateTime.Parse("2009-06-06"));
            DbTestHelper.LoadSinglePointSurvey(siteVisitId1, surveyId1, TestHelper.TestGuid4, DateTime.Parse("2009-06-06 11:00 AM"), DateTime.Parse("2009-06-06 11:05 AM"));


            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                // Just 1 in week 1 site 1
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId1,
                    new Guid(TestHelper.SPECIES_1_ID), PointCountWithin50.ObservationTypeGuid));
                // Two in week 2 site 1
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId2,
                    new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid));
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId2,
                    new Guid(TestHelper.SPECIES_1_ID), PointCountWithin50.ObservationTypeGuid));
                // One beyond 50 in week 2 site 2
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, surveyId3,
                    new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid));
            });

            // Finally, can exercise the system under test
            using (IDataReader reader = iba.Data.Mappers.ResultsMapper.GetSiteBySpecies(new Guid(TestHelper.SPECIES_1_ID), 2010))
            {
                // Results are supposed to be sorted by location name.
                Assert.IsTrue(reader.Read(), "missing row 1");
                Assert.AreEqual("Location 1", reader.GetString(reader.GetOrdinal("LocationName")), "LocationName wrong row 1");
                Assert.AreEqual("1", reader.GetValue(reader.GetOrdinal("Week")).ToString(), "Week wrong row 1");
                Assert.AreEqual("1", reader.GetValue(reader.GetOrdinal("SpeciesCount")).ToString(), "SpeciesCount wrong row 1");

                Assert.IsTrue(reader.Read(), "missing row 2");
                Assert.AreEqual("Location 1", reader.GetString(reader.GetOrdinal("LocationName")), "LocationName wrong row 2");
                Assert.AreEqual("2", reader.GetValue(reader.GetOrdinal("Week")).ToString(), "Week wrong row 2");
                Assert.AreEqual("2", reader.GetValue(reader.GetOrdinal("SpeciesCount")).ToString(), "SpeciesCount wrong row 2");

                Assert.IsTrue(reader.Read(), "missing row 3");
                Assert.AreEqual("Location 2", reader.GetString(reader.GetOrdinal("LocationName")), "LocationName wrong row 3");
                Assert.AreEqual("2", reader.GetValue(reader.GetOrdinal("Week")).ToString(), "Week wrong row 3");
                Assert.AreEqual("1", reader.GetValue(reader.GetOrdinal("SpeciesCount")).ToString(), "SpeciesCount wrong row 3");
            }
        }

        /// <summary>
        /// Validate that supplemental results are not included.
        /// </summary>
        [TestMethod]
        public void tGetSiteBySpecies_Supplemental()
        {
            Guid siteVisitId1 = TestHelper.TestGuid1;
            Guid surveyId1 = TestHelper.TestGuid4;
            DbTestHelper.LoadSingleSiteVisit(siteVisitId1, _siteList[0].LocationId, DateTime.Parse("2010-01-01"));
            DbTestHelper.LoadSamplingPoint(surveyId1, _siteList[0].LocationId);
            DbTestHelper.LoadSinglePointSurvey(surveyId1, siteVisitId1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                // Just 1 in week 1 site 1
                iba.AddToObservation_ado(Observation_ado.CreateObservation_ado(0, siteVisitId1,
                    new Guid(TestHelper.SPECIES_1_ID), SupplementalObservation.ObservationTypeGuid));
            });

            // Finally, can exercise the system under test
            using (IDataReader reader = iba.Data.Mappers.ResultsMapper.GetSiteBySpecies(new Guid(TestHelper.SPECIES_1_ID), 2010))
            {
                Assert.IsFalse(reader.Read());
            }
        }
    }
}
