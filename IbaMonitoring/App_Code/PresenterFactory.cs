using IbaMonitoring.Presenters;
using IbaMonitoring.Views;
using Microsoft.Practices.Unity;
using safnet.iba.Adapters;
using System;
using System.Collections.Generic;
using System.Web;

namespace IbaMonitoring.App_Code
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IUnityContainer _iocContainer;


        private IUserStateManager _userState;

        protected IUserStateManager UserState
        {
            get
            {
                if (_userState == null)
                {
                    // Lazy loading a real object
                    _userState = new UserStateManager(new HttpSessionStateWrapper(HttpContext.Current.Session));
                }
                return _userState;
            }
            set { _userState = value; }
        }


        private IGlobalMap _globalMap;

        protected IGlobalMap GlobalMap
        {
            get
            {
                if (_globalMap == null)
                {
                    _globalMap = new GlobalMap(new HttpApplicationStateWrapper(HttpContext.Current.Application));
                }
                return _globalMap;
            }
            set { _globalMap = value; }
        }


        public PresenterFactory(IUnityContainer iocContainer)
        {
            if (iocContainer == null)
            {
                throw new ArgumentNullException("ioContainer");
            }

            _iocContainer = iocContainer;
        }





        public ISiteConditionsPresenter BuildSiteConditionsPresenter(ISiteConditionsView view)
        {
            var resolvers = new List<ResolverOverride> {
                new DependencyOverride<ISiteConditionsView>(view)
            };

            return Resolve<ISiteConditionsPresenter>(resolvers);
        }

        private T Resolve<T>(List<ResolverOverride> overrides)
            where T :class
        {
            overrides.Add(new DependencyOverride<IUserStateManager>(UserState));
            overrides.Add(new DependencyOverride<IGlobalMap>(GlobalMap));

            return _iocContainer.Resolve(typeof(T), "", overrides.ToArray()) as T;
        }
}
}