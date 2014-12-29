using IbaMonitoring.Presenters;
using IbaMonitoring.Views;

namespace IbaMonitoring.App_Code
{
    public interface IPresenterFactory
    {
        ISiteConditionsPresenter BuildSiteConditionsPresenter(ISiteConditionsView view);
    }

}