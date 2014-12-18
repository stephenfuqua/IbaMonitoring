using System.Collections.Generic;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Summary description for ReviewSupplemental
    /// </summary>
    public class ReviewSupplemental
    {
        #region Constructors

        public ReviewSupplemental(SupplementalObservation observation)
        {
            Observation = observation;

            Species species = GlobalMap.SpeciesList.Find(x => x.AlphaCode.Equals(Observation.SpeciesCode));
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

        public static List<ReviewSupplemental> GetReviewSupplementalList(IUserStateManager state)
        {

            List<ReviewSupplemental> reviewList = new List<ReviewSupplemental>();
            state.SiteVisit.SupplementalObservations.ForEach(x =>
            {
                reviewList.Add(new ReviewSupplemental(x));
            });

            return reviewList;
        }

        #endregion

    }
}