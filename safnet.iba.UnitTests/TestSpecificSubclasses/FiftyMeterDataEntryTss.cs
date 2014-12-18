using System.Collections.Generic;
using safnet.iba.Business.Entities.Observations;

namespace safnet.iba.UnitTests.TestSpecificSubclasses
{
    public class FiftyMeterDataEntryTss : FiftyMeterDataEntry
    {
        public static new void UpdateCountAndList<T>(List<T> countList, int count, FiftyMeterDataEntry entry) where T : FiftyMeterPointObservation
        {
            FiftyMeterDataEntry.UpdateCountAndList<T>(countList, count, entry);
        }

        public new List<PointCountBeyond50> Beyond50
        {
            get { return base.Beyond50; }
        }
        public new List<PointCountWithin50> Within50
        {
            get { return base.Within50; }
        }
    }
}
