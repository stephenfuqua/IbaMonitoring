using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="SiteMapper"/> class.s
    /// </summary>
    [TestClass]
    public class tSiteMapper : DbTest
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Location");
            DbTestHelper.LoadLookupTypes();
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_Site_Save_Insert()
        {
            Site site = new Site()
            {
                CodeName = "codename",
                GeoCoordinate = new Business.DataTypes.Coordinate()
                {
                    Latitude = new Business.DataTypes.Degree() { Value = 56.789M },
                    Longitude = new Business.DataTypes.Degree() { Value = 67.281M }
                },
                Id = TestHelper.TestGuid1,
                Name = "Site Name"
            };
            SiteMapper.Insert(site);


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var siteQuery = from sites in iba.Site_ado select sites;
                Assert.IsNotNull(siteQuery, "Query result is null");
                Assert.AreEqual(1, siteQuery.Count(), "Wrong number of results in query");
                Site_ado adoSite = siteQuery.First();
                validateObjectEquality(site, adoSite);
            }
        }


        private static void validateObjectEquality(Site site, Site_ado adoSite)
        {
            Assert.IsNotNull(adoSite, "There is not Site with the ID to test for");
            Assert.AreEqual(site.CodeName, adoSite.CodeName, "CodeName");
            Assert.AreEqual(site.GeoCoordinate.Latitude.Value, adoSite.Latitude, "Latitude");
            Assert.AreEqual(site.GeoCoordinate.Longitude.Value, adoSite.Longitude, "Longitude");
            Assert.AreEqual(site.Id, adoSite.LocationId, "Id");
            Assert.AreEqual(site.Name, adoSite.LocationName, "Name");
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_Site_Save_Update()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(TestHelper.TestGuid1,
                    "locationName", LookupConstants.LocationTypeSite);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = null;
                iba.AddToLocation_ado(location);
            });
            List<Location_ado> extraList = DbTestHelper.LoadExtraneousLocations();

            // Setup object to be saved. Change everything except the Id.
            Site site = new Site()
            {
                CodeName = location.CodeName + "a",
                GeoCoordinate = new Business.DataTypes.Coordinate()
                {
                    Latitude = new Business.DataTypes.Degree() { Value = location.Latitude.Value + 1M },
                    Longitude = new Business.DataTypes.Degree() { Value = location.Longitude.Value + 1M }
                },
                Id = location.LocationId,
                Name = location.LocationName + "asd"
            };

            // Execute the test
            SiteMapper.Update(site);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var siteQuery = from sites in iba.Site_ado select sites;
                Assert.IsNotNull(siteQuery, "Query result is null");
                Assert.AreEqual(extraSites(extraList).Count() + 1, siteQuery.Count(), "Wrong number of results in query");
                Site_ado adoSite = siteQuery.First(x => x.LocationId == TestHelper.TestGuid1);
                validateObjectEquality(site, adoSite);

                validateExtraLocations(extraList, siteQuery);
            }
        }

        private static void validateExtraLocations(List<Location_ado> extraList, IQueryable<Site_ado> siteQuery)
        {
            foreach (Location_ado adoLocation in extraSites(extraList))
            {
                Site_ado adoSite = siteQuery.First(x => x.LocationId == adoLocation.LocationId);
                Assert.IsNotNull(adoSite, "There is no longer an object with id " + adoLocation.LocationId.ToString());
                Assert.AreEqual(adoLocation.CodeName, adoSite.CodeName, "Extra " + adoSite.LocationId.ToString() + " CodeName mismatch");
                Assert.AreEqual(adoLocation.Latitude, adoSite.Latitude, "Extra " + adoSite.LocationId.ToString() + " Latitude mismatch");
                Assert.AreEqual(adoLocation.Longitude, adoSite.Longitude, "Extra " + adoSite.LocationId.ToString() + " Longitude mismatch");
                Assert.AreEqual(adoSite.LocationName, adoSite.LocationName, "Extra " + adoSite.LocationId.ToString() + " Locationname mismatch");
            }
        }

        /// <summary>
        /// Validates selection of a Site object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_Site_Select_ByGuid()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(TestHelper.TestGuid1,
                    "locationName", LookupConstants.LocationTypeSite);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = null;
                iba.AddToLocation_ado(location);
            });
            DbTestHelper.LoadExtraneousLocations();

            // Exercise the test
            Site site = SiteMapper.Select(location.LocationId);

            validateResults(location, site);
        }

        /// <summary>
        /// Validates than an exception is thrown if an empty Guid is passed to the main Select function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Site_Select_ByGuid_Empty()
        {
            SiteMapper.Select(Guid.Empty);
        }

        /// <summary>
        /// Validates selection of all Site objects in the database.
        /// </summary>
        [TestMethod]
        public void t_Site_Select_All()
        {
            // Backdoor setup
            List<Location_ado> list = DbTestHelper.LoadExtraneousLocations();
            List<Location_ado> siteAdoList = extraSites(list);

            // Exercise the test
            List<Site> siteList = SiteMapper.SelectAll();

            // Validate results
            Assert.AreEqual(siteAdoList.Count(), siteList.Count, "Wrong number of objects in the result list");
            foreach (Location_ado ado in siteAdoList)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(siteList.Exists(x => x.Id.Equals(ado.LocationId)), "Location " + ado.LocationId.ToString() + " is not in the results");
            }
        }

        private static List<Location_ado> extraSites(List<Location_ado> list)
        {
            List<Location_ado> siteAdoList = list.FindAll(n => n.LocationTypeId.Equals(LookupConstants.LocationTypeSite));
            return siteAdoList;
        }

        /// <summary>
        /// Validates that the Selection of a Site by CodeName returns only the expected value.
        /// </summary>
        [TestMethod]
        public void t_Site_Select_ByCodeName()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(new Guid("87606168-3ac7-402a-8ae6-4f6905555581"),
                    "locationName", LookupConstants.LocationTypeSite);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = null;
                iba.AddToLocation_ado(location);
            });
            DbTestHelper.LoadExtraneousLocations();

            // Exercise the test
            Site site = SiteMapper.Select_ByCodeName(location.CodeName);

            validateResults(location, site);
        }


        /// <summary>
        /// Validates that a call to the Select_ByCodeName function throws an error if a null value is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Site_Select_ByCodeName_Null()
        {
            // Exercise the test
            Site site = SiteMapper.Select_ByCodeName(null);
        }


        /// <summary>
        /// Validates that a call to the Select_ByCodeName function throws an error if an empty string is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Site_Select_ByCodeName_EmptyString()
        {
            // Exercise the test
            Site site = SiteMapper.Select_ByCodeName(string.Empty);
        }

        /// <summary>
        /// Validates that a call to the Select_ByCodeName function throws an error if a string of more than 10 characters is passed to it.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Site_Select_ByCodeName_Long()
        {
            // Exercise the test
            Site site = SiteMapper.Select_ByCodeName("01234567890");
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_Site_Delete()
        {
            Location_ado location = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                location = Location_ado.CreateLocation_ado(new Guid("87606168-3ac7-402a-8ae6-4f6905555581"),
                    "locationName", LookupConstants.LocationTypeSite);
                location.CodeName = "abc";
                location.Latitude = 89.3M;
                location.Longitude = 90.10093M;
                location.ParentLocationId = null;
                iba.AddToLocation_ado(location);
            });
            List<Location_ado> locationlist = DbTestHelper.LoadExtraneousLocations();

            SiteMapper.Delete(new Site() { Id = location.LocationId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var siteQuery = from sites in iba.Site_ado select sites;
                Assert.IsNotNull(siteQuery, "Query result is null");
                Assert.AreEqual(extraSites(locationlist).Count(), siteQuery.Count(), "Wrong number of results in query");
                validateExtraLocations(locationlist, siteQuery);
            }
        }

        private static void validateResults(Location_ado location, Site site)
        {
            Assert.AreEqual(location.CodeName, site.CodeName, "CodeName");
            Assert.AreEqual(location.Latitude, site.GeoCoordinate.Latitude.Value, "Latitude");
            Assert.AreEqual(location.LocationId, site.Id, "Id");
            Assert.AreEqual(location.LocationName, site.Name, "name");
            Assert.AreEqual(location.Longitude, site.GeoCoordinate.Longitude.Value, "Longitude");
        }
    }
}
