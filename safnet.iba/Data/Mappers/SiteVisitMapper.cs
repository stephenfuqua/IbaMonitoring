using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="SiteVisit"/> object
    /// </summary>
    public class SiteVisitMapper
    {
        #region Constants

        private const string SAVE_STORED_PROC = "SiteVisit_Save";

        private const string SELECT_STORED_PROC = "SiteVisit_Get";

        #endregion

        #region Fields

        private static List<SiteVisit> _fakeDatabase = new List<SiteVisit>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Removes a <see cref="SiteVisit"/> object from permanent storage
        /// </summary>
        /// <param name="siteVisit"><see cref="SiteVisit"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(SiteVisit siteVisit)
        {
            BaseMapper.DeleteObject(siteVisit);
        }

        /// <summary>
        /// Inserts a <see cref="SiteVisit"/> object into permanent storage
        /// </summary>
        /// <param name="visit"><see cref="SiteVisit"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(SiteVisit visit)
        {
            save(visit);
        }

        /// <summary>
        /// Retrieves a single <see cref="SiteVisit" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="SiteVisit"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static SiteVisit Select(Guid id)
        {
            guidGuardClause(id, "Select");

            return BaseMapper.LoadSingleObjectById<SiteVisit>(Load, SELECT_STORED_PROC, id);
        }

        /// <summary>
        /// Retrieves all <see cref="SiteVisit" /> objects from permanent storage containing 
        /// a particular species in a particular year
        /// </summary>
        /// <returns>List of <see cref="SiteVisit"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<SiteVisit> SelectAllForSite(Guid siteId)
        {
            guidGuardClause(siteId, "SelectAllForSite");

            List<QueryParameter> queryList = new List<QueryParameter>() {
                new QueryParameter("LocationId", siteId)
            };
            List<SiteVisit> list = BaseMapper.LoadAllQuery<SiteVisit>(Load, SELECT_STORED_PROC, queryList);
            return list;
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="SiteVisit"/> object
        /// </summary>
        /// <param name="visit"><see cref="SiteVisit"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(SiteVisit visit)
        {
            save(visit);
        }

        #endregion

        #region Private Methods

        private static void guidGuardClause(Guid id, string method)
        {
            if (id == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to SiteVisitMapper." + method);
            }
        }

        private static SiteVisit Load(IDataReader reader)
        {
            SiteVisit visit = new SiteVisit();

            EventBaseMapper.Load(reader, visit);

            visit.ObserverId = reader.GetGuidFromName("ObserverId");
            visit.RecorderId = reader.GetGuidFromName("RecorderId");
            visit.EndConditions.Id = reader.GetGuidFromName("EndConditionId");
            visit.StartConditions.Id = reader.GetGuidFromName("StartConditionId");
            visit.Comments = reader.GetStringFromName("Comments");
            visit.IsDataEntryComplete = reader.GetBoolFromName("IsDataEntryComplete");

            return visit;
        }

        /// <summary>
        /// Saves the specified  <see cref="SiteCondition"/> object.
        /// </summary>
        /// <param name="visit">The SiteConditions object to save.</param>
        private static void save(SiteVisit visit)
        {
            List<QueryParameter> queryList = EventBaseMapper.CreateSaveParameters(visit);
            queryList.AddRange(new List<QueryParameter>()
            {
                new QueryParameter("ObserverId", visit.ObserverId),
                new QueryParameter("RecorderId", visit.RecorderId),
                new QueryParameter("StartConditionId", visit.StartConditions.Id),
                new QueryParameter("EndConditionId", visit.EndConditions.Id),
                new QueryParameter("Comments", visit.Comments)   ,
                new QueryParameter("IsDataEntryComplete", visit.IsDataEntryComplete),
            });
            BaseMapper.SaveObject(visit, queryList);
        }

        #endregion

    }
}
