using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static.Extensions;

namespace safnet.iba.Data.Mappers
{
    /// <summary>
    /// Provides database mapping for the <see cref="FiftyMeterPointSurvey"/> object
    /// </summary>
    [DataObject(true)]
    public static class PointSurveyMapper
    {

        #region Constants

        private const string TABLE_NAME = "PointSurvey";

        private const string SELECT_STORED_PROC = "PointSurvey_Get";

        #endregion


        /// <summary>
        /// Updates the permanent storage for a <see cref="FiftyMeterPointSurvey"/> object
        /// </summary>
        /// <param name="survey"><see cref="FiftyMeterPointSurvey"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static void Update(FiftyMeterPointSurvey survey)
        {
            save(survey);
        }

        /// <summary>
        /// Inserts a <see cref="FiftyMeterPointSurvey"/> object into permanent storage
        /// </summary>
        /// <param name="survey"><see cref="FiftyMeterPointSurvey"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static void Insert(FiftyMeterPointSurvey survey)
        {
            save(survey);
        }

        /// <summary>
        /// Removes a <see cref="FiftyMeterPointSurvey"/> object from permanent storage
        /// </summary>
        /// <param name="survey"><see cref="FiftyMeterPointSurvey"/> object</param>
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public static void Delete(FiftyMeterPointSurvey survey)
        {
            BaseMapper.DeleteObject(survey, TABLE_NAME);
        }


        /// <summary>
        /// Retrieves all <see cref="FiftyMeterPointSurvey"/> objects from permanent storage
        /// </summary>
        /// <param name="siteVisitId">The site visit id.</param>
        /// <returns>
        /// List of <see cref="FiftyMeterPointSurvey"/> objects
        /// </returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<FiftyMeterPointSurvey> SelectAllForSiteVisit(Guid siteVisitId)
        {
            guidGuardClause(siteVisitId, "SelectAllForSiteVisit");

            List<QueryParameter> queryList = new List<QueryParameter>() {
                new QueryParameter("SiteVisitId", siteVisitId)
            };
            List<FiftyMeterPointSurvey> list = BaseMapper.LoadAllQuery<FiftyMeterPointSurvey>(Load, SELECT_STORED_PROC, queryList);
            return list;
        }


        /// <summary>
        /// Retrieves a single <see cref="FiftyMeterPointSurvey" /> object from permanent storage based on search by ID
        /// </summary>
        /// <returns>List of <see cref="FiftyMeterPointSurvey"/> objects</returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static FiftyMeterPointSurvey Select(Guid id)
        {
            guidGuardClause(id, "Select");

            return BaseMapper.LoadSingleObjectById<FiftyMeterPointSurvey>(Load, SELECT_STORED_PROC, id);
        }

        private static void guidGuardClause(Guid id, string method)
        {
            if (id == Guid.Empty)
            {
                throw new IbaArgumentException("Empty Guid passed to PointSurveyMapper." + method);
            }
        }

        private static FiftyMeterPointSurvey Load(IDataReader reader)
        {
            FiftyMeterPointSurvey survey = new FiftyMeterPointSurvey();

            EventBaseMapper.Load(reader, survey);

            survey.NoiseCode = reader.GetByteFromName("NoiseCode");
            survey.SiteVisitId = reader.GetGuidFromName("SiteVisitId");

            return survey;
        }

        /// <summary>
        /// Saves the specified  <see cref="FiftyMeterPointSurvey"/> object.
        /// </summary>
        /// <param name="survey">The FiftyMeterPointSurvey object to save.</param>
        private static void save(FiftyMeterPointSurvey survey)
        {
            List<QueryParameter> queryList = EventBaseMapper.CreateSaveParameters(survey);
            queryList.AddRange(new List<QueryParameter>()
            {
                new QueryParameter("NoiseCode", survey.NoiseCode),
                new QueryParameter("SiteVisitId", survey.SiteVisitId)
            });
            BaseMapper.SaveObject(survey, queryList, TABLE_NAME);
        }
    }
}
