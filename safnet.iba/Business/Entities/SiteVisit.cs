using safnet.iba.Business.Entities.Observations;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Models a single survey outing, including point counts at all points during that outing.
    /// </summary>
    public class SiteVisit : Event
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="SiteVisit"/> with instantiated members.
        /// </summary>
        public SiteVisit()
        {
            SupplementalObservations = new List<SupplementalObservation>();
            PointSurveys = new List<FiftyMeterPointSurvey>();
            StartConditions = new SiteCondition();
            EndConditions = new SiteCondition();
            IsDataEntryComplete = false;
        }

        #endregion

        /// <summary>
        /// Converts the visit's surveys into a simple list of <see cref="FiftyMeterDataEntry"/> objects.
        /// </summary>
        /// <value>The flattened data entry list.</value>
        public List<FiftyMeterDataEntry> FlattenedDataEntryList
        {
            get
            {
                List<FiftyMeterDataEntry> flat = new List<FiftyMeterDataEntry>();

                PointSurveys.ForEach(x =>
                    {
                        flat.Add(new FiftyMeterDataEntry(x.Observations));
                    });

                return flat;
            }
        }

        #region Properties

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SiteCondition"/> at the end of a SiteVisit.
        /// </summary>
        public SiteCondition EndConditions { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating if all data entries is complete for this Event.
        /// </summary>
        public bool IsDataEntryComplete { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the <see cref="Person"/> object linked as the site visit Observer.
        /// </summary>
        public Guid ObserverId { get; set; }

        /// <summary>
        /// Gets a List of <see cref="FiftyMeterPointSurvey"/> objects
        /// </summary>
        public List<FiftyMeterPointSurvey> PointSurveys { get; private set; }

        /// <summary>
        /// Gets or sets the identifier for the <see cref="Person"/> object linked as the site visit Recorder.
        /// </summary>
        public Guid RecorderId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SiteCondition"/> at the start of a SiteVisit.
        /// </summary>
        public SiteCondition StartConditions { get; set; }

        /// <summary>
        /// Gets or sets a List of supplemental <see cref="Observations"/> for a survey.
        /// </summary>
        public List<SupplementalObservation> SupplementalObservations { get; private set; }

        /// <summary>
        /// Gets the week number 
        /// </summary>
        public int WeekNumber
        {
            get
            {
                if (StartTimeStamp != null)
                {
                    GregorianCalendar cal = new GregorianCalendar();
                    return cal.GetWeekOfYear(StartTimeStamp.Value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Factory method for creating a new SiteVisit object, automatically setting a new Guid for the identifier.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns>Instance of SiteVisit</returns>
        public static SiteVisit CreateNewSiteVisit(Guid siteId)
        {
            SiteVisit s = new SiteVisit()
            {
                LocationId = siteId
            };
            s.SetNewId();

            return s;
        }

        #endregion

    }
}
