using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using mappers = safnet.iba.Data.Mappers;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    /// <summary>
    /// Unit tests for <see cref="ResultsMapper"/>'s GetSiteList method.
    /// </summary>
    [TestClass]
    public class tGetSiteList
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DbTestHelper.ClearTable("Location");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.LoadLookupTypes();
        }

        /// <summary>
        /// Validates the stored procedure results for community breeding statistics. The actual results below were derived by 
        /// the script itself, rather than calculated while writing the test. The script was heavily tested by Stephen Fuqua 
        /// and Tania Homayoun (the avian modeling expert) to insure accuracy. This test merely proves that the stored 
        /// procedure is still accurate -- no illicit changes have been introduced. 
        /// Add one observation from a different year to ensure that it is not included.
        /// </summary>
        [TestMethod]
        public void t_GetCommunityBreeding()
        {
            DateTime breedingdate1 = new DateTime(2010, 07, 01);
            DateTime breedingdate2 = new DateTime(2010, 06, 11);
            DateTime breedingdate3 = new DateTime(2009, 06, 11, 9, 32, 0);

            List<Location_ado> siteList = DbTestHelper.LoadSites();
            SiteVisit_ado visit1 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid1, siteList[0].LocationId, breedingdate1);
            SiteVisit_ado visit2 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid2, siteList[1].LocationId, breedingdate2);
            SiteVisit_ado extra = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid3, siteList[0].LocationId, breedingdate3);
            PointSurvey_ado surveyExtra = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid5, extra.EventId, TestHelper.TestGuid1, breedingdate3, breedingdate3.AddMinutes(5));
            PointSurvey_ado survey1 = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid3, visit1.EventId, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            PointSurvey_ado survey2 = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid4, visit2.EventId, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            // To make this test interesting, there must be several factors:
            // A supplemental to ignore
            // At least one observation inside of 50m
            // At least one outside of 50m for each species
            // A site that has count of zero therefore shouldn't show up
            // And relative abundance cannot calculate as zero
            Observation_ado supplementalSite1 = Observation_ado.CreateObservation_ado(0, visit1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypeSupplemental);
            Observation_ado site1Species1Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site1Species1Less = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species1Less2 = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species2Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site1Species3Less = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species3Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species3Beyond = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species3Less = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site2Species2Beyond = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species2Less = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado extraObservation = Observation_ado.CreateObservation_ado(0, surveyExtra.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointBeyond50m);
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    iba.AddToObservation_ado(supplementalSite1);
                    iba.AddToObservation_ado(site1Species1Beyond);
                    iba.AddToObservation_ado(site1Species2Beyond);
                    iba.AddToObservation_ado(site2Species3Beyond);
                    iba.AddToObservation_ado(site1Species1Less);
                    iba.AddToObservation_ado(site1Species1Less2);
                    iba.AddToObservation_ado(site2Species3Less);
                    iba.AddToObservation_ado(site1Species3Less);
                    iba.AddToObservation_ado(site2Species2Beyond);
                    iba.AddToObservation_ado(site2Species2Less);
                    iba.AddToObservation_ado(extraObservation);
                    iba.SaveChanges();
                });

            DataSet resultSet = mappers.ResultsMapper.GetCommunityBreeding(2010);

            Assert.IsNotNull(resultSet, "Resultset is null");
            Assert.AreEqual(1, resultSet.Tables.Count, "Wrong table count");


            DataTable table = resultSet.Tables[0];


            Assert.IsTrue(table.Columns.Contains("LocationName"), "Missing LocationName column");
            Assert.IsTrue(table.Columns.Contains("Richness"), "Missing Richness column");
            Assert.IsTrue(table.Columns.Contains("DiversityIndex"), "Missing DiversityIndex column");
            Assert.IsTrue(table.Columns.Contains("Evenness"), "Missing Evenness column");
            Assert.AreEqual(2, table.Rows.Count, "Wrong row count");

            var locationQuery = from locs
                                in table.AsEnumerable()
                                 //where locs.Field<Guid>("LocationId").Equals(siteList[0].LocationId)
                                 select new
                                 {
                                     LocationId = locs.Field<Guid>("LocationId"),
                                     LocationName = locs.Field<string>("LocationName"),
                                     Richness = locs.Field<int>("Richness"),
                                     Diversity = locs.Field<decimal>("DiversityIndex"),
                                     Evenness = locs.Field<decimal>("Evenness")
                                 };
            var result = from l1 in locationQuery where l1.LocationName.Equals(siteList[0].LocationName) select l1;
            Assert.AreEqual(1, result.Count(), "does not contain row for location 1");
            Assert.AreEqual(siteList[0].LocationId, result.First().LocationId, "wrong location ID for location 1");
            Assert.AreEqual(3, result.First().Richness, "Wrong Richness for location 1");
            Assert.AreEqual(0.8044m, result.First().Diversity, "Wrong DiversityIndex for location 1");
            Assert.AreEqual(0.7322m, result.First().Evenness, "Wrong Evenness for location 1");

            result = from l1 in locationQuery where l1.LocationName.Equals(siteList[1].LocationName) select l1;
            Assert.AreEqual(1, result.Count(), "does not contain row for location 2");
            Assert.AreEqual(siteList[1].LocationId, result.First().LocationId, "wrong location ID for location 2");
            Assert.AreEqual(2, result.First().Richness, "Wrong Richness for location 2");
            Assert.AreEqual(0.5828m, result.First().Diversity, "Wrong DiversityIndex for location 2");
            Assert.AreEqual(0.8409m, result.First().Evenness, "Wrong Evenness for location 2");
        }

        /// <summary>
        /// Validates the stored procedure results for community migration statistics. See longer note in t_GetCommunityBreeding.
        /// Add one observation in a different year to ensure it is not included.
        /// </summary>
        [TestMethod]
        public void t_GetCommunityMigration()
        {
            DateTime migrationdate1 = new DateTime(2010, 05, 01);
            DateTime migrationdate2 = new DateTime(2010, 04, 11);
            DateTime migrationdate3 = new DateTime(2009, 04, 11, 9, 32, 0);

            List<Location_ado> siteList = DbTestHelper.LoadSites();
            SiteVisit_ado visit1 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid1, siteList[0].LocationId, migrationdate1);
            SiteVisit_ado visit2 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid2, siteList[1].LocationId, migrationdate2);
            PointSurvey_ado survey1 = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid3, visit1.EventId, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            PointSurvey_ado survey2 = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid4, visit2.EventId, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));

            SiteVisit_ado extra = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid3, siteList[0].LocationId, migrationdate3);
            PointSurvey_ado surveyExtra = DbTestHelper.LoadSinglePointSurvey(TestHelper.TestGuid5, extra.EventId, TestHelper.TestGuid1, migrationdate3, migrationdate3.AddMinutes(5));

            Observation_ado supplementalSite1 = Observation_ado.CreateObservation_ado(0, visit1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypeSupplemental);
            Observation_ado site1Species1Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site1Species1Less = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species1Less2 = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species2Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site1Species3Less = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site1Species3Beyond = Observation_ado.CreateObservation_ado(0, survey1.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species3Beyond = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species3Less = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_3_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado site2Species2Beyond = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointBeyond50m);
            Observation_ado site2Species2Less = Observation_ado.CreateObservation_ado(0, survey2.EventId, new Guid(TestHelper.SPECIES_2_ID), LookupConstants.ObservationTypePointLess50m);
            Observation_ado extraObservation = Observation_ado.CreateObservation_ado(0, surveyExtra.EventId, new Guid(TestHelper.SPECIES_1_ID), LookupConstants.ObservationTypePointBeyond50m);
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToObservation_ado(supplementalSite1);
                iba.AddToObservation_ado(site1Species1Beyond);
                iba.AddToObservation_ado(site1Species2Beyond);
                iba.AddToObservation_ado(site2Species3Beyond);
                iba.AddToObservation_ado(site1Species1Less);
                iba.AddToObservation_ado(site1Species1Less2);
                iba.AddToObservation_ado(site2Species3Less);
                iba.AddToObservation_ado(site1Species3Less);
                iba.AddToObservation_ado(site2Species2Beyond);
                iba.AddToObservation_ado(site2Species2Less);
                iba.AddToObservation_ado(extraObservation);
                iba.SaveChanges();
            });

            DataSet resultSet = mappers.ResultsMapper.GetCommunityMigration(2010);

            Assert.IsNotNull(resultSet, "Resultset is null");
            Assert.AreEqual(1, resultSet.Tables.Count, "Wrong table count");


            DataTable table = resultSet.Tables[0];


            Assert.IsTrue(table.Columns.Contains("LocationName"), "Missing LocationName column");
            Assert.IsTrue(table.Columns.Contains("Richness"), "Missing Richness column");
            Assert.IsTrue(table.Columns.Contains("DiversityIndex"), "Missing DiversityIndex column");
            Assert.IsTrue(table.Columns.Contains("Evenness"), "Missing Evenness column");
            Assert.AreEqual(2, table.Rows.Count, "Wrong row count");

            var locationQuery = from locs
                                in table.AsEnumerable()
                                //where locs.Field<Guid>("LocationId").Equals(siteList[0].LocationId)
                                select new
                                {
                                    LocationId = locs.Field<Guid>("LocationId"),
                                    LocationName = locs.Field<string>("LocationName"),
                                    Richness = locs.Field<int>("Richness"),
                                    Diversity = locs.Field<decimal>("DiversityIndex"),
                                    Evenness = locs.Field<decimal>("Evenness")
                                };
            var result = from l1 in locationQuery where l1.LocationName.Equals(siteList[0].LocationName) select l1;
            Assert.AreEqual(1, result.Count(), "does not contain row for location 1");
            Assert.AreEqual(siteList[0].LocationId, result.First().LocationId, "wrong location ID for location 1");
            Assert.AreEqual(3, result.First().Richness, "Wrong Richness for location 1");
            Assert.AreEqual(0.8044m, result.First().Diversity, "Wrong DiversityIndex for location 1");
            Assert.AreEqual(0.7322m, result.First().Evenness, "Wrong Evenness for location 1");

            result = from l1 in locationQuery where l1.LocationName.Equals(siteList[1].LocationName) select l1;
            Assert.AreEqual(1, result.Count(), "does not contain row for location 2");
            Assert.AreEqual(siteList[1].LocationId, result.First().LocationId, "wrong location ID for location 2");
            Assert.AreEqual(2, result.First().Richness, "Wrong Richness for location 2");
            Assert.AreEqual(0.5828m, result.First().Diversity, "Wrong DiversityIndex for location 2");
            Assert.AreEqual(0.8409m, result.First().Evenness, "Wrong Evenness for location 2");
        }
    }
}
