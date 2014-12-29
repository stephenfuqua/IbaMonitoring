using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;
using System.Data;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides functionality common to mapper classes for Location-derived classes.
    /// </summary>
    public static class LocationBaseMapper
    {
        #region Public Methods

        /// <summary>
        /// Loads data from the specified reader into the specified object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="location">The location.</param>
        public static void Load(IDataReader reader, Location location)
        {
            location.GeoCoordinate = new Coordinate()
            {
                Latitude = new Degree()
                {
                    Value = reader.GetDecimalFromName("Latitude")
                },
                Longitude = new Degree()
                {
                    Value = reader.GetDecimalFromName("Longitude")
                }
            };
            location.Id = reader.GetGuidFromName("LocationId");
            location.Name = reader.GetStringFromName("LocationName");
        }

        #endregion

    }
}
