using System;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Abstract base class for all entities in the safnet.iba framework.
    /// </summary>
    public abstract class SafnetBaseEntity
    {
        /// <summary>
        /// Unique identifier for an object
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets (protected) a boolean value indicating if any member values have been changed since retrieval from a database.
        /// </summary>
        public bool NeedsDatabaseRefresh { get; protected set; }

        /// <summary>
        /// Assigns a new Globally Unique Identifier (Guid) to the object and returns that value.
        /// </summary>
        /// <returns>Guid</returns>
        public Guid SetNewId()
        {
            NeedsDatabaseRefresh = true;

            Id = Guid.NewGuid();
            return Id;
        }
    }
}
