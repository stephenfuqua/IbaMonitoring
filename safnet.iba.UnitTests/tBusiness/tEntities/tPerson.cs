using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{


    /// <summary>
    ///This is a test class for <see cref="Person"/> and is intended
    ///to contain all tPerson Unit Tests
    ///</summary>
    [TestClass()]
    public class tPerson
    {
        /// <summary>
        /// Test-specific sub-class that overrides the SendEmail method with validations instead of actually sending a message.
        /// </summary>
        public class PersonTss : Person
        {
            protected override void SendEmail(string body, string fromAddress, string bccAddress, string subject, SendCompletedDelegate sendCompleted)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(body), "body is null, whitespace, or empty");
                Assert.IsFalse(string.IsNullOrWhiteSpace(fromAddress), "fromAddress is null, whitespace, or empty");
                Assert.IsFalse(string.IsNullOrWhiteSpace(bccAddress), "bccAddress is null, whitespace, or empty");
                Assert.IsFalse(string.IsNullOrWhiteSpace(subject), "subject is null, whitespace, or empty");
            }
        }

        public void SendCompletedDelegate(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // nothing to do
        }

        /// <summary>
        /// Validates the SendActivationConfirmation method
        /// </summary>
        [TestMethod]
        public void t_SendActivationConfirmation()
        {
            PersonTss sut = new PersonTss() { EmailAddress = "fake@fake.com" };
            sut.SendActivationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        /// Validates the SendActivationConfirmation method when no e-mail address has been set (null)
        /// </summary>
        [TestMethod]
        public void t_SendActivationConfirmation_NullEmail()
        {
            PersonTss sut = new PersonTss() { EmailAddress = null };
            sut.SendActivationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        /// Validates the SendActivationConfirmation method when no e-mail address has been set (empty)
        /// </summary>
        [TestMethod]
        public void t_SendActivationConfirmation_EmptyEmail()
        {
            PersonTss sut = new PersonTss() { EmailAddress = string.Empty };
            sut.SendActivationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        /// Validates the SendRegistrationConfirmation method
        /// </summary>
        [TestMethod]
        public void t_SendRegistrationConfirmation()
        {
            PersonTss sut = new PersonTss() { EmailAddress = "fake@fake.com" };
            sut.SendRegistrationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        /// Validates the SendRegistrationConfirmation method when no e-mail address has been set (null)
        /// </summary>
        [TestMethod]
        public void t_SendRegistrationConfirmation_NullEmail()
        {
            PersonTss sut = new PersonTss() { EmailAddress = null };
            sut.SendRegistrationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        /// Validates the SendRegistrationConfirmation method when no e-mail address has been set (empty string)
        /// </summary>
        [TestMethod]
        public void t_SendRegistrationConfirmation_EmptyEmail()
        {
            PersonTss sut = new PersonTss() { EmailAddress = string.Empty };
            sut.SendRegistrationConfirmation(SendCompletedDelegate);
        }

        /// <summary>
        ///A test for Person Constructor
        ///</summary>
        [TestMethod()]
        public void t_PersonConstructor()
        {
            Person target = new Person();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CreatePerson
        ///</summary>
        [TestMethod()]
        [HostType("Moles")]
        public void t_CreatePerson()
        {
            safnet.iba.Business.Entities.Moles.MSafnetBaseEntity.AllInstances.SetNewId = (SafnetBaseEntity entity) => { return entity.Id = LookupConstants.LocationTypeParent; };

            Person actual = Person.CreatePerson();
            Assert.IsNotNull(actual, "object is null");
            Assert.AreEqual(LookupConstants.LocationTypeParent, actual.Id, "ID not assigned");
        }

        /// <summary>
        ///A test for EmailAddress
        ///</summary>
        [TestMethod()]
        public void t_EmailAddress()
        {
            Person target = new Person();
            string expected = "bill.gates@microsoft.com";

            string actual;
            target.EmailAddress = expected;
            actual = target.EmailAddress;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FirstName
        ///</summary>
        [TestMethod()]
        public void t_FirstName()
        {
            Person target = new Person();
            string expected = "Bill";

            string actual;
            target.FirstName = expected;
            actual = target.FirstName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for FullName
        ///</summary>
        [TestMethod()]
        public void t_FullName()
        {
            Person target = new Person() { FirstName = "Bill", LastName = "Gates" };

            string actual;
            actual = target.FullName;
            Assert.AreEqual("Bill Gates", actual);
        }

        /// <summary>
        ///A test for IsActive
        ///</summary>
        [TestMethod()]
        public void t_IsActive_Active()
        {
            Person target = new Person();
            bool expected = true;

            bool actual;
            target.PersonStatus = Status.Active;
            actual = target.IsActive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsActive
        ///</summary>
        [TestMethod()]
        public void t_IsActive_None()
        {
            Person target = new Person();
            bool expected = false;

            bool actual;
            target.PersonStatus = Status.None;
            actual = target.IsActive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsActive
        ///</summary>
        [TestMethod()]
        public void t_IsActive_Inactive()
        {
            Person target = new Person();
            bool expected = false;

            bool actual;
            target.PersonStatus = Status.Inactive;
            actual = target.IsActive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsActive
        ///</summary>
        [TestMethod()]
        public void t_IsActive_Pending()
        {
            Person target = new Person();
            bool expected = false;

            bool actual;
            target.PersonStatus = Status.Pending;
            actual = target.IsActive;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LastName
        ///</summary>
        [TestMethod()]
        public void t_LastName()
        {
            Person target = new Person();
            string expected = "Gates";
            string actual;
            target.LastName = expected;
            actual = target.LastName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OpenId
        ///</summary>
        [TestMethod()]
        public void t_OpenId()
        {
            Person target = new Person();
            string expected = "http://www.hotmail.com/bill.gates";

            string actual;
            target.OpenId = expected;
            actual = target.OpenId;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for PhoneNumber
        ///</summary>
        [TestMethod()]
        public void t_PhoneNumber()
        {
            Person target = new Person();
            string expected = "(555) 555-5555";
            string actual;
            target.PhoneNumber = expected;
            actual = target.PhoneNumber;
            Assert.AreEqual(expected, actual);
        }
    }
}
