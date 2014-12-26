using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    /// <summary>
    /// Validates the function for retrieving Site boundaries from the database
    /// </summary>
    [TestClass]
    public class tSiteMapper_Boundaries : DbTest
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Location");
            DbTestHelper.ClearTable("SiteBoundary");
            DbTestHelper.LoadLookupTypes();
        }

        /// <summary>
        ///  Validates that three 3 boundary points come out in the right order.
        /// </summary>
        [TestMethod()]
        public void t_GetBoundaries_Positive()
        {
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();

            SiteBoundary boundary1 = new SiteBoundary() { SiteId = extraList[0].LocationId, Latitude = 1.02m, Longitude = 3.456m, VertexSequence = 1 };
            SiteBoundary boundary2 = new SiteBoundary() { SiteId = extraList[0].LocationId, Latitude = -7.8m, Longitude = 90.12m, VertexSequence = 2 };
            SiteBoundary boundary3 = new SiteBoundary() { SiteId = extraList[0].LocationId, Latitude = 34.5m, Longitude = -64.47m, VertexSequence = 3 };
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                iba.AddToSiteBoundaries(boundary2);
                iba.AddToSiteBoundaries(boundary1);
                iba.AddToSiteBoundaries(boundary3);
            });

            Site results = new Site() { Id = extraList[0].LocationId };
            SiteMapper.GetBoundaries(results);
            Assert.IsNotNull(results.Boundaries, "results are null");
            Assert.AreEqual(3, results.Boundaries.Count(), "wrong count");

            Coordinate coord = results.Boundaries.Dequeue();
            Assert.AreEqual(boundary1.Latitude, coord.Latitude.Value, "lat 1");
            Assert.AreEqual(boundary1.Longitude, coord.Longitude.Value, "long 1");

            coord = results.Boundaries.Dequeue();
            Assert.AreEqual(boundary2.Latitude, coord.Latitude.Value, "lat 2");
            Assert.AreEqual(boundary2.Longitude, coord.Longitude.Value, "long 2");

            coord = results.Boundaries.Dequeue();
            Assert.AreEqual(boundary3.Latitude, coord.Latitude.Value, "lat 3");
            Assert.AreEqual(boundary3.Longitude, coord.Longitude.Value, "long 3");
        }

        /// <summary>
        /// Validates that only the boundaries associated with the particular Site are retrieved.
        /// </summary>
        [TestMethod()]
        public void t_GetBoundaries_WrongSite()
        {
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();

            SiteBoundary boundary1 = new SiteBoundary() { SiteId = extraList[0].LocationId, Latitude = 1.02m, Longitude = 3.456m, VertexSequence = 1 };
            SiteBoundary boundary2 = new SiteBoundary() { SiteId = extraList[1].LocationId, Latitude = -7.8m, Longitude = 90.12m, VertexSequence = 2 };
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                iba.AddToSiteBoundaries(boundary2);
                iba.AddToSiteBoundaries(boundary1);
            });

            // using site 1
            Site results = new Site() { Id = extraList[0].LocationId };
            SiteMapper.GetBoundaries(results);
            Assert.IsNotNull(results.Boundaries, "results are null");
            Assert.AreEqual(1, results.Boundaries.Count(), "wrong count");

            Coordinate coord = results.Boundaries.Dequeue();
            Assert.AreEqual(boundary1.Latitude, coord.Latitude.Value, "lat 1");
            Assert.AreEqual(boundary1.Longitude, coord.Longitude.Value, "long 1");
        }

        /// <summary>
        /// Validates that no results are returned when there are no data.
        /// </summary>
        [TestMethod()]
        public void t_GetBoundaries_None()
        {
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();

            Site results = new Site() { Id = extraList[0].LocationId };
            SiteMapper.GetBoundaries(results);
            Assert.IsNotNull(results.Boundaries, "results are null");
            Assert.AreEqual(0, results.Boundaries.Count(), "wrong count");

        }
    }
}
