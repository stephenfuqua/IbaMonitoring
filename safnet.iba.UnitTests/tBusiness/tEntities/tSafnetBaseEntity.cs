using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tSafnetBaseEntity and is intended
    ///to contain all tSafnetBaseEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class tSafnetBaseEntity
    {



        internal virtual SafnetBaseEntity CreateSafnetBaseEntity()
        {

            SafnetBaseEntity target = new safnet.iba.Business.Entities.Moles.SSafnetBaseEntity();
            return target;
        }

        /// <summary>
        ///A test for SetNewId
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_SetNewId()
        {
            SafnetBaseEntity target = CreateSafnetBaseEntity(); 

            Guid actual;
            actual = target.SetNewId();
            Assert.AreNotEqual(Guid.Empty, actual, "Guid not created for Id");
            Assert.AreEqual(actual, target.Id, "Id not set");
            Assert.IsTrue(target.NeedsDatabaseRefresh, "NeedsDatabaseRefresh not true");
        }

        /// <summary>
        ///A test for Id
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_Id()
        {
            SafnetBaseEntity target = CreateSafnetBaseEntity();
            Guid expected = LookupConstants.LocationTypePoint;

            Guid actual;
            target.Id = expected;
            actual = target.Id;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for NeedsDatabaseRefresh
        ///</summary>
        [TestMethod()]
        public void t_NeedsDatabaseRefresh()
        {
            SafnetBaseEntityTss tss = new SafnetBaseEntityTss();
            tss.NeedsDatabaseRefresh = true;

            Assert.IsTrue(tss.NeedsDatabaseRefresh);
        }

        private class SafnetBaseEntityTss : SafnetBaseEntity
        {
            public new bool NeedsDatabaseRefresh
            {
                get
                {
                    return base.NeedsDatabaseRefresh;
                }
                set
                {
                    base.NeedsDatabaseRefresh = value;
                }
            }
        }
    }
}
