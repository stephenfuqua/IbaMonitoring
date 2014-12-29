using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="Species"/> object
    /// </summary>
    [DataObject(true)]
    public class SpeciesMapper
    {
        #region Constants

        private const string SELECT_STORED_PROC = "Species_Get";

        #endregion

        #region Fields

        private static List<Species> _fakeDatabase = new List<Species>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a <see cref="Species"/> object from permanent storage
        /// </summary>
        /// <param name="species"><see cref="Species"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(Species species)
        {
            BaseMapper.DeleteObject(species);
        }

        /// <summary>
        /// Inserts a <see cref="Species"/> object into permanent storage
        /// </summary>
        /// <param name="species"><see cref="Species"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(Species species)
        {
            save(species);
        }

        /// <summary>
        /// Retrieves all <see cref="Species" /> objects from permanent storage
        /// </summary>
        /// <returns>List of <see cref="Species"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<Species> SelectAll()
        {
            return BaseMapper.LoadAll<Species>(Load, SELECT_STORED_PROC);
        }

        /// <summary>
        /// Retrieves a single <see cref="Species" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="Species"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Species Select(Guid id)
        {
            guidGuardClause(id, "Select");

            return BaseMapper.LoadSingleObjectById<Species>(Load, SELECT_STORED_PROC, id);
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="Species"/> object
        /// </summary>
        /// <param name="species"><see cref="Species"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(Species species)
        {
            save(species);
        }

        #endregion

        #region Private Methods

        private static void guidGuardClause(Guid id, string method)
        {
            if (id == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to SpeciesMapper." + method);
            }
        }

        private static Species Load(IDataReader reader)
        {
            Species person = new Species()
            {
                AlphaCode = reader.GetStringFromName("AlphaCode"),
                CommonName = reader.GetStringFromName("CommonName"),
                Id = reader.GetGuidFromName("SpeciesId"),
                ScientificName = reader.GetStringFromName("ScientificName"),
                WarningCount = reader.GetIntFromName("WarningCount")
            };
            return person;
        }

        private static void save(Species species)
        {
            BaseMapper.SaveObject(species, new List<QueryParameter>()
            {
                new QueryParameter("AlphaCode", species.AlphaCode),
                new QueryParameter("CommonName", species.CommonName),
                new QueryParameter("ScientificName", species.ScientificName),
                new QueryParameter("Id", species.Id),
                new QueryParameter("WarningCount", species.WarningCount),
            });
        }

        #endregion

    }
}
