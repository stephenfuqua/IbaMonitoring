using System.Collections.Generic;

namespace safnet.iba.Business.Dictionaries
{
    /// <summary>
    /// A singleton class implementing a byte, string dictionary containing WindCode values for a menu.
    /// </summary>
    public class WindCode : Dictionary<byte,string>
    {
        private static WindCode _instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindCode"/> class.
        /// </summary>
        protected WindCode() : base()
        {
            base.Add(0, "Calm");
            base.Add(1, "Slight");
            base.Add(2, "Wind felt on face");
            base.Add(3, "Leaves in constant motion");
            base.Add(4, "Raises dust; small branches move");
            base.Add(5, "Small trees sway");
            base.Add(6, "Greater than 15 mph");
        }

        /// <summary>
        /// Retrieves the instance of this singleton class
        /// </summary>
        /// <returns>Insance of WindCode</returns>
        public static WindCode Instance()
        {
            lock (typeof(WindCode))
            {
                if (_instance == null)
                {
                    _instance = new WindCode();
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
