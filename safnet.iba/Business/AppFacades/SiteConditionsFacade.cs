using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.Static;

namespace safnet.iba.Business.AppFacades
{
    public interface ISiteConditionsFacade
    {
        SiteVisit SaveSiteConditions(SiteVisit visit);
    }

    public class SiteConditionsFacade : ISiteConditionsFacade
    {
        public SiteVisit SaveSiteConditions(SiteVisit visit)
        {
            if (visit.EndConditions.Id.IsEmptyGuid())
            {
                visit.EndConditions = SiteCondition.CreateNewConditions(visit.Id);
            }
            if (visit.StartConditions.Id.IsEmptyGuid())
            {
                visit.StartConditions = SiteCondition.CreateNewConditions(visit.Id);
            }
            if (visit.Id.IsEmptyGuid())
            {
                visit.SetNewId();
            }

            SiteVisitMapper.Insert(visit);
            ConditionsMapper.Insert(visit.StartConditions);
            ConditionsMapper.Insert(visit.EndConditions);

            return visit;
        }
    }
}
