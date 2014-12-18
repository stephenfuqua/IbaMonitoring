using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.UnitTests.TestSpecificSubclasses;

namespace safnet.iba.UnitTests
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
