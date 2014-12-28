using Microsoft.Practices.Unity;
using safnet.iba;
using safnet.iba.Adapters;
using safnet.iba.Presenters;
using safnet.iba.Views;
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