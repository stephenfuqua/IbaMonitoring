using Microsoft.Practices.EnterpriseLibrary.Data;
using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="Site"/> object
    /// </summary>(
    public class SiteMapper
    {

        /// <summary>
        /// Gets the Site's boundaries.
        /// </summary>
        /// <param name="site">The site.</param>
        public static void GetBoundaries(Site site)
        {
            List<QueryParameter> queryList = new List<QueryParameter>()
            {
                new QueryParameter("SiteId", site.Id)
            };

            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Site_Get_Boundaries", queryList, out iba, out cmd);

            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    site.Boundaries.Enqueue(new Coordinate()
                    {
                        Latitude = new Degree() { Value = reader.GetDecimalFromName("Latitude") },
                        Longitude = new Degree() { Value = reader.GetDecimalFromName("Longitude") }
                    });
                }
            }
        }

        /// <summary>
        /// Updates the permanent storage for a <see cref="Site"/> object
        /// </summary>
        /// <param name="site"><see cref="Site"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(Site site)
        {
            save(site);
        }

        /// <summary>
        /// Inserts a <see cref="Site"/> object into permanent storage
        /// </summary>
        /// <param name="site"><see cref="Site"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(Site site)
        {
            save(site);
        }

        /// <summary>
        /// Saves the specified site.
        /// </summary>
        /// <param name="site">The site.</param>
        private static void save(Site site)
        {
            List<QueryParameter> queryList = new List<QueryParameter>()
            {
                new QueryParameter("CodeName",site.CodeName),
                new QueryParameter("Latitude",site.GeoCoordinate.Latitude.Value),
                new QueryParameter("Longitude", site.GeoCoordinate.Longitude.Value),
                new QueryParameter("Id", site.Id),
                new QueryParameter("LocationName", site.Name)
            };
            BaseMapper.SaveObject(site, queryList);
        }

        /// <summary>
        /// Removes a <see cref="Site"/> object from permanent storage
        /// </summary>
        /// <param name="site"><see cref="Site"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static void Delete(Site site)
        {
            BaseMapper.DeleteObject(site);
        }

        /// <summary>
        /// Retrieves all <see cref="Site" /> objects from permanent storage.
        /// </summary>
        /// <returns>List of <see cref="Site"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<Site> SelectAll()
        {
            return BaseMapper.LoadAll<Site>(Load, SELECT_STORED_PROC);
        }

        private const string SELECT_STORED_PROC = "Site_Get";
        private const string SAVE_STORED_PROC = "Site_Save";

        /// <summary>
        /// Retrieves a specific <see cref="Site"/> object from permanent storage.
        /// </summary>
        /// <param name="siteId">Guid for a Site</param>
        /// <returns>Instance of Site</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Site Select(Guid siteId)
        {
            if (siteId == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to Site.Select");
            }

            return BaseMapper.LoadSingleObjectById<Site>(Load, SELECT_STORED_PROC, siteId);
        }

        /// <summary>
        /// Retrieves a s specific <see cref="Site"/> object from permanent storage.
        /// </summary>
        /// <param name="codeName">Site's codename</param>
        /// <returns>Instance of Site</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Site Select_ByCodeName(string codeName)
        {
            if (string.IsNullOrEmpty(codeName))
            {
                throw new IbaArgumentException("Empty string passed to Site.Select_ByCodeName");
            }
            if (codeName.Length > 10)
            {
                throw new IbaArgumentException("Invalid CodeName length in call to Site.Select_ByCodename. Max characters: 10");
            }

            QueryParameter parm = new QueryParameter("CodeName", codeName);
            List<Site> list = BaseMapper.LoadAllQuery<Site>(Load, SELECT_STORED_PROC, new List<QueryParameter>() { parm });
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null as Site;
            }
        }

        private static Site Load(IDataReader reader)
        {
            Site site = new Site();
            site.CodeName = reader.GetStringFromName("CodeName");
            LocationBaseMapper.Load(reader, site);
            return site;
        }
    }
}
