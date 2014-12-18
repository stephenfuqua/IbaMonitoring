using System;
using System.Collections.Generic;
using System.ComponentModel;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;

namespace safnet.iba.Business.AppFacades
{
    public class SupplementalObservationFacade
    {
        private IUserStateManager _state;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplementalObservationFacade"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        public SupplementalObservationFacade(IUserStateManager state)
        {
            _state = state;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a <see cref="SupplementalObservation"/> object from permanent storage
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(SupplementalObservation observation)
        {
            ObservationMapper.Delete(observation);
            _state.SiteVisit.SupplementalObservations.RemoveAll(x => x.Id.Equals(observation.Id));
        }

        /// <summary>
        /// Inserts a <see cref="SupplementalObservation"/> object into permanent storage
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(SupplementalObservation observation)
        {
            observation.SpeciesCode = observation.SpeciesCode.ToUpper();
            ObservationMapper.Insert(observation);
            _state.SiteVisit.SupplementalObservations.Add(observation);
        }

        /// <summary>
        /// Retrieves all <see cref="SupplementalObservation" /> objects from permanent storage for a particular PointSurvey
        /// </summary>
        /// <param name="eventId">ID for a <see cref="Event"/> object</param>
        /// <returns>List of <see cref="SupplementalObservation"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<SupplementalObservation> SelectAllForEvent(Guid eventId)
        {
            if (_state.SiteVisit.Id.Equals(eventId))
            {
                return _state.SiteVisit.SupplementalObservations;
            }
            else
            {
                return ObservationMapper.SelectAllForEvent<SupplementalObservation>(eventId);
            }
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="SupplementalObservation"/> object
        /// </summary>
        /// <param name="observation"><see cref="SupplementalObservation"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(SupplementalObservation observation)
        {
            observation.SpeciesCode = observation.SpeciesCode.ToUpper();
            ObservationMapper.Update(observation);

            _state.SiteVisit.SupplementalObservations.RemoveAll(x => x.Id.Equals(observation.Id));
            _state.SiteVisit.SupplementalObservations.Add(observation);
        }

        #endregion

    }
}
