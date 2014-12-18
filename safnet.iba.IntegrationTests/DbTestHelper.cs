using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests
{
    /// <summary>
    /// Methods that help speed up creation of unit tests that interact with database
    /// </summary>
    public static class DbTestHelper
    {



        #region Delegates

        public delegate void LoadAdoMethod(IbaUnitTestEntities iba);

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the table.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public static void ClearTable(string tableName)
        {
            string sql = "DELETE FROM dbo." + tableName;


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                iba.ExecuteStoreCommand(sql);
            }
        }

        public static void LoadAdoObjects(LoadAdoMethod method)
        {
            try
            {
                using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
                {
                    method(iba);
                    iba.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                Assert.Fail(message);
            }
        }

        /// <summary>
        /// Loads extra site conditions to improve testing
        /// </summary>
        /// <returns>List of <see cref="SiteCondition_ado"/> objects</returns>
        public static List<SiteCondition_ado> LoadExtraneousConditions()
        {
            List<SiteCondition_ado> conditionList = new List<SiteCondition_ado>();

            SiteCondition_ado extra1 = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid2, 1, "F", 1, 1,
                TestHelper.TestGuid2);
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSiteCondition_ado(extra1);
            });
            conditionList.Add(extra1);

            SiteCondition_ado extra2 = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid3, 2, "C", 2, 2,
                TestHelper.TestGuid2);
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSiteCondition_ado(extra2);
            });
            conditionList.Add(extra2);

            return conditionList;
        }

        /// <summary>
        /// Loads extra records into the Location table, useful for insuring that selects and updates are truly updating the right
        /// record, rather than all records (unless ALL is what was intended).
        /// </summary>
        public static List<Location_ado> LoadExtraneousLocations()
        {
            List<Location_ado> locationList = new List<Location_ado>();

            Location_ado extra1 = null;
            LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                extra1 = Location_ado.CreateLocation_ado(TestHelper.TestGuid2,
                    "locationName", LookupConstants.LocationTypeSite);
                extra1.CodeName = "ex1";
                extra1.Latitude = 89.3M;
                extra1.Longitude = 90.10093M;
                extra1.ParentLocationId = null;
                iba.AddToLocation_ado(extra1);
            });
            locationList.Add(extra1);

            Location_ado extra2 = null;
            LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                extra2 = Location_ado.CreateLocation_ado(TestHelper.TestGuid3,
                     "locationName", LookupConstants.LocationTypeSite);
                extra2.CodeName = "ex2";
                extra2.Latitude = 69.3M;
                extra2.Longitude = 90.10293M;
                extra2.ParentLocationId = null;
                iba.AddToLocation_ado(extra2);
            });
            locationList.Add(extra2);

            Location_ado extra3 = null;
            LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                extra3 = Location_ado.CreateLocation_ado(TestHelper.TestGuid4,
                    "locationName", LookupConstants.LocationTypePoint);
                extra3.CodeName = string.Empty;
                extra3.Latitude = 79.3M;
                extra3.Longitude = 91.10093M;
                extra3.ParentLocationId = TestHelper.TestGuid3;
                iba.AddToLocation_ado(extra3);
            });
            locationList.Add(extra3);

            return locationList;
        }

        /// <summary>
        /// Loads extra observations to improve testing
        /// </summary>
        /// <returns>List of <see cref="Observation_ado"></see> objects</returns>
        /// <param name="pointSurveyId"></param>
        public static List<Observation_ado> LoadExtraneousObservations(Guid pointSurveyId)
        {
            List<Observation_ado> conditionList = new List<Observation_ado>();

            Guid observationTypeId = SupplementalObservation.ObservationTypeGuid;

            Observation_ado extra1 = Observation_ado.CreateObservation_ado(0, pointSurveyId, new Guid(TestHelper.SPECIES_2_ID), observationTypeId);
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToObservation_ado(extra1);
            });
            conditionList.Add(extra1);

            observationTypeId = PointCountBeyond50.ObservationTypeGuid;
            Observation_ado extra2 = Observation_ado.CreateObservation_ado(0, pointSurveyId, new Guid(TestHelper.SPECIES_2_ID), observationTypeId);
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToObservation_ado(extra2);
            });
            conditionList.Add(extra2);

            observationTypeId = PointCountWithin50.ObservationTypeGuid;
            Observation_ado extra3 = Observation_ado.CreateObservation_ado(0, pointSurveyId, new Guid(TestHelper.SPECIES_2_ID), observationTypeId);
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToObservation_ado(extra3);
            });
            conditionList.Add(extra3);

            return conditionList;
        }

        /// <summary>
        /// Loads a single person into the database.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="id">The id.</param>
        public static void LoadSinglePerson(string firstName, string lastName, Guid id)
        {
            LoadAdoObjects((IbaUnitTestEntities iba) =>
               {
                   Person_ado person = Person_ado.CreatePerson_ado(id, firstName, lastName, 0, 1, true, true);
                   iba.Person_ado.AddObject(person);
                   iba.SaveChanges();
               });
        }

        /// <summary>
        /// Loads extra Person objects to improve testing
        /// </summary>
        /// <returns>
        /// List of <see cref="Person_ado"/> objects
        /// </returns>
        public static List<Person_ado> LoadExtraneousPersons()
        {
            List<Person_ado> personList = new List<Person_ado>();

            Person_ado extra1 = Person_ado.CreatePerson_ado(TestHelper.TestGuid2, "first 1", "last 1", 0, 1, true, true);
            extra1.PersonStatus = 1;
            extra1.OpenId = "this is another openid";
            extra1.PhoneNumber = "7uhkj,";
            extra1.EmailAddress = "bill@microsoft.com";
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToPerson_ado(extra1);
            });
            personList.Add(extra1);

            Person_ado extra2 = Person_ado.CreatePerson_ado(TestHelper.TestGuid3, "first 2", "last 2", 0, 1, true, true);
            extra2.PersonStatus = 2;
            extra2.OpenId = "this is an openid";
            extra2.PhoneNumber = "12346677888";
            extra2.EmailAddress = "not@anemails.address";
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToPerson_ado(extra2);
            });
            personList.Add(extra2);

            return personList;
        }

        /// <summary>
        /// Loads extra PointSurvey objects to improve testing
        /// </summary>
        /// <returns>
        /// List of <see cref="PointSurvey_ado"/> objects
        /// </returns>
        public static List<PointSurvey_ado> LoadExtraneousPointSurveys()
        {
            List<PointSurvey_ado> surveyList = new List<PointSurvey_ado>();

            PointSurvey_ado extra1 = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid1, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddMinutes(5));
            extra1.NoiseCode = 0;
            extra1.SiteVisitId = TestHelper.TestGuid1;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToPointSurvey_ado1(extra1);
            });
            surveyList.Add(extra1);

            PointSurvey_ado extra2 = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid2, TestHelper.TestGuid2, DateTime.Now.AddMinutes(10), DateTime.Now.AddMinutes(15));
            extra2.NoiseCode = 0;
            extra2.SiteVisitId = TestHelper.TestGuid1;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToPointSurvey_ado1(extra2);
            });
            surveyList.Add(extra2);

            return surveyList;
        }

        /// <summary>
        /// Loads extra SiteVisit objects to improve testing
        /// </summary>
        /// <returns>
        /// List of <see cref="SiteVisit_ado"/> objects
        /// </returns>
        public static List<SiteVisit_ado> LoadExtraneousSiteVisits()
        {
            List<SiteVisit_ado> siteVisitlist = new List<SiteVisit_ado>();

            SiteVisit_ado extra1 = SiteVisit_ado.CreateSiteVisit_ado(TestHelper.TestGuid2, true, TestHelper.TestGuid2,
                    DateTime.Now, DateTime.Now.AddHours(2), TestHelper.TestGuid3);
            extra1.ObserverId = TestHelper.TestGuid4;
            extra1.RecorderId = TestHelper.TestGuid3;
            extra1.EndConditionId = TestHelper.TestGuid4;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSiteVisit_ado1(extra1);
            });
            siteVisitlist.Add(extra1);

            SiteVisit_ado extra2 = SiteVisit_ado.CreateSiteVisit_ado(TestHelper.TestGuid3, true, TestHelper.TestGuid1,
                    DateTime.Now, DateTime.Now.AddHours(2), TestHelper.TestGuid3);
            extra1.ObserverId = TestHelper.TestGuid4;
            extra1.RecorderId = TestHelper.TestGuid3;
            extra2.EndConditionId = TestHelper.TestGuid4;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSiteVisit_ado1(extra2);
            });
            siteVisitlist.Add(extra2);

            return siteVisitlist;
        }

        /// <summary>
        /// Loads extra <see cref="Species_ado"/> records to improve testing.
        /// </summary>
        /// <returns>
        /// List of <see cref="Species_ado"/> objects
        /// </returns>
        public static List<Species_ado> LoadExtraneousSpecies()
        {
            List<Species_ado> conditionList = new List<Species_ado>();

            Species_ado extra1 = Species_ado.CreateSpecies_ado(TestHelper.TestGuid2, "a2", 34, true);
            extra1.ScientificName = "extra1";
            extra1.CommonName = "extra1 bird";
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSpecies_ado(extra1);
            });
            conditionList.Add(extra1);

            Species_ado extra2 = Species_ado.CreateSpecies_ado(TestHelper.TestGuid3, "a2", 34, true);
            extra2.ScientificName = "extra2";
            extra2.CommonName = "extra2 bird";
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                iba.AddToSpecies_ado(extra2);
            });
            conditionList.Add(extra2);

            return conditionList;
        }

        /// <summary>
        /// Loads the Lookup table types.
        /// </summary>
        public static void LoadLookupTypes()
        {
            ClearTable("Observation");
            ClearTable("Location");
            ClearTable("Lookup");
            LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    Lookup_ado ado1 = Lookup_ado.CreateLookup_ado(LookupConstants.LocationTypeParent, "Location Type");
                    iba.AddToLookup_ado(ado1);
                    Lookup_ado ado5 = Lookup_ado.CreateLookup_ado(LookupConstants.ObservationTypeParent, "Observation Type");
                    iba.AddToLookup_ado(ado5);
                    iba.SaveChanges();

                    Lookup_ado ado2 = Lookup_ado.CreateLookup_ado(LookupConstants.LocationTypePoint, "Point");
                    ado2.ParentLookupId = ado1.LookupId;
                    iba.AddToLookup_ado(ado2);
                    Lookup_ado ado3 = Lookup_ado.CreateLookup_ado(LookupConstants.LocationTypeSite, "Site");
                    ado3.ParentLookupId = ado1.LookupId;
                    iba.AddToLookup_ado(ado3);

                    Lookup_ado ado4 = Lookup_ado.CreateLookup_ado((new PointCountBeyond50()).ObservationTypeId, "Beyond 50m");
                    ado4.ParentLookupId = ado2.LookupId;
                    iba.AddToLookup_ado(ado4);
                    Lookup_ado ado7 = Lookup_ado.CreateLookup_ado((new PointCountWithin50()).ObservationTypeId, "Less than 50m");
                    ado7.ParentLookupId = ado2.LookupId;
                    iba.AddToLookup_ado(ado7);
                    Lookup_ado ado6 = Lookup_ado.CreateLookup_ado((new SupplementalObservation()).ObservationTypeId, "Supplemental");
                    ado6.ParentLookupId = ado2.LookupId;
                    iba.AddToLookup_ado(ado6);
                });
        }

        /// <summary>
        /// Loads a single PointSurvey to the database
        /// </summary>
        /// <param name="endDateTime"></param>
        /// <param name="startdateTime"></param>
        /// <param name="locationId"></param>
        /// <param name="siteVisitId"></param>
        /// <param name="pointSurveyId">Guid</param>
        public static PointSurvey_ado LoadSinglePointSurvey(Guid pointSurveyId, Guid siteVisitId, Guid locationId, DateTime startdateTime, DateTime endDateTime)
        {
            PointSurvey_ado ado = null;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                ado = PointSurvey_ado.CreatePointSurvey_ado(pointSurveyId, locationId, startdateTime, endDateTime);
                ado.SiteVisitId = siteVisitId;
                iba.AddToPointSurvey_ado1(ado);
            });

            return ado;
        }

        /// <summary>
        /// Loads a single PointSurvey to the database
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="locationId"></param>
        /// <param name="pointSurveyId">Guid</param>
        public static SiteVisit_ado LoadSingleSiteVisit(Guid siteVisitid, Guid locationId, DateTime startDateTime)
        {
            SiteVisit_ado ado = null;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                DateTime startDateTimeme = DateTime.Now;
                ado = SiteVisit_ado.CreateSiteVisit_ado(siteVisitid, true, locationId, startDateTime,
                    startDateTime.AddHours(1), TestHelper.TestGuid1);
                iba.AddToSiteVisit_ado1(ado);
            });
            return ado;
        }

        public static List<Location_ado> LoadSites()
        {
            List<Location_ado> siteList = new List<Location_ado>();
            LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    Location_ado site1 = Location_ado.CreateLocation_ado(TestHelper.TestGuid1, "Location 1",
                        LookupConstants.LocationTypeSite);
                    iba.AddToLocation_ado(site1);
                    siteList.Add(site1);
                    Location_ado site2 = Location_ado.CreateLocation_ado(TestHelper.TestGuid2, "Location 2",
                        LookupConstants.LocationTypeSite);
                    iba.AddToLocation_ado(site2);
                    siteList.Add(site2);
                    Location_ado site3 = Location_ado.CreateLocation_ado(TestHelper.TestGuid3, "Location 3",
                        LookupConstants.LocationTypeSite);
                    iba.AddToLocation_ado(site3);
                    siteList.Add(site3);
                });

            return siteList;
        }

        public static Location_ado LoadSamplingPoint(Guid pointId, Guid siteId)
        {
            Location_ado point1= null;
            LoadAdoObjects((IbaUnitTestEntities iba) =>
            {
                point1 = Location_ado.CreateLocation_ado(pointId, "Point " + pointId.ToString().Substring(0, 5),
                    LookupConstants.LocationTypePoint);
                iba.AddToLocation_ado(point1);
            });

            return point1;
        }

        /// <summary>
        /// Loads the a few species into the Species table
        /// </summary>
        public static void LoadSpecies()
        {
            ClearTable("Species");
            LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    Species_ado species1 = Species_ado.CreateSpecies_ado(new Guid(TestHelper.SPECIES_1_ID), TestHelper.SPECIES_1_CODE, TestHelper.SPECIES_1_WARNING, true);
                    species1.ScientificName = TestHelper.SPECIES_1_SCIENTIFIC;
                    species1.CommonName = TestHelper.SPECIES_1_COMMON;
                    iba.AddToSpecies_ado(species1);
                    Species_ado species2 = Species_ado.CreateSpecies_ado(new Guid(TestHelper.SPECIES_2_ID), TestHelper.SPECIES_2_CODE, TestHelper.SPECIES_2_WARNING, true);
                    species2.ScientificName = TestHelper.SPECIES_2_SCIENTIFIC;
                    species2.CommonName = TestHelper.SPECIES_2_COMMON;
                    iba.AddToSpecies_ado(species2);
                    Species_ado species3 = Species_ado.CreateSpecies_ado(new Guid(TestHelper.SPECIES_3_ID), TestHelper.SPECIES_3_CODE, TestHelper.SPECIES_3_WARNING, true);
                    species3.ScientificName = TestHelper.SPECIES_3_SCIENTIFIC;
                    species3.CommonName = TestHelper.SPECIES_3_COMMON;
                    iba.AddToSpecies_ado(species3);
                });
        }

        /// <summary>
        /// Extension method for extracting year month day hour minute  AM/PM from time for simplifying unit test comparison.s
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TestString(this DateTime value)
        {
            return value.ToString("dt");
        }

        #endregion

    }
}
