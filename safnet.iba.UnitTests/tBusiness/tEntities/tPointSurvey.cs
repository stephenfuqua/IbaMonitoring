using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTest.tBusiness.tEntities
{
    [TestClass]
    public class tPointSurvey
    {
        /// <summary>
        /// Validates that the correct text is returned for noise code 0
        /// </summary>
        [TestMethod]
        public void tPointSurvey_NoiseCode_0()
        {
            FiftyMeterPointSurvey sut = new FiftyMeterPointSurvey() { NoiseCode = 0 };
            string text = sut.NoiseCodeText;
            Assert.AreEqual("No background noise", text);
        }

        /// <summary>
        /// Validates that the correct text is returned for noise code 1
        /// </summary>
        [TestMethod]
        public void tPointSurvey_NoiseCode_1()
        {
            FiftyMeterPointSurvey sut = new FiftyMeterPointSurvey() { NoiseCode = 1 };
            string text = sut.NoiseCodeText;
            Assert.AreEqual("Barely reduces hearing", text);
        }

        /// <summary>
        /// Validates that the correct text is returned for noise code 2
        /// </summary>
        [TestMethod]
        public void tPointSurvey_NoiseCode_2()
        {
            FiftyMeterPointSurvey sut = new FiftyMeterPointSurvey() { NoiseCode = 2 };
            string text = sut.NoiseCodeText;
            Assert.AreEqual("Noticeable reduction of hearing", text);
        }

        /// <summary>
        /// Validates that the correct text is returned for noise code 3
        /// </summary>
        [TestMethod]
        public void tPointSurvey_NoiseCode_3()
        {
            FiftyMeterPointSurvey sut = new FiftyMeterPointSurvey() { NoiseCode = 3 };
            string text = sut.NoiseCodeText;
            Assert.AreEqual("Prohibitive (greatly reduces hearing)", text);
        }

        /// <summary>
        /// Tests the CreateNewPointSurvey method of <see cref="PointSurvey"/>.
        /// </summary>
        [TestMethod]
        [HostType("Moles")]
        public void t_PointSurvey_CreateNewPointSurvey()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Guid pointId = LookupConstants.LocationTypeSite;

            FiftyMeterPointSurvey actual = FiftyMeterPointSurvey.CreateNewPointSurvey(pointId);
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
            Assert.AreEqual(pointId, actual.LocationId, "LocationId not assigned");
        }

        /// <summary>
        /// Tests the <see cref="PointSurvey"/> constructor.
        /// </summary>
        [TestMethod]
        public void t_PointSurvey_Constructor()
        {
            FiftyMeterPointSurvey survey = new FiftyMeterPointSurvey();
            Assert.IsNotNull(survey);
        }

    }
}
