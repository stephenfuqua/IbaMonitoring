using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;

namespace safnet.iba.IntegrationTests.Data.Mappers
{
    /// <summary>
    /// Validates the database mapping functions of the <see cref="PersonMapper"/> class.
    /// </summary>
    [TestClass]
    public class tPersonMapper
    {
        /// <summary>
        /// Sets up each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            DbTestHelper.ClearTable("Person");
        }

        /// <summary>
        /// Validates that a new record can be saved correctly.
        /// </summary>
        [TestMethod]
        public void t_Person_Save_Insert()
        {
            Person Person = new Person()
            {
                Id = TestHelper.TestGuid1,
                FirstName = "First Name",
                LastName = "Last Name",
                EmailAddress = "asdf@adfasd",
                PhoneNumber = "(651) 646-8007"
            };
            PersonMapper.Insert(Person);


            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PersonQuery = from Persons in iba.Person_ado select Persons;
                Assert.IsNotNull(PersonQuery, "Query result is null");
                Assert.AreEqual(1, PersonQuery.Count(), "Wrong number of results in query");
                Person_ado adoPerson = PersonQuery.First();
                validateObjectEquality(Person, adoPerson);
            }
        }


        private static void validateObjectEquality(Person Person, Person_ado adoPerson)
        {
            Assert.IsNotNull(adoPerson, "There is not Person with the ID to test for");
            Assert.AreEqual(Person.FirstName, adoPerson.FirstName, "FirstName");
            Assert.AreEqual(Person.LastName, adoPerson.LastName, "LastName");
            Assert.AreEqual(Person.EmailAddress, adoPerson.EmailAddress, "EmailAddress");
            Assert.AreEqual(Person.Id, adoPerson.PersonId, "Id");
            Assert.AreEqual(Person.PhoneNumber, adoPerson.PhoneNumber, "PhoneNumber");
            Assert.AreEqual(Person.Address1, adoPerson.Address1, "Address1");
            Assert.AreEqual(Person.Address2, adoPerson.Address2, "Address2");
            Assert.AreEqual(Person.City, adoPerson.City, "City");
            Assert.AreEqual(Person.Country, adoPerson.Country, "Country");
            Assert.AreEqual(Person.HasBeenTrained, adoPerson.HasBeenTrained, "HasBeenTrained");
            Assert.AreEqual(Person.HasClipboard, adoPerson.HasClipboard, "HasClipboard");
            Assert.AreEqual(Person.OpenId, adoPerson.OpenId, "OpenId");
            Assert.AreEqual((short)Person.PersonRole, adoPerson.PersonRole, "PersonRole");
            Assert.AreEqual((short)Person.PersonStatus, adoPerson.PersonStatus, "PersonStatus");
            Assert.AreEqual(Person.State, adoPerson.State, "State");
            Assert.AreEqual(Person.ZipCode, adoPerson.ZipCode, "ZipCode");
        }

        /// <summary>
        /// Validates that update of an existing record works and doesn't update any other records.s
        /// </summary>
        [TestMethod]
        public void t_Person_Save_Update()
        {
            Person_ado personAdo = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                personAdo = Person_ado.CreatePerson_ado(TestHelper.TestGuid1, "First Name", "Last Name", 0, 0, true,true);
                personAdo.EmailAddress = "asdf@adfasd";
                personAdo.PhoneNumber = "(651) 646-8007";
                personAdo.Address1 = "address1";
                personAdo.Address2 = "asddress 2";
                personAdo.City = "city";
                personAdo.Country = "cou    ";
                personAdo.HasBeenTrained = true;
                personAdo.HasClipboard = true;
                personAdo.OpenId = "httP;//www.googl.ecom/asdf.adsfae";
                personAdo.PersonRole = 1;
                personAdo.PersonStatus = 2;
                personAdo.State = "TX";
                personAdo.ZipCode = "55555";

                iba.AddToPerson_ado(personAdo);
            });
            List<Person_ado> extraList = DbTestHelper.LoadExtraneousPersons();

            // Setup object to be saved. Change everything except the Id.
            Person Person = new Person()
            {
                Id = TestHelper.TestGuid1,
                FirstName = "First Name 2",
                LastName = "Last Nam 2e",
                EmailAddress = "asdf@adfasd 2",
                PhoneNumber = "(651) 646-8888",
                Address1 = "address1",
                Address2 = "asddress 2",
                City = "city",
                Country = "cou    ",
                HasBeenTrained = true,
                HasClipboard = true,
                OpenId = "httP;//www.googl.ecom/asdf.adsfae",
                PersonRole = Role.Administrator,
                PersonStatus = Status.Active,
                State = "TX",
                ZipCode = "55555"
            };

            // Execute the test
            PersonMapper.Update(Person);

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PersonQuery = from Persons in iba.Person_ado select Persons;
                Assert.IsNotNull(PersonQuery, "Query result is null");
                Assert.AreEqual(extraList.Count() + 1, PersonQuery.Count(), "Wrong number of results in query");
                Person_ado adoPerson = PersonQuery.First(x => x.PersonId == TestHelper.TestGuid1);
                validateObjectEquality(Person, adoPerson);

                validateExtrapersonAdos(extraList, PersonQuery);
            }
        }

        private static void validateExtrapersonAdos(List<Person_ado> extraList, IQueryable<Person_ado> PersonQuery)
        {
            foreach (Person_ado extra in extraList)
            {
                Person_ado adoPerson = PersonQuery.First(x => x.PersonId == extra.PersonId);
                Assert.IsNotNull(adoPerson, "There is no longer an object with id " + extra.PersonId.ToString());
                Assert.AreEqual(extra.EmailAddress, adoPerson.EmailAddress, "EmailAddress for " + extra.PersonId.ToString());
                Assert.AreEqual(extra.FirstName, adoPerson.FirstName, "FirstName for " + extra.PersonId.ToString());
                Assert.AreEqual(extra.LastName, adoPerson.LastName, "LastName for " + extra.PersonId.ToString());
                Assert.AreEqual(extra.OpenId, adoPerson.OpenId, "OpenId for " + extra.PersonId.ToString());
                Assert.AreEqual(extra.PhoneNumber, adoPerson.PhoneNumber, "PhoneNumber for " + extra.PersonId.ToString());
                Assert.AreEqual(extra.Address1, adoPerson.Address1, "Address1 for " + extra.PersonStatus.ToString());
                Assert.AreEqual(extra.Address2, adoPerson.Address2, "Address2 for " + extra.PersonStatus.ToString());
                Assert.AreEqual(extra.City, adoPerson.City, "City for " + extra.PersonStatus.ToString());
            }
        }

        /// <summary>
        /// Validates selection of a Person object by unique identifier.
        /// </summary>
        [TestMethod]
        public void t_Person_Select_ByGuid()
        {
            Person_ado personAdo = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                personAdo = Person_ado.CreatePerson_ado(TestHelper.TestGuid1, "First Name", "Last Name", 1, 2,true, true);
                personAdo.EmailAddress = "asdf@adfasd";
                personAdo.PhoneNumber = "(651) 646-8007";
                personAdo.Address1 = "address1";
                personAdo.Address2 = "asddress 2";
                personAdo.City = "city";
                personAdo.Country = "cou    ";
                personAdo.HasBeenTrained = true;
                personAdo.HasClipboard = true;
                personAdo.OpenId = "httP;//www.googl.ecom/asdf.adsfae";
                personAdo.PersonRole = 1;
                personAdo.PersonStatus = 2;
                personAdo.State = "TX";
                personAdo.ZipCode = "55555";

                iba.AddToPerson_ado(personAdo);

            });
            List<Person_ado> extraList = DbTestHelper.LoadExtraneousPersons();

            // Exercise the test
            Person Person = PersonMapper.Select(personAdo.PersonId);

            validateResults(personAdo, Person);
        }

        /// <summary>
        /// Validates than an exception is thrown if an empty Guid is passed to the main Select function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IbaArgumentException))]
        public void t_Person_Select_ByGuid_Empty()
        {
            PersonMapper.Select(Guid.Empty);
        }

        /// <summary>
        /// Validates selection of all Person objects in the database.
        /// </summary>
        [TestMethod]
        public void t_Person_Select_All()
        {
            // Backdoor setup
            List<Person_ado> list = DbTestHelper.LoadExtraneousPersons();

            // Exercise the test
            List<Person> PersonList = PersonMapper.SelectAll();

            // Validate results
            Assert.AreEqual(list.Count(), PersonList.Count, "Wrong number of objects in the result list");
            foreach (Person_ado ado in list)
            {
                // just check to make sure the object is there; leave specific value check for the Select_ByGuid test
                Assert.IsTrue(PersonList.Exists(x => x.Id.Equals(ado.PersonId)), "personAdo " + ado.PersonId.ToString() + " is not in the results");
            }
        }

        /// <summary>
        /// Validates that the Delete function only deletes the specified record.
        /// </summary>
        [TestMethod]
        public void t_Person_Delete()
        {
            Person_ado personAdo = null;
            // backdoor data setup
            DbTestHelper.LoadAdoObjects(delegate(IbaUnitTestEntities iba)
            {
                personAdo = Person_ado.CreatePerson_ado(TestHelper.TestGuid1, "First Name", "Last Name", 1, 2, true, true);
                personAdo.EmailAddress = "asdf@adfasd";
                personAdo.PhoneNumber = "(651) 646-8007";
                iba.AddToPerson_ado(personAdo);
            });
            List<Person_ado> personAdolist = DbTestHelper.LoadExtraneousPersons();

            PersonMapper.Delete(new Person() { Id = personAdo.PersonId });

            // Validate results
            using (IbaUnitTestEntities iba = new IbaUnitTestEntities())
            {
                var PersonQuery = from Persons in iba.Person_ado select Persons;
                Assert.IsNotNull(PersonQuery, "Query result is null");
                Assert.AreEqual(personAdolist.Count(), PersonQuery.Count(), "Wrong number of results in query");
                validateExtrapersonAdos(personAdolist, PersonQuery);
            }
        }

        private static void validateResults(Person_ado personAdo, Person Person)
        {
            Assert.AreEqual(personAdo.EmailAddress, Person.EmailAddress, "EmailAddress");
            Assert.AreEqual(personAdo.FirstName, Person.FirstName, "FirstName");
            Assert.AreEqual(personAdo.PersonId, Person.Id, "Id");
            Assert.AreEqual(personAdo.LastName, Person.LastName, "LastName");
            Assert.AreEqual(personAdo.OpenId, Person.OpenId, "OpenId");
            Assert.AreEqual(personAdo.PhoneNumber, Person.PhoneNumber, "PhoneNumber");
            Assert.AreEqual(personAdo.Address1, Person.Address1, "Address1");
            Assert.AreEqual(personAdo.Address2, Person.Address2, "Address2");
            Assert.AreEqual(personAdo.City, Person.City, "City");
            Assert.AreEqual(personAdo.Country, Person.Country, "Country");
            Assert.AreEqual(personAdo.HasBeenTrained, Person.HasBeenTrained, "HasBeenTrained");
            Assert.AreEqual(personAdo.HasClipboard, Person.HasClipboard, "HasClipboard");
            Assert.AreEqual(personAdo.OpenId, Person.OpenId, "OpenID");
            Assert.AreEqual(personAdo.PersonRole, (short)Person.PersonRole, "PersonRole");
            Assert.AreEqual(personAdo.PersonStatus,(short) Person.PersonStatus, "PersonStatus");
            Assert.AreEqual(personAdo.State, Person.State, "State");
            Assert.AreEqual(personAdo.ZipCode, Person.ZipCode, "ZipCode");
        }
    }
}
