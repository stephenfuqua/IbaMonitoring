using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Business.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.UnitTests.tBusiness.Factories
{
    [TestClass]
    public class tFiftyMeterDataEntryFactory
    {
        /// <summary>
        /// Validates that no error is thrown, and the List&lt;<see cref="FiftyMeterDataEntry"/>&gt is empty,
        /// when both the first list is empty and the count list is null.
        /// </summary>
        [TestMethod]
        public void t_IncrementPointCounts_NullLists()
        {
            Guid pointSurveyId = TestHelper.TestGuid1;
            List<FiftyMeterDataEntry> entrylist = new List<FiftyMeterDataEntry>();
            FiftyMeterDataEntryFactory.IncrementPointCounts<PointCountBeyond50>(pointSurveyId, entrylist, null);

            Assert.IsNotNull(entrylist, "entrylist is null");
            Assert.AreEqual(0, entrylist.Count(), "entrylist does not contain zero objects");
        }

        /// <summary>
        /// Validates that the function correctly loads up a  List&lt;<see cref="FiftyMeterDataEntry"/>&gt; with entries
        /// "Beyond 50 m" when the input list contains 2 counts of one species and 1 count of another.
        /// </summary>
        [TestMethod]
        public void t_IncrementPointCounts_Beyond50_3Entries()
        {
            Guid pointSurveyId = TestHelper.TestGuid1;
            string comments1 = "Comments";
            string comments2 = string.Empty;
            List<FiftyMeterDataEntry> entryList = new List<FiftyMeterDataEntry>();
            List<PointCountBeyond50> countList = new List<PointCountBeyond50>()
            {
                new PointCountBeyond50() {
                    Comments = comments1,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountBeyond50() {
                    Comments = comments1,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountBeyond50() {
                    Comments = comments2,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_2_CODE
                }
            };


            // Exercising the system under test now...
            FiftyMeterDataEntryFactory.IncrementPointCounts<PointCountBeyond50>(pointSurveyId, entryList, countList);

            // Validations -- the PointCountBeyond50 objects with same species code should be combined into 
            // a single entry, and the third should be a second entry in the list.
            Assert.AreEqual(2, entryList.Count(), "entryList does not contain 2 entries");

            ValidateFiftyMeterDataEntryObject(pointSurveyId, comments1, entryList, "1", 2, 0, TestHelper.SPECIES_1_CODE);
            ValidateFiftyMeterDataEntryObject(pointSurveyId, comments2, entryList, "2", 1, 0, TestHelper.SPECIES_2_CODE);
        }

        /// <summary>
        /// Validates that the function correctly loads up a  List&lt;<see cref="FiftyMeterDataEntry"/>&gt; with entries
        /// "Within 50 m" when the input list contains 2 counts of one species and 1 count of another.
        /// </summary>
        [TestMethod]
        public void t_IncrementPointCounts_Within50_3Entries()
        {
            Guid pointSurveyId = TestHelper.TestGuid1;
            string comments1 = "Comments";
            string comments2 = string.Empty;
            List<FiftyMeterDataEntry> entryList = new List<FiftyMeterDataEntry>();
            List<PointCountWithin50> countList = new List<PointCountWithin50>()
            {
                new PointCountWithin50() {
                    Comments = comments1,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountWithin50() {
                    Comments = comments1,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountWithin50() {
                    Comments = comments2,
                    EventId = pointSurveyId,
                    SpeciesCode = TestHelper.SPECIES_2_CODE
                }
            };


            // Exercising the system under test now...
            FiftyMeterDataEntryFactory.IncrementPointCounts<PointCountWithin50>(pointSurveyId, entryList, countList);

            // Validations -- the PointCountBeyond50 objects with same species code should be combined into 
            // a single entry, and the third should be a second entry in the list.
            Assert.AreEqual(2, entryList.Count(), "entryList does not contain 2 entries");

            ValidateFiftyMeterDataEntryObject(pointSurveyId, comments1, entryList, "1", 0, 2, TestHelper.SPECIES_1_CODE);
            ValidateFiftyMeterDataEntryObject(pointSurveyId, comments2, entryList, "2", 0, 1, TestHelper.SPECIES_2_CODE);
        }

        [TestMethod]
        public void t_CountByPointSurveyId_Within50()
        {
            List<FiftyMeterDataEntry> dataEntryList = new List<FiftyMeterDataEntry>();
            List<PointCountWithin50> fiftyList = new List<PointCountWithin50>()
            {
                // 2 of species 1 at same point
                new PointCountWithin50()
                {
                    EventId = TestHelper.TestGuid1,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountWithin50()
                {
                    EventId = TestHelper.TestGuid1,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                // 1 of species 1 at different point
                new PointCountWithin50()
                {
                    EventId = TestHelper.TestGuid2,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                // 1 of different species at the second point
                new PointCountWithin50()
                {
                    EventId = TestHelper.TestGuid2,
                    SpeciesCode= TestHelper.SPECIES_2_CODE
                }
            };

            // Exercise the system under test
            FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountWithin50>(fiftyList, dataEntryList);

            // There should be three entries:
            // 1. Species 1 at point 1 with count 2
            // 2. Species 1 at point 2 with count 1
            // 3. Species 2 at point 2 with count 1
            Assert.AreEqual(3, dataEntryList.Count(), "wrong number of results");
            FiftyMeterDataEntry entry1 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid1)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_1_CODE));
            Assert.IsNotNull(entry1, "entry 1 missing");
            Assert.AreEqual(2, entry1.CountWithin50, "entry 1 wrong count");

            FiftyMeterDataEntry entry2 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid2)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_1_CODE));
            Assert.IsNotNull(entry2, "entry 1 missing");
            Assert.AreEqual(1, entry2.CountWithin50, "entry 2 wrong count");

            FiftyMeterDataEntry entry3 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid2)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_2_CODE));
            Assert.IsNotNull(entry3, "entry 1 missing");
            Assert.AreEqual(1, entry3.CountWithin50, "entry 3 wrong count");
        }


        [TestMethod]
        public void t_CountByPointSurveyId_Beyond50()
        {
            List<FiftyMeterDataEntry> dataEntryList = new List<FiftyMeterDataEntry>();
            List<PointCountBeyond50> fiftyList = new List<PointCountBeyond50>()
            {
                // 2 of species 1 at same point
                new PointCountBeyond50()
                {
                    EventId = TestHelper.TestGuid1,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                new PointCountBeyond50()
                {
                    EventId = TestHelper.TestGuid1,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                // 1 of species 1 at different point
                new PointCountBeyond50()
                {
                    EventId = TestHelper.TestGuid2,
                    SpeciesCode = TestHelper.SPECIES_1_CODE
                },
                // 1 of different species at the second point
                new PointCountBeyond50()
                {
                    EventId = TestHelper.TestGuid2,
                    SpeciesCode= TestHelper.SPECIES_2_CODE
                }
            };

            // Exercise the system under test
            FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountBeyond50>(fiftyList, dataEntryList);

            // There should be three entries:
            // 1. Species 1 at point 1 with count 2
            // 2. Species 1 at point 2 with count 1
            // 3. Species 2 at point 2 with count 1
            Assert.AreEqual(3, dataEntryList.Count(), "wrong number of results");
            FiftyMeterDataEntry entry1 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid1)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_1_CODE));
            Assert.IsNotNull(entry1, "entry 1 missing");
            Assert.AreEqual(2, entry1.CountBeyond50, "entry 1 wrong count");

            FiftyMeterDataEntry entry2 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid2)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_1_CODE));
            Assert.IsNotNull(entry2, "entry 1 missing");
            Assert.AreEqual(1, entry2.CountBeyond50, "entry 2 wrong count");

            FiftyMeterDataEntry entry3 = dataEntryList.Single(x => x.PointSurveyId.Equals(TestHelper.TestGuid2)
                   && x.SpeciesCode.Equals(TestHelper.SPECIES_2_CODE));
            Assert.IsNotNull(entry3, "entry 1 missing");
            Assert.AreEqual(1, entry3.CountBeyond50, "entry 3 wrong count");
        }

        public static void ValidateFiftyMeterDataEntryObject(Guid pointSurveyId, string comments1, List<FiftyMeterDataEntry> entryList, string whichSpecies, int expectedCount, int expectedCountWithin50, string speciesCode)
        {
            FiftyMeterDataEntry species1 = entryList.Single(x => x.SpeciesCode.Equals(speciesCode) && x.PointSurveyId.Equals(pointSurveyId));
            Assert.IsNotNull(species1, "species " + whichSpecies + " isn't in the list");
            Assert.AreEqual(expectedCount, species1.CountBeyond50, "Count beyond 50 is wrong for species " + whichSpecies);
            Assert.AreEqual(expectedCountWithin50, species1.CountWithin50, "count within 50 is wrong for species " + whichSpecies);
            Assert.AreEqual(comments1, species1.Comments, "wrong count for species " + whichSpecies);
            Assert.AreEqual(pointSurveyId, species1.PointSurveyId, "wrong point survey id for species " + whichSpecies);
        }

    }


}
