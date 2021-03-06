﻿using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.Static;
using safnet.iba.Static.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Methods for obtaining various counts of species detected during site surveys.
    /// </summary>
    [DataObject(true)]
    public class ResultsFacade
    {
        /// <summary>
        /// Gets the distinct years in which site visits have been conducted.
        /// </summary>
        /// <returns></returns>
        public static SortedSet<int> GetAvailableYears()
        {
            return ResultsMapper.GetAvailableYears();
        }

        /// <summary>
        /// Gets the BMDE xml tree as a string.
        /// </summary>
        /// <returns>XML as string</returns>
        public static string GetBMDE()
        {
            return ResultsMapper.GetBMDE();
        }

        /// <summary>
        /// Retrieves total counts for all individuals across all seasons, divided by species code.
        /// </summary>
        /// <returns>DataTable</returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static DataTable MasterCount(int year)
        {
            DataSet set = ResultsMapper.GetSpeciesCount(year);

            if (set.Tables.Count != 1)
            {
                throw new IbaArgumentException("No data returned");
                // TODO: log this as an error
            }

            return set.Tables[0];
        }

        /// <summary>
        /// Builds a pivot table for species results, with park as the row variable and week as the column variable.
        /// </summary>
        /// <param name="speciesId">The species id.</param>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SpeciesSitePivotTable(Guid speciesId, int year)
        {
            DataTable resultTable = new DataTable();
            DataColumn locationName = new DataColumn("Species Name", typeof(string));
            resultTable.Columns.Add(locationName);
            resultTable.PrimaryKey = new DataColumn[] { locationName };
            resultTable.Columns.Add("Grand Total", typeof(int));

            Dictionary<int, string> weekTranslation = new Dictionary<int, string>();

            using (IDataReader reader = ResultsMapper.GetSpeciesBySite(speciesId, year))
            {
                while (reader.Read())
                {
                    int weekNumber = reader.GetIntFromName("Week");
                    string week = string.Empty;
                    if (weekTranslation.ContainsKey(weekNumber))
                    {
                        week = weekTranslation[weekNumber];
                    }
                    else
                    {
                        week = Conversion.GetDateForWeekNumber(weekNumber);
                    }
                    if (!resultTable.Columns.Contains(week))
                    {
                        resultTable.Columns.Add(week, typeof(string));
                    }

                    string speciesName = reader.GetValueFromName("CommonName").ToString();
                    int count = reader.GetIntFromName("SpeciesCount");

                    DataRow row = null;
                    if (!resultTable.Rows.Contains(speciesName))
                    {
                        row = resultTable.NewRow();
                        row["Species Name"] = speciesName;
                        resultTable.Rows.Add(row);
                    }
                    else
                    {
                        row = resultTable.Rows.Find(speciesName);
                    }

                    row[week] = count.ToString();
                    int currentTotal = 0;
                    if (!int.TryParse(row["Grand Total"].ToString(), out currentTotal))
                    {
                        currentTotal = 0;
                    }
                    row["Grand Total"] = currentTotal + count;


                }
            }
            return resultTable;
        }


        /// <summary>
        /// Builds a pivot table for species results, with park as the row variable and week as the column variable.
        /// </summary>
        /// <param name="speciesId">The species id.</param>
        /// <param name="combinedCount">The combined count.</param>
        /// <param name="year">The year.</param>
        /// <returns>DataTable</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable SiteBySpeciesPivotTable(Guid speciesId, SortedDictionary<DateTime, int> combinedCount, int year)
        {
            DataTable resultTable = new DataTable("Site");
            DataColumn locationName = new DataColumn("Site Name", typeof(string));
            resultTable.Columns.Add(locationName);
            resultTable.PrimaryKey = new DataColumn[] { locationName };
            resultTable.Columns.Add("Grand Total", typeof(int));
            resultTable.Columns.Add("Adjusted Count", typeof(string));

            Dictionary<int, string> weekTranslation = new Dictionary<int, string>();

            using (IDataReader reader = ResultsMapper.GetSiteBySpecies(speciesId, year))
            {
                while (reader.Read())
                {
                    // Populate the table containing the list of sites
                    int weekNumber = reader.GetIntFromName("Week");
                    string week = string.Empty;
                    if (weekTranslation.ContainsKey(weekNumber))
                    {
                        week = weekTranslation[weekNumber];
                    }
                    else
                    {
                        week = Conversion.GetDateForWeekNumber(weekNumber);
                    }
                    if (!resultTable.Columns.Contains(week))
                    {
                        resultTable.Columns.Add(week, typeof(string));
                    }

                    string speciesName = reader.GetValueFromName("LocationName").ToString();
                    int count = reader.GetIntFromName("SpeciesCount");

                    DataRow row = null;
                    if (!resultTable.Rows.Contains(speciesName))
                    {
                        row = resultTable.NewRow();
                        row["Site Name"] = speciesName;
                        resultTable.Rows.Add(row);
                    }
                    else
                    {
                        row = resultTable.Rows.Find(speciesName);
                    }

                    row[week] = count.ToString();
                    int currentTotal = 0;
                    if (!int.TryParse(row["Grand Total"].ToString(), out currentTotal))
                    {
                        currentTotal = 0;
                    }
                    row["Grand Total"] = currentTotal + count;

                    // Populate the table (indirectly) that will drive the combined counts used in a histogram chart
                    DateTime weekDate = DateTime.Parse(week + "/10");
                    if (!combinedCount.ContainsKey(weekDate))
                    {
                        combinedCount.Add(weekDate, 0);
                    }
                    combinedCount[weekDate] += count;
                }
            }

            // add the adjusted counts to the site listing
            Collection<AdjustedCountBySite> adjcounts = ResultsMapper.GetAdjustedCounts(speciesId, year);
            foreach (DataRow row in resultTable.Rows)
            {
                row["Adjusted Count"] = adjcounts.SingleOrDefault(x => x.SiteName.Equals(row["Site Name"])).AdjustedCount.ToString("0.000");
            }
            

            return resultTable;
        }

        /// <summary>
        /// Gets the species map counts
        /// </summary>
        /// <param name="speciesId">The species id.</param>
        /// <returns>List of <see cref="MapCount"/></returns>
        public static List<MapCount> GetSpeciesMap(Guid speciesId, int year)
        {
            List<MapCount> mapList = new List<MapCount>();
            using (IDataReader reader = ResultsMapper.GetSiteBySpecies_ForMap(speciesId, year))
            {
                while (reader.Read())
                {
                    mapList.Add(new MapCount()
                    {
                        Count = reader.GetIntFromName("SpeciesCount"),
                        Latitude = reader.GetDecimalFromName("Latitude"),
                        Longitude = reader.GetDecimalFromName("Longitude")
                    });
                }
            }

            return mapList;
        }

    }
}
