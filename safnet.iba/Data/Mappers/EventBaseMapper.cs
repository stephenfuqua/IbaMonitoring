using System;
using System.Collections.Generic;
using System.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Maps common elements to and from database queries for <see cref="Event"/> objects.
    /// </summary>
    public static class EventBaseMapper
    {

        #region Public Methods

        /// <summary>
        /// Creates the parameters for an <see cref="Event"/> Save method
        /// </summary>
        /// <param name="evt">The Event.</param>
        /// <returns>List of QueryParameter objects</returns>
        public static List<QueryParameter> CreateSaveParameters(Event evt)
        {
            if (!evt.EndTimeStamp.HasValue || !evt.StartTimeStamp.HasValue)
            {
                throw new ArgumentException("Cannot save null EndTimeStamp or StartTimeStamp for Event object");
            }
            List<QueryParameter> list = new List<QueryParameter>()
            {
                new QueryParameter("EndTime", evt.EndTimeStamp.Value),
                new QueryParameter("Id", evt.Id),
                new QueryParameter("LocationId", evt.LocationId),
                new QueryParameter("StartTime", evt.StartTimeStamp.Value),
            };
            return list;
        }

        /// <summary>
        /// Loads data from IDataReader to an <see cref="Event"/> object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="evt">The Event.</param>
        public static void Load(IDataReader reader, Event evt)
        {
            evt.StartTimeStamp = reader.GetDateTimeFromName("StartTime");
            evt.EndTimeStamp = reader.GetDateTimeFromName("EndTime");
            evt.Id = reader.GetGuidFromName("Id");
            evt.LocationId = reader.GetGuidFromName("LocationId");
        }

        #endregion

    }
}
