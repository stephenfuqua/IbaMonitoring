using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace safnet.iba.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for tIbaArgumentException and is intended
    ///to contain all tIbaArgumentException Unit Tests
    ///</summary>
    [TestClass()]
    public class tIbaArgumentException
    {
        

        /// <summary>
        ///A test for IbaArgumentException Constructor
        ///</summary>
        [TestMethod()]
        public void t_IbaArgumentExceptionConstructor()
        {
            string message = "a test string";
            try
            {
                IbaArgumentException target = new IbaArgumentException(message);
            }
            catch (IbaArgumentException ex)
            {
                Assert.AreEqual(message, ex.Message);
            }
        }
    }
}
