using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;
using System.Collections.Generic;
using System.Data;

namespace safnet.iba.Adapters
{
    public interface IUserStateManager
    {
        Person CurrentUser { get; set; }
        OpenIdResponse OpenIdResponse { get; set; }
        List<MapCount> MapList { get; set; }
        DataTable SpeciesResultsTable { get; set; }
        DataTable SiteResultsTable { get; set; }
        SiteVisit SiteVisit { get; set; }
        FiftyMeterPointSurvey PointSurvey { get; set; }
        SamplingPoint SamplingPoint { get; set; }
        List<SamplingPoint> PointsRemaining { get; }
        List<SamplingPoint> PointsCompleted { get; }
        string SearchTerms { get; set; }
        string SelectedYear { get; set; }
    }
}
