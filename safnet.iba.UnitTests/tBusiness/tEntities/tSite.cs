using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.DataTypes;
using System.Collections.Generic;

namespace safnet.iba.UnitTest
{


    /// <summary>
    ///This is a test class for tSite and is intended
    ///to contain all tSite Unit Tests
    ///</summary>
    [TestClass()]
    public class tSite
    {
        /// <summary>
        ///A test for Site Constructor
        ///</summary>
        [TestMethod()]
        public void t_SiteConstructor()
        {
            Site target = new Site();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddSamplingPoint
        ///</summary>
        [TestMethod()]
        public void t_AddSamplingPoint()
        {
            Site target = new Site();
            target.Id = LookupConstants.LocationTypePoint;
            SamplingPoint point = new SamplingPoint() { Name = "a name" };

            target.AddSamplingPoint(point);

            Assert.IsTrue(target.SamplingPoints.Contains(point), "Does not contain point");
            Assert.AreEqual(LookupConstants.LocationTypePoint, point.SiteId, "SiteId not assigned to point");
        }

        /// <summary>
        ///A test for CreateNewSite
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreateNewSite()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            string name = "site name";
            string codeName = "code";

            Site actual = Site.CreateNewSite(name, codeName);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
            Assert.AreEqual(name, actual.Name, "Name not assigned");
            Assert.AreEqual(codeName, actual.CodeName, "CodeName not assigned");
        }

        /// <summary>
        ///A test for Boundaries
        ///</summary>
        [TestMethod()]
        [DeploymentItem("safnet.iba.dll")]
        public void t_Boundaries()
        {
            Site_Accessor target = new Site_Accessor();
            Queue<Coordinate> expected = new Queue<Coordinate>();
            expected.Enqueue(new Coordinate());

            Queue<Coordinate> actual;
            target.Boundaries = expected;
            actual = target.Boundaries;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for CodeName
        ///</summary>
        [TestMethod()]
        public void t_CodeName()
        {
            Site target = new Site();
            string expected = "Code naee";

            string actual;
            target.CodeName = expected;
            actual = target.CodeName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SamplingPoints
        ///</summary>
        [TestMethod()]
        [DeploymentItem("safnet.iba.dll")]
        public void t_SamplingPoints()
        {
            Site_Accessor target = new Site_Accessor();
            List<SamplingPoint> expected = new List<SamplingPoint>() { new SamplingPoint() { Name = "sample" } };

            List<SamplingPoint> actual;
            target.SamplingPoints = expected;
            actual = target.SamplingPoints;
            Assert.AreEqual(expected, actual);
        }
    }
}
