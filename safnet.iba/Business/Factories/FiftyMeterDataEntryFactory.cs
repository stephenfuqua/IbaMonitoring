using System;
using System.Collections.Generic;
using System.Linq;
using safnet.iba.Business.Entities.Observations;

namespace safnet.iba.Business.Factories
{
    /// <summary>
    /// Transforms observations into <see cref="FiftyMeterDataEntry" /> objects and performs related functions.
    /// </summary>
    public static class FiftyMeterDataEntryFactory
    {


        /// <summary>
        /// Separates an observation list by PointSurveyId, creating a new <see cref="FiftyMeterDataEntry"></see> for each PointSurveyId. 
        /// </summary>
        /// <typeparam name="T">FiftyMeterPointObservation</typeparam>
        /// <param name="dataEntryList">A list of <see cref="FiftyMeterDataEntry"></see></param>
        /// <param name="fiftyList">a list of <typeparamref name="T"></typeparamref></param>
        public static void CreateEntriesFromPointCounts<T>(List<T> fiftyList, List<FiftyMeterDataEntry> dataEntryList) where T : FiftyMeterPointObservation
        {
            var groupEvent = fiftyList.GroupBy(x => x.EventId);
            foreach (var iEvent in groupEvent)
            {
                var groupSpecies = fiftyList.GroupBy(x => x.SpeciesCode);

                foreach (var species in groupSpecies)
                {
                    var subset = fiftyList.Where(x => x.EventId.Equals(iEvent.Key) && x.SpeciesCode.Equals(species.Key));
                    int count = subset.Count();
                    IncrementPointCounts<T>(iEvent.Key, dataEntryList, subset.ToList());
                }
            }

        }

        /// <summary>
        /// Increments the point counts in a <see cref="FiftyMeterDataEntry"/> object.
        /// </summary>
        /// <typeparam name="T">Must be of type FiftyMeterPointObservation</typeparam>
        /// <param name="pointSurveyId">The point survey id.</param>
        /// <param name="dataEntryList">The data entry list.</param>
        /// <param name="countList">The beyond50 list.</param>
        public static void IncrementPointCounts<T>(Guid pointSurveyId, List<FiftyMeterDataEntry> dataEntryList, List<T> countList) where T : FiftyMeterPointObservation
        {
            if (countList == null)
            {
                return;
            }
            foreach (T pointCount in countList)
            {
                FiftyMeterDataEntry entry = dataEntryList.Find(y => y.SpeciesCode.Equals(pointCount.SpeciesCode)
                    && y.PointSurveyId.Equals(pointSurveyId));
                if (entry == null)    // match wasn't found, must create a new entry
                {
                    entry = new FiftyMeterDataEntry()
                    {
                        Comments = pointCount.Comments,
                        PointSurveyId = pointSurveyId,
                        SpeciesCode = pointCount.SpeciesCode
                    };
                    dataEntryList.Add(entry);
                }

                Type ty = typeof(T);
                if (ty == typeof(PointCountBeyond50))
                {
                    entry.CountBeyond50++;
                }
                else if (ty == typeof(PointCountWithin50))
                {
                    entry.CountWithin50++;
                }
            }
        }

    }
}
