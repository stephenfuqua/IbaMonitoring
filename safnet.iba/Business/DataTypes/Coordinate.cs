
namespace safnet.iba.Business.DataTypes
{
    /// <summary>
    /// Abstract class for modeling a coordinate system
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or the latitude of a coordinate with respect to Earth.
        /// </summary>
        public Degree Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of a coordinate with respect to Earth.
        /// </summary>
        public Degree Longitude { get; set; }
    }
}
