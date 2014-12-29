using safnet.iba.Adapters;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Web;

namespace IbaMonitoring.App_Code
{
    /// <summary>
    /// Singleton for retrieving data from Http Session.
    /// </summary>
    public class GlobalMap : IGlobalMap
    {
        private static GlobalMap _instance;
        private HttpApplicationStateBase _application;
        private static object _threadLocker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalMap"/> class.
        /// </summary>
        protected GlobalMap()
        {
            _application = new HttpApplicationStateWrapper(HttpContext.Current.Application);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalMap"/> class.
        /// </summary>
        /// <param name="appState">An instance of <see cref="HttpApplicationStateBase"/></param>
        public GlobalMap(HttpApplicationStateBase appState)
        {
            if (appState == null)
            {
                throw new ArgumentNullException("appState");
            }

            _application = appState;
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <returns></returns>
        public static GlobalMap GetInstance()
        {
            if (_instance == null)
            {
                lock (_threadLocker)
                {
                    _instance = new GlobalMap();
                }
            }
            return _instance;
        }

        /// <summary>
        /// Gets the HTTP application-level state container.
        /// </summary>
        /// <value>The application.</value>
        public static HttpApplicationStateBase Application
        {
            get { return GetInstance()._application; }
        }

        /// <summary>
        /// Gets a List of <see cref="Site"/> objects from global Application state.
        /// </summary>
        public List<Site> SiteList
        {
            get
            {
                List<Site> list = (List<Site>)Application["SiteList"];
                if (list == null)
                {
                    list = new List<Site>();
                    Application["SiteList"] = list;
                    list.AddRange(SiteMapper.SelectAll());
                }
                return list;
            }
        }

        /// <summary>
        /// Gets a List of <see cref="Person"/> objects from global Application state.
        /// </summary>
        public List<Person> PersonList
        {
            get
            {
                List<Person> list = (List<Person>)Application["PersonList"];
                if (list == null)
                {
                    list = new List<Person>();
                    Application["PersonList"] = list;
                    list.AddRange(PersonMapper.SelectAll());
                }
                return list;
            }
        }


        /// <summary>
        /// Gets a List of <see cref="Species"/> objects from global Application state.
        /// </summary>
        public List<Species> SpeciesList
        {
            get
            {
                List<Species> list = (List<Species>)Application["SpeciesList"];
                if (list == null)
                {
                    list = new List<Species>();
                    Application["SpeciesList"] = list;
                    list.AddRange(SpeciesMapper.SelectAll());
                }
                return list;
            }
        }

        /// <summary>
        /// Gets the available years in which SiteVisits have been conducted.
        /// </summary>
        /// <value>The available years.</value>
        public SortedSet<int> AvailableYears
        {
            get
            {
                SortedSet<int> set = (SortedSet<int>)Application["AvailableYears"];
                if (set == null)
                {
                    set = ResultsFacade.GetAvailableYears();
                }
                return set;
            }
        }
    }
}