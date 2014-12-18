using safnet.iba;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using safnet.iba.Business.Entities;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tIdentityMap and is intended
    ///to contain all tIdentityMap Unit Tests
    ///</summary>
    [TestClass()]
    public class tIdentityMap
    {
        

        /// <summary>
        ///A test for IdentityMap Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("safnet.iba.dll")]
        public void t_IdentityMapConstructor()
        {
            IdentityMap_Accessor target = new IdentityMap_Accessor();
            Assert.IsNotNull(target._dictionary);
        }

        /// <summary>
        ///A test for GetInstance
        ///</summary>
        [TestMethod()]
        public void t_GetInstance()
        {
            IdentityMap actual;
            actual = IdentityMap.GetInstance();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetInstance
        ///</summary>
        [TestMethod()]
        public void t_GetInstance_null()
        {
            IdentityMap_Accessor._instance = null;
            IdentityMap actual = IdentityMap.GetInstance();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void t_Item()
        {
            IdentityMap_Accessor target = new IdentityMap_Accessor(); // TODO: Initialize to an appropriate value
            Guid identity = LookupConstants.LocationTypeParent;
            SafnetBaseEntity expected = new Person() { FirstName = "Stephen", Id =  identity};

            SafnetBaseEntity actual;
            target[identity] = expected;
            actual = target[identity];
            Assert.AreEqual(expected, actual);
        }
    }
}
