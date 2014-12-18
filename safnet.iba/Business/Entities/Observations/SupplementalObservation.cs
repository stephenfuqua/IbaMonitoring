using System;

namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// Models the data for a Supplemental Observation that is made during a site visit but not during a 
    /// specific point survey.
    /// </summary>
    public class SupplementalObservation : Observation
    {

        /// <summary>
        /// Factory method for creating a new SupplementalObservation object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="eventId">ID of <see cref="SupplementalObservation"/> instance</param>
        /// <returns>Instance of SupplementalObservation</returns>
        public static SupplementalObservation CreateNewSupplementalObservation(Guid eventId)
        {
            SupplementalObservation s = new SupplementalObservation()
            {
                EventId = eventId
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
        /// Guid for Supplemental Observations
        /// </summary>
        public static readonly Guid ObservationTypeGuid = new Guid("CE0D1C95-C0C6-4D1E-8D5A-ADB8D0A297AF");
    }
}
