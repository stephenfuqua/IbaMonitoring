using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Maps statistical result stored procedures.
    /// </summary>
    public static class ResultsMapper
    {
        /// <summary>
        /// Gets the distinct years in which site visits have been conducted.
        /// </summary>
        /// <returns></returns>
        public static SortedSet<int> GetAvailableYears()
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_AvailableYears", parameters, out iba, out cmd);
            SortedSet<int> set = new SortedSet<int>();

            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    set.Add(reader.GetInt32(0));
                }
            }

            return set;
        }

        /// <summary>
        /// Gets the relative abundance for all species.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static Collection<AdjustedCountBySite> GetRelativeAbundance(int year)
        {
            return GetAdjustedCounts(Guid.Empty, year);
        }

        /// <summary>
        /// Gets the relative abundance for a species.
        /// </summary>
        /// <param name="speciesId">The species Guid.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// Collection of <see cref="AdjustedCountBySite"/>
        /// </returns>
        public static Collection<AdjustedCountBySite> GetAdjustedCounts(Guid speciesId, int year)
        {
            List<QueryParameter> parameters = new List<QueryParameter>() 
            { 
                new QueryParameter("SpeciesId", speciesId),
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_Adjusted", parameters, out iba, out cmd);

            using (IDataReader reader = iba.ExecuteReader(cmd))
            {
                Collection<AdjustedCountBySite> list = new Collection<AdjustedCountBySite>();
                while (reader.Read())
                {
                    list.Add(new AdjustedCountBySite()
                    {
                        AdjustedCount = reader.GetDecimalFromName("RelativeAbundance"),
                        CommonName = reader.GetStringFromName("CommonName"),
                        SiteName = reader.GetStringFromName("SiteName")
                    });
                }
                return list;
            }
        }




        /// <summary>
        /// Retrieves XML data in a BMDE format for exchange with the Avian Knowledge Network (AKN).
        /// </summary>
        /// <returns>string containing XML observation data</returns>
        public static string GetBMDE()
        {
            using (SqlConnection sqlConnection = (SqlConnection)DatabaseFactory.CreateDatabase().CreateConnection())
            {
                sqlConnection.Open();
                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandText = "Results_BMDE";
                command.CommandType = CommandType.StoredProcedure;

                using (XmlReader xmlReader = command.ExecuteXmlReader())
                {
                    if (xmlReader.Read())
                    {
                        return xmlReader.ReadInnerXml();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves all of birds seen in Supplemental Observations for a particular Site
        /// </summary>
        /// <param name="siteId">Site GUID</param>
        /// <param name="year">The year.</param>
        /// <returns>DataSet</returns>
        public static DataSet GetSiteSupplemental(Guid siteId, int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>()
            { 
                new QueryParameter("SiteId", siteId),
                 new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SiteSupplemental", parameterList, out iba, out cmd);

            return iba.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Retrieves community measures at each site during breeding.
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetCommunityBreeding(int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>() 
            { 
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SiteMeasures_Breeding", parameterList, out iba, out cmd);

            return iba.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Retrieves community measures at each site during migration.
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetCommunityMigration(int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>() 
            { 
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SiteMeasures_Migration", parameterList, out iba, out cmd);

            return iba.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// Retrieves the total observation count for a particular species, at all sites.
        /// </summary>
        /// <param name="speciesId">Species GUID</param>
        /// <returns>IDataReader</returns>
        public static IDataReader GetSiteBySpecies(Guid speciesId, int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>() 
            { 
                new QueryParameter("SpeciesId", speciesId),
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SiteBySpecies", parameterList, out iba, out cmd);

            return iba.ExecuteReader(cmd);
        }

        /// <summary>
        /// Retrieves the total observation count for each species at a particular Site.
        /// </summary>
        /// <param name="siteId">Site GUID</param>
        /// <returns>IDataReader</returns>
        public static IDataReader GetSpeciesBySite(Guid siteId, int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>()
            { 
                new QueryParameter("SiteId", siteId),
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SpeciesBySite", parameterList, out iba, out cmd);

            return iba.ExecuteReader(cmd);
        }

        /// <summary>
        /// Retrieves the total observation count for a particular species, grouped by SamplingPoint.
        /// </summary>
        /// <param name="speciesId">Species GUID</param>
        /// <returns>IDataReader</returns>
        public static IDataReader GetSiteBySpecies_ForMap(Guid speciesId, int year)
        {
            List<QueryParameter> parameterList = new List<QueryParameter>() 
            { 
                new QueryParameter("SpeciesId", speciesId),
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SiteBySpecies_ForMap", parameterList, out iba, out cmd);

            return iba.ExecuteReader(cmd);
        }


        /// <summary>
        /// Retrieves the total observation count for all species, at a particular site
        /// </summary>
        /// <param name="siteId">Site GUID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetSpeciesCountBySite(Guid siteId, int year)
        {
            DataSet results = null;

            List<QueryParameter> parameterList = new List<QueryParameter>() 
            { 
                new QueryParameter("SiteId", siteId),
                new QueryParameter("Year", year)
            };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SpeciesCountBySite", parameterList, out iba, out cmd);

            results = iba.ExecuteDataSet(cmd);

            return results;
        }

        /// <summary>
        /// Retrieves the total observation count for all species, across all sites.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>DataSet</returns>
        public static DataSet GetSpeciesCount(int year)
        {
            DataSet results = null;

            List<QueryParameter> parameterList = new List<QueryParameter>() { new QueryParameter("Year", year) };
            Database iba = null;
            DbCommand cmd = null;
            BaseMapper.CreateDatabaseCommand("Results_SpeciesCount", parameterList, out iba, out cmd);

            results = iba.ExecuteDataSet(cmd);

            return results;
        }
    }
}
