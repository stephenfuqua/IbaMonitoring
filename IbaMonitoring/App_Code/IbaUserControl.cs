
using System.Web;
using System.Web.UI;
using IbaMonitoring.App_Code;
using safnet.iba.Adapters;

namespace IbaMonitoring
{
    /// <summary>
    /// Summary description for IbaUserControl
    /// </summary>
    public class IbaUserControl : UserControl
    {
        private IUserStateManager _userState;

        /// <summary>
        /// Gets or sets the current user's Session state.
        /// </summary>
        /// <value>The state.</value>
        protected IUserStateManager UserState
        {
            get
            {
                if (_userState == null)
                {
                    // Lazy loading a real object
                    _userState = new UserStateManager(new HttpSessionStateWrapper(Session));
                }
                return _userState;
            }
            set { _userState = value; }
        }


    }
}