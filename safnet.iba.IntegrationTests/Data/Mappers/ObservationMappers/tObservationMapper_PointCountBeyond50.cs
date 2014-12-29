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
    /// Validates the database mapping functions of the <see cref="PointCountBeyond50Mapper"/> class.s
    /// </summary>
    [TestClass]
    public class tObservationMapper_PointCountBeyond50 : DbTest
    {
        #region Fields

        private Guid _PointCountBeyond50TypeId;

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

            _PointCountBeyond50TypeId = (new PointCountBeyond50()).ObservationTypeId;
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Delete()
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
                    ObservationTypeId = _PointCountBeyond50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> Observationlist = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            ObservationMapper.Delete(new PointCountBeyond50() { Id = setupObject.ObservationId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointCountBeyond50Query = from PointCountBeyond50s in iba.Observation_ado
                                                   select PointCountBeyond50s;
                Assert.IsNotNull(PointCountBeyond50Query, "Query result is null");
                Assert.AreEqual(Observationlist.Count(), PointCountBeyond50Query.Count(), "Wrong number of results in query");
                validateExtraObservations(Observationlist, PointCountBeyond50Query);
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Save_Insert()
        {
            //loadSiteVisit(TestHelper.TestGuid1);
            PointCountBeyond50 supplemental = new PointCountBeyond50()
            {
                Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                SpeciesCode = TestHelper.SPECIES_1_CODE,
                EventId = TestHelper.TestGuid1
            };
            ObservationMapper.Insert(supplemental);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PointCountBeyond50Query = from PointCountBeyond50s in iba.Observation_ado select PointCountBeyond50s;
                Assert.IsNotNull(PointCountBeyond50Query, "Query result is null");
                Assert.AreEqual(1, PointCountBeyond50Query.Count(), "Wrong number of results in query");
                Observation_ado adoPointCountBeyond50 = PointCountBeyond50Query.First();
                validateObjectEquality(supplemental, adoPointCountBeyond50, iba);
            }
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Save_Update()
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
                    ObservationTypeId = _PointCountBeyond50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> extraList = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Setup object to be saved. Change everything except the Id.
            PointCountBeyond50 testObject = new PointCountBeyond50()
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
                var PointCountBeyond50Query = from PointCountBeyond50s in iba.Observation_ado select PointCountBeyond50s;
                Assert.IsNotNull(PointCountBeyond50Query, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, PointCountBeyond50Query.Count(), "Wrong number of results in query");

                Observation_ado adoPointCountBeyond50 = PointCountBeyond50Query.First(x => x.ObservationId == setupObject.ObservationId);
                validateObjectEquality(testObject, adoPointCountBeyond50, iba);

                validateExtraObservations(extraList, PointCountBeyond50Query);
            }
        }

        /// <summary>
        /// Validates selection of all PointCountBeyond50 objects in the database.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Select_All()
        {
            // Backdoor setup
            Guid pointSurveyId = TestHelper.TestGuid3;
            DbTestHelper.LoadSinglePointSurvey(pointSurveyId, TestHelper.TestGuid2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            List<Observation_ado> list = DbTestHelper.LoadExtraneousObservations(pointSurveyId); List<Observation_ado> PointCountBeyond50AdoList = extraPointCountBeyond50s(list);

            // Exercise the test
            List<PointCountBeyond50> PointCountBeyond50List = ObservationMapper.SelectAll<PointCountBeyond50>();

            // Validate results
            Assert.AreEqual(PointCountBeyond50AdoList.Count(), PointCountBeyond50List.Count, "Wrong number of objects in the result list");
            foreach (Observation_ado ado in PointCountBeyond50AdoList)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(PointCountBeyond50List.Exists(x => x.Id.Equals(ado.ObservationId)), "Observation " + ado.ObservationId.ToString() + " is not in the results");
            }
        }

        /// <summary>
        /// Validates that the Selection of a PointCountBeyond50 by EventId returns only the expected value.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Select_ByEventId()
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
                    ObservationTypeId = _PointCountBeyond50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            List<PointCountBeyond50> selectList = ObservationMapper.SelectAllForEvent<PointCountBeyond50>(setupObject.EventId);
            Assert.AreEqual(1, selectList.Count(), "Does not contain just one object");
            PointCountBeyond50 result = selectList[0];

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(result, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates selection of a PointCountBeyond50 object by the primary key.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_Select()
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
                    ObservationTypeId = _PointCountBeyond50TypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            PointCountBeyond50 PointCountBeyond50 = ObservationMapper.Select<PointCountBeyond50>(setupObject.ObservationId);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(PointCountBeyond50, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates that a call to the Select_ByPointSurveyId function throws an error if a null value is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_PointCountBeyond50_Select_ByPointSurveyId_Empty()
        {
            // Exercise the test
            List<PointCountBeyond50> PointCountBeyond50 = ObservationMapper.SelectAllForEvent<PointCountBeyond50>(Guid.Empty);
        }

        #endregion

        #region Private Methods

        private List<Observation_ado> extraPointCountBeyond50s(List<Observation_ado> list)
        {
            List<Observation_ado> PointCountBeyond50AdoList = list.FindAll(n => n.ObservationTypeId.Equals(_PointCountBeyond50TypeId));
            return PointCountBeyond50AdoList;
        }


        private void validateExtraObservations(List<Observation_ado> extraList, IQueryable<Observation_ado> PointCountBeyond50Query)
        {
            foreach (Observation_ado extra in extraPointCountBeyond50s(extraList))
            {
                Observation_ado result = PointCountBeyond50Query.First(x => x.ObservationId == extra.ObservationId);
                Assert.IsNotNull(result, "There is no longer an object with id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.Comments, result.Comments, "Comments for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.EventId, result.EventId, "EventId for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.ObservationTypeId, result.ObservationTypeId, "ObservationTypeId  for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.SpeciesId, result.SpeciesId, "SpeciesId for id " + extra.ObservationId.ToString());
            }
        }

        private static void validateObjectEquality(PointCountBeyond50 supplemental, Observation_ado adoPointCountBeyond50, IbaUnitTestEntities iba)
        {
            Assert.IsNotNull(adoPointCountBeyond50, "There is no PointCountBeyond50 with the ID to test for");
            Assert.IsNotNull(adoPointCountBeyond50.Comments, "ADO Comments are null");
            Assert.AreEqual(supplemental.Comments, adoPointCountBeyond50.Comments, "Comments");
            Assert.AreEqual(supplemental.EventId, adoPointCountBeyond50.EventId, "EventId");
            // ID is now an identity field
            //Assert.AreEqual(supplemental.Id, adoPointCountBeyond50.ObservationId, "ObservationId");
            Assert.AreEqual(supplemental.ObservationTypeId, adoPointCountBeyond50.ObservationTypeId, "ObservationTypeId");
            var speciesQuery = from specieses in iba.Species_ado where specieses.AlphaCode == supplemental.SpeciesCode select specieses.SpeciesId;
            Assert.AreEqual(speciesQuery.First().ToString(), adoPointCountBeyond50.SpeciesId.ToString(), "SpeciesId");
        }

        #endregion

    }
}
