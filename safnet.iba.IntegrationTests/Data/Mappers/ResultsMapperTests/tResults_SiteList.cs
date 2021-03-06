﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    /// <summary>
    /// Validation of Results_SiteList based on worked examples from "Ecological Diversity and Its Measurement" by Anne E. Magurran.
    /// </summary>
    [TestClass]
    [Ignore]
    public class tResults_SiteList : DbTest
    {
        private const string OAKWOOD = "OAKWOOD";
        private const string SPRUCES = "SPRUCES";

        public List<Species_ado> List { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DbTestHelper.ClearTable("Location");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("Observation");

            DbTestHelper.ClearTable("Species");

            setupSpecies();
        }


        [TestMethod]
        public void t_SiteBreeding()
        {
            setupOakwood();
            setupSprucewood();

            DataSet set = iba.Data.Mappers.ResultsMapper.GetCommunityBreeding(DateTime.Now.Year);

            IEnumerable<DataRow> rowSet = set.Tables[0].AsEnumerable();

            // Find the oakwood entry
            DataRow oakRow = rowSet.First(x => x["LocationName"].Equals(OAKWOOD));
            Assert.AreEqual("20", oakRow["Richness"].ToString(), "Richness Oaks");
            Assert.AreEqual("2.4", ((decimal)oakRow["AdjustedDiversityIndex"]).ToString("0.0"), "Diversity index Oaks");
            Assert.AreEqual("0.8", ((decimal)oakRow["Evenness"]).ToString("0.0"), "Evenness Oaks");


            // Find the spruces entry
            DataRow spruceRow = rowSet.First(x => x["LocationName"].Equals(SPRUCES));
            Assert.AreEqual("14", spruceRow["Richness"].ToString(), "Richness Oaks");
            Assert.AreEqual("2.0", ((decimal)spruceRow["AdjustedDiversityIndex"]).ToString("0.0"), "Diversity index Oaks");
            Assert.AreEqual("0.7", ((decimal)spruceRow["Evenness"]).ToString("0.0"), "Evenness Oaks");
        }

        private void setupOakwood()
        {
            Location_ado site = Location_ado.CreateLocation_ado(TestHelper.TestGuid1, OAKWOOD, LookupConstants.LocationTypeSite);
            Location_ado point = Location_ado.CreateLocation_ado(TestHelper.TestGuid2, "Point", LookupConstants.LocationTypePoint);
            point.ParentLocationId = site.LocationId;
            SiteVisit_ado visit = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid1, site.LocationId, DateTime.Now.AddHours(-7));
            PointSurvey_ado survey = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid2, point.LocationId, DateTime.Now.AddHours(-7), DateTime.Now.AddHours(-6));
            survey.SiteVisitId = visit.EventId;
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    iba.AddToPointSurvey_ado1(survey);
                    iba.AddToLocation_ado(site);
                    iba.AddToLocation_ado(point);
                    iba.SaveChanges();
                });

            List<Observation_ado> obsList = new List<Observation_ado>();
            addABunchOfObservations(obsList, 35, List[0].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 26, List[1].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 21, List[2].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 16, List[3].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 11, List[4].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 6, List[5].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 5, List[6].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[7].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[8].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[9].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[10].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[11].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 2, List[12].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 2, List[13].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 2, List[14].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[15].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[16].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[17].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[18].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 25, List[19].SpeciesId, survey.EventId);
            loadObservations(obsList);
        }

        private void setupSprucewood()
        {
            Location_ado site = Location_ado.CreateLocation_ado(TestHelper.TestGuid3, SPRUCES, LookupConstants.LocationTypeSite);
            Location_ado point = Location_ado.CreateLocation_ado(TestHelper.TestGuid4, "Point 2", LookupConstants.LocationTypePoint);
            point.ParentLocationId = site.LocationId;
            SiteVisit_ado visit = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid5, site.LocationId, DateTime.Now.AddHours(-7));
            PointSurvey_ado survey = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid6, point.LocationId, DateTime.Now.AddHours(-7), DateTime.Now.AddHours(-6));
            survey.SiteVisitId = visit.EventId;
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToPointSurvey_ado1(survey);
                iba.AddToLocation_ado(site);
                iba.AddToLocation_ado(point);
                iba.SaveChanges();
            });

            List<Observation_ado> obsList = new List<Observation_ado>();
            addABunchOfObservations(obsList, 65, List[0].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 30, List[1].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 30, List[2].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 20, List[3].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 14, List[4].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 11, List[5].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 9, List[6].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 5, List[7].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 4, List[8].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[9].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 3, List[10].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 2, List[11].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[12].SpeciesId, survey.EventId);
            addABunchOfObservations(obsList, 1, List[13].SpeciesId, survey.EventId);
            loadObservations(obsList);
        }

        private static void loadObservations(List<Observation_ado> obsList)
        {
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    obsList.ForEach(x => iba.AddToObservation_ado(x));
                    iba.SaveChanges();
                });
        }

        private static void addABunchOfObservations(List<Observation_ado> obsList, int entries, Guid speciesId, Guid eventId)
        {
            for (int i = 0; i < entries; i++)
            {
                obsList.Add(Observation_ado.CreateObservation_ado(0, eventId, speciesId, LookupConstants.ObservationTypePointLess50m));
            }
        }


        /// <summary>
        /// Must setup a many species for this set of tests. Pull GUIDs directly from the Species set up script for the website.
        /// </summary>
        /// <returns></returns>
        private void setupSpecies()
        {
            List = new List<Species_ado>();
            List.Add(new Species_ado() { SpeciesId = new Guid("00a1c72b-8b7e-4438-bad4-4ea0c7b3d9bc"), AlphaCode = "WIFL", CommonName = "Willow Flycatcher", ScientificName = "Empidonax traillii", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("02503ce7-9ff8-44f4-b9d2-2ba42c5d4c46"), AlphaCode = "VESP", CommonName = "Vesper Sparrow", ScientificName = "Pooecetes gramineus", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("02e338ce-44c1-4a78-9663-89b3f43d821e"), AlphaCode = "KILL", CommonName = "Killdeer", ScientificName = "Charadrius vociferus", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("060f5d1c-9976-405a-ba8c-6adfbf1fc312"), AlphaCode = "OCWA", CommonName = "Orange-crowned Warbler", ScientificName = "Vermivora celata", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0b3500a7-2b76-420c-bdc7-cfa159dad32b"), AlphaCode = "COHA", CommonName = "Cooper''s Hawk", ScientificName = "Accipiter cooperii", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0bf41a72-e8b6-4b01-ae61-a9bc71869506"), AlphaCode = "EAKI", CommonName = "Eastern Kingbird", ScientificName = "Tyrannus tyrannus", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0cb95daf-5e81-4534-afa8-6e295c447518"), AlphaCode = "PAWA", CommonName = "Palm Warbler", ScientificName = "Dendroica palmarum", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0cc616d9-a9e4-4d4a-9734-705e614b23a8"), AlphaCode = "CHSP", CommonName = "Chipping Sparrow", ScientificName = "Spizella passerina", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0d244d67-7582-4317-92f7-fbd1c97f0b66"), AlphaCode = "CMWA", CommonName = "Cape May Warbler", ScientificName = "Dendroica tigrina", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0f724dd2-b900-404f-a761-64269ecc660f"), AlphaCode = "ALFL", CommonName = "Alder Flycatcher", ScientificName = "Empidonax alnorum", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("0fa6b44b-b687-465e-acc8-9f03f21d66e0"), AlphaCode = "OROR", CommonName = "Orchard Oriole", ScientificName = "Icterus spurius", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("112b8804-5ae7-4ec1-a05d-a906a5e6cfb3"), AlphaCode = "FISP", CommonName = "Field Sparrow", ScientificName = "Spizella pusilla", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("17adddc8-c2af-4073-8257-edd353348312"), AlphaCode = "GRCA", CommonName = "Gray Catbird", ScientificName = "Dumetella carolinensis", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("18f1b0ae-6a51-4fd7-97c3-c99405b154b9"), AlphaCode = "OSFL", CommonName = "Olive-sided Flycatcher", ScientificName = "Contopus cooperi", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("18f68aa9-1818-4136-8858-1011ed01d576"), AlphaCode = "SEWR", CommonName = "Sedge Wren", ScientificName = "Cistothorus platensis", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("1db935f0-7d59-4af3-8e06-a4dbfccf89f1"), AlphaCode = "BADO", CommonName = "Barred Owl", ScientificName = "Strix varia", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("1e6ff54a-0963-4e53-85f4-bb350a3bc384"), AlphaCode = "LISP", CommonName = "Lincoln''s Sparrow", ScientificName = "Melospiza lincolnii", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("22785ab2-e03b-4299-b281-20c384724058"), AlphaCode = "BRCR", CommonName = "Brown Creeper", ScientificName = "Certhia americana", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("23dde214-80b3-42aa-a813-95a2d8834362"), AlphaCode = "BLPW", CommonName = "Blackpoll Warbler", ScientificName = "Dendroica striata", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("252d4143-7157-4481-aade-64c8f163c069"), AlphaCode = "SWTH", CommonName = "Swainson''s Thrush", ScientificName = "Catharus ustulatus", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("27b67ac2-7258-4b67-8a20-faf6f88280e6"), AlphaCode = "BOBO", CommonName = "Bobolink", ScientificName = "Dolichonyx oryzivorus", WarningCount = 10, UseForCommunityMeasures = true });
            List.Add(new Species_ado() { SpeciesId = new Guid("27c9d9c2-af15-4bb1-9648-c5e9b8acf490"), AlphaCode = "YBCU", CommonName = "Yellow-billed Cuckoo", ScientificName = "Coccyzus americanus", WarningCount = 10, UseForCommunityMeasures = true });

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    List.ForEach(x => iba.AddToSpecies_ado(x));
                    iba.SaveChanges();
                });
        }

    }
}
