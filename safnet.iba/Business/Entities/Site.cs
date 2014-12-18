using System.Collections.Generic;
using safnet.iba.Business.DataTypes;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Models the data representing a single site in the IBA system.
    /// </summary>
    public class Site : Location
    {
        /// <summary>
        /// Gets or sets the Site's Boundaries.
        /// </summary>
        /// <value>The boundaries.</value>
        public Queue<Coordinate> Boundaries { get; private set; }

        /// <summary>
        /// Gets or sets the short "code/nick" name for a site.
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// Gets a List of SitePoint objects.
        /// </summary>
        public List<SamplingPoint> SamplingPoints { get; private set; }

        /// <summary>
        /// Creates an new instance of Site.
        /// </summary>
        public Site()
        {
            SamplingPoints = new List<SamplingPoint>();
            Boundaries = new Queue<Coordinate>();
        }

        /// <summary>
        /// Adds a <see cref="SamplingPoint"/> to the Site, setting the point's LocationId.
        /// </summary>
        /// <param name="point"></param>
        public void AddSamplingPoint(SamplingPoint point)
        {
            point.SiteId = this.Id;
            this.SamplingPoints.Add(point);
        }

        /// <summary>
        /// Factory method for creating a new Site object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="name">Site name</param>
        /// <param name="codeName">Site code name (3 letter typically)</param>
        /// <returns>Instance of Site</returns>
        public static Site CreateNewSite(string name, string codeName)
        {
            Site s = new Site()
            {
                CodeName = codeName,
                Name = name
            };
            s.SetNewId();

            return s;
        }
    }
}
