using System;
using System.Collections.Generic;
using System.ComponentModel;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Maps supplemental observations to database stored procedures.
    /// </summary>
    public static class SupplementalObservationMapper
    {
        #region Public Methods

        /// <summary>
        /// Removes a <see cref="SupplementalObservation"/> object from permanent storage
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(SupplementalObservation observation)
        {
            ObservationMapper.Delete(observation);
        }

        /// <summary>
        /// Inserts a <see cref="SupplementalObservation"/> object into permanent storage
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(SupplementalObservation observation)
        {
            ObservationMapper.Update(observation);
        }

        /// <summary>
        /// Retrieves all <see cref="SupplementalObservation" /> objects from permanent storage for a particular PointSurvey
        /// </summary>
        /// <param name="eventId">ID for a <see cref="Event"/> object</param>
        /// <returns>List of <see cref="SupplementalObservation"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<SupplementalObservation> SelectAllForEvent(Guid eventId)
        {
            return ObservationMapper.SelectAllForEvent<SupplementalObservation>(eventId);
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="SupplementalObservation"/> object
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(SupplementalObservation observation)
        {
            ObservationMapper.Update(observation);
        }

        #endregion

    }
}
