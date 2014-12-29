using Microsoft.Practices.EnterpriseLibrary.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Maps common elements to and from database queries for <see cref="Observation"/> objects.
    /// </summary>
    public static class ObservationMapper
    {
        #region Constants

        private const string SELECT_STORED_PROC = "Observation_Get";

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a <see cref="Observation"/> object from permanent storage
        /// </summary>
        /// <param name="observation"><see cref="Observation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(Observation observation)
        {
            string storedProcedure = "Observation_Delete";
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand(storedProcedure, new List<QueryParameter>() { new QueryParameter("Id", observation.Id) }, out iba, out cmd);

            iba.ExecuteNonQuery(cmd);
        }



        /// <summary>
        /// Deletes the first X instances of a particular observation type. Primarily used when translating between
        /// a summary point count observation and an individual (single count) observation.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="observationTypeId">The observation type id.</param>
        /// <param name="speciesCode">The species code.</param>
        /// <param name="topXCount">The number of records  to delete.</param>
        public static void DeleteTopX(Guid eventId, Guid observationTypeId, string speciesCode, int topXCount)
        {
            string storedProcedure = "Observation_Delete_TopX";
            Database iba = null;
            DbCommand cmd = null;
            List<QueryParameter> parameterList = new List<QueryParameter>()
            {
                new QueryParameter("EventId", eventId),
                new QueryParameter("ObservationTypeId", observationTypeId),
                new QueryParameter("SpeciesCode", speciesCode),
                new QueryParameter("TopXCount", topXCount)
            };
            BaseMapper.CreateDatabaseCommand(storedProcedure, parameterList, out iba, out cmd);

            iba.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Inserts a <see cref="Observation"/> object into permanent storage
        /// </summary>
        /// <param name="observation"><see cref="Observation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(Observation observation)
        {
            save(observation);
        }

        /// <summary>
        /// Retrieves a specific <see cref="Observation"/> object by its unique identifier.
        /// </summary>
        /// <typeparam name="T">An leaf of Observation</typeparam>
        /// <param name="observationId">The observation id.</param>
        /// <returns>Instance of <see cref="Observation"/></returns>
        public static T Select<T>(int observationId) where T : Observation
        {
            return BaseMapper.LoadSingleObjectByIntegerId<T>(Load<T>, SELECT_STORED_PROC, observationId);
        }

        /// <summary>
        /// Retrieves all <see cref="Observation" /> objects from permanent storage
        /// </summary>
        /// <typeparam name="T">An leaf of Observation</typeparam>
        /// <returns>List of <see cref="Observation"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<T> SelectAll<T>() where T : Observation
        {
            List<QueryParameter> parameterList = new List<QueryParameter>() { new QueryParameter("ObservationTypeId", (Activator.CreateInstance<T>()).ObservationTypeId) };
            return BaseMapper.LoadAllQuery<T>(Load<T>, SELECT_STORED_PROC, parameterList);
        }

        /// <summary>
        /// Retrieves all <see cref="Observation" /> objects from permanent storage for a particular PointSurvey
        /// </summary>
        /// <typeparam name="T">An leaf of Observation</typeparam>
        /// <param name="eventId">ID for a <see cref="Event"/> object</param>
        /// <returns>List of <see cref="Observation"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<T> SelectAllForEvent<T>(Guid eventId) where T : Observation
        {
            guidGuardClause(eventId, "eventId");
            List<QueryParameter> parameterList = new List<QueryParameter>() {
                new QueryParameter("EventId", eventId),
                new QueryParameter("ObservationTypeId", (Activator.CreateInstance<T>()).ObservationTypeId)
            };
            return BaseMapper.LoadAllQuery<T>(Load<T>, SELECT_STORED_PROC, parameterList);
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="Observation"/> object
        /// </summary>
        /// <param name="observation"><see cref="Observation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(Observation observation)
        {
            save(observation);
        }

        #endregion

        #region Private Methods

        private static void guidGuardClause(Guid eventId, string argument)
        {

            if (eventId == Guid.Empty)
            {
                throw new IbaArgumentException("Parameter " + argument + " cannot be an empty guid.");
            }
        }

        private static T Load<T>(IDataReader reader) where T : Observation
        {
            T entity = Activator.CreateInstance<T>();

            entity.Comments = reader.GetStringFromName("Comments");
            entity.EventId = reader.GetGuidFromName("EventId");

            entity.Id = reader.GetLongFromName("Id");
            entity.SpeciesCode = reader.GetStringFromName("SpeciesCode");

            return entity;
        }

        private static void save(Observation observation)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>()
            {
                new QueryParameter("Comments", observation.Comments),
                new QueryParameter("Id", observation.Id),
                new QueryParameter("SpeciesCode", observation.SpeciesCode),
                new QueryParameter("EventId", observation.EventId), 
                new QueryParameter("ObservationTypeId", observation.ObservationTypeId)
            };
            observation.Id = BaseMapper.SaveObjectWithIdentity(observation, parameterList, "Observation");
        }

        #endregion

    }
}
