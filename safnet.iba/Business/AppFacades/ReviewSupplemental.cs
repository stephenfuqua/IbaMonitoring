using safnet.iba.Adapters;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using System.Collections.Generic;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Summary description for ReviewSupplemental
    /// </summary>
    public class ReviewSupplemental
    {
        #region Constructors

        public ReviewSupplemental(SupplementalObservation observation, IGlobalMap globalMap)
        {
            Observation = observation;

            Species species = globalMap.SpeciesList.Find(x => x.AlphaCode.Equals(Observation.SpeciesCode));
            if (species == null)
            {
                Warning = "Unknown species code. ";
            }
            else
            {
                SpeciesName = species.CommonName;
            }
        }

        #endregion

        #region Properties

        public string Comments
        {
            get { return Observation.Comments; }
        }

        public SupplementalObservation Observation { get; private set; }

        public string SpeciesCode
        {
            get { return Observation.SpeciesCode; }
        }

        public string SpeciesName { get; private set; }

        public string Warning { get; private set; }

        #endregion

        #region Public Methods

        public static List<ReviewSupplemental> GetReviewSupplementalList(IUserStateManager state, IGlobalMap globalMap)
        {
            // TODO: use of state should be moved to a presenter class

            List<ReviewSupplemental> reviewList = new List<ReviewSupplemental>();
            state.SiteVisit.SupplementalObservations.ForEach(x =>
            {
                reviewList.Add(new ReviewSupplemental(x, globalMap));
            });

            return reviewList;
        }

        #endregion

    }
}