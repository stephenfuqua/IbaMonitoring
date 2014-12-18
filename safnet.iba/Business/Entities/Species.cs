namespace safnet.iba.Business.Entities
{
    /// <summary>
    ///  Models an individual bird species.
    /// </summary>
    public class Species : SafnetBaseEntity
    {
        /// <summary>
        /// Gets or sets the four-letter code used ot distinguish one species from another (Ornithological Union standard).
        /// </summary>
        public string AlphaCode { get; set; }

        /// <summary>
        /// Gets or sets the common name for the species.
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Gets or sets the formal scientific name for the species.
        /// </summary>
        public string ScientificName { get; set; }

        /// <summary>
        /// Gets or sets a count that would be considered unusual for this species.
        /// </summary>
        public int WarningCount { get; set; }

        /// <summary>
        /// Factory method to create a new Species object, automatically creating a new unique identifier for the object.
        /// </summary>
        /// <returns><see cref="Species"/></returns>
        public static Species CreateNewSpecies()
        {
            

            Species s = new Species();
            s.SetNewId();
            return s;
        }
    }
}
