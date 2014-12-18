using System.Collections.Generic;

namespace safnet.iba.Business.Dictionaries
{
    /// <summary>
    /// A singleton class implementing a byte, string dictionary containing SkyCode values for a menu.
    /// </summary>
    public class SkyCode : Dictionary<byte, string>
    {
        private static SkyCode _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyCode"/> class.
        /// </summary>
        protected SkyCode()
        {
            base.Add(0, "Clear or a few clouds");
            base.Add(1, "Partly cloudy (scattered)");
            base.Add(2, "Cloudy (broken) or overcast");
            base.Add(3, "Fog or smoke");
            base.Add(4, "Drizzle");
            base.Add(5, "Snow");
            base.Add(6, "Showers");
        }

        /// <summary>
        /// Retrieves the instance of this singleton class
        /// </summary>
        /// <returns>Insance of SkyCode</returns>
        public static SkyCode Instance()
        {
            lock (typeof(SkyCode))
            {
                if (_instance == null)
                {
                    _instance = new SkyCode();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Gets the values for use in a dropdown menu
        /// </summary>
        /// <returns>Dictionary of byte, string values</returns>
        public static Dictionary<byte, string> GetDropdownValues()
        {
            return DictionaryBase.GetDropdownValues(Instance());
        }
    }
}
