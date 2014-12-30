using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.Adapters;
using safnet.iba.TestHelpers;
using System;
using IbaMonitoring.App_Code;

namespace IbaMonitoring.UnitTests.App_Code
{

    [TestClass]
    public class PresenterFactoryTests : BaseMocker
    {
        private Mock<IUnityContainer> _unityMock;
        private Mock<ISiteConditionsView> _siteConditionsViewMock;
        private Mock<ISiteConditionsPresenter> _mockPresenter;

        protected override void TestInitialize()
        {
            _unityMock = MoqRepository.Create<IUnityContainer>();
            _siteConditionsViewMock = MoqRepository.Create<ISiteConditionsView>();
            _mockPresenter = MoqRepository.Create<ISiteConditionsPresenter>();
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
            new PresenterFactory(null);
        }

        [TestMethod]
        public void BuildSiteConditionsPresenter()
        {
            _unityMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ISiteConditionsPresenter)),
                                            It.IsAny<string>(),
                                            It.IsAny<ResolverOverride[]>()))
                .Callback((Type t, string name, ResolverOverride[] overrides) =>
                {
                    Assert.AreEqual(0, overrides.Length, "overrides length");
                })
                .Returns(_mockPresenter.Object);

            var system = GivenTheSystemUnderTest();

            var actual = system.BuildSiteConditionsPresenter();

            Assert.AreSame(_mockPresenter.Object, actual, "return value");
        }

        private PresenterFactory GivenTheSystemUnderTest()
        {
            return new PresenterFactory(_unityMock.Object);
        }

    }
}
