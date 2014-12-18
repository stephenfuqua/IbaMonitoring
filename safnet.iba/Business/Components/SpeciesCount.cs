using System.Collections.Generic;
using System.ComponentModel;

namespace safnet.iba.Business.Components
{
    /// <summary>
    /// Methods for obtaining various counts of species detected during site surveys.
    /// </summary>
    [DataObject(true)]
    public class SpeciesCount
    {
        /// <summary>
        /// Retrieves total counts for all individuals across all seasons, divided by species code.
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static Dictionary<string, int> MasterCount()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            // TODO: decide best approach for this.
            //  List<FiftyMeterPointSurvey> points = PointSurveyMapper.SelectAll();
            //foreach (FiftyMeterPointSurvey point in points)
            //{
            //    foreach (FiftyMeterPointObservation obs in point.Observations)
            //    {
            //        if (!counts.ContainsKey(obs.SpeciesCode))
            //        {
            //            counts.Add(obs.SpeciesCode, 1);
            //        }
            //        else
            //        {
            //            counts[obs.SpeciesCode] += 1;
            //        }
            //    }
            //}
            return counts;
        }

    }
}
