using System;

namespace safnet.iba.Business.DataTransferObjects
{
    /// <summary>
    /// A Data Transfer Objecdt for populating map data
    /// </summary>
    public class MapCount
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public Decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public Decimal Longitude { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; set; }
    }
}
