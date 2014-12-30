using System;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;

namespace safnet.iba.Business.AppFacades
{
    public interface ISiteConditionsFacade
    {
        void SaveSiteConditions(SiteVisit visit);
    }

    public class SiteConditionsFacade : ISiteConditionsFacade
    {
        public void SaveSiteConditions(SiteVisit visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            SiteVisitMapper.Insert(visit);
            ConditionsMapper.Insert(visit.StartConditions);
            ConditionsMapper.Insert(visit.EndConditions);

        }
    }
}
