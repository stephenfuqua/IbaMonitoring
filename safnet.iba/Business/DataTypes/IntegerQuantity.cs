namespace safnet.iba.Business.DataTypes
{
    /// <summary>
    /// Models an integer and an associated unit.
    /// </summary>
    public class IntegerQuantity
    {
        /// <summary>
        /// Gets or sets the base value of the object.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the unit of the object.
        /// </summary>
        public string Unit { get; set; }
    }
}
