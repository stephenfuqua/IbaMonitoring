using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId.RelyingParty;
using IbaMonitoring;
using safnet.iba;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;

public partial class IbaMasterPage : MasterPage
{
    private IUserStateManager _UserState;

    /// <summary>
    /// Gets or sets the current user's Session UserState.
    /// </summary>
    /// <value>The UserState.</value>
    protected IUserStateManager UserState
    {
        get
        {
            if (_UserState == null)
            {
                // Lazy loading a real object
                _UserState = new UserStateManager(new HttpSessionStateWrapper(Session));
            }
            return _UserState;
        }
        set { _UserState = value; }
    }



    public delegate void ExceptionHandlerMethod();

    /// <summary>
    /// Wraps a method call in a try/catch block, passing an ArgumentOutOfRangeException exception to the standard error message routine.
    /// </summary>
    /// <param name="master">The master.</param>
    /// <param name="method">The method.</param>
    public static void ExceptionHandler(IbaMasterPage master, ExceptionHandlerMethod method)
    {
        try
        {
            method();
        }
        catch (ArgumentOutOfRangeException outOfRange)
        {
            
            Utility.LogSiteError(outOfRange);
            master.SetErrorMessage(outOfRange.Message);
        }
        catch (InvalidOperationException invalid)
        {
            Utility.LogSiteError(invalid);
            master.SetErrorMessage(invalid.Message);
        }
    }

    /// <summary>
    /// Sets the observation tab active.
    /// </summary>
    public void SetObservationActive()
    {

        activeTab(this.SubmitObservationsLink);
    }

    /// <summary>
    /// Sets the about tab active.
    /// </summary>
    public void SetAboutActive()
    {
        activeTab(this.AboutLink);
    }

    /// <summary>
    /// Sets the home tab active.
    /// </summary>
    public void SetHomeActive()
    {
        activeTab(this.HomeLink);
    }

    /// <summary>
    /// Sets the view tab active.
    /// </summary>
    public void SetViewActive()
    {
        activeTab(this.ViewLink);
    }

    /// <summary>
    /// Sets the resources tab active.
    /// </summary>
    public void SetResourcesActive()
    {
        activeTab(this.ResourcesLink);
    }

    /// <summary>
    /// Sets the error message.
    /// </summary>
    /// <param name="innerHtml">The inner HTML.</param>
    public void SetErrorMessage(string innerHtml)
    {
        this.ErrorMessage.Visible = true;
        this.ErrorMessage.InnerHtml = innerHtml;
        this.SuccessMessage.Visible = false;
    }

    /// <summary>
    /// Sets the success message.
    /// </summary>
    /// <param name="innerHtml">The inner HTML.</param>
    public void SetSuccessMessage(string innerHtml)
    {
        this.SuccessMessage.Visible = true;
        this.SuccessMessage.InnerHtml = innerHtml;
        this.ErrorMessage.Visible = false;
    }

    public void ClearMessages()
    {
        this.ErrorMessage.Visible = false;
        this.SuccessMessage.Visible = false;
    }

    public bool NeedsLogin { get; set; }

    /// <summary>
    /// Determines whether an authenticated user is logged; if not, displays the login and an error message.
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if [is authenticated user]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsAuthenticatedUser()
    {
        if (UserState.OpenIdResponse == null ||
            !UserState.OpenIdResponse.IsAuthenticated ||
            UserState.CurrentUser == null ||
            !UserState.CurrentUser.PersonStatus.Equals(Status.Active))
        {
            NeedsLogin = true;
            this.SetErrorMessage(
                "<p>You must be an active, registered user in order to use this portion of the site. If you have recently registered, then you will need to wait until you receive an e-mail indicating that your registration has been approved by a site administrator.</p>");
            this.contentBody.Visible = false;
            return false;
        }
        else return true;
    }


    /// <summary>
    /// Sets the login form active.
    /// </summary>
    public void SetLoginFormActive()
    {
        this.login.Visible = true;
        this.loginForm.Visible = true;
    }

