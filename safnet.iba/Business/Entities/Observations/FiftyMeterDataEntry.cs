using System;
using System.Collections.Generic;
using System.Linq;

namespace safnet.iba.Business.Entities.Observations
{
    /// <summary>
    /// Models data from a single PointSurvey Count observation
    /// </summary>
    public class FiftyMeterDataEntry : SafnetBaseEntity
    {
        #region Fields

        /// <summary>
        /// List of <see cref="PointCountBeyond50"/> objects.
        /// </summary>
        protected List<PointCountBeyond50> Beyond50 = new List<PointCountBeyond50>();

        /// <summary>
        /// List of <see cref="PointCountWithin50"/> objects.
        /// </summary>
        protected List<PointCountWithin50> Within50 = new List<PointCountWithin50>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyMeterDataEntry"/> class.
        /// </summary>
        /// <param name="observationList">The observation list.</param>
        public FiftyMeterDataEntry(List<FiftyMeterPointObservation> observationList)
        {
            observationList.ForEach(x =>
            {
                if (x is PointCountBeyond50)
                {
                    Beyond50.Add(x as PointCountBeyond50);
                }
                else if (x is PointCountWithin50)
                {
                    Within50.Add(x as PointCountWithin50);
                }
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiftyMeterDataEntry"/> class.
        /// </summary>
        public FiftyMeterDataEntry()
        {
            // nothing needs to be done
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the count beyond 50 meters.
        /// </summary>
        /// <value>The count beyond50.</value>
        public int CountBeyond50 { get; set; }

        /// <summary>
        /// Gets or sets the count within 50 meters.
        /// </summary>
        /// <value>The count within50.</value>
        public int CountWithin50 { get; set; }

        /// <summary>
        /// Gets or sets the ID for the associated PointSurvey.
        /// </summary>
        /// <value>The point survey id.</value>
        public Guid PointSurveyId { get; set; }

        /// <summary>
        /// Gets or sets the species code.
        /// </summary>
        /// <value>The species code.</value>
        public string SpeciesCode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the list beyond50.
        /// </summary>
        /// <returns></returns>
        public List<PointCountBeyond50> GetListBeyond50()
        {
            UpdateCountAndList(Beyond50, CountBeyond50, this);
            return Beyond50;
        }

        /// <summary>
        /// Gets the within50.
        /// </summary>
        /// <returns></returns>
        public List<PointCountWithin50> GetListWithin50()
        {
            UpdateCountAndList(Within50, CountWithin50, this);
            return Within50;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Updates the list of <see cref="FiftyMeterPointObservation" /> objects based on a count (basically for translating from count to distinct objects).
        /// </summary>
        /// <typeparam name="T">Must be of type <see cref="FiftyMeterPointObservation" /></typeparam>
        /// <param name="countList">The count list.</param>
        /// <param name="count">The count.</param>
        /// <param name="entry">The entry.</param>
        protected static void UpdateCountAndList<T>(List<T> countList, int count, FiftyMeterDataEntry entry) where T : FiftyMeterPointObservation
        {
            while (countList.Count() < count)
            {
                T pointCount = Activator.CreateInstance<T>();
                pointCount.Comments = entry.Comments;
                pointCount.EventId = entry.PointSurveyId;
                pointCount.SpeciesCode = entry.SpeciesCode;
                countList.Add(pointCount);
            }
            while (countList.Count(x => !x.MarkForDeletion) > count)
            {
                countList.First(x => !x.MarkForDeletion).MarkForDeletion = true;
            }
        }

        #endregion

    }
}
