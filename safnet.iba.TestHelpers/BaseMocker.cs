using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using safnet.iba.Adapters;
using System;

namespace safnet.iba.TestHelpers
{
    public abstract class BaseMocker
    {
        public static readonly Guid TEST_GUID_1 = new Guid("{6F1C5660-0C70-45F7-B0AF-FAF44DEF0CE4}");
        public static readonly Guid TEST_GUID_2 = new Guid("{9CFABF4F-BC79-4505-B281-9081DC8BD3D3}");
        public static readonly Guid TEST_GUID_3 = new Guid("{2F6A2BB9-5FCC-4EF5-B1E2-A1EC608A503B}");
        public static readonly Guid TEST_GUID_4 = new Guid("{158253AD-48E4-4D2E-9079-CE18E58A2EA1}");
        public static readonly Guid TEST_GUID_5 = new Guid("{BE2C20FA-18DD-4281-B7C2-5D6DB65D8070}");
        public static readonly Guid TEST_GUID_6 = new Guid("{8118DD25-2BD5-48F0-A994-85D12334BD10}");


        protected MockRepository MoqRepository = new MockRepository(MockBehavior.Strict);
        protected Mock<IUserStateManager> UserStateMock;
        protected Mock<IGlobalMap> GlobalMapMock;

        protected virtual void TestInitialize()
        {

        }


        protected BaseMocker() { }


        [TestInitialize]
        public void BaseTestInitialization()
        {

            UserStateMock = MoqRepository.Create<IUserStateManager>();
            GlobalMapMock = MoqRepository.Create<IGlobalMap>();

            TestInitialize();
        }

        protected void ThenThereIsNothingToValidate()
        {
            // This function exists just to indicate that the absence of validation is by design, not accident.
        }



    }
}
