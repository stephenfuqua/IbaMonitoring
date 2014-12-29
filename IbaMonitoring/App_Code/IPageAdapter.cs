using System.Web.UI;

namespace IbaMonitoring.App_Code
{
    public interface IPageAdapter
    {
        bool IsValid { get; }
        bool IsCallback { get; }
        bool IsPostback { get; }
    }

    public class PageAdapter : IPageAdapter
    {
        private readonly Page _page;

        public PageAdapter(Page page)
        {
            _page = page;
        }

        public bool IsCallback
        {
            get
            {
                return _page.IsCallback;
            }
        }

        public bool IsPostback
        {
            get
            {
                return _page.IsPostBack;
            }
        }

        public bool IsValid
        {
            get
            {
                return _page.IsValid;
            }
        }
    }
}