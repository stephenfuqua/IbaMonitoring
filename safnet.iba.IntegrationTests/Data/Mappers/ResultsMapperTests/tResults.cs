using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.AppFacades;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers.ResultsMapperTests
{
    [TestClass]
    public class tResults
    {

        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Observation");
            DbTestHelper.ClearTable("PointSurvey");
            DbTestHelper.ClearTable("SiteVisit");
            DbTestHelper.ClearTable("Location");
            DbTestHelper.LoadSpecies();
        }

        /// <summary>
        /// Validates results, including sorting, of stored procedure "Results_AvailableYears".
        /// </summary>
        [TestMethod]
        public void t_GetAvailableYears()
        {
            // Load some SiteVisit data, including a few duplicate years (ensuring only distinct values)
            SiteVisit_ado sv1 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid1, TestHelper.TestGuid1, new DateTime(1992, 12,31));
            SiteVisit_ado sv2 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid2, TestHelper.TestGuid1, new DateTime(1992,1,1));
            SiteVisit_ado sv3 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid3, TestHelper.TestGuid1, new DateTime(2008, 2, 29));
            SiteVisit_ado sv4 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid4, TestHelper.TestGuid1, new DateTime(2008, 2, 28));
            SiteVisit_ado sv5 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid5, TestHelper.TestGuid1, new DateTime(2009, 2, 22));
            SiteVisit_ado sv6 = DbTestHelper.LoadSingleSiteVisit(TestHelper.TestGuid6, TestHelper.TestGuid1, new DateTime(2908, 2, 20));
            
            // Expected years are 1992, 2008, 2009, 2908 - in that order
            SortedSet<int> set = ResultsFacade.GetAvailableYears();
            IEnumerator<int> enumerator = set.GetEnumerator();
            enumerator.MoveNext();

            Assert.AreEqual(4, set.Count, "wrong count");
            Assert.AreEqual(1992, enumerator.Current, "missing 1992");
            enumerator.MoveNext();
            Assert.AreEqual(2008, enumerator.Current, "missing 2008");
            enumerator.MoveNext();
            Assert.AreEqual(2009, enumerator.Current, "missing 2009");
            enumerator.MoveNext();
            Assert.AreEqual(2908, enumerator.Current, "missing 2908");
        }
    }
}
