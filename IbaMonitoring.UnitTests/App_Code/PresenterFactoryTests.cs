using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.Adapters;
using IbaMonitoring.Presenters;
using System.Linq;
using safnet.iba.TestHelpers;
using IbaMonitoring.Views;
using System;

namespace IbaMonitoring.UnitTests.App_Code
{

    public class PresenterFactoryTss : IbaMonitoring.App_Code.PresenterFactory
    {
        public PresenterFactoryTss(IUnityContainer iocContainer) : base(iocContainer)
        {
        }

        public PresenterFactoryTss SetUserStateManager(IUserStateManager manager)
        {
            base.UserState = manager;
            return this;
        }

        public PresenterFactoryTss SetGlobalMap(IGlobalMap map)
        {
            base.GlobalMap = map;
            return this;
        }
    }

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
            new PresenterFactoryTss(null);
        }

        [TestMethod]
        public void BuildSiteConditionsPresenter()
        {
            _unityMock.Setup(x => x.Resolve(It.Is<Type>(y => y == typeof(ISiteConditionsPresenter)),
                                            It.IsAny<string>(),
                                            It.IsAny<ResolverOverride[]>()))
                .Callback((Type t, string name, ResolverOverride[] overrides) =>
                {
                    Assert.AreEqual(3, overrides.Length, "overrides length");
                    // Ought to validate the three overrides, but that is non-trivial, low risk,
                    // and not worth the time in this situation. 
                })
                .Returns(_mockPresenter.Object);

            var system = GivenTheSystemUnderTest();

            var actual = system.BuildSiteConditionsPresenter(_siteConditionsViewMock.Object);

            Assert.AreSame(_mockPresenter.Object, actual, "return value");
        }

        private PresenterFactoryTss GivenTheSystemUnderTest()
        {
            return new PresenterFactoryTss(_unityMock.Object)
                .SetGlobalMap(GlobalMapMock.Object)
                .SetUserStateManager(UserStateMock.Object);
        }

    }
}
