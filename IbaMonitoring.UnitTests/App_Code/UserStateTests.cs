using IbaMonitoring.App_Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.TestHelpers;

namespace IbaMonitoring.UnitTests.App_Code
{
    [TestClass]
    public class UserStateTests
    {
        private SessionTss _session = new SessionTss();

        [TestMethod]
        public void ConstructorHappyPath()
        {
            new UserStateManager(_session);
        }

        [TestMethod]
        public void CurrentUserProperty()
        {
            var expected = new Person();

            var system = new UserStateManager(_session);

            system.CurrentUser = expected;

            var actual = system.CurrentUser;

            Assert.AreSame(expected, actual, "same object");

        }
    }
}
