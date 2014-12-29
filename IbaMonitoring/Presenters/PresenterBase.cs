using System.Web;
using IbaMonitoring.App_Code;
using safnet.iba.Adapters;

namespace IbaMonitoring.Presenters
{
    public abstract class PresenterBase
    {
        private HttpContextBase Context
        {
            get { return new 
                HttpContextWrapper(HttpContext.Current); }
        }


        private IUserStateManager _userState;

        protected IUserStateManager UserState
        {
            get
            {
                if (_userState == null)
                {
                    _userState = new UserStateManager(Context.Session);
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
                    _globalMap = new GlobalMap(Context.Application);
                }
                return _globalMap;
            }
            set { _globalMap = value; }
        }


        private HttpResponseBase _responseBase;

        protected HttpResponseBase HttpResponse
        {
            get
            {
                if (_responseBase == null)
                {
                    _responseBase = Context.Response;
                }
                return _responseBase;
            }
            set
            {
                _responseBase = value;
            }
        }


        private HttpRequestBase _requestBase;

        protected HttpRequestBase HttpRequest
        {
            get
            {
                if (_requestBase == null)
                {
                    _requestBase = Context.Request;
                }
                return _requestBase;
            }
            set
            {
                _requestBase = value;
            }
        }
    }
}