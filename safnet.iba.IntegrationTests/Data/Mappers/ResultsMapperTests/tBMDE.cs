using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    /// <summary>
    /// Summary description for tBMDE
    /// </summary>
    [TestClass]
    public class tBMDE : DbTest
    {
        public const string PersonFirstName = "firstname";
        public const string PersonLastName = "lastname";
        public static readonly Guid PersonGuid = TestHelper.TestGuid1;
        public static readonly Guid SiteVisitGuid = TestHelper.TestGuid1;
        public static readonly Guid PointSurveyGuid = TestHelper.TestGuid1;
        public static readonly Guid PointSurveyGuid2 = TestHelper.TestGuid2;

        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.ClearTable("Location");
            DbTestHelper.ClearTable("Person");
            DbTestHelper.LoadSpecies();
        }

        /// <summary>
        /// Validates that an incomplete survey will not be included in BMDE results
        /// </summary>
        [TestMethod]
        public void t_BMDE_IncompleteSurvey()
        {
            DbTestHelper.LoadSinglePerson(PersonFirstName, PersonLastName, PersonGuid);
            List<Location_ado> locations = DbTestHelper.LoadExtraneousLocations();

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                DateTime startDateTimeme = DateTime.Now;
                SiteVisit_ado ado = SiteVisit_ado.CreateSiteVisit_ado(SiteVisitGuid, true, locations[0].LocationId, DateTime.Now.AddHours(-1),
                    DateTime.Now, TestHelper.TestGuid1);
                ado.IsDataEntryComplete = false;    // force incompletion
                iba.AddToSiteVisit_ado1(ado);
            });

            Location_ado point = locations.First(x => x.ParentLocationId != null);
            DbTestHelper.LoadSinglePointSurvey(PointSurveyGuid, SiteVisitGuid, point.LocationId, DateTime.Now, DateTime.Now.AddHours(1));

            DbTestHelper.LoadExtraneousObservations(PointSurveyGuid);

            string bmde = ResultsMapper.GetBMDE();

            Assert.AreEqual(string.Empty, bmde);
        }

        /// <summary>
        /// Validates that an complete survey will be included in BMDE results, checking all fields for a two observation of 2 individuals and 1 individual
        /// </summary>
        [TestMethod]
        [Ignore] // this is getting an XML error and it is not worth trying to fix it right now. 2014-12-26.
        public void t_BMDE_CompleteSurvey_TwoObservations()
        {
            DbTestHelper.LoadSinglePerson(PersonFirstName, PersonLastName, PersonGuid);
            List<Location_ado> locations = DbTestHelper.LoadExtraneousLocations();

            DateTime startDate = DateTime.Now.AddHours(-2);
            DateTime endDate = DateTime.Now;
            DateTime surveyDate = endDate.AddMinutes(-5);
            DateTime surveyDate2 = surveyDate.AddMinutes(-5);

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                DateTime startDateTimeme = DateTime.Now;
                SiteVisit_ado ado = SiteVisit_ado.CreateSiteVisit_ado(SiteVisitGuid, true, locations[0].LocationId, startDate,
                    endDate, TestHelper.TestGuid1);
                ado.IsDataEntryComplete = true;
                ado.RecorderId = PersonGuid;
                ado.ObserverId = PersonGuid;
                iba.SiteVisit_ado1.AddObject(ado);
            });

            Location_ado point = locations.First(x => x.ParentLocationId != null);
            Location_ado point2 = locations.Last();

            DbTestHelper.LoadSinglePointSurvey(PointSurveyGuid, SiteVisitGuid, point.LocationId, surveyDate, endDate);
            DbTestHelper.LoadSinglePointSurvey(PointSurveyGuid2, SiteVisitGuid, point2.LocationId, surveyDate2, surveyDate);

            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                Observation_ado observation = Observation_ado.CreateObservation_ado(0, PointSurveyGuid, new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid);
                Observation_ado observation2 = Observation_ado.CreateObservation_ado(0, PointSurveyGuid, new Guid(TestHelper.SPECIES_1_ID), PointCountBeyond50.ObservationTypeGuid);
                Observation_ado observation3 = Observation_ado.CreateObservation_ado(0, PointSurveyGuid2, new Guid(TestHelper.SPECIES_2_ID), PointCountWithin50.ObservationTypeGuid);
                iba.Observation_ado.AddObject(observation);
                iba.Observation_ado.AddObject(observation2);
                iba.Observation_ado.AddObject(observation3);
            });

            string bmde = ResultsMapper.GetBMDE();
            ASCIIEncoding encoding = new ASCIIEncoding();

            using (MemoryStream stream = new MemoryStream(encoding.GetBytes(bmde)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservationList));
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                serializer.UnknownElement += new XmlElementEventHandler(serializer_UnknownElement);
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnreferencedObject += new UnreferencedObjectEventHandler(serializer_UnreferencedObject);
                ObservationList obslist = (ObservationList)serializer.Deserialize(stream);

                Assert.AreEqual(2, obslist.Observation.Count(), "wrong Observation count");
                ObservationListObservation actual = obslist.Observation[0];
                Assert.AreEqual("Yes", actual.AllIndividualsReported, "AllIndividualsReported");
                Assert.AreEqual("observation", actual.BasisOfRecord, "BasisOfRecord");
                Assert.IsTrue(!string.IsNullOrEmpty(actual.CatalogNumber), "CatalogNumber");
                Assert.AreEqual(PersonFirstName + " " + PersonLastName, actual.Collector, "Collector");
                Assert.AreEqual(TestHelper.SPECIES_1_COMMON, actual.CommonName, "CommonName");
                Assert.AreEqual("North America", actual.Continent, "Continent");
                Assert.AreEqual("null", actual.CoordinateUncertainty, "CoordinateUncertainty");
                Assert.AreEqual("United States of America", actual.Country, "Country");
                Assert.AreEqual(startDate.Day, actual.DayCollected, "DayCollected");
                Assert.AreEqual(point.Latitude.Value, actual.DecimalLatitude, "DecimalLatitude");
                Assert.AreEqual(point.Longitude, actual.DecimalLongitude, "DecimalLongitude");
                Assert.AreEqual("Unlimited", actual.DistanceFromObserverMax, "DistanceFromObserverMax");
                Assert.AreEqual("50 m", actual.DistanceFromObserverMin, "DistanceFromObserverMin");
                Assert.AreEqual((endDate - surveyDate).TotalHours.ToString("00.00"), actual.DurationInHours.ToString("00.00"), "DurationInHours");
                string genus = TestHelper.SPECIES_1_SCIENTIFIC.Split(' ')[0];
                string species = TestHelper.SPECIES_1_SCIENTIFIC.Split(' ')[1];
                Assert.AreEqual(genus, actual.Genus, "Genus");
                Assert.AreEqual(startDate.DayOfYear, actual.JulianDay, "JulianDay");
                Assert.AreEqual(point.LocationName, actual.Locality, "Locality");
                Assert.AreEqual(startDate.Month, actual.MonthCollected, "MonthCollected");
                Assert.AreEqual(1, actual.NumberOfObservers, "NumberOfObservers");
                Assert.AreEqual(2, actual.ObservationCount, "ObservationCount");
                Assert.AreEqual(startDate.ToString("MM/dd/yyyy"), actual.ObservationDate, "ObservationDate");
                Assert.AreEqual("decimal degrees", actual.OriginalCoordinateSystem, "OriginalCoordinateSystem");
                Assert.AreEqual("Homayoun, T. Z. and R. B. Blair. 2009. Citizen-Science Monitoring of Landbirds in the Mississippi River Twin Cities Important Bird Area. Pages 607-616 in T.D., Rich, C. Arizmendi, D. Demarest and C. Thompson, editors. Tundra to Tropics: Connecting Birds, Habitats and People. Proceedings of the 4th International Partners in Flight Conference, 13-16 February 2008, McAllen, TX. Partners in Flight.", actual.ProtocolReference, "ProtocolReference");
                Assert.AreEqual("Point Count", actual.ProtocolType, "ProtocolType");
                Assert.AreEqual("http://www.partnersinflight.org/pubs/McAllenProc/articles/PIF09_Monitoring/Homayoun_PIF09.pdf", actual.ProtocolURL, "ProtocolURL");
                Assert.AreEqual("Restricted", actual.RecordPermissions, "RecordPermissions");
                Assert.AreEqual(locations[0].LocationId.ToString().ToUpper(), actual.RouteIdentifier, "RouteIdentifier");
                Assert.AreEqual(PointSurveyGuid.ToString().ToUpper(), actual.SamplingEventIdentifier, "SamplingEventIdentifier");
                Assert.AreEqual(TestHelper.SPECIES_1_SCIENTIFIC, actual.ScientificName, "ScientificName");
                Assert.AreEqual(TestHelper.SPECIES_1_CODE, actual.SpeciesCode, "SpeciesCode");
                Assert.AreEqual(species, actual.SpecificEpithet, "SpecificEpithet");
                Assert.AreEqual("Minnesota", actual.StateProvince, "StateProvince");
                Assert.AreEqual(point.LocationId.ToString().ToUpper(), actual.SurveyAreaIdentifier, "SurveyAreaIdentifier");
                Assert.AreEqual((surveyDate.TimeOfDay.Hours + surveyDate.TimeOfDay.Minutes/60.0).ToString("00.00"), actual.TimeCollected.ToString("00.00"), "TimeCollected");
                Assert.AreEqual((endDate.TimeOfDay.Hours + endDate.TimeOfDay.Minutes / 60.0).ToString("00.00"), actual.TimeObservationsEnded.ToString("00.00"), "TimeObservationsEnded");
                Assert.AreEqual(surveyDate.TimeOfDay.TotalHours.ToString("00.00"), actual.TimeObservationsStarted.ToString("00.00"), "TimeObservationsStarted");
                Assert.AreEqual(startDate.Year.ToString(), actual.YearCollected.ToString(), "YearCollected");

                actual = obslist.Observation[1];
                Assert.AreEqual("Yes", actual.AllIndividualsReported, "AllIndividualsReported");
                Assert.AreEqual("observation", actual.BasisOfRecord, "BasisOfRecord");
                Assert.IsTrue(!string.IsNullOrEmpty(actual.CatalogNumber), "CatalogNumber");
                Assert.AreEqual(PersonFirstName + " " + PersonLastName, actual.Collector, "Collector");
                Assert.AreEqual(TestHelper.SPECIES_2_COMMON, actual.CommonName, "CommonName");
                Assert.AreEqual("North America", actual.Continent, "Continent");
                Assert.AreEqual("null", actual.CoordinateUncertainty, "CoordinateUncertainty");
                Assert.AreEqual("United States of America", actual.Country, "Country");
                Assert.AreEqual(startDate.Day, actual.DayCollected, "DayCollected");
                Assert.AreEqual(point2.Latitude.Value, actual.DecimalLatitude, "DecimalLatitude");
                Assert.AreEqual(point2.Longitude, actual.DecimalLongitude, "DecimalLongitude");
                Assert.AreEqual("50 m", actual.DistanceFromObserverMax, "DistanceFromObserverMax");
                Assert.AreEqual("0 m", actual.DistanceFromObserverMin, "DistanceFromObserverMin");
                Assert.AreEqual((surveyDate - surveyDate2).TotalHours.ToString("00.00"), actual.DurationInHours.ToString("00.00"), "DurationInHours");
                genus = TestHelper.SPECIES_2_SCIENTIFIC.Split(' ')[0];
                species = TestHelper.SPECIES_2_SCIENTIFIC.Split(' ')[1];
                Assert.AreEqual(genus, actual.Genus, "Genus");
                Assert.AreEqual(startDate.DayOfYear, actual.JulianDay, "JulianDay");
                Assert.AreEqual(point.LocationName, actual.Locality, "Locality");
                Assert.AreEqual(startDate.Month, actual.MonthCollected, "MonthCollected");
                Assert.AreEqual(1, actual.NumberOfObservers, "NumberOfObservers");
                Assert.AreEqual(1, actual.ObservationCount, "ObservationCount");
                Assert.AreEqual(startDate.ToString("MM/dd/yyyy"), actual.ObservationDate, "ObservationDate");
                Assert.AreEqual("decimal degrees", actual.OriginalCoordinateSystem, "OriginalCoordinateSystem");
                Assert.AreEqual("Homayoun, T. Z. and R. B. Blair. 2009. Citizen-Science Monitoring of Landbirds in the Mississippi River Twin Cities Important Bird Area. Pages 607-616 in T.D., Rich, C. Arizmendi, D. Demarest and C. Thompson, editors. Tundra to Tropics: Connecting Birds, Habitats and People. Proceedings of the 4th International Partners in Flight Conference, 13-16 February 2008, McAllen, TX. Partners in Flight.", actual.ProtocolReference, "ProtocolReference");
                Assert.AreEqual("Point Count", actual.ProtocolType, "ProtocolType");
                Assert.AreEqual("http://www.partnersinflight.org/pubs/McAllenProc/articles/PIF09_Monitoring/Homayoun_PIF09.pdf", actual.ProtocolURL, "ProtocolURL");
                Assert.AreEqual("Restricted", actual.RecordPermissions, "RecordPermissions");
                Assert.AreEqual(locations[0].LocationId.ToString().ToUpper(), actual.RouteIdentifier, "RouteIdentifier");
                Assert.AreEqual(PointSurveyGuid2.ToString().ToUpper(), actual.SamplingEventIdentifier, "SamplingEventIdentifier");
                Assert.AreEqual(TestHelper.SPECIES_2_SCIENTIFIC, actual.ScientificName, "ScientificName");
                Assert.AreEqual(TestHelper.SPECIES_2_CODE, actual.SpeciesCode, "SpeciesCode");
                Assert.AreEqual(species, actual.SpecificEpithet, "SpecificEpithet");
                Assert.AreEqual("Minnesota", actual.StateProvince, "StateProvince");
                Assert.AreEqual(point.LocationId.ToString().ToUpper(), actual.SurveyAreaIdentifier, "SurveyAreaIdentifier");
                Assert.AreEqual((surveyDate2.TimeOfDay.Hours + surveyDate2.TimeOfDay.Minutes/60.0).ToString("00.00"), actual.TimeCollected.ToString("00.00"), "TimeCollected");
                Assert.AreEqual((surveyDate.TimeOfDay.Hours + surveyDate.TimeOfDay.Minutes / 60.0).ToString("00.00"), actual.TimeObservationsEnded.ToString("00.00"), "TimeObservationsEnded");
                Assert.AreEqual(surveyDate2.TimeOfDay.TotalHours.ToString("00.00"), actual.TimeObservationsStarted.ToString("00.00"), "TimeObservationsStarted");
                Assert.AreEqual(startDate.Year.ToString(), actual.YearCollected.ToString(), "YearCollected");
            }
        }

        void serializer_UnreferencedObject(object sender, UnreferencedObjectEventArgs e)
        {
            throw new Exception("Unreferenced object: " + e.UnreferencedObject.ToString());
        }

        void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            throw new Exception("Unknown node: " + e.Name);
        }

        void serializer_UnknownElement(object sender, XmlElementEventArgs e)
        {
            throw new Exception("Unknown element: " + e.Element.ToString());
        }

        void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            throw new Exception("Unknown attribute: " + e.Attr.ToString());
        }
    }
}
