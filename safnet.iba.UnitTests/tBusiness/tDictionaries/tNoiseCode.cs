using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Dictionaries;

namespace safnet.iba.UnitTests.tBusiness.tDictionaries
{
    [TestClass]
    public class tNoiseCode
    {
        /// <summary>
        /// Validates that the function returns a dictionary of values of the form "x - description"
        /// </summary>
        [TestMethod]
        public void tNoiseCode_GetDropdownValues()
        {
            Dictionary<byte, string> dropdown = NoiseCode.GetDropdownValues();
            Assert.IsTrue(dropdown.Count > 0, "there are no values in the dictionary");
            Assert.IsTrue(Regex.IsMatch(dropdown[0],@"\d - [^$]+$"),"text does not match the pattern");
        }

        /// <summary>
        /// Validates that the function returns a dictionary of values of the form "description"
        /// </summary>
        [TestMethod]
        public void tNoiseCode_Instance()
        {
            Dictionary<byte, string> dropdown = NoiseCode.GetDropdownValues();
            Assert.IsTrue(dropdown.Count > 0, "there are no values in the dictionary");
            Assert.IsTrue(Regex.IsMatch(dropdown[0], @"[^\d][^$]+$"), "text does not match the pattern");
        
        }
    }
}
