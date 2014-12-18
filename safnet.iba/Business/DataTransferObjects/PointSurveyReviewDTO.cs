namespace safnet.iba.Business.DataTransferObjects
{
    /// <summary>
    /// Data Transfer Object containing string values "flattening" out a particular point survey
    /// </summary>
    public class PointSurveyReviewDTO
    {
        /// <summary>
        /// Gets or sets the name of the <see cref="safnet.iba.Business.Entities.SamplingPoint"/>.
        /// </summary>
        public string SamplingPointName { get; set; }

        /// <summary>
        /// Gets or sets the AlphaCode of the <see cref="safnet.iba.Business.Entities.Species"/> surveyed.
        /// </summary>
        public string AlphaCode { get; set; }

        /// <summary>
        /// Gets or sets tne name of the <see cref="safnet.iba.Business.Entities.Species"/> surveyed.
        /// </summary>
        public string SpeciesName { get; set; }

        /// <summary>
        /// Gets or sets tne count observed within 50 meters of the point.
        /// </summary>
        public string Within50 { get; set; }

        /// <summary>
        /// Gets or sets the count observed beyond 50 meters of the point.
        /// </summary>
        public string Beyond50 { get; set; }

        /// <summary>
        /// Gets or sets any additional comments about observation.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets optional warning text for observatio counts that seem to be beyond normal expectations.
        /// </summary>
        public string Warning { get; set; }
    }
}
