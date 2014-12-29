using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Provides database mapping for the <see cref="Person"/> object. 
    /// </summary>
    [DataObject(true)]
    public static class PersonFacade
    {

        /// <summary>
        /// Updates the permanent storage for a <see cref="Person"/> object
        /// </summary>
        /// <param name="person">object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(Person person)
        {
            PersonMapper.Update(person);
        }
         /// <summary>
        /// Inserts a <see cref="Person"/> object into permanent storage
        /// </summary>
        /// <param name="newPerson"><see cref="Person"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(Person newPerson)
        {
            PersonMapper.Insert(newPerson);
        }
        /// <summary>
        /// Retrieves a single <see cref="SiteCondition" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="SiteCondition"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Person Select(string openId)
        {
            return PersonMapper.Select(openId);// BaseMapper.LoadSingleObjectById<Person>(Load, SELECT_STORED_PROC, "OpenId", openId);
        }

        /// <summary>
        /// Retrieves a single <see cref="SiteCondition" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="SiteCondition"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Person Select(Guid id)
        {
            return PersonMapper.Select(id);
        }

        /// <summary>
        /// Retrieves all <see cref="Person" /> objects from permanent storage that are not inactive
        /// </summary>
        /// <returns>List of <see cref="Person"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<Person> SelectAll()
        {
            return PersonMapper.SelectAll();
        }

        /// <summary>
        /// Removes a <see cref="FiftyMeterDataEntry"/> object from permanent storage
        /// </summary>
        /// <param name="entry"><see cref="FiftyMeterDataEntry"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(Person person)
        {
            PersonMapper.Delete(person);
        }
    }
}