using System;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Models a particular Point within a Site.
    /// </summary>
    public class SamplingPoint : Location
    {
        /// <summary>
        /// Gets or sets the identifier for the <see cref="Site"/> to which this SamplingPoint belongs.
        /// </summary>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Factory method for creating a new SamplingPoint object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="name">Point name</param>
        /// <returns>Instance of SamplingPoint</returns>
        public static SamplingPoint CreateNewSamplingPoint(string name)
        {
            SamplingPoint s = new SamplingPoint()
            {
                Name = name
            };
            s.SetNewId();

            return s;
        }
    }
}
