using System;
using IbaMonitoring.Views;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Static;

namespace IbaMonitoring.Presenters
{
    public class SiteConditionsPresenter : PresenterBase, ISiteConditionsPresenter
    {
        private readonly ISiteConditionsFacade _facade;

        public SiteConditionsPresenter(ISiteConditionsFacade facade)
        {
            if (facade == null)
            {
                throw new ArgumentNullException("facade");
            }

            _facade = facade;
        }

        public void SaveConditions(ISiteConditionsView view)
        {
            if (view.IsValid)
            {
                SiteVisit visit = UserState.SiteVisit;

                visit.LocationId = view.SiteVisitedAccessor.ToGuid();

                visit.ObserverId = view.SiteVisitObserverAccessor.ToGuid();
                visit.RecorderId = view.SiteVisitRecorderAccessor.ToGuid();

                visit.EndConditions.Sky = view.EndSkyAccessor.ToByte();
                visit.EndConditions.Temperature = new Temperature()
                {
                    Units = view.EndTempUnitsAccessor,
                    Value = view.EndTempAccessor.ToInt()
                };
                visit.EndConditions.Wind = view.EndWindAccessor.ToByte();
                visit.EndTimeStamp = view.VisitDateAccessor.ToDateTime(view.EndTimeAccessor);


                visit.StartConditions.Sky = view.StartSkyAccessor.ToByte();
                visit.StartConditions.Temperature = new Temperature()
                {
                    Units = view.StartTempUnitsAccessor,
                    Value = view.StartTempAccessor.ToInt()
                };
                visit.StartConditions.Wind = view.StartWindAccessor.ToByte();
                visit.StartTimeStamp = view.VisitDateAccessor.ToDateTime(view.StartTimeAccessor);


                UserState.SiteVisit = _facade.SaveSiteConditions(visit);

                HttpResponse.Redirect("PointCounts.aspx", true);
            }
        }
    }

}
