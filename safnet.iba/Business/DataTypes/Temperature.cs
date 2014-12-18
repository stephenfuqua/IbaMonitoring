namespace safnet.iba.Business.DataTypes
{
    /// <summary>
    /// Models a temperature reading
    /// </summary>
    public class Temperature
    {
        #region Constants

        /// <summary>
        /// Constant letter C for degrees Celsius
        /// </summary>
        public const string CELSIUS = "C";

        /// <summary>
        /// constant letter F for degrees Fahrenheit
        /// </summary>
        public const string FAHRENHEIT = "F";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the units for the temperature value.
        /// </summary>
        /// <value>The units.</value>
        public string Units { get; set; }

        /// <summary>
        /// Gets or sets the temperature value.
        /// </summary>
        /// <value>The value.</value>
        public int Value { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> with value, degree sign, and units.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value.ToString() + "&deg; " + Units;
        }

        #endregion

    }
}
