using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Static;

namespace safnet.iba.UnitTests
{
    [TestClass]
    public class t_CustomValidation
    {
        [TestMethod]
        public void t_ValidateTime_Valid_Midnight()
        {
            string time = "00:00";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_Valid_Before10()
        {
            string time = "9:42";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_Valid_Before10Padded()
        {
            string time = "09:31";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_Valid_After10()
        {
            string time = "10:01";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_Valid_Noon()
        {
            string time = "12:00";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_InValid_Hour()
        {
            string time = "13:31";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void t_ValidateTime_InValid_Minute()
        {
            string time = "12:61";
            bool isValid = CustomValidation.ValidateMorning(time);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_100F()
        {
            string temp = "100";
            bool isValid = CustomValidation.ValidateTemperature(temp, "F");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_99F()
        {
            string temp = "99";
            bool isValid = CustomValidation.ValidateTemperature(temp, "F");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_0F()
        {
            string temp = "0";
            bool isValid = CustomValidation.ValidateTemperature(temp, "F");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_Minus1F()
        {
            string temp = "-1";
            bool isValid = CustomValidation.ValidateTemperature(temp, "F");
            Assert.IsFalse(isValid);
        }


        [TestMethod]
        public void t_ValidateTemperature_Minus18F()
        {
            string temp = "-18";
            bool isValid = CustomValidation.ValidateTemperature(temp, "F");
            Assert.IsFalse(isValid);
        }




        [TestMethod]
        public void t_ValidateTemperature_38C()
        {
            string temp = "38";
            bool isValid = CustomValidation.ValidateTemperature(temp, "C");
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_37C()
        {
            string temp = "37";
            bool isValid = CustomValidation.ValidateTemperature(temp, "C");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_Minus18C()
        {
            string temp = "-18";
            bool isValid = CustomValidation.ValidateTemperature(temp, "C");
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void t_ValidateTemperature_Minus19C()
        {
            string temp = "-19";
            bool isValid = CustomValidation.ValidateTemperature(temp, "C");
            Assert.IsFalse(isValid);
        }

    }
}
