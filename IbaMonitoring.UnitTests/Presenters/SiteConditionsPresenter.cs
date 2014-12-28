using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;
using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using safnet.iba.TestHelpers;
using safnet.iba.Business.DataTypes;

namespace IbaMonitoring.UnitTests.Presenters
{
    [TestClass]
    public class SiteConditionsPresenterTests : BaseMocker
    {

        
        private Mock<ISiteConditionsView> _viewMock;
        private Mock<ISiteConditionsFacade> _facadeMock;

        protected override void TestInitialize()
        {
            _viewMock = MoqRepository.Create<ISiteConditionsView>();
            _facadeMock = MoqRepository.Create<ISiteConditionsFacade>();
        }

        [TestMethod]
        public void ConstructorHappyPath()
        {
            GivenTheSystemUnderTest();
            ThenThereIsNothingToValidate();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorRejectsNullArgumentOne()
        {
            new SiteConditionsPresenter(null, _viewMock.Object, _facadeMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorRejectsNullArgumentTwo()
        {
            new SiteConditionsPresenter(UserStateMock.Object, null, _facadeMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorRejectsNullArgumentThree()
        {
            new SiteConditionsPresenter(UserStateMock.Object, _viewMock.Object, null);
        }


        [TestMethod]
        public void InitialSaveOfConditionsWhenAllParametersSetCorrectly()
        {
            var locationid = Guid.Empty;
            var observerId = Guid.Empty;
            var recorderId = Guid.Empty;
            var startSky = (byte) 0;
            var startTempUnit = "";
            var startTemp = 0;
            var startWind = (byte) 0;
            var startDateTime = DateTime.MinValue;
            var endSky = (byte) 0;
            var endTempUnit = "";
            var endTemp = 0;
            var endWind = (byte) 0;
            var endDateTime = DateTime.MinValue.AddMilliseconds(234);
            var siteVisitId = Guid.Empty;
            var startConditionId = Guid.Empty;
            var endConditionId = Guid.Empty;

            GivenSiteVisitStoredInUserSession(endConditionId, siteVisitId, endSky, endTempUnit, endTemp, endWind, endDateTime, locationid, observerId, recorderId, startDateTime, startConditionId, startSky, startTempUnit, startTemp);

            var expectedLocationid = BaseMocker.TEST_GUID_1;
            var expectedObserverId = BaseMocker.TEST_GUID_2;
            var expectedRecorderId = BaseMocker.TEST_GUID_3;
            var expectedStartSky = (byte)5;
            var expectedStartTempUnit = "F";
            var expectedStartTemp = 23;
            var expectedStartWind = (byte)1;
            var expectedStartDateTime = new DateTime(2014,12,25,5,30,0);
            var expectedEndSky = (byte)2;
            var expectedEndTempUnit = "C";
            var expectedEndTemp = -5;
            var expectedEndWind = (byte)3;
            var expectedEndDateTime = new DateTime(2014, 12, 25, 7, 30, 0);
            //var expectedSiteVisitId = BaseMocker.TEST_GUID_4;
            //var expectedStartConditionId = BaseMocker.TEST_GUID_5;
            //var expectedEndConditionId = BaseMocker.TEST_GUID_6;

            GivenUserFormSubmission(expectedEndSky.ToString(), expectedEndTemp.ToString(), expectedEndTempUnit.ToString(), expectedEndDateTime.ToString("hh:mm:ss"), expectedEndWind.ToString(), expectedLocationid.ToString(), expectedObserverId.ToString(), expectedRecorderId.ToString(), expectedStartSky.ToString(), expectedStartTemp.ToString(), expectedStartTempUnit.ToString(), expectedStartDateTime.ToString("hh:mm:ss"), expectedStartDateTime.ToString("yyyy-MM-dd"), expectedStartWind.ToString());


            var modifiedSiteVisit = new SiteVisit();
            ExpectToSendTheseConditionsToTheSiteConditionsFacade(expectedLocationid, expectedEndSky, expectedEndTemp, expectedEndTempUnit, expectedEndDateTime, expectedEndWind, expectedObserverId, expectedRecorderId, expectedStartSky, expectedStartTemp, expectedStartTempUnit, expectedStartDateTime, expectedStartWind, modifiedSiteVisit);
            ExpectToSaveSiteVisitBackIntoSession(modifiedSiteVisit);

            WhenTheUserSavesTheSiteVisitConditions();

            ThenThereIsNothingToValidate();
        }

        private void ExpectToSaveSiteVisitBackIntoSession(SiteVisit modifiedSiteVisit)
        {
            UserStateMock.SetupSet(
                x => x.SiteVisit = It.Is<SiteVisit>(y => object.ReferenceEquals(y, modifiedSiteVisit)));
        }

        private void ExpectToSendTheseConditionsToTheSiteConditionsFacade(Guid locationid, byte endSky, int endTemp, string endTempUnit, DateTime endDateTime, byte endWind, Guid observerId, Guid recorderId, byte startSky, int startTemp, string startTempUnit, DateTime startDateTime, byte startWind, SiteVisit returnValue)
        {
            _facadeMock.Setup(x => x.SaveSiteConditions(It.IsAny<SiteVisit>()))
                .Callback((SiteVisit actual) =>
                {
                    Assert.IsNull(actual.Comments, "Comments");
                    Assert.AreEqual(actual.Id, actual.EndConditions.SiteVisitId, "EndConditions.SiteVisitId");
                    Assert.AreEqual(endSky, actual.EndConditions.Sky, "EndConditions.Sky");
                    Assert.AreNotEqual(Guid.Empty, actual.EndConditions.Id, "EndConditions.Id");
                    Assert.AreEqual(endTempUnit, actual.EndConditions.Temperature.Units, "EndConditions.Temperature.Units");
                    Assert.AreEqual(endTemp, actual.EndConditions.Temperature.Value, "EndConditions.Temperature.Value");
                    Assert.AreEqual(endDateTime , actual.EndTimeStamp, "EndTimeStamp");
                    Assert.AreEqual(0, actual.FlattenedDataEntryList.Count, "FlattenedDataEntryList");
                    Assert.AreNotEqual(Guid.Empty, actual.Id, "Id");
                    Assert.IsFalse(actual.IsDataEntryComplete, "IsDataEntryComplete");
                    Assert.AreEqual(locationid, actual.LocationId, "LocationId");
                    Assert.IsTrue(actual.NeedsDatabaseRefresh, "NeedsDatabaseRefresh");
                    Assert.AreEqual(observerId, actual.ObserverId, "ObserverId");
                    Assert.AreEqual(0, actual.PointSurveys.Count, "PointSurveys");
                    Assert.AreEqual(recorderId, actual.RecorderId, "RecorderId");
                    Assert.AreEqual(actual.Id, actual.StartConditions.SiteVisitId, "StartConditions.SiteVisitId");
                    Assert.AreNotEqual(Guid.Empty, actual.StartConditions.Id, "StartConditions.Id");
                    Assert.AreEqual(startSky, actual.StartConditions.Sky, "StartConditions.Sky");
                    Assert.AreEqual(startTempUnit, actual.StartConditions.Temperature.Units, "StartConditions.Temperature.Units");
                    Assert.AreEqual(startTemp, actual.StartConditions.Temperature.Value, "StartConditions.Temperature.Value");
                    Assert.AreEqual(startDateTime, actual.StartTimeStamp, "StartTimeStamp");
                    Assert.AreEqual(0, actual.SupplementalObservations.Count, "SupplementalObservations");
                    Assert.AreEqual(52, actual.WeekNumber, "WeekNumber");
                })
                .Returns(returnValue);
        }

        private void WhenTheUserSavesTheSiteVisitConditions()
        {
            GivenTheSystemUnderTest().SaveConditions();
        }

        private void GivenUserFormSubmission(string expectedEndSky, string expectedEndTemp, string expectedEndTempUnit,
            string expectedEndDateTime, string expectedEndWind, string expectedLocationid, string expectedObserverId,
            string expectedRecorderId, string expectedStartSky, string expectedStartTemp, string expectedStartTempUnit,
            string expectedStartTime, string expectedStartDate, string expectedStartWind)
        {
            _viewMock.SetupGet(x => x.EndSkyAccessor).Returns(expectedEndSky);
            _viewMock.SetupGet(x => x.EndTempAccessor).Returns(expectedEndTemp);
            _viewMock.SetupGet(x => x.EndTempUnitsAccessor).Returns(expectedEndTempUnit);
            _viewMock.SetupGet(x => x.EndTimeAccessor).Returns(expectedEndDateTime);
            _viewMock.SetupGet(x => x.EndWindAccessor).Returns(expectedEndWind);
            _viewMock.SetupGet(x => x.SiteVisitedAccessor).Returns(expectedLocationid);
            _viewMock.SetupGet(x => x.SiteVisitObserverAccessor).Returns(expectedObserverId);
            _viewMock.SetupGet(x => x.SiteVisitRecorderAccessor).Returns(expectedRecorderId);
            _viewMock.SetupGet(x => x.StartSkyAccessor).Returns(expectedStartSky);
            _viewMock.SetupGet(x => x.StartTempAccessor).Returns(expectedStartTemp);
            _viewMock.SetupGet(x => x.StartTempUnitsAccessor).Returns(expectedStartTempUnit);
            _viewMock.SetupGet(x => x.StartTimeAccessor).Returns(expectedStartTime);
            _viewMock.SetupGet(x => x.StartWindAccessor).Returns(expectedStartWind);
            _viewMock.SetupGet(x => x.VisitDateAccessor).Returns(expectedStartDate);
        }

        private void GivenSiteVisitStoredInUserSession(Guid endConditionId, Guid siteVisitId, byte endSky, string endTempUnit,
            int endTemp, byte endWind, DateTime endDateTime, Guid locationid, Guid observerId, Guid recorderId,
            DateTime startDateTime, Guid startConditionId, byte startSky, string startTempUnit, int startTemp)
        {
            var siteVisit = new SiteVisit
            {
                EndConditions = new SiteCondition
                {
                    Id = endConditionId, 
                    SiteVisitId = siteVisitId,
                    Sky = endSky,
                    Temperature = new Temperature {Units = endTempUnit, Value = endTemp},
                    Wind = endWind
                },
                EndTimeStamp = endDateTime,
                Id = siteVisitId,
                LocationId = locationid,
                ObserverId = observerId,
                RecorderId = recorderId,
                StartTimeStamp = startDateTime,
                StartConditions = new SiteCondition
                {
                    Id = startConditionId,
                    SiteVisitId = siteVisitId,
                    Sky = startSky,
                    Temperature = new Temperature {Units = startTempUnit, Value = startTemp}
                }
            };

            UserStateMock.SetupGet(x => x.SiteVisit).Returns(siteVisit);
        }


        private SiteConditionsPresenter GivenTheSystemUnderTest()
        {
            return new SiteConditionsPresenter(UserStateMock.Object, _viewMock.Object, _facadeMock.Object);
        }
    }
}
