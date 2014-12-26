using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers.EventMappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="SiteVisitMapper"/> class.
    /// </summary>
    [TestClass]
    public class tSiteVisitMapper : DbTest
    {
        #region Public Methods

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("SiteVisit");
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_SiteVisit_Delete()
        {
            SiteVisit_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = SiteVisit_ado.CreateSiteVisit_ado(TestHelper.TestGuid1, true, TestHelper.TestGuid2,
                    DateTime.Now, DateTime.Now.AddHours(2), TestHelper.TestGuid3);
                setupObject.ObserverId = TestHelper.TestGuid4;
                setupObject.RecorderId = TestHelper.TestGuid3;
                setupObject.Comments = "asdf asdfasdf";
                iba.AddToSiteVisit_ado1(setupObject);
            });
            List<SiteVisit_ado> SiteVisitAdolist = DbTestHelper.LoadExtraneousSiteVisits();

            SiteVisitMapper.Delete(new SiteVisit() { Id = setupObject.EventId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SiteVisitQuery = from SiteVisits in iba.SiteVisit_ado1 select SiteVisits;
                Assert.IsNotNull(SiteVisitQuery, "Query result is null");
                Assert.AreEqual(SiteVisitAdolist.Count(), SiteVisitQuery.Count(), "Wrong number of results in query");
                validateExtraSiteVisitAdos(SiteVisitAdolist, SiteVisitQuery);
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_SiteVisit_Save_Insert()
        {
            SiteVisit toInsert = new SiteVisit()
            {
                Id = TestHelper.TestGuid1,
                EndConditions = new SiteCondition()
                {
                    Id = TestHelper.TestGuid1,
                    SiteVisitId = TestHelper.TestGuid1,
                    Sky = 0,
                    Temperature = new Business.DataTypes.Temperature() { Units = "C", Value = 20 },
                    Wind = 2
                },
                EndTimeStamp = DateTime.Now,
                IsDataEntryComplete = true,
                LocationId = TestHelper.TestGuid4,
                ObserverId = TestHelper.TestGuid3,
                RecorderId = TestHelper.TestGuid2,
                StartConditions = new SiteCondition()
                {
                    Id = TestHelper.TestGuid2,
                    SiteVisitId = TestHelper.TestGuid1,
                    Sky = 1,
                    Temperature = new Business.DataTypes.Temperature() { Units = "C", Value = 23 },
                    Wind = 2
                },
                StartTimeStamp = DateTime.Now.AddHours(-2),
                Comments = "asdf asdfa sdf asdfasdfasdf"
            };
            SiteVisitMapper.Insert(toInsert);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SiteVisitQuery = from SiteVisits in iba.SiteVisit_ado1 select SiteVisits;
                Assert.IsNotNull(SiteVisitQuery, "Query result is null");
                Assert.AreEqual(1, SiteVisitQuery.Count(), "Wrong number of results in query");
                SiteVisit_ado adoSiteVisit = SiteVisitQuery.First();
                validateObjectEquality(toInsert, adoSiteVisit);
            }
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_SiteVisit_Save_Update()
        {
            SiteVisit_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = SiteVisit_ado.CreateSiteVisit_ado(TestHelper.TestGuid1, true, TestHelper.TestGuid2,
                    DateTime.Now, DateTime.Now.AddHours(2), TestHelper.TestGuid3);
                setupObject.ObserverId = TestHelper.TestGuid4;
                setupObject.RecorderId = TestHelper.TestGuid3;
                setupObject.Comments = "asdf asdfasdf";
                iba.AddToSiteVisit_ado1(setupObject);
            });
            List<SiteVisit_ado> extraList = DbTestHelper.LoadExtraneousSiteVisits();

            // Setup object to be saved. Change everything except the Id.
            SiteVisit toSave = new SiteVisit()
            {
                Id = setupObject.EventId,
                EndTimeStamp = setupObject.EndTime.AddHours(1),
                IsDataEntryComplete = false,
                LocationId = TestHelper.TestGuid4,
                ObserverId = TestHelper.TestGuid3,
                RecorderId = TestHelper.TestGuid2,
                StartTimeStamp = setupObject.StartTime.AddHours(0.5),
                Comments = "asdf asdfa sdf asdfasdfasdf"
            };

            // Execute the test
            SiteVisitMapper.Update(toSave);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SiteVisitQuery = from SiteVisits in iba.SiteVisit_ado1 select SiteVisits;
                Assert.IsNotNull(SiteVisitQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, SiteVisitQuery.Count(), "Wrong number of results in query");
                SiteVisit_ado adoSiteVisit = SiteVisitQuery.First(x => x.EventId == TestHelper.TestGuid1);
                validateObjectEquality(toSave, adoSiteVisit);

                validateExtraSiteVisitAdos(extraList, SiteVisitQuery);
            }
        }

        /// <summary>
        /// Validates than an exception is thrown if an empty Guid is passed to the main Select function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_SiteVisit_Select_ByGuid_Empty()
        {
            SiteVisitMapper.Select(Guid.Empty);
        }

        /// <summary>
        /// Validates selection of a SiteVisit object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_SiteVisit_Select_ByGuid()
        {
            SiteVisit_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = SiteVisit_ado.CreateSiteVisit_ado(TestHelper.TestGuid1, true, TestHelper.TestGuid2,
                    DateTime.Now, DateTime.Now.AddHours(2), TestHelper.TestGuid3);
                setupObject.ObserverId = TestHelper.TestGuid4;
                setupObject.RecorderId = TestHelper.TestGuid3;
                setupObject.Comments = "asdf asdfasdf";
                iba.AddToSiteVisit_ado1(setupObject);
            });
            List<SiteVisit_ado> extraList = DbTestHelper.LoadExtraneousSiteVisits();

            // Exercise the test
            SiteVisit resultObject = SiteVisitMapper.Select(setupObject.EventId);

            validateObjectEquality(resultObject, setupObject);
        }
        
        /// <summary>
        /// Validates selection of all SiteVisit objects in the database.
        /// </summary>
        [TestMethod]
        public void t_SiteVisit_SelectAllForSite()
        {
            // Backdoor setup
            List<SiteVisit_ado> list = DbTestHelper.LoadExtraneousSiteVisits();

            // Exercise the test
            List<SiteVisit> resultList = SiteVisitMapper.SelectAllForSite(list[0].LocationId);

            // Validate results
            Assert.AreEqual(resultList.Count(), 1, "Wrong number of objects in the result list");
            foreach (SiteVisit_ado ado in list.FindAll(x=>x.LocationId.Equals(list[0].LocationId)))
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(resultList.Exists(x => x.Id.Equals(ado.EventId)), "SiteVisitAdo " + ado.EventId.ToString() + " is not in the results");
            }
        }

        #endregion

        #region Private Methods

        private static void validateExtraSiteVisitAdos(List<SiteVisit_ado> extraList, IQueryable<SiteVisit_ado> SiteVisitQuery)
        {
            foreach (SiteVisit_ado extra in extraList)
            {
                SiteVisit_ado adoSiteVisit = SiteVisitQuery.First(x => x.EventId == extra.EventId);
                Assert.IsNotNull(adoSiteVisit, "There is no longer an object with id " + extra.EventId.ToString());
                Assert.AreEqual(extra.EndConditionId, adoSiteVisit.EndConditionId, "EndConditionId for id " + extra.EventId.ToString());
                Assert.AreEqual(TestHelper.TestString(extra.EndTime), TestHelper.TestString(adoSiteVisit.EndTime), "EndTime for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.EventId, adoSiteVisit.EventId, "EventId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.IsDataEntryComplete, adoSiteVisit.IsDataEntryComplete, "IsDataEntryComplete for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.LocationId, adoSiteVisit.LocationId, "LocationId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.ObserverId, adoSiteVisit.ObserverId, "ObserverId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.RecorderId, adoSiteVisit.RecorderId, "RecorderId for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.StartConditionId, adoSiteVisit.StartConditionId, "StartConditionId for id " + extra.EventId.ToString());
                Assert.AreEqual(TestHelper.TestString(extra.StartTime), TestHelper.TestString(adoSiteVisit.StartTime), "StartTime for id " + extra.EventId.ToString());
                Assert.AreEqual(extra.Comments, adoSiteVisit.Comments, "Comments");
            }
        }

        private static void validateObjectEquality(SiteVisit siteVisit, SiteVisit_ado adoSiteVisit)
        {
            Assert.IsNotNull(adoSiteVisit, "There is no SiteVisit with the ID to test for");
            Assert.AreEqual(TestHelper.TestString(siteVisit.EndTimeStamp.Value), TestHelper.TestString(adoSiteVisit.EndTime), "EndTimeStamp");
            Assert.AreEqual(siteVisit.Id, adoSiteVisit.EventId, "SiteVisitId");
            Assert.AreEqual(siteVisit.IsDataEntryComplete, adoSiteVisit.IsDataEntryComplete, "IsDataEntryComplete");
            Assert.AreEqual(siteVisit.LocationId, adoSiteVisit.LocationId, "LocationId");
            Assert.AreEqual(siteVisit.ObserverId, adoSiteVisit.ObserverId, "ObserverId");
            Assert.AreEqual(siteVisit.RecorderId, adoSiteVisit.RecorderId, "RecorderId");
            Assert.AreEqual(TestHelper.TestString(siteVisit.StartTimeStamp.Value), TestHelper.TestString(adoSiteVisit.StartTime), "StartTimeStamp");
            Assert.AreEqual(siteVisit.EndConditions.Id, (adoSiteVisit.EndConditionId.HasValue) ? adoSiteVisit.EndConditionId.Value : Guid.Empty, "EndConditions");
            Assert.AreEqual(siteVisit.StartConditions.Id, adoSiteVisit.StartConditionId, "StartConditions");
            Assert.AreEqual(siteVisit.Comments, adoSiteVisit.Comments, "Comments");
        }

        #endregion

    }
}
