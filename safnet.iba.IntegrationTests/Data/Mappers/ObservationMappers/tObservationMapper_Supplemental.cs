﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers.ObservationMappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="SupplementalObservationMapper"/> class.s
    /// </summary>
    [TestClass]
    public class tObservationMapper_Supplemental : DbTest
    {
        #region Fields

        private Guid _supplementalObservationTypeId;

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

            _supplementalObservationTypeId = (new SupplementalObservation()).ObservationTypeId;
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Delete()
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
                    ObservationTypeId = _supplementalObservationTypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> Observationlist = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            ObservationMapper.Delete(new SupplementalObservation() { Id = setupObject.ObservationId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SupplementalObservationQuery = from SupplementalObservations in iba.Observation_ado
                                                   select SupplementalObservations;
                Assert.IsNotNull(SupplementalObservationQuery, "Query result is null");
                Assert.AreEqual(Observationlist.Count(), SupplementalObservationQuery.Count(), "Wrong number of results in query");
                validateExtraObservations(Observationlist, SupplementalObservationQuery);
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Save_Insert()
        {
            //loadSiteVisit(TestHelper.TestGuid1);
            SupplementalObservation supplemental = new SupplementalObservation()
            {
                Comments = "asdfasdf asdf adsfa dsfads fasdf adsfasd fadsf awefr34fr34r34 ",
                SpeciesCode = TestHelper.SPECIES_1_CODE,
                EventId = TestHelper.TestGuid1
            };
            ObservationMapper.Insert(supplemental);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SupplementalObservationQuery = from SupplementalObservations in iba.Observation_ado select SupplementalObservations;
                Assert.IsNotNull(SupplementalObservationQuery, "Query result is null");
                Assert.AreEqual(1, SupplementalObservationQuery.Count(), "Wrong number of results in query");
                Observation_ado adoSupplementalObservation = SupplementalObservationQuery.First();
                validateObjectEquality(supplemental, adoSupplementalObservation, iba);
            }
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Save_Update()
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
                    ObservationTypeId = _supplementalObservationTypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            List<Observation_ado> extraList = DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Setup object to be saved. Change everything except the Id.
            SupplementalObservation testObject = new SupplementalObservation()
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
                var SupplementalObservationQuery = from SupplementalObservations in iba.Observation_ado select SupplementalObservations;
                Assert.IsNotNull(SupplementalObservationQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, SupplementalObservationQuery.Count(), "Wrong number of results in query");

                Observation_ado adoSupplementalObservation = SupplementalObservationQuery.First(x => x.ObservationId == setupObject.ObservationId);
                validateObjectEquality(testObject, adoSupplementalObservation, iba);

                validateExtraObservations(extraList, SupplementalObservationQuery);
            }
        }

        /// <summary>
        /// Validates selection of all SupplementalObservation objects in the database.
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Select_All()
        {
            // Backdoor setup
            Guid pointSurveyId = TestHelper.TestGuid3;
            DbTestHelper.LoadSinglePointSurvey(pointSurveyId, TestHelper.TestGuid2, TestHelper.TestGuid1, DateTime.Now, DateTime.Now.AddHours(1));
            List<Observation_ado> list = DbTestHelper.LoadExtraneousObservations(pointSurveyId);
            List<Observation_ado> SupplementalObservationAdoList = extraSupplementalObservations(list);

            // Exercise the test
            List<SupplementalObservation> SupplementalObservationList = ObservationMapper.SelectAll<SupplementalObservation>();

            // Validate results
            Assert.AreEqual(SupplementalObservationAdoList.Count(), SupplementalObservationList.Count, "Wrong number of objects in the result list");
            foreach (Observation_ado ado in SupplementalObservationAdoList)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(SupplementalObservationList.Exists(x => x.Id.Equals(ado.ObservationId)), "Observation " + ado.ObservationId.ToString() + " is not in the results");
            }
        }



        /// <summary>
        /// Validates that the Selection of a SupplementalObservation by EventId returns only the expected value.
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Select_ByEventId()
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
                    ObservationTypeId = _supplementalObservationTypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            List<SupplementalObservation> selectList = ObservationMapper.SelectAllForEvent<SupplementalObservation>(setupObject.EventId);
            Assert.AreEqual(1, selectList.Count(), "Does not contain just one object");
            SupplementalObservation result = selectList[0];

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(result, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates selection of a SupplementalObservation object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_SupplementalObservation_Select_ByGuid()
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
                    ObservationTypeId = _supplementalObservationTypeId
                };
                iba.AddToObservation_ado(setupObject);
            });
            DbTestHelper.LoadExtraneousObservations(TestHelper.TestGuid3);

            // Exercise the test
            SupplementalObservation SupplementalObservation = ObservationMapper.Select<SupplementalObservation>(setupObject.ObservationId);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                validateObjectEquality(SupplementalObservation, setupObject, iba);
            }
        }

        /// <summary>
        /// Validates that a call to the Select_ByPointSurveyId function throws an error if a null value is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_SupplementalObservation_Select_ByPointSurveyId_Empty()
        {
            // Exercise the test
            List<SupplementalObservation> SupplementalObservation = ObservationMapper.SelectAllForEvent<SupplementalObservation>(Guid.Empty);
        }

        #endregion

        #region Private Methods

        private List<Observation_ado> extraSupplementalObservations(List<Observation_ado> list)
        {
            List<Observation_ado> SupplementalObservationAdoList = list.FindAll(n => n.ObservationTypeId.Equals(_supplementalObservationTypeId));
            return SupplementalObservationAdoList;
        }

        private void validateExtraObservations(List<Observation_ado> extraList, IQueryable<Observation_ado> SupplementalObservationQuery)
        {
            foreach (Observation_ado extra in extraSupplementalObservations(extraList))
            {
                Observation_ado result = SupplementalObservationQuery.First(x => x.ObservationId == extra.ObservationId);
                Assert.IsNotNull(result, "There is no longer an object with id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.Comments, result.Comments, "Comments for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.EventId, result.EventId, "EventId for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.ObservationTypeId, result.ObservationTypeId, "ObservationTypeId  for id " + extra.ObservationId.ToString());
                Assert.AreEqual(extra.SpeciesId, result.SpeciesId, "SpeciesId for id " + extra.ObservationId.ToString());
            }
        }

        private static void validateObjectEquality(SupplementalObservation supplemental, Observation_ado adoSupplementalObservation, IbaUnitTestEntities iba)
        {
            Assert.IsNotNull(adoSupplementalObservation, "There is no SupplementalObservation with the ID to test for");
            Assert.IsNotNull(adoSupplementalObservation.Comments, "ADO Comments are null");
            Assert.AreEqual(supplemental.Comments, adoSupplementalObservation.Comments, "Comments");
            Assert.AreEqual(supplemental.EventId, adoSupplementalObservation.EventId, "EventId");
            // ID is now an identity field, so these should not match
            //Assert.AreEqual(supplemental.Id, adoSupplementalObservation.ObservationId, "ObservationId");
            Assert.AreEqual(supplemental.ObservationTypeId, adoSupplementalObservation.ObservationTypeId, "ObservationTypeId");
            var speciesQuery = from specieses in iba.Species_ado where specieses.AlphaCode == supplemental.SpeciesCode select specieses.SpeciesId;
            Assert.AreEqual(speciesQuery.First().ToString(), adoSupplementalObservation.SpeciesId.ToString(), "SpeciesId");
        }

        #endregion

    }
}
