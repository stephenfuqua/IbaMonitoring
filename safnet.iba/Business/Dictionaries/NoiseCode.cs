using System.Collections.Generic;

namespace safnet.iba.Business.Dictionaries
{
    /// <summary>
    /// A singleton class implementing a byte, string dictionary containing NoiseCode values for a menu.
    /// </summary>
    public class NoiseCode : Dictionary<byte, string>
    {
        private static NoiseCode _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoiseCode"/> class.
        /// </summary>
        protected NoiseCode()
        {
            base.Add(0, "No background noise");
            base.Add(1, "Barely reduces hearing");
            base.Add(2, "Noticeable reduction of hearing");
            base.Add(3, "Prohibitive (greatly reduces hearing)");
        }

        /// <summary>
        /// Retrieves the instance of this singleton class
        /// </summary>
        /// <returns>Insance of NoiseCode</returns>
        public static NoiseCode Instance()
        {
            lock (typeof(WindCode))
            {
                if (_instance == null)
                {
                    _instance = new NoiseCode();
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
