using Microsoft.Practices.Unity;
using safnet.iba;
using safnet.iba.Adapters;
using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using System;
using System.Collections.Generic;
using System.Web;

namespace IbaMonitoring.App_Code
{
    public interface IPresenterFactory
    {
        ISiteConditionsPresenter BuildSiteConditionsPresenter(ISiteConditionsView view);
    }

}