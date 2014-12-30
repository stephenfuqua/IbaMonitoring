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
                var visit = MapViewModelToBusinessObject(view);

                UserState.SiteVisit = visit;
                _facade.SaveSiteConditions(visit);

                HttpResponse.Redirect("PointCounts.aspx", true);
            }
        }

        private SiteVisit MapViewModelToBusinessObject(ISiteConditionsView view)
        {
            var visit = UserState.SiteVisit ?? SiteVisit.CreateNewSiteVisit(view.SiteVisited.ToGuid());

            visit.ObserverId = view.Observer.ToGuid();
            visit.RecorderId = view.Recorder.ToGuid();

            visit.EndConditions.Sky = view.EndSky.ToByte();
            visit.EndConditions.Temperature = new Temperature()
            {
                Units = view.EndUnit,
                Value = view.EndTemp.ToInt()
            };
            visit.EndConditions.Wind = view.EndWind.ToByte();
            visit.EndTimeStamp = view.VisitDate.ToDateTime(view.EndTime);
            //visit.EndConditions.SiteVisitId;


            visit.StartConditions.Sky = view.StartSky.ToByte();
            visit.StartConditions.Temperature = new Temperature()
            {
                Units = view.StartUnit,
                Value = view.StartTemp.ToInt()
            };
            visit.StartConditions.Wind = view.StartWind.ToByte();
            visit.StartTimeStamp = view.VisitDate.ToDateTime(view.StartTime);

            return visit;
        }
    }

}
