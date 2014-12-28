using safnet.iba.Business.Entities;
using System.Collections.Generic;

namespace safnet.iba.Adapters
{

    public interface IGlobalMap
    {
        /// <summary>
        /// Gets a List of <see cref="Site"/> objects from global Application state.
        /// </summary>
        List<Site> SiteList { get; }

        /// <summary>
        /// Gets a List of <see cref="Person"/> objects from global Application state.
        /// </summary>
        List<Person> PersonList { get; }

        /// <summary>
        /// Gets a List of <see cref="Species"/> objects from global Application state.
        /// </summary>
        List<Species> SpeciesList { get; }

        /// <summary>
        /// Gets the available years in which SiteVisits have been conducted.
        /// </summary>
        /// <value>The available years.</value>
        SortedSet<int> AvailableYears { get; }
    }

}
