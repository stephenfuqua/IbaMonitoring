namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// An inheritance heirarchy, defining all Observations that are part of a 50m point count survey.
    /// </summary>
    public abstract class FiftyMeterPointObservation : Observation
    {
        /// <summary>
        /// Gets or sets a value indicating whether the record should be deleted.
        /// </summary>
        /// <value><c>true</c> if [mark for deletion]; otherwise, <c>false</c>.</value>
        public bool MarkForDeletion { get; set; }

    }
}
