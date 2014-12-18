using System.Collections.Generic;
using System.Web;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;

namespace safnet.iba
{
    /// <summary>
    /// Singleton for retrieving data from Http Session.
    /// </summary>
    public class GlobalMap
    {
        private static GlobalMap _instance;
        private HttpApplicationState _application;
        private static object _threadLocker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalMap"/> class.
        /// </summary>
        protected GlobalMap()
        {
            _application = HttpContext.Current.Application;
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
        public static HttpApplicationState Application
        {
            get { return GetInstance()._application; }
        }

        /// <summary>
        /// Gets a List of <see cref="Site"/> objects from global Application state.
        /// </summary>
        public static List<Site> SiteList
        {
            get
            {
                List<Site> list = (List<Site>) Application["SiteList"];
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
        public static List<Person> PersonList
        {
            get
            {
                List<Person> list = (List<Person>) Application["PersonList"];
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
        public static List<Species> SpeciesList
        {
            get
            {
                List<Species> list = (List<Species>) Application["SpeciesList"];
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
        public static SortedSet<int> AvailableYears
        {
            get
            {
                SortedSet<int> set = (SortedSet<int>) Application["AvailableYears"];
                if (set == null)
                {
                    set = ResultsFacade.GetAvailableYears();
                }
                return set;
            }
        }
    }
}