using safnet.iba.Adapters;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Business.Factories;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.Business.AppFacades
{
    public class ReviewObservation
    {


        #region Constructors

        public ReviewObservation(FiftyMeterPointSurvey survey, FiftyMeterDataEntry entry, IUserStateManager state, IGlobalMap globalMap)
        {
            State = state;
            DataEntry = entry;

            SamplingPointName =
                state.PointsRemaining.Union(state.PointsCompleted).Single(x => x.Id.Equals(survey.LocationId)).Name;

            Species species = globalMap.SpeciesList.Find(x => x.AlphaCode.Equals(entry.SpeciesCode));
            if (species == null)
            {
                Warning = "Unknown species code. ";
            }
            else
            {
                SpeciesName = species.CommonName;

                if (entry.CountBeyond50 > species.WarningCount)
                {
                    Warning += "Unusually high count beyond 50m, please verify. ";
                }
                if (entry.CountWithin50 > species.WarningCount)
                {
                    Warning += "Unusually high count within 50m, please verify. ";
                }
            }
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the user state.
        /// </summary>
        /// <value>The state.</value>
        protected IUserStateManager State { get; set; }

        /// <summary>
        /// Gets the count beyond 50 m.
        /// </summary>
        /// <value>The count beyond50.</value>
        public string CountBeyond50
        {
            get { return DataEntry.CountBeyond50.ToString(); }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments
        {
            get { return DataEntry.Comments; }
        }

        /// <summary>
        /// Gets or sets the data entry.
        /// </summary>
        /// <value>The data entry.</value>
        public FiftyMeterDataEntry DataEntry { get; private set; }

        /// <summary>
        /// Gets or sets the name of the sampling point.
        /// </summary>
        /// <value>The name of the sampling point.</value>
        public string SamplingPointName { get; set; }

        /// <summary>
        /// Gets the species code.
        /// </summary>
        /// <value>The species code.</value>
        public string SpeciesCode
        {
            get { return DataEntry.SpeciesCode; }
        }

        /// <summary>
        /// Gets or sets the name of the species.
        /// </summary>
        /// <value>The name of the species.</value>
        public string SpeciesName { get; private set; }

        /// <summary>
        /// Gets or sets the warning text.
        /// </summary>
        /// <value>The warning.</value>
        public string Warning { get; private set; }

        /// <summary>
        /// Gets the count within 50 m.
        /// </summary>
        /// <value>The count within50.</value>
        public string CountWithin50
        {
            get { return DataEntry.CountWithin50.ToString(); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Factory method to build and retrieve all observation information for a particular site visit, for use on the review page.
        /// </summary>
        /// <returns></returns>
        public static List<ReviewObservation> GetReviewList(IUserStateManager state, IGlobalMap globalMap)
        {
            // TODO: use of state should be moved to a presenter class
            List<ReviewObservation> reviewList = new List<ReviewObservation>();
            state.SiteVisit.PointSurveys.OrderBy(x => x.StartTimeStamp).ToList().ForEach(x =>
            {
                List<FiftyMeterDataEntry> entryList = new List<FiftyMeterDataEntry>();
                FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountBeyond50>(
                    x.Observations.OfType<PointCountBeyond50>().ToList<PointCountBeyond50>(), entryList);
                FiftyMeterDataEntryFactory.CreateEntriesFromPointCounts<PointCountWithin50>(
                    x.Observations.OfType<PointCountWithin50>().ToList<PointCountWithin50>(), entryList);

                entryList.ForEach(y =>
                {
                    reviewList.Add(new ReviewObservation(x, y, state, globalMap));
                });
            });

            return reviewList.OrderBy(x => x.SamplingPointName).ToList();
        }

        #endregion

    }
}