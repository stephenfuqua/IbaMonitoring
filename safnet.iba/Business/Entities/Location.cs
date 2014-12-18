using safnet.iba.Business.DataTypes;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Abstract class for modeling a Location with latitude, longitude, and name.
    /// </summary>
    public abstract class Location : SafnetBaseEntity
    {
        /// <summary>
        /// Gets or sets the fundamental name given to the Location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude and longitude for the location.
        /// </summary>
        public Coordinate GeoCoordinate { get; set; }
    }
}
