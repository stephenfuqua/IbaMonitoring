using IbaMonitoring.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.TestHelpers;
using System;
using Moq;
using System.Web;
using safnet.iba.Presenters;

namespace IbaMonitoring.UnitTests.Observations
{
    public class SiteConditionsPageTss : SiteConditionsPage
    {
        public SiteConditionsPageTss(IPresenterFactory factory) : base(factory)
        {

        }

        public SiteConditionsPageTss SetHttpResponse(HttpResponseBase response)
        {
            base.HttpResponse = response;
            return this;
        }

        public SiteConditionsPageTss SetPageAdapter(IPageAdapter adapter)
        {
            base.PageAdapter = adapter;
            return this;
        }

        public void SubmitSiteConditions_Click(object sender, EventArgs e)
        {
            base.submitSiteConditions_Click(sender, e);
        }
    }

    [TestClass]
    public class SiteConditionsPageTests : BaseMocker
    {
        private Mock<IPresenterFactory> _mockFactory;
        private Mock<ISiteConditionsPresenter> _mockPresenter;
        private Mock<HttpResponseBase> _mockResponse;
        private Mock<IPageAdapter> _mockPageAdapter;

        protected override void TestInitialize()
        {
            _mockFactory = MoqRepository.Create<IPresenterFactory>();
            _mockResponse = MoqRepository.Create<HttpResponseBase>();
            _mockPageAdapter = MoqRepository.Create<IPageAdapter>();
            _mockPresenter = MoqRepository.Create<ISiteConditionsPresenter>();

            base.TestInitialize();
        }

        [TestMethod]
        public void ConstructorHappyPath()
        {
            GivenTheSystemUnderTest();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorRejectsNullArgument()
        {
            new SiteConditionsPage(null);
        }

        [TestMethod]
        public void RedirectToPointCountsPageWhenSubmittingValidForm()
        {
            GivenTheFormSubmissionIsValid();

            var expectedPage = "PointCounts.aspx";
            ExpectToRedirectTo(expectedPage);

            var system = GivenTheSystemUnderTest();
            ExpectToSendTheFormToThePresenter();
            ExpectToCreateThePresenterUsingTheFactory(system);

            WhenTheFormIsSubitted(system);

            ThenThereIsNothingToValidate();
        }

        [TestMethod]
        public void DoNothingWhenSubmittingInvalidForm()
        {
            GivenTheFormSubmissionIsNotValid();

            var system = GivenTheSystemUnderTest();

            WhenTheFormIsSubitted(system);

            ThenThereIsNothingToValidate();
        }

        private void GivenTheFormSubmissionIsNotValid()
        {
            _mockPageAdapter.SetupGet(x => x.IsValid)
                .Returns(false);
        }

        private static void WhenTheFormIsSubitted(SiteConditionsPageTss system)
        {
            system.SubmitSiteConditions_Click(null, null);
        }

        private void ExpectToSendTheFormToThePresenter()
        {
            _mockPresenter.Setup(x => x.SaveConditions());
        }

        private void ExpectToCreateThePresenterUsingTheFactory(SiteConditionsPage page)
        {
            _mockFactory.Setup(x => x.BuildSiteConditionsPresenter(It.Is<SiteConditionsPage>(y => object.ReferenceEquals(y, page))))
                            .Returns(_mockPresenter.Object);
        }

        private void GivenTheFormSubmissionIsValid()
        {
            _mockPageAdapter.SetupGet(x => x.IsValid)
                            .Returns(true);
        }

        private void ExpectToRedirectTo(string expectedPage)
        {
            _mockResponse.Setup(x => x.Redirect(It.Is<string>(y => y == expectedPage), It.Is<bool>(y => y)));
        }

        private SiteConditionsPageTss GivenTheSystemUnderTest()
        {
            return new SiteConditionsPageTss(_mockFactory.Object)
                .SetHttpResponse(_mockResponse.Object)
                .SetPageAdapter(_mockPageAdapter.Object);
        }


    }
}
