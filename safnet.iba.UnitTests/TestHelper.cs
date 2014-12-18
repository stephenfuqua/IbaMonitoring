using System;

namespace safnet.iba.UnitTests
{
    /// <summary>
    /// Methods that help speed up creation of unit tests that interact with database
    /// </summary>
    public static class TestHelper
    {

        #region Constants

        public const string SPECIES_1_CODE = "BHCO";

        public const string SPECIES_1_COMMON = "Brown-headed Cowbird";

        public const string SPECIES_1_ID = "87606168-3ac7-402a-8ae6-4f6905555582";

        public const string SPECIES_1_SCIENTIFIC = "Molothrus ater";

        public const byte SPECIES_1_WARNING = 2;

        public const string SPECIES_2_CODE = "RWBL";

        public const string SPECIES_2_COMMON = "Red-winged Blackbird";

        public const string SPECIES_2_ID = "87606168-3ac7-402a-8ae6-4f6905555583";

        public const string SPECIES_2_SCIENTIFIC = "Gelaius phoeniceus";

        public const byte SPECIES_2_WARNING = 5;

        public const string SPECIES_3_CODE = "HESP";

        public const string SPECIES_3_COMMON = "Henslow's Sparrow";

        public const string SPECIES_3_ID = "87606168-3ac7-402a-8ae6-4f6905555571";

        public const string SPECIES_3_SCIENTIFIC = "Ammodramus henslowii";

        public const byte SPECIES_3_WARNING = 10;

        #endregion

        #region Fields

        
        public static readonly Guid TestGuid1 = new Guid("87606168-3ac7-402a-8ae6-4f6905555581");

        public static readonly Guid TestGuid2 = new Guid("87606168-3ac7-402a-8ae6-4f6905555582");

        public static readonly Guid TestGuid3 = new Guid("87606168-3ac7-402a-8ae6-4f6905555583");

        public static readonly Guid TestGuid4 = new Guid("87606168-3ac7-402a-8ae6-4f6905555584");
        public static readonly Guid TestGuid5 = new Guid("77606168-3ac7-402a-8ae6-4f6905555584");
        public static readonly Guid TestGuid6 = new Guid("86606168-3ac7-402a-8ae6-4f6905555584");

        public static readonly Guid TestParentGuid = new Guid("87606168-3ac7-402a-8ae6-4f6905555580");

        #endregion




        /// <summary>
        /// Extension method for extracting year month day hour minute  AM/PM from time for simplifying unit test comparison.s
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string TestString(this DateTime value)
        {
            return value.ToString("dt");
        }
    }
}