    /// <summary>
    /// Sets the login form in active.
    /// </summary>
    public void SetLoginFormInActive()
    {
        this.login.Visible = false;
        this.loginForm.Visible = false;
    }

    /// <summary>
    /// Sets the login authenticated active.
    /// </summary>
    public void SetLoginAuthenticatedActive()
    {

        login.Visible = true;
        loginAuthenticated.Visible = true;
        if (UserState.CurrentUser != null)
        {
            this.UserNameLabel.Text = UserState.CurrentUser.FullName;
        }
        this.contentBody.Visible = true;
        this.ClearMessages();
    }

    /// <summary>
    /// Sets the login authenticated in active.
    /// </summary>
    public void SetLoginAuthenticatedInActive()
    {
        loginAuthenticated.Visible = false;
        this.UserNameLabel.Text = string.Empty;
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Url.ToString().IndexOf("demo") > 0)
        {
            DemoSite.Visible = true;
        }


        if (NeedsLogin)
        {
            this.SetLoginFormActive();
            this.SetLoginAuthenticatedInActive();
        }
        else if (UserState.OpenIdResponse != null && UserState.OpenIdResponse.IsAuthenticated)
        {
            SetLoginFormInActive();
            SetLoginAuthenticatedActive();
        }
        else
        {
            SetLoginFormInActive();
            SetLoginAuthenticatedInActive();
        }
    }

    private void activeTab(HyperLink activeTab)
    {
        this.HomeLink.CssClass = "";
        this.AboutLink.CssClass = "";
        this.SubmitObservationsLink.CssClass = "";
        this.ViewLink.CssClass = "";
        this.ResourcesLink.CssClass = "";

        activeTab.CssClass = "Active";
    }

    public void DisableLinks()
    {
        this.HomeLink.Enabled = false;
        this.AboutLink.Enabled = false;
        this.SubmitObservationsLink.Enabled = false;
        this.ViewLink.Enabled = false;
        this.ResourcesLink.Enabled = false;

    }


    protected void OpenIdLogin1_LoggedIn1(object sender, OpenIdEventArgs e)
    {
        ExceptionHandler(this, () =>
        {
            e.Cancel = true;

            if (e.Response != null)
            {

                string openId = e.Response.ClaimedIdentifier.ToString().Trim();

                UserState.OpenIdResponse = new OpenIdResponse(e.Response);

                if (UserState.OpenIdResponse.IsAuthenticated)
                {
                    UserState.CurrentUser = PersonFacade.Select(openId);

                    if (UserState.CurrentUser != null && UserState.CurrentUser.PersonStatus.Equals(Status.Active))
                    {
                        SetLoginFormInActive();
                        SetLoginAuthenticatedActive();
                    }
                    else
                    {
                        SetLoginAuthenticatedInActive();
                        SetLoginFormInActive();
                        Response.Redirect("~/Account/register.aspx", true);
                    }
                }
                else
                {
                    SetLoginAuthenticatedInActive();
                    SetLoginFormInActive();
                    Response.Redirect("~/Default.aspx", true);
                }
            }
        });
    }

    protected void OpenId_LoggedIn(IAuthenticationResponse response)
    {
        ExceptionHandler(this, () =>
        {
            if (response.Exception != null)
            {
                throw response.Exception;
            }
        });

    }

    protected void OpenIdLogin_Click(object sender, EventArgs e)
    {
        ExceptionHandler(this, () =>
        {
            if (string.IsNullOrEmpty(OpenIdTextBox1.Text))
            {
                loginFailedLabel.Visible = true;
                loginFailedLabel.Text = "Login requires that an OpenID provider be selected.";
                loginForm.Visible = true;
            }
            else
            {
                OpenIdTextBox1.LogOn();
            }
        });
    }

    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        ExceptionHandler(this, () =>
        {
            LogoutUser();
        });
    }

    private void LogoutUser()
    {

        UserState.OpenIdResponse = null;
        SetLoginAuthenticatedInActive();
        Response.Redirect("~/Default.aspx");
    }

}