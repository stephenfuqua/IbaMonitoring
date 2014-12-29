using safnet.iba.Adapters;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Business.Factories;
using safnet.iba.Data.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Provides database mapping for the <see cref="FiftyMeterPointSurvey"/> object. Because it uses 
    /// _stateMap information, this class must be in the main website project.
    /// </summary>
    [DataObject(true)]
    public class FiftyMeterDataEntryFacade
    {
        private IUserStateManager _state;

        /// <summary>
        /// Fifties the meter data entry.
        /// </summary>
        /// <param name="_state">The _state.</param>
        public FiftyMeterDataEntryFacade(IUserStateManager state)
        {
            // TODO: use of state should be moved to a presenter class
            _state = state;
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="FiftyMeterDataEntry"/> object
        /// </summary>
        /// <param name="survey"><see cref="FiftyMeterDataEntry"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void Update(FiftyMeterDataEntry entry)
        {
            entry.SpeciesCode = entry.SpeciesCode.ToUpper();
            List<PointCountBeyond50> beyond50 = entry.GetListBeyond50();
            List<PointCountWithin50> within50 = entry.GetListWithin50();

            beyond50.FindAll(y => y.Id.Equals(0)).ForEach(x =>
                {
                    ObservationMapper.Update(x);
                    _state.PointSurvey.Observations.Add(x);
                });
            within50.FindAll(y => y.Id.Equals(0)).ForEach(x =>
                {
                    ObservationMapper.Update(x);
                    _state.PointSurvey.Observations.Add(x);
                });



            int deleteBeyond = _state.PointSurvey.Observations.OfType<PointCountBeyond50>().Where(x => x.SpeciesCode.Equals(entry.SpeciesCode)).Count() - beyond50.Count();
            int deleteWithin = _state.PointSurvey.Observations.OfType<PointCountWithin50>().Where(x => x.SpeciesCode.Equals(entry.SpeciesCode)).Count() - within50.Count();

            if (deleteBeyond > 0)
            {
                var toDelete = _state.PointSurvey.Observations.Where(x => x.SpeciesCode.Equals(entry.SpeciesCode)).Take(deleteBeyond);
                toDelete.ToList().ForEach(x =>
                    {
                        ObservationMapper.Delete(x);
                        _state.PointSurvey.Observations.Remove(x);
                    });
            }
            if (deleteWithin > 0)
            {
                var toDelete = _state.PointSurvey.Observations.Where(x => x.SpeciesCode.Equals(entry.SpeciesCode)).Take(deleteWithin);
                toDelete.ToList().ForEach(x =>
                {
                    ObservationMapper.Delete(x);
                    _state.PointSurvey.Observations.Remove(x);
                });
            }


        }

        /// <summary>
        /// Inserts a <see cref="FiftyMeterDataEntry"/> object into permanent storage
        /// </summary>
        /// <param name="entry"><see cref="FiftyMeterDataEntry"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void Insert(FiftyMeterDataEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            entry.SpeciesCode = entry.SpeciesCode.ToUpper();
            List<PointCountBeyond50> beyond50 = entry.GetListBeyond50();
            List<PointCountWithin50> within50 = entry.GetListWithin50();

            beyond50.FindAll(y => !y.MarkForDeletion).ForEach(x =>
                {
                    ObservationMapper.Insert(x);
                    _state.PointSurvey.Observations.Add(x);
                });
            within50.FindAll(y => !y.MarkForDeletion).ForEach(x =>
                {
                    ObservationMapper.Insert(x);
                    _state.PointSurvey.Observations.Add(x);
                });
        }

        /// <summary>
        /// Removes a <see cref="FiftyMeterDataEntry"/> object from permanent storage
        /// </summary>
        /// <param name="entry"><see cref="FiftyMeterDataEntry"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void Delete(FiftyMeterDataEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            ObservationMapper.DeleteTopX(entry.PointSurveyId, PointCountWithin50.ObservationTypeGuid,
                entry.SpeciesCode, entry.CountWithin50);
            ObservationMapper.DeleteTopX(entry.PointSurveyId, PointCountBeyond50.ObservationTypeGuid,
                entry.SpeciesCode, entry.CountBeyond50);

            entry.GetListBeyond50().ForEach(x =>
                {
                    _state.PointSurvey.Observations.RemoveAll(y => y.ObservationTypeId.Equals(x.ObservationTypeId)
                        && y.SpeciesCode.Equals(x.SpeciesCode));
                });
            entry.GetListWithin50().ForEach(x =>
                {
                    _state.PointSurvey.Observations.RemoveAll(y => y.ObservationTypeId.Equals(x.ObservationTypeId)
                        && y.SpeciesCode.Equals(x.SpeciesCode));
                });

        }


        /// <summary>
        /// Retrieves all <see cref="FiftyMeterPointSurvey"/> objects from permanent storage
        /// </summary>
        /// <param name="eventId">The site visit id.</param>
        /// <returns>
        /// List of <see cref="FiftyMeterPointSurvey"/> objects
        /// </returns>
        /// <remarks>Empty <paramref name="eventId"/> is allowed</remarks>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<FiftyMeterDataEntry> SelectAllForEvent(Guid eventId)
        {

            List<FiftyMeterDataEntry> dataEntryList = new List<FiftyMeterDataEntry>();

            if (!eventId.Equals(Guid.Empty))
            {
                List<PointCountBeyond50> beyond50List = null;
                List<PointCountWithin50> less50List = null;
                if (_state.PointSurvey.Id.Equals(eventId))
                {
                    beyond50List = _state.PointSurvey.Observations.OfType<PointCountBeyond50>().ToList();
                    less50List = _state.PointSurvey.Observations.OfType<PointCountWithin50>().ToList();
                }
                else
                {
                    FiftyMeterPointSurvey survey = _state.SiteVisit.PointSurveys.Find(x => x.Id.Equals(eventId));
                    if (survey != null)
                    {
                        beyond50List = survey.Observations.OfType<PointCountBeyond50>().ToList();
                        less50List = survey.Observations.OfType<PointCountWithin50>().ToList();
                    }
                }

                FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountBeyond50>(beyond50List, dataEntryList);
                FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountWithin50>(less50List, dataEntryList);
            }

            return dataEntryList;
        }
    }
}