using safnet.iba.Adapters;
using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace IbaMonitoring.App_Code
{
        /// <summary>
    ///  methods for retrieving typed Session state values for 
    /// objects currently being edited.
    /// </summary>
    public class UserStateManager : IUserStateManager
    {

        private readonly HttpSessionStateBase _session;

        public UserStateManager(HttpSessionStateBase session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            _session = session;
        }


        private const string CURRENT_USER_KEY = "CurrentUser";

        public Person CurrentUser
        {
            get { return _session[CURRENT_USER_KEY] as Person; }
            set { _session[CURRENT_USER_KEY] = value; }
        }


        /// <summary>
        /// Gets or sets the map list.
        /// </summary>
        /// <value>The map list.</value>
        public List<MapCount> MapList
        {
            get
            {
                List<MapCount> visit = (List<MapCount>) _session["MapList"];
                if (visit == null)
                {
                    visit = new List<MapCount>();
                    _session["MapList"] = visit;
                }
                return visit;
            }
            set { _session["MapList"] = value; }
        }

        /// <summary>
        /// Gets or sets a DataTable used for displaying results/statistics at the Species level.
        /// </summary>
        /// <value>The results table.</value>
        public DataTable SpeciesResultsTable
        {
            get
            {
                DataTable visit = (DataTable) _session["SpeciesResultsTable"];
                if (visit == null)
                {
                    visit = new DataTable();
                    _session["SpeciesResultsTable"] = visit;
                }
                return visit;
            }
            set { _session["SpeciesResultsTable"] = value; }
        }

        /// <summary>
        /// Gets or sets a DataTable used for displaying results/statistics at the Site level.
        /// </summary>
        /// <value>The results table.</value>
        public DataTable SiteResultsTable
        {
            get
            {
                DataTable visit = (DataTable) _session["SiteResultsTable"];
                if (visit == null)
                {
                    visit = new DataTable();
                    _session["SiteResultsTable"] = visit;
                }
                return visit;
            }
            set { _session["SiteResultsTable"] = value; }
        }

        /// <summary>
        /// Gets or sets, in HttpSessionState, the current SiteVisit object being edited.
        /// </summary>
        public SiteVisit SiteVisit
        {
            get
            {
                SiteVisit visit = (SiteVisit) _session["SiteVisit"];
                if (visit == null)
                {
                    visit = new SiteVisit();
                    _session["SiteVisit"] = visit;
                }
                return visit;
            }
            set { _session["SiteVisit"] = value; }
        }

        public FiftyMeterPointSurvey PointSurvey
        {
            get
            {
                FiftyMeterPointSurvey survey = (FiftyMeterPointSurvey) _session["PointSurvey"];
                if (survey == null)
                {
                    survey = new FiftyMeterPointSurvey();
                    _session["PointSurvey"] = survey;
                }
                return survey;
            }
            set { _session["PointSurvey"] = value; }
        }


        public SamplingPoint SamplingPoint
        {
            get
            {
                object o = _session["SamplingPoint"];
                if (o != null)
                {
                    SamplingPoint point = (SamplingPoint) o;
                    return point;
                }
                else
                {
                    return null;
                }
            }
            set { _session["SamplingPoint"] = value; }
        }

        /// <summary>
        /// Gets or sets, in HttpSessionState, a list of all the SamplingPoints that have already been entered for the current survey.
        /// </summary>
        public List<SamplingPoint> PointsRemaining
        {
            get
            {
                List<SamplingPoint> list = (List<SamplingPoint>) _session["PointsRemaining"];
                if (list == null)
                {
                    list = new List<SamplingPoint>();
                    _session["PointsRemaining"] = list;
                }
                return list;
            }
        }

        public List<SamplingPoint> PointsCompleted
        {
            get
            {
                List<SamplingPoint> list = (List<SamplingPoint>) _session["PointsCompleted"];
                if (list == null)
                {
                    list = new List<SamplingPoint>();
                    _session["PointsCompleted"] = list;
                }
                return list;
            }
        }

        /// <summary>
        /// Gets or sets the search terms.
        /// </summary>
        /// <value>The search terms.</value>
        public string SearchTerms
        {
            get { return (string) _session["SearchTerms"]; }
            set { _session["SearchTerms"] = value; }
        }

        public string SelectedYear
        {
            get
            {
                string year = (string) _session["SelectedYear"];
                if (year == null)
                {
                    return string.Empty;
                }
                return year;
            }
            set { _session["SelectedYear"] = value; }
        }


        /// <summary>
        /// Gets or sets the open id response.
        /// </summary>
        /// <value>The open id response.</value>
        public OpenIdResponse OpenIdResponse
        {
            get { return _session["AuthenticationResponse"] as OpenIdResponse; }
            set { _session["AuthenticationResponse"] = value; }
        }




    }
}