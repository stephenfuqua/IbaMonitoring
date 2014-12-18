using System.Collections.Generic;
using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;

namespace safnet.iba.Business.AppFacades
{
    /// <summary>
    /// Facade class for the Observation Review page
    /// </summary>
    public static class ObsevationsReviewFacade
    {
        /// <summary>
        /// Gets the point surveys.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns></returns>
        public static List<PointSurveyReviewDTO> GetPointSurveys(SiteVisit visit)
        {
            List<PointSurveyReviewDTO> dtoList = new List<PointSurveyReviewDTO>();
            foreach (FiftyMeterPointSurvey survey in visit.PointSurveys)
            {
                foreach (FiftyMeterPointObservation observation in survey.Observations)
                {
                    // TODO: create separate objects for each count
                    PointSurveyReviewDTO dto = new PointSurveyReviewDTO()
                    {
                        AlphaCode = observation.SpeciesCode,
                        //Beyond50 = observation.CountBeyond50.ToString(),
                        Comments = observation.Comments,
                        //Within50 = observation.CountWithin50.ToString()
                    };
                    dto.SamplingPointName = survey.LocationId.ToString();
                    // TODO: fill in Warnings, proper SamplingPointName, and SpeciesName.
                }
            }
            return dtoList;
        }
    }
}
