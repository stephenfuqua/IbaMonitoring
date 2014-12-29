using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="SamplingPointMapper"/> class.
    /// </summary>
    [TestClass]
    public class tSamplingPointMapper : DbTest
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Location");
        }

        /// <summary>
        /// Validates that a record can be deleted correctly
        /// </summary>
        [TestMethod]
        public void t_SamplingPointMapper_Delete()
        {
            Location_ado location = null;

            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(TestHelper.TestParentGuid,
                    "locationName", LookupConstants.LocationTypeSite);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = null;
                iba.AddToLocation_ado(location);
            });
            Location_ado pointAdo = null;
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    pointAdo = Location_ado.CreateLocation_ado(TestHelper.TestGuid1,
                        "pointName", LookupConstants.LocationTypePoint);
                    location.ParentLocationId = TestHelper.TestParentGuid;
                    iba.AddToLocation_ado(pointAdo);
                });
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();


            SamplingPoint point = new SamplingPoint()
            {
                Id = TestHelper.TestGuid1,
                SiteId = TestHelper.TestParentGuid
            };

            // Exercise the system under test
            SamplingPointMapper.Delete(point);

            // Validate that the one record was deleted but none others were
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var pointQuery = from points in iba.Location_ado select points;
                Assert.IsNotNull(pointQuery, "Query result is null");
                Assert.AreEqual(0, pointQuery.Count(x => x.LocationId == point.Id), "Point wasn't deleted");

                // Check to see if extra points and locations are still in the database
                foreach (Location_ado extra in extraList)
                {
                    Assert.AreEqual(1, pointQuery.Count(x => x.LocationId == extra.LocationId), "Location " + extra.LocationId.ToString() + " is no longer in the database.");
                }
                Assert.AreEqual(1, pointQuery.Count(x => x.LocationId == location.LocationId), "Parent was deleted");
            }
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_SamplingPointMapper_Save_Insert()
        {
            loadASite();
            SamplingPoint site = new SamplingPoint()
            {
                GeoCoordinate = new Business.DataTypes.Coordinate()
                {
                    Latitude = new Business.DataTypes.Degree() { Value = 56.789M },
                    Longitude = new Business.DataTypes.Degree() { Value = 67.281M }
                },
                Id = TestHelper.TestGuid1,
                Name = "Site Name",
                SiteId = TestHelper.TestParentGuid
            };
            SamplingPointMapper.Insert(site);


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var siteQuery = from sites in iba.SamplingPoint_ado select sites;
                Assert.IsNotNull(siteQuery, "Query result is null");
                Assert.AreEqual(1, siteQuery.Count(), "Wrong number of results in query");
                SamplingPoint_ado adoSite = siteQuery.First();
                validateObjectEquality(site, adoSite);
            }
        }


        private static void validateObjectEquality(SamplingPoint site, SamplingPoint_ado adoSite)
        {
            Assert.IsNotNull(adoSite, "There is not Site with the ID to test for");
            Assert.AreEqual(site.GeoCoordinate.Latitude.Value, adoSite.Latitude, "Latitude");
            Assert.AreEqual(site.GeoCoordinate.Longitude.Value, adoSite.Longitude, "Longitude");
            Assert.AreEqual(site.Id, adoSite.LocationId, "Id");
            Assert.AreEqual(site.Name, adoSite.LocationName, "Name");
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_SamplingPointMapper_Save_Update()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(TestHelper.TestGuid1, "locationName", LookupConstants.LocationTypePoint);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = TestHelper.TestParentGuid;
                iba.AddToLocation_ado(location);
            });
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();

            // Setup object to be saved. Change everything except the Id.
            SamplingPoint site = new SamplingPoint()
            {
                GeoCoordinate = new Business.DataTypes.Coordinate()
                {
                    Latitude = new Business.DataTypes.Degree() { Value = location.Latitude.Value + 1M },
                    Longitude = new Business.DataTypes.Degree() { Value = location.Longitude.Value + 1M }
                },
                Id = location.LocationId,
                Name = location.LocationName + "asd",
                SiteId = TestHelper.TestParentGuid
            };

            // Execute the test
            SamplingPointMapper.Update(site);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var siteQuery = from sites in iba.SamplingPoint_ado select sites;
                Assert.IsNotNull(siteQuery, "Query result is null");
                Assert.AreEqual(extraSamplingPoints(extraList).Count() + 1, siteQuery.Count(), "Wrong number of results in query");
                SamplingPoint_ado adoSite = siteQuery.First(x => x.LocationId == TestHelper.TestGuid1);
                validateObjectEquality(site, adoSite);

                // double check the other objects as well, must make sure they remain unchanged.
                foreach (Location_ado adoLocation in extraSamplingPoints(extraList))
                {
                    adoSite = siteQuery.First(x => x.LocationId == adoLocation.LocationId);
                    Assert.IsNotNull(adoSite, "There is no longer an object with id " + adoLocation.LocationId.ToString());
                    Assert.AreEqual(adoLocation.Latitude, adoSite.Latitude, "Extra " + adoSite.LocationId.ToString() + " Latitude mismatch");
                    Assert.AreEqual(adoLocation.Longitude, adoSite.Longitude, "Extra " + adoSite.LocationId.ToString() + " Longitude mismatch");
                    Assert.AreEqual(adoSite.LocationName, adoSite.LocationName, "Extra " + adoSite.LocationId.ToString() + " Locationname mismatch");
                }
            }
        }

        [TestMethod]
        public void t_SamplingPointMapper_Select_ByGuid()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(TestHelper.TestGuid1,
                    "locationName", LookupConstants.LocationTypePoint);
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = TestHelper.TestParentGuid;
                iba.AddToLocation_ado(location);
            });
            DbTestHelper.LoadExtraneousLocations();

            // Exercise the test
            SamplingPoint site = SamplingPointMapper.Select(location.LocationId);

            validateResults(location, site);
        }

        [TestMethod]
        public void t_SamplingPointMapper_Select_All()
        {
            // Backdoor setup
            List<Location_ado> list = DbTestHelper.LoadExtraneousLocations();
            List<Location_ado> siteAdoList = extraSamplingPoints(list);

            // Exercise the test
            List<SamplingPoint> siteList = SamplingPointMapper.SelectAll();

            // Validate results
            Assert.AreEqual(siteAdoList.Count(), siteList.Count, "Wrong number of objects in the result list");
            foreach (Location_ado ado in siteAdoList)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(siteList.Exists(x => x.Id.Equals(ado.LocationId)), "Location " + ado.LocationId.ToString() + " is not in the results");
            }
        }

        private static List<Location_ado> extraSamplingPoints(List<Location_ado> list)
        {
            List<Location_ado> siteAdoList = list.FindAll(n => n.LocationTypeId.Equals(LookupConstants.LocationTypePoint));
            return siteAdoList;
        }

        private static void validateResults(Location_ado location, SamplingPoint point)
        {
            Assert.IsNotNull(point, "Point object is null");
            Assert.IsNotNull(point.GeoCoordinate, "Coordinates are null");
            Assert.AreEqual(location.Latitude, point.GeoCoordinate.Latitude.Value, "Latitude");
            Assert.AreEqual(location.LocationId, point.Id, "Id");
            Assert.AreEqual(location.LocationName, point.Name, "name");
            Assert.AreEqual(location.Longitude, point.GeoCoordinate.Longitude.Value, "Longitude");
            Assert.AreEqual(location.ParentLocationId, point.SiteId, "SiteId");
        }

        private void loadASite()
        {
            DbTestHelper.LoadAdoObjects((IbaUnitTestEntities iba) =>
                {
                    Location_ado site = Location_ado.CreateLocation_ado(TestHelper.TestParentGuid, "Parent", LookupConstants.LocationTypeSite);
                    iba.AddToLocation_ado(site);
                });
        }
    }
}
