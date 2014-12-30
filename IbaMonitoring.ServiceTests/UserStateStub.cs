using System.Collections.Generic;
using System.Data;
using safnet.iba.Adapters;
using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;

namespace IbaMonitoring.ServiceTests
{
    public class UserStateStub : IUserStateManager
    {
        public Person CurrentUser
        {
            get;
            set;
        }

        public OpenIdResponse OpenIdResponse
        {
            get;
            set;
        }

        public List<MapCount> MapList
        {
            get;
            set;
        }

        public DataTable SpeciesResultsTable
        {
            get;
            set;
        }

        public DataTable SiteResultsTable
        {
            get;
            set;
        }

        public SiteVisit SiteVisit
        {
            get;
            set;
        }

        public FiftyMeterPointSurvey PointSurvey
        {
            get;
            set;
        }

        public SamplingPoint SamplingPoint
        {
            get;
            set;
        }

        public List<SamplingPoint> PointsRemaining
        {
            get;
            set;
        }

        public List<SamplingPoint> PointsCompleted
        {
            get;
            set;
        }

        public string SearchTerms
        {
            get;
            set;
        }

        public string SelectedYear
        {
            get;
            set;
        }
    }
}
