using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using safnet.iba;

namespace IbaMonitoring
{
    /// <summary>
    /// Summary description for IbaPage
    /// </summary>
    public class IbaPage : Page
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


        private IGlobalMap _globalMap;

        protected IGlobalMap GlobalMap
        {
            get
            {
                if (_globalMap == null)
                {
                    _globalMap = new GlobalMap(new HttpApplicationStateWrapper(Application));
                }
                return _globalMap;
            }
            set { _globalMap = value; }
        }


        /// <summary>
        /// Assigns the color of the table cell based on the value in the cell.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        /// <param name="cell">The cell.</param>
        public void AssignCellColor(string cssClass, TableCell cell)
        {
            int parse = 0;
            if (int.TryParse(cell.Text, out parse))
            {
                if (parse > 0 && parse < 5)
                {
                    cssClass = "Bin1";
                }
                else if (parse > 4 && parse < 9)
                {
                    cssClass = "Bin5";
                }
                else if (parse > 8 && parse < 13)
                {
                    cssClass = "Bin9";
                }
                else if (parse > 12 && parse < 17)
                {
                    cssClass = "Bin13";
                }
                else if (parse > 16 && parse < 21)
                {
                    cssClass = "Bin17";
                }
                else if (parse > 20 && parse < 25)
                {
                    cssClass = "Bin21";
                }
                else if (parse > 24)
                {
                    cssClass = "Bin25";
                }
                cell.CssClass = cssClass;
            }
        }

    }
}