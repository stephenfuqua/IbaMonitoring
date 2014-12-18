using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tSpecies and is intended
    ///to contain all tSpecies Unit Tests
    ///</summary>
    [TestClass()]
    public class tSpecies
    {
        /// <summary>
        ///A test for Species Constructor
        ///</summary>
        [TestMethod()]
        public void t_SpeciesConstructor()
        {
            Species target = new Species();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreateNewSpecies
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewSpecies()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Species actual= Species.CreateNewSpecies();
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
        }

        /// <summary>
        ///A test for AlphaCode
        ///</summary>
        [TestMethod()]
        public void t_AlphaCode()
        {
            Species target = new Species();
            string expected = "ABDE";
            string actual;
            target.AlphaCode = expected;
            actual = target.AlphaCode;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CommonName
        ///</summary>
        [TestMethod()]
        public void t_CommonName()
        {
            Species target = new Species();
            string expected = "Red-bellied Woodpecker";

            string actual;
            target.CommonName = expected;
            actual = target.CommonName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ScientificName
        ///</summary>
        [TestMethod()]
        public void t_ScientificName()
        {
            Species target = new Species();
            string expected = "Genus species";
            string actual;
            target.ScientificName = expected;
            actual = target.ScientificName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for WarningCount
        ///</summary>
        [TestMethod()]
        public void t_WarningCount()
        {
            Species target = new Species();
            int expected = 13; 
            int actual;
            target.WarningCount = expected;
            actual = target.WarningCount;
            Assert.AreEqual(expected, actual);
        }
    }
}
