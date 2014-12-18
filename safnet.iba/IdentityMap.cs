using System;
using System.Collections.Generic;
using safnet.iba.Business.Entities;

namespace safnet.iba
{
    /// <summary>
    /// Singleton class containing a Dictionary linking IDs to objects.
    /// </summary>
    public class IdentityMap
    {
        private static IdentityMap _instance;
        private Dictionary<Guid, SafnetBaseEntity> _dictionary;

        /// <summary>
        /// Creates a new instance of IdentityMap
        /// </summary>
        protected IdentityMap()
        {
            _dictionary = new Dictionary<Guid, SafnetBaseEntity>();
        }

        /// <summary>
        /// Returns the single instance of IdentityMap.
        /// </summary>
        /// <returns>IdentityMap</returns>
        public static IdentityMap GetInstance()
        {
            if (_instance == null)
            {
                _instance = new IdentityMap();
            }
            return _instance;
        }

        /// <summary>
        /// Overloads the [] indexer operator in order to return a value from the internal Dictionary.
        /// </summary>
        /// <param name="identity">Guid identifier value</param>
        /// <returns>SafnetBaseEntity instance</returns>
        public SafnetBaseEntity this[Guid identity]
        {
            get
            {
                return GetInstance()._dictionary[identity];
            }
            set
            {
                if (GetInstance()._dictionary.ContainsKey(identity))
                {
                    GetInstance()._dictionary[identity] = value;
                }
                else
                {
                    GetInstance()._dictionary.Add(identity, value);
                }
            }
        }
    }
}
