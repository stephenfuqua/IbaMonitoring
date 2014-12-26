using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers.ObservationMappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="PointCountBeyond50Mapper"/> class.s
    /// </summary>
    [TestClass]
    public class tObservationMapper_DeleteTopX : DbTest
    {


        #region Public Methods

        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.LoadLookupTypes();
            DbTestHelper.LoadSpecies();
        }

        /// <summary>
        /// Validates that the function will delete just 2 of 3 records that match criteria, and leave 1 that doesn't match criteria.
        /// </summary>
        [TestMethod]
        public void t_PointCountBeyond50_DeleteTopX()
        {
            Guid eventId = TestHelper.TestGuid1;
            Guid speciesId = new Guid(TestHelper.SPECIES_2_ID);
            string speciesCode = TestHelper.SPECIES_2_CODE;
            Guid observationTypeId1 = LookupConstants.ObservationTypePointLess50m;
            Guid observationTypeId2 = LookupConstants.ObservationTypeSupplemental;
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                Observation_ado obs1 = Observation_ado.CreateObservation_ado(0, eventId, speciesId, observationTypeId1);
                iba.AddToObservation_ado(obs1);
                Observation_ado ob2 = Observation_ado.CreateObservation_ado(0, eventId, speciesId, observationTypeId1);
                iba.AddToObservation_ado(ob2);
                Observation_ado obs3 = Observation_ado.CreateObservation_ado(0, eventId, speciesId, observationTypeId1);
                iba.AddToObservation_ado(obs3);
                Observation_ado obs4 = Observation_ado.CreateObservation_ado(0, eventId, speciesId, observationTypeId2);
                iba.AddToObservation_ado(obs4);
            });

            ObservationMapper.DeleteTopX(eventId, observationTypeId1, speciesCode, 2);

            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var observationQuery = from observations in iba.Observation_ado select observations;
                Assert.IsNotNull(observationQuery, "observationQuery is null");
                Assert.AreEqual(2, observationQuery.Count(), "observationQuery has wrong count");
                Assert.AreEqual(1, observationQuery.Count(x => x.ObservationTypeId.Equals(observationTypeId1)), "wrong count for type 1");
                Assert.AreEqual(1, observationQuery.Count(x => x.ObservationTypeId.Equals(observationTypeId2)), "wrong count for type 2");
            }
        }

        #endregion


    }
}
