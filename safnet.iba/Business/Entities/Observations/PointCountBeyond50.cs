using System;

namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// Models data from a single PointSurvey Count observation
    /// </summary>
    public class PointCountBeyond50 : FiftyMeterPointObservation
    {
        /// <summary>
        /// Factory method for creating a new PointCountBeyond50 object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="surveyId">ID of <see cref="FiftyMeterPointSurvey"/> instance</param>
        /// <returns>Instance of PointCountObservation</returns>
        public static PointCountBeyond50 CreateNewPointCountObservation(Guid surveyId)
        {
            PointCountBeyond50 s = new PointCountBeyond50()
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
        /// Guid for "Beyon 50m" point counts
        /// </summary>
        public static readonly Guid ObservationTypeGuid = new Guid("F7D5E189-233E-4B9E-B15A-4094640C3880");
    }
}
