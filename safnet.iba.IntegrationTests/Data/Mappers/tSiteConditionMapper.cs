using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="ConditionsMapper"/> class.s
    /// </summary>
    [TestClass]
    public class tConditionsMapper : DbTest
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("SiteCondition");
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_Conditions_Save_Insert()
        {
            SiteCondition Conditions = new SiteCondition()
            {
                Id = TestHelper.TestGuid1,
                SiteVisitId = TestHelper.TestParentGuid,
                Sky = 3,
                Temperature = new Business.DataTypes.Temperature() { Value = 54, Units = "C" },
                Wind = 0
            };
            ConditionsMapper.Insert(Conditions);


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var ConditionsQuery = from Conditionss in iba.SiteCondition_ado select Conditionss;
                Assert.IsNotNull(ConditionsQuery, "Query result is null");
                Assert.AreEqual(1, ConditionsQuery.Count(), "Wrong number of results in query");
                SiteCondition_ado adoConditions = ConditionsQuery.First();
                validateObjectEquality(Conditions, adoConditions);
            }
        }


        private static void validateObjectEquality(SiteCondition condition, SiteCondition_ado adoConditions)
        {
            Assert.IsNotNull(adoConditions, "There is not Conditions with the ID to test for");
            Assert.AreEqual(condition.SiteVisitId, adoConditions.SiteVisitId, "SiteVisitId");
            Assert.AreEqual(condition.Sky, adoConditions.Sky, "Sky");
            Assert.AreEqual(condition.Temperature.Value, adoConditions.Temperature, "Temperature");
            Assert.AreEqual(condition.Wind, adoConditions.Wind, "Wind");
            Assert.AreEqual(condition.Temperature.Units, adoConditions.Scale, "Units/scale");
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.
        /// </summary>
        [TestMethod]
        public void t_Conditions_Save_Update()
        {
            SiteCondition_ado conditionAdo = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                conditionAdo = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid1, 32, "F", 2, 3, 
                    TestHelper.TestParentGuid);
            });
            List<SiteCondition_ado> extraList = DbTestHelper.LoadExtraneousConditions();

            // Setup object to be saved. Change everything except the Id.
            SiteCondition conditions = new SiteCondition()
            {
                Id = TestHelper.TestGuid1,
                SiteVisitId = TestHelper.TestGuid1,
                Sky = 2,
                Temperature = new Business.DataTypes.Temperature() { Value = 45, Units = "F" },
                Wind = 2
            };

            // Execute the test
            ConditionsMapper.Update(conditions);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var ConditionsQuery = from Conditionss in iba.SiteCondition_ado select Conditionss;
                Assert.IsNotNull(ConditionsQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, ConditionsQuery.Count(), "Wrong number of results in query");
                SiteCondition_ado adoConditions = ConditionsQuery.First(x => x.ConditionId == TestHelper.TestGuid1);
                validateObjectEquality(conditions, adoConditions);

                // double check the other objects as well, must make sure they remain unchanged.
                foreach (SiteCondition_ado cond in extraList)
                {
                    SiteCondition_ado posttest = ConditionsQuery.First(x => x.ConditionId == cond.ConditionId);
                    Assert.IsNotNull(posttest, cond.ConditionId.ToString() + " is missing from results");
                    Assert.AreEqual(cond.Scale, posttest.Scale, cond.ConditionId.ToString() + " different scale");
                    Assert.AreEqual(cond.SiteVisitId, posttest.SiteVisitId, cond.ConditionId.ToString() + " different SiteVisitId");
                    Assert.AreEqual(cond.Sky, posttest.Sky, cond.ConditionId.ToString() + " different sky");
                    Assert.AreEqual(cond.Temperature, posttest.Temperature, cond.ConditionId.ToString() + " different temperature");
                    Assert.AreEqual(cond.Wind, posttest.Wind, cond.ConditionId.ToString() + " different wind");
                }
            }
        }

        /// <summary>
        /// Validates the Select By Guid function for <see cref="SiteCondition"/>
        /// </summary>
        [TestMethod]
        public void t_Conditions_Select_ByGuid()
        {
            SiteCondition_ado conditionAdo = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid1, 32, 
                "F", 2, 3, TestHelper.TestParentGuid);
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                iba.AddToSiteCondition_ado(conditionAdo);
            });
            DbTestHelper.LoadExtraneousConditions();

            // Exercise the test
            SiteCondition Conditions = ConditionsMapper.Select(conditionAdo.ConditionId);

            validateResults(conditionAdo, Conditions);
        }


        /// <summary>
        /// Validates the Select By SiteVisitId function for <see cref="SiteCondition"/>
        /// </summary>
        [TestMethod]
        public void t_Conditions_Select_BySiteVisitId()
        {
            SiteCondition_ado conditionAdo = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid1, 32, "F", 2, 3, 
                TestHelper.TestParentGuid);
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                iba.AddToSiteCondition_ado(conditionAdo);
            });
            SiteCondition_ado conditionAdo2 = SiteCondition_ado.CreateSiteCondition_ado(TestHelper.TestGuid2, 32, "F", 2, 3,
                TestHelper.TestParentGuid);
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                iba.AddToSiteCondition_ado(conditionAdo2);
            });

            // Exercise the test
            List<SiteCondition> conditions = ConditionsMapper.Select_BySiteVisitId(TestHelper.TestParentGuid);

            SiteCondition cond = conditions.First(x => x.Id == conditionAdo.ConditionId);
            validateResults(conditionAdo, cond);
            cond = conditions.First(x => x.Id == conditionAdo2.ConditionId);
            validateResults(conditionAdo2, cond);
        }

        private static void validateResults(SiteCondition_ado condAdo, SiteCondition conditions)
        {
            Assert.IsNotNull(conditions, "Business object does not exist");
            Assert.AreEqual(condAdo.ConditionId, conditions.Id, "ConditionId");
            Assert.AreEqual(condAdo.Scale, conditions.Temperature.Units, "Units");
            Assert.AreEqual(condAdo.Temperature, conditions.Temperature.Value, "Temperature");
            Assert.AreEqual(condAdo.Wind, conditions.Wind, "Wind");
        }
    }
}
