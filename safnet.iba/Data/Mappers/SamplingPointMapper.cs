using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="SamplingPoint"/> object
    /// </summary>
    public class SamplingPointMapper
    {
        /// <summary>
        /// Updates the permanent storage for a <see cref="SamplingPoint"/> object.
        /// </summary>
        /// <param name="point"><see cref="SamplingPoint"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(SamplingPoint point)
        {
            saveSamplingPoint(point);
        }

        /// <summary>
        /// Inserts a <see cref="SamplingPoint"/> object into permanent storage.
        /// </summary>
        /// <param name="point"><see cref="SamplingPoint"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(SamplingPoint point)
        {
            saveSamplingPoint(point);
        }

        private static void saveSamplingPoint(SamplingPoint point)
        {

            List<QueryParameter> parmList = new List<QueryParameter>()
            {
                new QueryParameter("Id", point.Id),
                new QueryParameter("Longitude", point.GeoCoordinate.Longitude.Value),
                new QueryParameter("Latitude", point.GeoCoordinate.Latitude.Value),
                new QueryParameter("LocationName", point.Name),
                new QueryParameter("ParentLocationId", point.SiteId)
            };
            BaseMapper.SaveObject(point, parmList);
        }

        /// <summary>
        /// Removes a <see cref="SamplingPoint"/> object from permanent storage.
        /// </summary>
        /// <param name="point"><see cref="SamplingPoint"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(SamplingPoint point)
        {
            BaseMapper.DeleteObject(point);
        }

        /// <summary>
        /// Retrieves all <see cref="SamplingPoint" /> objects from permanent storage.
        /// </summary>
        /// <returns>List of <see cref="SamplingPoint"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<SamplingPoint> SelectAll()
        {
            return BaseMapper.LoadAll(Load, SELECT_STORED_PROC);
        }

        /// <summary>
        /// Retrieves all <see cref="SamplingPoint"/> objects for a particular Site from permanent storage.
        /// </summary>
        /// <returns>List of <see cref="SamplingPoint"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static List<SamplingPoint> SelectAllForSite(Guid siteId)
        {
            QueryParameter parm = new QueryParameter("ParentId", siteId);
            return BaseMapper.LoadAllQuery(Load, SELECT_STORED_PROC, new List<QueryParameter>() { parm });
        }

        
        /// <summary>
        /// Retrieves a specific <see cref="SamplingPoint"/> object by its unique identifier.
        /// </summary>
        /// <param name="pointId">The point id.</param>
        /// <returns>Instance of <see cref="SamplingPoint"/></returns>
        public static SamplingPoint Select(Guid pointId)
        {
            return BaseMapper.LoadSingleObjectById<SamplingPoint>(Load, SELECT_STORED_PROC, pointId);
        }


        private const string SELECT_STORED_PROC = "SamplingPoint_Get";

        private static SamplingPoint Load(IDataReader reader)
        {
            SamplingPoint site = new SamplingPoint();
            LocationBaseMapper.Load(reader, site);

            site.SiteId = reader.GetGuidFromName("ParentLocationId");

            return site;
        }
    }
}
