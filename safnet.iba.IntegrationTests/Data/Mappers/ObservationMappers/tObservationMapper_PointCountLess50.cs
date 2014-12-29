using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers.ObservationMappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="PointCountLess50Mapper"/> class.
    /// </summary>
    [TestClass]
    public class tObservationMapper_PointCountLess50 : DbTest
    {
        #region Fields

        private Guid _PointCountLess50TypeId;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.LoadLookupTypes();
            DbTestHelper.LoadSpecies();

            _PointCountLess50TypeId = (new PointCountWithin50()).ObservationTypeId;
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Delete()
        {
            Observation_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = new Observation_ado()
                {
                    Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                    SpeciesId = new Guid(TestHelper.SPECIES_1_ID),
                    EventId = TestHelper.TestGuid1,
                    ObservationTypeId = _PointCountLess50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> Observationlist = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            ObservationMapper.Delete(new PointCountWithin50() { Id = setupObject.ObservationId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointCountLess50Query = from PointCountLess50s in iba.Observation_ado
                                                   select PointCountLess50s;
                Assert.IsNotNull(PointCountLess50Query, "Query result is null");
                Assert.AreEqual(Observationlist.Count(), PointCountLess50Query.Count(), "Wrong number of results in query");
                validateExtraObservations(Observationlist, PointCountLess50Query);
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Save_Insert()
        {
            //loadSiteVisit(TestHelper.TestGuid1);
            PointCountWithin50 supplemental = new PointCountWithin50()
            {
                Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                SpeciesCode = TestHelper.SPECIES_1_CODE,
                EventId = TestHelper.TestGuid1
            };
            ObservationMapper.Insert(supplemental);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointCountLess50Query = from PointCountLess50s in iba.Observation_ado select PointCountLess50s;
                Assert.IsNotNull(PointCountLess50Query, "Query result is null");
                Assert.AreEqual(1, PointCountLess50Query.Count(), "Wrong number of results in query");
                Observation_ado adoPointCountLess50 = PointCountLess50Query.First();
                validateObjectEquality(supplemental, adoPointCountLess50, iba);
            }
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Save_Update()
        {
            //loadSiteVisit(TestHelper.TestGuid1);
            Observation_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = new Observation_ado()
                {
                    Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                    SpeciesId = new Guid(TestHelper.SPECIES_1_ID),
                    EventId = TestHelper.TestGuid1,
                    ObservationTypeId = _PointCountLess50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> extraList = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Setup object to be saved. Change everything except the Id.
            PointCountWithin50 testObject = new PointCountWithin50()
            {
                Comments = "hurdy gurdy",
                SpeciesCode = TestHelper.SPECIES_2_CODE,
                EventId = setupObject.EventId,
                Id = setupObject.ObservationId
            };

            // Execute the test
            ObservationMapper.Update(testObject);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointCountLess50Query = from PointCountLess50s in iba.Observation_ado select PointCountLess50s;
                Assert.IsNotNull(PointCountLess50Query, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, PointCountLess50Query.Count(), "Wrong number of results in query");

                Observation_ado adoPointCountLess50 = PointCountLess50Query.First(x => x.ObservationId == setupObject.ObservationId);
                validateObjectEquality(testObject, adoPointCountLess50, iba);

                validateExtraObservations(extraList, PointCountLess50Query);
            }
        }

        /// <summary>
        /// Validates selection of all PointCountLess50 objects in the database.
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Select_All()
        {
            // Backdoor setup
            Guid pointSurveyId = TestHelper.TestGuid3;
            DbTestHelper.LoadSinglePointSurvey(pointSurveyId, TestHelper.TestGuid2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            List<Observation_ado> list = DbTestHelper.LoadExtraneousObservations(pointSurveyId); List<Observation_ado> PointCountLess50AdoList = extraPointCountLess50s(list);

            // Exercise the test
            List<PointCountWithin50> PointCountLess50List = ObservationMapper.SelectAll<PointCountWithin50>();

            // Validate results
            Assert.AreEqual(PointCountLess50AdoList.Count(), PointCountLess50List.Count, "Wrong number of objects in the result list");
            foreach (Observation_ado ado in PointCountLess50AdoList)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(PointCountLess50List.Exists(x => x.Id.Equals(ado.ObservationId)), "Observation " + ado.ObservationId.ToString() + " is not in the results");
            }
        }

        /// <summary>
        /// Validates that the Selection of a PointCountLess50 by EventId returns only the expected value.
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Select_ByEventId()
        {
            Guid pointSurveyId = TestHelper.TestGuid2;
            DbTestHelper.LoadSinglePointSurvey(pointSurveyId, TestHelper.TestGuid2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            Observation_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = new Observation_ado()
                {
                    Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                    SpeciesId = new Guid(TestHelper.SPECIES_1_ID),
                    EventId = pointSurveyId,
                    ObservationTypeId = _PointCountLess50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            List<PointCountWithin50> selectList = ObservationMapper.SelectAllForEvent<PointCountWithin50>(setupObject.EventId);
            Assert.AreEqual(1, selectList.Count(), "Does not contain just one object");
            PointCountWithin50 result = selectList[0];

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(result, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates selection of a PointCountLess50 object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_PointCountLess50_Select_ByGuid()
        {
            Observation_ado setupObject = null;
            // backdoor data setup
            Guid pointSurveyId = TestHelper.TestGuid3;
            DbTestHelper.LoadSinglePointSurvey(pointSurveyId, TestHelper.TestGuid2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = new Observation_ado()
                {
                    Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                    SpeciesId = new Guid(TestHelper.SPECIES_1_ID),
                    EventId = pointSurveyId,
                    ObservationTypeId = _PointCountLess50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            PointCountWithin50 PointCountLess50 = ObservationMapper.Select<PointCountWithin50>(setupObject.ObservationId);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(PointCountLess50, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates that a call to the Select_ByPointSurveyId function throws an error if a null value is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_PointCountLess50_Select_ByPointSurveyId_Empty()
        {
            // Exercise the test
            List<PointCountWithin50> PointCountLess50 = ObservationMapper.SelectAllForEvent<PointCountWithin50>(Guid.Empty);
        }

        #endregion

        #region Private Methods

        private List<Observation_ado> extraPointCountLess50s(List<Observation_ado> list)
        {
            List<Observation_ado> PointCountLess50AdoList = list.FindAll(n => n.ObservationTypeId.Equals(_PointCountLess50TypeId));
            return PointCountLess50AdoList;
        }

        //private static void loadSiteVisit(Guid visitGuid)
        //{
        //    DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
        //        {
        //            SiteVisit_ado visit = SiteVisit_ado.CreateSiteVisit_ado(visitGuid, false, TestHelper.TestGuid2, DateTime.Now, DateTime.Now);
        //            iba.AddToSiteVisit_ado(visit);
        //        });
        //}

        private void validateExtraObservations(List<Observation_ado> extraList, IQueryable<Observation_ado> PointCountLess50Query)
        {
            foreach (Observation_ado extra in extraPointCountLess50s(extraList))
            {
                Observation_ado result = PointCountLess50Query.First(x => x.ObservationId == extra.ObservationId);
                Assert.IsNotNull(result, "There is no longer an object with id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.Comments, result.Comments, "Comments for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.EventId, result.EventId, "EventId for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.ObservationTypeId, result.ObservationTypeId, "ObservationTypeId  for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.SpeciesId, result.SpeciesId, "SpeciesId for id " + extra.ObservationId.ToString());
            }
        }

        private static void validateObjectEquality(PointCountWithin50 supplemental, Observation_ado adoPointCountLess50, IbaUnitTestEntities iba)
        {
            Assert.IsNotNull(adoPointCountLess50, "There is no PointCountLess50 with the ID to test for");
            Assert.IsNotNull(adoPointCountLess50.Comments, "ADO Comments are null");
            Assert.AreEqual(supplemental.Comments, adoPointCountLess50.Comments, "Comments");
            Assert.AreEqual(supplemental.EventId, adoPointCountLess50.EventId, "EventId");
            // ID is now an identity field
            //Assert.AreEqual(supplemental.Id, adoPointCountLess50.ObservationId, "ObservationId");
            Assert.AreEqual(supplemental.ObservationTypeId, adoPointCountLess50.ObservationTypeId, "ObservationTypeId");
            var speciesQuery = from specieses in iba.Species_ado where specieses.AlphaCode == supplemental.SpeciesCode select specieses.SpeciesId;
            Assert.AreEqual(speciesQuery.First().ToString(), adoPointCountLess50.SpeciesId.ToString(), "SpeciesId");
        }

        #endregion

    }
}
