using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Dictionaries;
using System;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Models for data collected at the beginning of a time frame.
    /// </summary>
    public class SiteCondition: SafnetBaseEntity
    {
        /// <summary>
        /// Gets or sets the Id for the <see cref="Event"/> related to a Condition.
        /// </summary>
        /// <value>The site visit id.</value>
        public Guid SiteVisitId { get; set; }

        /// <summary>
        /// Gets or sets the sky conditions.
        /// </summary>
        /// <value>The sky.</value>
        public byte Sky { get; set; }

        /// <summary>
        /// Gets the full text description of the sky conditions.
        /// </summary>
        public string SkyText
        {
            get
            {
                return SkyCode.Instance()[Sky];
            }
        }

        /// <summary>
        /// Gets or sets the starting wind conditions.
        /// </summary>
        /// <value>The wind.</value>
        public byte Wind { get; set; }

        /// <summary>
        /// Gets the full text description of the wind conditions.
        /// </summary>
        public string WindText
        {
            get
            {
                return WindCode.Instance()[Wind];
            }
        }

        /// <summary>
        /// Gets or sets the starting temperature.
        /// </summary>
        /// <value>The temperature.</value>
        public Temperature Temperature { get; set; }

        /// <summary>
        /// Factory method for creating a new Conditions object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="siteVisitId">The site visit id.</param>
        /// <returns>Instance of SiteVisit</returns>
        public static SiteCondition CreateNewConditions(Guid siteVisitId)
        {
            SiteCondition s = new SiteCondition()
            {
                SiteVisitId = siteVisitId
            };
            s.SetNewId();

            return s;
        }
    }
}
