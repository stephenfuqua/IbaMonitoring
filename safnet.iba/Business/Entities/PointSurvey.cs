using safnet.iba.Business.Dictionaries;
using safnet.iba.Business.Entities.Observations;
using System;
using System.Collections.Generic;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Models data for counts at a particular PointSurvey during a SiteVisit.
    /// </summary>
    public class FiftyMeterPointSurvey : Event
    {
        /// <summary>
        /// Gets or sets the ID of the <see cref="SiteVisit"/> to which this PointSurvey belongs.
        /// </summary>
        public Guid SiteVisitId { get; set; }

        /// <summary>
        /// Gets or sets ID of the <see cref="SamplingPoint"/> to which this PointSurvey belongs.
        /// </summary>
        /// <value>The sampling point id.</value>
        public Guid SamplingPointId { get { return base.LocationId; } set { base.LocationId = value; } }

        /// <summary>
        /// Gets or sets the noise code for the point.
        /// </summary>
        public byte NoiseCode { get; set; }

        /// <summary>
        /// Gets the noise code text.
        /// </summary>
        /// <value>The noise code text.</value>
        public string NoiseCodeText
        {
            get
            {
                if (Dictionaries.NoiseCode.Instance().ContainsKey(NoiseCode))
                {
                    NoiseCode noises = Dictionaries.NoiseCode.Instance();
                    return noises[NoiseCode];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets a List of <see cref="FiftyMeterPointObservation"/> objects related to this PointSurvey.
        /// </summary>
        public List<FiftyMeterPointObservation> Observations { get; private set; }

        /// <summary>
        /// Creates a new instance of PointSurvey.
        /// </summary>
        public FiftyMeterPointSurvey()
        {
            Observations = new List<FiftyMeterPointObservation>();
        }

        /// <summary>
        /// Factory method for creating a new PointSurvey object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="pointId">Id of the <see cref="SamplingPoint"/> being Surveyed</param>
        /// <returns>Instance of PointSurvey</returns>
        public static FiftyMeterPointSurvey CreateNewPointSurvey(Guid pointId)
        {
            FiftyMeterPointSurvey s = new FiftyMeterPointSurvey()
            {
                LocationId = pointId
            };
            s.SetNewId();

            return s;
        }
    }
}
