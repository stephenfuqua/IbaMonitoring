using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.UnitTests.TestSpecificSubclasses;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.UnitTests.tBusiness.tEntities
{
    /// <summary>
    /// Validates functionality of the <see cref="FiftyMeterDataEntry"/> class.
    /// </summary>
    [TestClass]
    public class tFiftyMeterDataEntry
    {
        /// <summary>
        ///A test for PointCountBeyond50 Constructor
        ///</summary>
        [TestMethod()]
        public void t_FiftyMeterDataEntry()
        {
            FiftyMeterDataEntry target = new FiftyMeterDataEntry();
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// Validates that a new <see cref="PointCountWithin50"/> object is added to the list when the count is greater than the number of elements in the list.
        /// </summary>
        [TestMethod]
        public void t_UpdateCountAndList_Beyond50_Increment()
        {
            FiftyMeterDataEntry entry = new FiftyMeterDataEntry()
            {
                Comments = "Comments",
                CountBeyond50 = 0,
                PointSurveyId = TestHelper.TestGuid1,
                SpeciesCode = TestHelper.SPECIES_1_CODE
            };
            List<PointCountBeyond50> beyond50List = new List<PointCountBeyond50>();
            int count = 1;

            FiftyMeterDataEntryTss.UpdateCountAndList<PointCountBeyond50>(beyond50List, count, entry);

            Assert.AreEqual(1, beyond50List.Count(), "Does not contain 1 object");
            Assert.AreEqual(entry.Comments, beyond50List[0].Comments, "Comments");
            Assert.AreEqual(entry.PointSurveyId, beyond50List[0].EventId, "Point Survey ID");
            Assert.AreEqual(entry.SpeciesCode, beyond50List[0].SpeciesCode, "SpeciesCode");
            Assert.IsFalse(beyond50List[0].MarkForDeletion, "MarkFordeletion");
        }

        /// <summary>
        /// Validates that a new <see cref="PointCountWithin50"/> object is added to the list when the count is greater than the number of elements in the list.
        /// </summary>
        [TestMethod]
        public void t_UpdateCountAndList_Less50_Increment()
        {
            FiftyMeterDataEntry entry = new FiftyMeterDataEntry()
            {
                Comments = "Comments",
                CountWithin50 = 0,
                PointSurveyId = TestHelper.TestGuid1,
                SpeciesCode = TestHelper.SPECIES_1_CODE
            };
            List<PointCountWithin50> less50List = new List<PointCountWithin50>();
            int count = 1;

            FiftyMeterDataEntryTss.UpdateCountAndList<PointCountWithin50>(less50List, count, entry);

            Assert.AreEqual(1, less50List.Count(), "Does not contain 1 object");
            Assert.AreEqual(entry.Comments, less50List[0].Comments, "Comments");
            Assert.AreEqual(entry.PointSurveyId, less50List[0].EventId, "Point Survey ID");
            Assert.AreEqual(entry.SpeciesCode, less50List[0].SpeciesCode, "SpeciesCode");
            Assert.IsFalse(less50List[0].MarkForDeletion, "MarkFordeletion");
        }


        /// <summary>
        /// Validates that an <see cref="PointCountBeyond50"/> object is marked for deletion when the count is less than the number of elements in the list.
        /// </summary>
        [TestMethod]
        public void t_UpdateCountAndList_Beyond50_Decrement()
        {
            FiftyMeterDataEntry entry = new FiftyMeterDataEntry()
            {
                Comments = "Comments",
                CountBeyond50 = 0,
                PointSurveyId = TestHelper.TestGuid1,
                SpeciesCode = TestHelper.SPECIES_1_CODE
            };
            List<PointCountBeyond50> beyond50List = new List<PointCountBeyond50>()
                {
                    new PointCountBeyond50() {
                        Comments  = entry.Comments,
                        EventId = entry.PointSurveyId,
                        MarkForDeletion = false,
                        SpeciesCode = entry.SpeciesCode
                    }
                };
            int count = 0;

            FiftyMeterDataEntryTss.UpdateCountAndList<PointCountBeyond50>(beyond50List, count, entry);

            Assert.AreEqual(1, beyond50List.Count(), "Does not contain 1 object");
            Assert.AreEqual(entry.Comments, beyond50List[0].Comments, "Comments");
            Assert.AreEqual(entry.PointSurveyId, beyond50List[0].EventId, "Point Survey ID");
            Assert.AreEqual(entry.SpeciesCode, beyond50List[0].SpeciesCode, "SpeciesCode");
            // Now it _should_ be marked for deletion
            Assert.IsTrue(beyond50List[0].MarkForDeletion, "MarkFordeletion");
        }

        /// <summary>
        /// Validates that an <see cref="PointCountWithin50"/> object is marked for deletion when the count is less than the number of elements in the list.
        /// </summary>
        [TestMethod]
        public void t_UpdateCountAndList_Less50_Decrement()
        {
            FiftyMeterDataEntry entry = new FiftyMeterDataEntry()
            {
                Comments = "Comments",
                CountWithin50 = 0,
                PointSurveyId = TestHelper.TestGuid1,
                SpeciesCode = TestHelper.SPECIES_1_CODE
            };
            List<PointCountWithin50> less50List = new List<PointCountWithin50>()
                {
                    new PointCountWithin50() {
                        Comments  = entry.Comments,
                        EventId = entry.PointSurveyId,
                        MarkForDeletion = false,
                        SpeciesCode = entry.SpeciesCode
                    }
                };
            int count = 0;

            FiftyMeterDataEntryTss.UpdateCountAndList<PointCountWithin50>(less50List, count, entry);

            Assert.AreEqual(1, less50List.Count(), "Does not contain 1 object");
            Assert.AreEqual(entry.Comments, less50List[0].Comments, "Comments");
            Assert.AreEqual(entry.PointSurveyId, less50List[0].EventId, "Point Survey ID");
            Assert.AreEqual(entry.SpeciesCode, less50List[0].SpeciesCode, "SpeciesCode");
            // Now it _should_ be marked for deletion
            Assert.IsTrue(less50List[0].MarkForDeletion, "MarkFordeletion");
        }
    }
}
