using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using Microsoft.Practices.Unity;
using System;

namespace IbaMonitoring.App_Code
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IUnityContainer _iocContainer;


        public PresenterFactory(IUnityContainer iocContainer)
        {
            if (iocContainer == null)
            {
                throw new ArgumentNullException("iocContainer");
            }

            _iocContainer = iocContainer;
        }
        

        public ISiteConditionsPresenter BuildSiteConditionsPresenter()
        {
            return _iocContainer.Resolve(typeof(ISiteConditionsPresenter), "ISiteConditionsPresenter") as ISiteConditionsPresenter;
        }

}
}