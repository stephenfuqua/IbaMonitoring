using System;

namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// Models data from a single PointSurvey Count observation
    /// </summary>
    public abstract class Observation : SafnetBaseEntity
    {
        /// <summary>
        /// Gets or sets any additional comments about the observation.
        /// </summary>
        public string Comments { get; set; }


        /// <summary>
        /// Gets or sets CodeName for the observed <see cref="Species"/>.
        /// </summary>
        /// <value>The species code.</value>
        public string SpeciesCode { get; set; }

        /// <summary>
        /// Gets or sets the ID for the <see cref="Event"/> during which this Observation was made.
        /// </summary>
        /// <value>The Event ID.</value>
        public Guid EventId { get ; set; }

        /// <summary>
        /// Gets or sets the observation type id.
        /// </summary>
        /// <value>The observation type id.</value>
        public abstract Guid ObservationTypeId { get; }

        /// <summary>
        /// Unique identifier for an object - overriden to be an Int32 for <see cref="Observation"/> objects.
        /// </summary>
        /// <value></value>
        public new long Id { get; set; }
    }
}
