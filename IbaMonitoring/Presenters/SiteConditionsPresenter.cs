﻿using IbaMonitoring.Views;
using safnet.iba.Adapters;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.DataTypes;
using safnet.iba.Business.Entities;
using safnet.iba.Static;
using System;

namespace IbaMonitoring.Presenters
{
    public class SiteConditionsPresenter : ISiteConditionsPresenter
    {
        private readonly IUserStateManager _userState;
        private readonly ISiteConditionsView _view;
        private readonly ISiteConditionsFacade _facade;


        public SiteConditionsPresenter(IUserStateManager userState, 
            ISiteConditionsView view, ISiteConditionsFacade facade)
        {
            if (userState == null)
            {
                throw new ArgumentNullException("userState");
            }
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            if (facade == null)
            {
                throw new ArgumentNullException("facade");
            }

            _userState = userState;
            _view = view;
            _facade = facade;
        }

        public void SaveConditions()
        {
            SiteVisit visit = _userState.SiteVisit;

            visit.LocationId = _view.SiteVisitedAccessor.ToGuid();

            visit.ObserverId = _view.SiteVisitObserverAccessor.ToGuid();
            visit.RecorderId = _view.SiteVisitRecorderAccessor.ToGuid();

            visit.EndConditions.Sky = _view.EndSkyAccessor.ToByte();
            visit.EndConditions.Temperature = new Temperature()
            {
                Units = _view.EndTempUnitsAccessor,
                Value = _view.EndTempAccessor.ToInt()
            };
            visit.EndConditions.Wind = _view.EndWindAccessor.ToByte();
            visit.EndTimeStamp = _view.VisitDateAccessor.ToDateTime(_view.EndTimeAccessor);


            visit.StartConditions.Sky = _view.StartSkyAccessor.ToByte();
            visit.StartConditions.Temperature = new Temperature()
            {
                Units = _view.StartTempUnitsAccessor,
                Value = _view.StartTempAccessor.ToInt()
            };
            visit.StartConditions.Wind = _view.StartWindAccessor.ToByte();
            visit.StartTimeStamp = _view.VisitDateAccessor.ToDateTime(_view.StartTimeAccessor);


            _userState.SiteVisit = _facade.SaveSiteConditions(visit);
        }
    }

}
