using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;

namespace safnet.iba.Data.Mappers
{    
    /// <summary>
    /// Provides database mapping for the <see cref="Person"/> object
    /// </summary>
    [DataObject(true)]
    public class PersonMapper
    {

        /// <summary>
        /// Updates the permanent storage for a <see cref="Person"/> object
        /// </summary>
        /// <param name="person"><see cref="Person"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(Person person)
        {
            save(person);
        }

        /// <summary>
        /// Inserts a <see cref="Person"/> object into permanent storage
        /// </summary>
        /// <param name="person"><see cref="Person"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(Person person)
        {
            save(person);
        }

        private static void save(Person person)
        {
            BaseMapper.SaveObject(person, new List<QueryParameter>()
            {
                new QueryParameter("EmailAddress", person.EmailAddress),
                new QueryParameter("FirstName", person.FirstName),
                new QueryParameter("LastName", person.LastName),
                new QueryParameter("Id", person.Id),
                new QueryParameter("OpenId", person.OpenId),
                new QueryParameter("PhoneNumber", person.PhoneNumber),
                new QueryParameter("Address1",person.Address1),
                new QueryParameter("Address2",person.Address2),
                new QueryParameter("City",person.City),
                new QueryParameter("State",person.State),
                new QueryParameter("ZipCode",person.ZipCode),
                new QueryParameter("Country",person.Country),
                new QueryParameter("HasBeenTrained",person.HasBeenTrained),
                new QueryParameter("HasClipboard",person.HasClipboard),
                new QueryParameter("PersonStatus",(short)Enum.Parse(typeof(Status),person.PersonStatus.ToString())),
                new QueryParameter("PersonRole",(short)Enum.Parse(typeof(Role),person.PersonRole.ToString()))
            });
        }

        /// <summary>
        /// Removes a <see cref="Person"/> object from permanent storage
        /// </summary>
        /// <param name="person"><see cref="Person"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(Person person)
        {
            BaseMapper.DeleteObject(person);
        }

        /// <summary>
        /// Retrieves all <see cref="Person" /> objects from permanent storage
        /// </summary>
        /// <returns>List of <see cref="Person"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<Person> SelectAll()
        {
            return BaseMapper.LoadAll<Person>(Load, SELECT_STORED_PROC);
        }

        private const string SELECT_STORED_PROC = "Person_Get";

        /// <summary>
        /// Retrieves a single <see cref="SiteCondition" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="SiteCondition"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Person Select(Guid id)
        {
            guidGuardClause(id, "Select");

            return BaseMapper.LoadSingleObjectById<Person>(Load, SELECT_STORED_PROC, id);
        }

        private static void guidGuardClause(Guid id, string method)
        {
            if (id == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to PersonMapper." + method);
            }
        }

        /// <summary>
        /// Retrieves a single <see cref="SiteCondition" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="SiteCondition"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Person Select(string openId)
        {
            openIdGuardClause(openId, "Select");

            return BaseMapper.LoadSingleObjectById<Person>(Load, SELECT_STORED_PROC,"OpenId",openId);
        }
        private static void openIdGuardClause(string openId, string method)
        {
            if (string.IsNullOrEmpty(openId))
            {
                throw new IbaArgumentException("Empty OpenId passed to PersonMapper." + method);
            }
        }

        private static Person Load(IDataReader reader)
        {
            Person person = new Person()
            {
               EmailAddress = reader.GetStringFromName("EmailAddress"),
               FirstName = reader.GetStringFromName("FirstName"),
               Id = reader.GetGuidFromName("PersonId"),
               LastName = reader.GetStringFromName("LastName"),
               OpenId = reader.GetStringFromName("OpenId"),
               PhoneNumber = reader.GetStringFromName("PhoneNumber"),
               Address1=reader.GetStringFromName("Address1"),
               Address2 = reader.GetStringFromName("Address2"),
               City=reader.GetStringFromName("City"),
               State=reader.GetStringFromName("State"),
               ZipCode=reader.GetStringFromName("ZipCode"),
               Country=reader.GetStringFromName("Country"),
               HasBeenTrained = reader.GetBoolFromName("HasBeenTrained"),
               HasClipboard = reader.GetBoolFromName("HasClipboard"),
               PersonStatus=(Status)reader.GetShortFromName("PersonStatus"),
               PersonRole = (Role)reader.GetShortFromName("PersonRole")
            };
            return person;
        }
    }
}
