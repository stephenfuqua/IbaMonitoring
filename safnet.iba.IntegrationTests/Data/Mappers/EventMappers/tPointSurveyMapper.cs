using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers.EventMappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="PointSurveyMapper"/> class.
    /// </summary>
    [TestClass]
    public class tPointSurveyMapper : DbTest
    {
        #region Public Methods

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("PointSurvey");
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_Delete()
        {
            PointSurvey_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid3, TestHelper.TestGuid1, 
                    DateTime.Now, DateTime.Now.AddMinutes(5));
                setupObject.NoiseCode = 0;
                setupObject.SiteVisitId = TestHelper.TestGuid1;
                iba.AddToPointSurvey_ado1(setupObject);
            });
            List<PointSurvey_ado> PointSurveyAdolist = DbTestHelper.LoadExtraneousPointSurveys();

            PointSurveyMapper.Delete(new FiftyMeterPointSurvey() { Id = setupObject.EventId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointSurveyQuery = from PointSurveys in iba.PointSurvey_ado1 select PointSurveys;
                Assert.IsNotNull(PointSurveyQuery, "Query result is null");
                Assert.AreEqual(PointSurveyAdolist.Count(), PointSurveyQuery.Count(), "Wrong number of results in query");
                validateExtraPointSurveyAdos(PointSurveyAdolist, PointSurveyQuery);
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_Save_Insert()
        {
            FiftyMeterPointSurvey toInsert = new FiftyMeterPointSurvey()
            {
                Id = TestHelper.TestGuid1,
                EndTimeStamp = DateTime.Now,
                LocationId = TestHelper.TestGuid4,
                NoiseCode = 0,
                StartTimeStamp = DateTime.Now.AddHours(-2)
            };
            PointSurveyMapper.Insert(toInsert);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointSurveyQuery = from PointSurveys in iba.PointSurvey_ado1 select PointSurveys;
                Assert.IsNotNull(PointSurveyQuery, "Query result is null");
                Assert.AreEqual(1, PointSurveyQuery.Count(), "Wrong number of results in query");
                PointSurvey_ado adoPointSurvey = PointSurveyQuery.First();
                validateObjectEquality(toInsert, adoPointSurvey);
            }
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_Save_Update()
        {
            PointSurvey_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid3, TestHelper.TestGuid1,
                    DateTime.Now, DateTime.Now.AddMinutes(5));
                setupObject.NoiseCode = 0;
                setupObject.SiteVisitId = TestHelper.TestGuid1;
                iba.AddToPointSurvey_ado1(setupObject);
            });
            List<PointSurvey_ado> extraList = DbTestHelper.LoadExtraneousPointSurveys();

            // Setup object to be saved. Change everything except the Id.
            FiftyMeterPointSurvey toSave = new FiftyMeterPointSurvey()
            {
                Id = setupObject.EventId,
                EndTimeStamp = setupObject.EndTime.AddHours(1),
                LocationId = TestHelper.TestGuid4,
                NoiseCode = 0,
                StartTimeStamp = setupObject.StartTime.AddHours(0.5)
            };

            // Execute the test
            PointSurveyMapper.Update(toSave);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointSurveyQuery = from PointSurveys in iba.PointSurvey_ado1 select PointSurveys;
                Assert.IsNotNull(PointSurveyQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, PointSurveyQuery.Count(), "Wrong number of results in query");
                PointSurvey_ado adoPointSurvey = PointSurveyQuery.First(x => x.EventId == setupObject.EventId);
                validateObjectEquality(toSave, adoPointSurvey);

                validateExtraPointSurveyAdos(extraList, PointSurveyQuery);
            }
        }

        /// <summary>
        /// Validates than an exception is thrown if an empty Guid is passed to the main Select function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_PointSurvey_Select_ByGuid_Empty()
        {
            PointSurveyMapper.Select(Guid.Empty);
        }

        /// <summary>
        /// Validates selection of a PointSurvey object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_Select_ByGuid()
        {
            PointSurvey_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = PointSurvey_ado.CreatePointSurvey_ado(TestHelper.TestGuid3, TestHelper.TestGuid1,
                    DateTime.Now, DateTime.Now.AddMinutes(5));
                setupObject.NoiseCode = 0;
                setupObject.SiteVisitId = TestHelper.TestGuid1;
                iba.AddToPointSurvey_ado1(setupObject);
            });
            List<PointSurvey_ado> extraList = DbTestHelper.LoadExtraneousPointSurveys();

            // Exercise the test
            FiftyMeterPointSurvey resultObject = PointSurveyMapper.Select(setupObject.EventId);

            validateObjectEquality(resultObject, setupObject);
        }
        
        /// <summary>
        /// Validates selection of all PointSurvey objects in the database.
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_SelectAllForSiteVisit()
        {
            // Backdoor setup -- both have same SameVisitId
            List<PointSurvey_ado> list = DbTestHelper.LoadExtraneousPointSurveys();

            // Exercise the test
            List<FiftyMeterPointSurvey> resultList = PointSurveyMapper.SelectAllForSiteVisit(list[0].SiteVisitId.Value);

            // Validate results
            Assert.AreEqual(resultList.Count(), list.Count(), "Wrong number of objects in the result list");
            foreach (PointSurvey_ado ado in list)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(resultList.Exists(x => x.Id.Equals(ado.EventId)), "PointSurvey_ado " + ado.EventId.ToString() + " is not in the results");
            }
        }

        #endregion

        #region Private Methods

        private static void validateExtraPointSurveyAdos(List<PointSurvey_ado> extraList, IQueryable<PointSurvey_ado> PointSurveyQuery)
        {
            foreach (PointSurvey_ado extra in extraList)
            {
                PointSurvey_ado adoPointSurvey = PointSurveyQuery.First(x => x.EventId == extra.EventId);
                Assert.IsNotNull(adoPointSurvey, "There is no longer an object with id " + extra.EventId.ToString());
                Assert.AreEqual(TestHelper.TestString(extra.EndTime), TestHelper.TestString(adoPointSurvey.EndTime), "EndTime for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.EventId, adoPointSurvey.EventId, "EventId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.LocationId, adoPointSurvey.LocationId, "LocationId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.NoiseCode, adoPointSurvey.NoiseCode, "NoiseCode for id " + extra.EventId.ToString());
                Assert.AreEqual(TestHelper.TestString(extra.StartTime), TestHelper.TestString(adoPointSurvey.StartTime), "StartTime for id " + extra.EventId.ToString());
            }
        }

        private static void validateObjectEquality(FiftyMeterPointSurvey survey, PointSurvey_ado adoPointSurvey)
        {
            Assert.IsNotNull(adoPointSurvey, "There is no PointSurvey with the ID to test for");
            Assert.AreEqual(TestHelper.TestString(survey.EndTimeStamp.Value), TestHelper.TestString(adoPointSurvey.EndTime), "EndTimeStamp");
            Assert.AreEqual(survey.Id, adoPointSurvey.EventId, "PointSurveyId");
            Assert.AreEqual(survey.LocationId, adoPointSurvey.LocationId, "LocationId");
            Assert.AreEqual(TestHelper.TestString(survey.StartTimeStamp.Value), TestHelper.TestString(adoPointSurvey.StartTime), "StartTimeStamp");
            Assert.AreEqual(survey.NoiseCode, adoPointSurvey.NoiseCode, "NoiseCode");
        }

        #endregion

    }
}
