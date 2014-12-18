using System;

namespace safnet.iba.Business.DataTypes
{
    /// <summary>
    /// Represents a decimal degree in a coordinate system with +/- reference
    /// to a specific line (thus ranging from -180 to +180)
    /// </summary>
    public class Degree
    {
        private Decimal _value;

        /// <summary>
        /// Gets or sets the base degree value, ensuring that it is in the expected range.
        /// </summary>
        public Decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value >= -180m && value <= 180m)
                {
                    _value = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Illegal Degree value: must be between -180 and 180");
                }
            }
        }

        /// <summary>
        /// Converts the base value to a string with five significant digits and a degree sign.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return _value.ToString("###.#####°");
        }
    }
}
