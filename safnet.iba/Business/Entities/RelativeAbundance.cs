namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Basic container for holding species count by site.
    /// </summary>
    public struct AdjustedCountBySite
    {
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the common name of a species.
        /// </summary>
        /// <value>The name of the common.</value>
        public string CommonName { get; set; }

        /// <summary>
        /// Gets or sets the adjusted count.
        /// </summary>
        /// <value>The abundance.</value>
        public decimal AdjustedCount { get; set; }
    }
}
