using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    [TestClass]
    public class tSpeciesMapper : DbTest
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Species");
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_Species_Save_Insert()
        {
            Species Species = new Species()
            {
                Id = TestHelper.TestGuid1,
                AlphaCode = "Alpha",
                CommonName = "Alpha One Two Three",
                ScientificName = "Sillius maximum",
                WarningCount = 12
            };
            SpeciesMapper.Insert(Species);


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SpeciesQuery = from Speciess in iba.Species_ado select Speciess;
                Assert.IsNotNull(SpeciesQuery, "Query result is null");
                Assert.AreEqual(1, SpeciesQuery.Count(), "Wrong number of results in query");
                Species_ado adoSpecies = SpeciesQuery.First();
                validateObjectEquality(Species, adoSpecies);
            }
        }


        private static void validateObjectEquality(Species testObject, Species_ado adoSpecies)
        {
            Assert.IsNotNull(adoSpecies, "There is not Species with the ID to test for");
            Assert.AreEqual(testObject.Id, adoSpecies.SpeciesId, "Id");
            Assert.AreEqual(testObject.AlphaCode, adoSpecies.AlphaCode, "AlphaCode");
            Assert.AreEqual(testObject.CommonName, adoSpecies.CommonName, "CommonName");
            Assert.AreEqual(testObject.ScientificName, adoSpecies.ScientificName, "ScientificName");
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_Species_Save_Update()
        {
            Species_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = Species_ado.CreateSpecies_ado(TestHelper.TestGuid1, "a1", 1, true);
                setupObject.CommonName = "asdf@adfasd";
                setupObject.ScientificName = "(651) 646-8007";
                iba.AddToSpecies_ado(setupObject);
            });
            List<Species_ado> extraList = DbTestHelper.LoadExtraneousSpecies();

            // Setup object to be saved. Change everything except the Id.
            Species testObject = new Species()
            {
                AlphaCode = setupObject.AlphaCode + "a",
                CommonName = setupObject.CommonName + "b",
                ScientificName = setupObject.ScientificName + "c",
                WarningCount = setupObject.WarningCount - 1,
                Id = setupObject.SpeciesId
            };

            // Execute the test
            SpeciesMapper.Update(testObject);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SpeciesQuery = from Speciess in iba.Species_ado select Speciess;
                Assert.IsNotNull(SpeciesQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, SpeciesQuery.Count(), "Wrong number of results in query");
                Species_ado adoSpecies = SpeciesQuery.First(x => x.SpeciesId == TestHelper.TestGuid1);
                validateObjectEquality(testObject, adoSpecies);

                validateExtrapersonAdos(extraList, SpeciesQuery);
            }
        }

        private static void validateExtrapersonAdos(List<Species_ado> extraList, IQueryable<Species_ado> SpeciesQuery)
        {
            foreach (Species_ado extra in extraList)
            {
                Species_ado adoSpecies = SpeciesQuery.First(x => x.SpeciesId == extra.SpeciesId);
                Assert.IsNotNull(adoSpecies, "There is no longer an object with id " + extra.SpeciesId.ToString());
                Assert.AreEqual(extra.AlphaCode, adoSpecies.AlphaCode, "AlphaCode for " + extra.SpeciesId.ToString());
                Assert.AreEqual(extra.CommonName, adoSpecies.CommonName, "CommonName for " + extra.SpeciesId.ToString());
                Assert.AreEqual(extra.ScientificName, adoSpecies.ScientificName, "ScientificName for " + extra.SpeciesId.ToString());
                Assert.AreEqual(extra.WarningCount, adoSpecies.WarningCount, "WarningCount for " + extra.SpeciesId.ToString());
            }
        }

        /// <summary>
        /// Validates selection of a Species object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_Species_Select()
        {
            Species_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = Species_ado.CreateSpecies_ado(TestHelper.TestGuid1, "a1", 1, true);
                setupObject.CommonName = "asdf@adfasd";
                setupObject.ScientificName = "(651) 646-8007";
                iba.AddToSpecies_ado(setupObject);
            });
            List<Species_ado> extraList = DbTestHelper.LoadExtraneousSpecies();

            // Exercise the test
            Species testObject = SpeciesMapper.Select(setupObject.SpeciesId);

            validateObjectEquality(testObject, setupObject);
        }

        /// <summary>
        /// Validates than an exception is thrown if an empty Guid is passed to the main Select function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Species_Select_ByGuid_Empty()
        {
            SpeciesMapper.Select(Guid.Empty);
        }

        /// <summary>
        /// Validates selection of all Species objects in the database.
        /// </summary>
        [TestMethod]
        public void t_Species_Select_All()
        {
            // Backdoor setup
            List<Species_ado> list = DbTestHelper.LoadExtraneousSpecies();

            // Exercise the test
            List<Species> speciesList = SpeciesMapper.SelectAll();

            // Validate results
            Assert.AreEqual(list.Count(), speciesList.Count, "Wrong number of objects in the result list");
            foreach (Species_ado ado in list)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(speciesList.Exists(x => x.Id.Equals(ado.SpeciesId)), "Species_ado " + ado.SpeciesId.ToString() + " is not in the results");
            }
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_Species_Delete()
        {
            Species_ado setupObject = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                setupObject = Species_ado.CreateSpecies_ado(TestHelper.TestGuid1, "a1", 1, true);
                setupObject.CommonName = "asdf@adfasd";
                setupObject.ScientificName = "(651) 646-8007";
                iba.AddToSpecies_ado(setupObject);
            });
            List<Species_ado> personAdolist = DbTestHelper.LoadExtraneousSpecies();

            SpeciesMapper.Delete(new Species() { Id = setupObject.SpeciesId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var SpeciesQuery = from Speciess in iba.Species_ado select Speciess;
                Assert.IsNotNull(SpeciesQuery, "Query result is null");
                Assert.AreEqual(personAdolist.Count(), SpeciesQuery.Count(), "Wrong number of results in query");
                validateExtrapersonAdos(personAdolist, SpeciesQuery);
            }
        }
    }
}
