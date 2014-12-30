using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.TestHelpers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using safnet.iba.Adapters;

namespace IbaMonitoring.UnitTests.Presenters
{
    [ExcludeFromCodeCoverage]
    public class SiteConditionsPresenterTss : SiteConditionsPresenter
    {
        public SiteConditionsPresenterTss(ISiteConditionsFacade facade)
            : base(facade)
        {
        }

        public SiteConditionsPresenterTss SetUserState(IUserStateManager userState)
        {
            base.UserState = userState;
            return this;
        }


        public SiteConditionsPresenterTss SetGlobalMap(IGlobalMap globalMap)
        {
            base.GlobalMap = globalMap;
            return this;
        }

        public SiteConditionsPresenterTss SetHttpResponse(HttpResponseBase httpResponse)
        {
            base.HttpResponse = httpResponse;
            return this;
        }

        public SiteConditionsPresenterTss SetHttpRequest(HttpRequestBase httpRequest)
        {
            base.HttpRequest = httpRequest;
            return this;
        }
    }

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SiteConditionsPresenterTests : BaseMocker
    {
        private Mock<ISiteConditionsView> _viewMock;
        private Mock<ISiteConditionsFacade> _facadeMock;
        private Mock<HttpResponseBase> _mockResponse;


        protected override void TestInitialize()
        {
            _viewMock = MoqRepository.Create<ISiteConditionsView>();
            _facadeMock = MoqRepository.Create<ISiteConditionsFacade>();
            _mockResponse = MoqRepository.Create<HttpResponseBase>();
        }

        [TestMethod]
        public void ConstructorHappyPath()
        {
            GivenTheSystemUnderTest();
            ThenThereIsNothingToValidate();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorRejectsNullArgumentOne()
        {
            new SiteConditionsPresenter(null);
        }


        [TestMethod]
        public void InitialSaveOfConditionsWhenAllParametersSetCorrectly()
        {

            GivenThereIsNoSiteVisitInSession();

            var expectedLocationid = BaseMocker.TEST_GUID_1;
            var expectedObserverId = BaseMocker.TEST_GUID_2;
            var expectedRecorderId = BaseMocker.TEST_GUID_3;
            var expectedStartSky = (byte)5;
            var expectedStartTempUnit = "F";
            var expectedStartTemp = 23;
            var expectedStartWind = (byte)1;
            var expectedStartDateTime = new DateTime(2014, 12, 25, 5, 30, 0);
            var expectedEndSky = (byte)2;
            var expectedEndTempUnit = "C";
            var expectedEndTemp = -5;
            var expectedEndWind = (byte)3;
            var expectedEndDateTime = new DateTime(2014, 12, 25, 7, 30, 0);

            GivenUserFormSubmission(expectedEndSky.ToString(), expectedEndTemp.ToString(), expectedEndTempUnit.ToString(), expectedEndDateTime.ToString("hh:mm:ss"), expectedEndWind.ToString(), expectedLocationid.ToString(), expectedObserverId.ToString(), expectedRecorderId.ToString(), expectedStartSky.ToString(), expectedStartTemp.ToString(), expectedStartTempUnit.ToString(), expectedStartDateTime.ToString("hh:mm:ss"), expectedStartDateTime.ToString("yyyy-MM-dd"), expectedStartWind.ToString());


            ExpectToSendTheseConditionsToTheSiteConditionsFacade(expectedLocationid, expectedEndSky, expectedEndTemp, expectedEndTempUnit, expectedEndDateTime, expectedEndWind, expectedObserverId, expectedRecorderId, expectedStartSky, expectedStartTemp, expectedStartTempUnit, expectedStartDateTime, expectedStartWind);
            ExpectToSaveSiteVisitBackIntoSession();

            var expectedPage = "PointCounts.aspx";
            ExpectToBeRedirectedTo(expectedPage);

            GivenTheDataAreValid(true);

            WhenTheUserSavesTheSiteVisitConditions();

            ThenThereIsNothingToValidate();
        }

        private void GivenThereIsNoSiteVisitInSession()
        {
            UserStateMock.SetupGet(x => x.SiteVisit)
                .Returns(null as SiteVisit);
        }


        [TestMethod]
        public void StayOnThePageWhenThereAreValidationErrors()
        {
            GivenTheDataAreValid(false);

            WhenTheUserSavesTheSiteVisitConditions();

            ThenThereIsNothingToValidate();
        }

        private void GivenTheDataAreValid(bool b)
        {
            _viewMock.SetupGet(x => x.IsValid).Returns(b);
        }

        private void ExpectToBeRedirectedTo(string expectedPage)
        {
            _mockResponse.Setup(x => x.Redirect(It.Is<string>(y => y == expectedPage), It.Is<bool>(y => y == true)));
        }

        private void ExpectToSaveSiteVisitBackIntoSession()
        {
            UserStateMock.SetupSet(
                x => x.SiteVisit = It.IsAny<SiteVisit>());
        }

        private void ExpectToSendTheseConditionsToTheSiteConditionsFacade(Guid locationid, byte endSky, int endTemp, string endTempUnit, DateTime endDateTime, byte endWind, 
            Guid observerId, Guid recorderId, byte startSky, int startTemp, string startTempUnit, DateTime startDateTime, byte startWind)
        {
            _facadeMock.Setup(x => x.SaveSiteConditions(It.IsAny<SiteVisit>()))
                .Callback((SiteVisit actual) =>
                {
                    Assert.IsNull(actual.Comments, "Comments");
                    Assert.AreEqual(actual.Id, actual.EndConditions.SiteVisitId, "EndConditions.SiteVisitId");
                    Assert.AreEqual(endSky, actual.EndConditions.Sky, "EndConditions.Sky");
                    Assert.AreNotEqual(Guid.Empty, actual.EndConditions.Id, "EndConditions.Id");
                    Assert.AreEqual(endWind, actual.EndConditions.Wind, "EndConditions.Wind");
                    Assert.AreEqual(endTempUnit, actual.EndConditions.Temperature.Units,
                        "EndConditions.Temperature.Units");
                    Assert.AreEqual(endTemp, actual.EndConditions.Temperature.Value, "EndConditions.Temperature.Value");
                    Assert.AreEqual(endDateTime, actual.EndTimeStamp, "EndTimeStamp");
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
                    Assert.AreEqual(startWind, actual.StartConditions.Wind, "StartConditions.Wind");
                    Assert.AreEqual(startTempUnit, actual.StartConditions.Temperature.Units,
                        "StartConditions.Temperature.Units");
                    Assert.AreEqual(startTemp, actual.StartConditions.Temperature.Value,
                        "StartConditions.Temperature.Value");
                    Assert.AreEqual(startDateTime, actual.StartTimeStamp, "StartTimeStamp");
                    Assert.AreEqual(0, actual.SupplementalObservations.Count, "SupplementalObservations");
                    Assert.AreEqual(52, actual.WeekNumber, "WeekNumber");
                });
        }

        private void WhenTheUserSavesTheSiteVisitConditions()
        {
            GivenTheSystemUnderTest().SaveConditions(_viewMock.Object);
        }

        private void GivenUserFormSubmission(string expectedEndSky, string expectedEndTemp, string expectedEndTempUnit,
            string expectedEndDateTime, string expectedEndWind, string expectedLocationid, string expectedObserverId,
            string expectedRecorderId, string expectedStartSky, string expectedStartTemp, string expectedStartTempUnit,
            string expectedStartTime, string expectedStartDate, string expectedStartWind)
        {
            _viewMock.SetupGet(x => x.EndSky).Returns(expectedEndSky);
            _viewMock.SetupGet(x => x.EndTemp).Returns(expectedEndTemp);
            _viewMock.SetupGet(x => x.EndUnit).Returns(expectedEndTempUnit);
            _viewMock.SetupGet(x => x.EndTime).Returns(expectedEndDateTime);
            _viewMock.SetupGet(x => x.EndWind).Returns(expectedEndWind);
            _viewMock.SetupGet(x => x.SiteVisited).Returns(expectedLocationid);
            _viewMock.SetupGet(x => x.Observer).Returns(expectedObserverId);
            _viewMock.SetupGet(x => x.Recorder).Returns(expectedRecorderId);
            _viewMock.SetupGet(x => x.StartSky).Returns(expectedStartSky);
            _viewMock.SetupGet(x => x.StartTemp).Returns(expectedStartTemp);
            _viewMock.SetupGet(x => x.StartUnit).Returns(expectedStartTempUnit);
            _viewMock.SetupGet(x => x.StartTime).Returns(expectedStartTime);
            _viewMock.SetupGet(x => x.StartWind).Returns(expectedStartWind);
            _viewMock.SetupGet(x => x.VisitDate).Returns(expectedStartDate);
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
                    Temperature = new Temperature { Units = endTempUnit, Value = endTemp },
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
                    Temperature = new Temperature { Units = startTempUnit, Value = startTemp }
                }
            };

            UserStateMock.SetupGet(x => x.SiteVisit).Returns(siteVisit);
        }


        private SiteConditionsPresenterTss GivenTheSystemUnderTest()
        {
            return new SiteConditionsPresenterTss(_facadeMock.Object)
                .SetHttpResponse(_mockResponse.Object)
                .SetUserState(UserStateMock.Object);
        }
    }
}
