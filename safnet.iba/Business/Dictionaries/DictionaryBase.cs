using System.Collections.Generic;

namespace safnet.iba.Business.Dictionaries
{
    /// <summary>
    /// A singleton class implementing a byte, string dictionary containing values for a menu.
    /// </summary>
    public static class DictionaryBase
    {
        /// <summary>
        /// Gets the values for use in a dropdown menu
        /// </summary>
        /// <returns>Dictionary of byte, string values</returns>
        public static Dictionary<byte,string> GetDropdownValues(Dictionary<byte, string> instance)
        {
            Dictionary<byte, string> dropdown = new Dictionary<byte, string>();
            foreach (KeyValuePair<byte, string> pair in instance)
            {
                dropdown.Add(pair.Key, pair.Key.ToString() + " - " + pair.Value);
            }
            return dropdown;
        }
    }
}
