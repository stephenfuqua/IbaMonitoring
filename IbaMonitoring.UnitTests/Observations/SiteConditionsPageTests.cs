using IbaMonitoring.App_Code;
using IbaMonitoring.Presenters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.TestHelpers;
using System;
using System.Web;
using IbaMonitoring.Views;

namespace IbaMonitoring.UnitTests.Observations
{
    public class SiteConditionsPageTss : SiteConditionsPage
    {
        public SiteConditionsPageTss(IPresenterFactory factory) : base(factory)
        {

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

        protected override void TestInitialize()
        {
            _mockFactory = MoqRepository.Create<IPresenterFactory>();
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
        public void SubmitValidFormData()
        {
            
            var system = GivenTheSystemUnderTest();
            ExpectToSendTheFormToThePresenter(system);
            ExpectToCreateThePresenterUsingTheFactory(system);

            WhenTheFormIsSubmited(system);

            ThenThereIsNothingToValidate();
        }

   


        private static void WhenTheFormIsSubmited(SiteConditionsPageTss system)
        {
            system.SubmitSiteConditions_Click(null, null);
        }

        private void ExpectToSendTheFormToThePresenter(ISiteConditionsView view)
        {
            _mockPresenter.Setup(x => x.SaveConditions(view));
        }

        private void ExpectToCreateThePresenterUsingTheFactory(SiteConditionsPage page)
        {
            _mockFactory.Setup(
                x => x.BuildSiteConditionsPresenter())
                .Returns(_mockPresenter.Object);
        }

        private SiteConditionsPageTss GivenTheSystemUnderTest()
        {
            return new SiteConditionsPageTss(_mockFactory.Object);
        }


    }
}
