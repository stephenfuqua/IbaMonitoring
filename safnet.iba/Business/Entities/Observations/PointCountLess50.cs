using System;

namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// Models data from a single PointSurvey Count observation
    /// </summary>
    public class PointCountWithin50 : FiftyMeterPointObservation
    {
        /// <summary>
        /// Factory method for creating a new PointCountLess50 object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="surveyId">ID of <see cref="FiftyMeterPointSurvey"/> instance</param>
        /// <returns>Instance of PointCountObservation</returns>
        public static PointCountWithin50 CreateNewPointCountObservation(Guid surveyId)
        {
            PointCountWithin50 s = new PointCountWithin50()
            {
                EventId = surveyId
            };
            s.SetNewId();

            return s;
        }

        /// <summary>
        /// Gets or sets the observation type id.
        /// </summary>
        /// <value>The observation type id.</value>
        public override Guid ObservationTypeId
        {
            get
            {
                return ObservationTypeGuid;
            }
        }

        /// <summary>
        /// Guid for "Less than 50" point count
        /// </summary>
        public static readonly Guid ObservationTypeGuid = new Guid("8E4055BC-7644-4670-8C1D-648BF81519D8");
    }
}
