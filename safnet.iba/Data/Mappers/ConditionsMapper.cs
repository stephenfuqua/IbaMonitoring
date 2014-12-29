using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="SiteCondition"/> object
    /// </summary>
    [DataObject(true)]
    public class ConditionsMapper
    {
        #region Constants

        private const string SAVE_STORED_PROC = "SiteCondition_Save";

        private const string SELECT_STORED_PROC = "SiteCondition_Get";

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a <see cref="SiteCondition"/> object from permanent storage
        /// </summary>
        /// <param name="conditions"><see cref="SiteCondition"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(SiteCondition conditions)
        {
            BaseMapper.DeleteObject(conditions);
        }

        /// <summary>
        /// Inserts a <see cref="SiteCondition"/> object into permanent storage
        /// </summary>
        /// <param name="conditions"><see cref="SiteCondition"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(SiteCondition conditions)
        {
            save(conditions);
        }

        /// <summary>
        /// Retrieves all <see cref="SiteCondition"/> objects from permanent storage for a particular <see cref="SiteVisit"/>
        /// </summary>
        /// <param name="siteVisitId">The site visit id.</param>
        /// <returns>
        /// List of <see cref="SiteCondition"/> objects
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<SiteCondition> Select_BySiteVisitId(Guid siteVisitId)
        {
            if (siteVisitId == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to Site.Select_BySiteVisitId");
            }

            QueryParameter parm = new QueryParameter("SiteVisitId", siteVisitId);
            List<SiteCondition> list = BaseMapper.LoadAllQuery<SiteCondition>(Load, SELECT_STORED_PROC, new List<QueryParameter>() { parm });
            return list;
        }

        /// <summary>
        /// Retrieves a single <see cref="SiteCondition" /> object from permanent storage based on search by ID
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// List of <see cref="SiteCondition"/> objects
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SiteCondition Select(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to SiteCondition.Select");
            }

            return BaseMapper.LoadSingleObjectById<SiteCondition>(Load, SELECT_STORED_PROC, id);
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="SiteCondition"/> object
        /// </summary>
        /// <param name="conditions"><see cref="SiteCondition"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(SiteCondition conditions)
        {
            save(conditions);
        }

        #endregion

        #region Private Methods

        private static SiteCondition Load(IDataReader reader)
        {
            SiteCondition conditions = new SiteCondition();
            conditions.Id = reader.GetGuidFromName("ConditionId");
            conditions.SiteVisitId = reader.GetGuidFromName("SiteVisitId");
            conditions.Sky = reader.GetByteFromName("Sky");
            conditions.Temperature = new Temperature() { Units = reader.GetStringFromName("Scale"), Value = reader.GetIntFromName("Temperature") };
            conditions.Wind = reader.GetByteFromName("Wind");

            return conditions;
        }

        /// <summary>
        /// Saves the specified  <see cref="SiteCondition"/> object.
        /// </summary>
        /// <param name="conditions">The SiteConditions object to save.</param>
        private static void save(SiteCondition conditions)
        {
            List<QueryParameter> queryList = new List<QueryParameter>()
            {
                new QueryParameter("Id", conditions.Id),
                new QueryParameter("Scale", conditions.Temperature.Units),
                new QueryParameter("SiteVisitId", conditions.SiteVisitId),
                new QueryParameter("Sky", conditions.Sky),
                new QueryParameter("Temperature", conditions.Temperature.Value),
                new QueryParameter("Wind", conditions.Wind)
            };
            BaseMapper.SaveObject(conditions, queryList);
        }

        #endregion

    }
}
