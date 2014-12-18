using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using IbaMonitoring;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;

public partial class Account : IbaPage
{


    #region Private Methods

    private void loadListViewControl()
    {
        if (this.UserState.CurrentUser != null)
        {
            List<Person> userlist = PersonFacade.SelectAll();

            if (ExcludeInactive.Value.Equals("true"))
            {
                userlist = (from users in userlist
                            where users.PersonStatus != Status.Inactive
                            select users).ToList<Person>();
            }

            lvUsers.DataSource = userlist;
            lvUsers.DataBind();
            NonAdminLevelAccessProfile1.Visible = false;
            OnlyAdminLevelAccessProfile1.Visible = false;
        }
        else
        {
            NonAdminLevelAccessProfile1.Visible = false;
            OnlyAdminLevelAccessProfile1.Visible = false;
            UserProfilesPanel.Visible = false;
        }
    }

    private void loadDetailUserProfile(Person person)
    {
        refershUserProfileControls();
        if (person != null)
        {
            NonAdminLevelAccessProfile1.Address1 = person.Address1;
            NonAdminLevelAccessProfile1.Address2 = person.Address2;
            NonAdminLevelAccessProfile1.City = person.City;
            NonAdminLevelAccessProfile1.EmailAddress = person.EmailAddress;
            NonAdminLevelAccessProfile1.FirstName = person.FirstName;
            NonAdminLevelAccessProfile1.LastName = person.LastName;
            NonAdminLevelAccessProfile1.OpenID = person.OpenId;
            NonAdminLevelAccessProfile1.PhoneNumber = person.PhoneNumber;
            NonAdminLevelAccessProfile1.State = person.State;
            NonAdminLevelAccessProfile1.ZipCode = person.ZipCode;

            OnlyAdminLevelAccessProfile1.HasUserClipboard = person.HasClipboard;
            OnlyAdminLevelAccessProfile1.HasUserBeenTrained = person.HasBeenTrained;
            OnlyAdminLevelAccessProfile1.Status =
                (short)Enum.Parse(typeof(Status), person.PersonStatus.ToString());
            OnlyAdminLevelAccessProfile1.UserID = person.Id.ToString();
            OnlyAdminLevelAccessProfile1.PersonRole =
                (short)Enum.Parse(typeof(Role), person.PersonRole.ToString());

            OnlyAdminLevelAccessProfile1.Visible = false;
            NonAdminLevelAccessProfile1.Visible = true;
            if (this.UserState.CurrentUser.PersonRole.Equals(Role.Administrator))
            {
                OnlyAdminLevelAccessProfile1.Visible = true;
            }
        }

    }

    private void refershUserProfileControls()
    {
        NonAdminLevelAccessProfile1.Address1 = string.Empty;
        NonAdminLevelAccessProfile1.Address2 = string.Empty;
        NonAdminLevelAccessProfile1.City = string.Empty;
        NonAdminLevelAccessProfile1.EmailAddress = string.Empty;
        NonAdminLevelAccessProfile1.FirstName = string.Empty;
        NonAdminLevelAccessProfile1.LastName = string.Empty;
        NonAdminLevelAccessProfile1.OpenID = string.Empty;
        NonAdminLevelAccessProfile1.PhoneNumber = string.Empty;
        NonAdminLevelAccessProfile1.State = string.Empty;
        NonAdminLevelAccessProfile1.ZipCode = string.Empty;

        if (this.UserState.CurrentUser.PersonRole.Equals(Role.Administrator))
        {
            OnlyAdminLevelAccessProfile1.HasUserClipboard = false;
            OnlyAdminLevelAccessProfile1.HasUserBeenTrained = false;
            OnlyAdminLevelAccessProfile1.Status = 0;
            OnlyAdminLevelAccessProfile1.UserID = string.Empty;
            ;
        }
    }

    private void UpdateUserProfile()
    {
        Person toupdate = PersonFacade.Select(new Guid(OnlyAdminLevelAccessProfile1.UserID));
        bool statusChanged = toupdate.PersonStatus.Equals(Status.Pending)
                             && ((Status)OnlyAdminLevelAccessProfile1.Status).Equals(Status.Active);

        toupdate.OpenId = NonAdminLevelAccessProfile1.OpenID;
        toupdate.FirstName = NonAdminLevelAccessProfile1.FirstName;
        toupdate.LastName = NonAdminLevelAccessProfile1.LastName;
        toupdate.EmailAddress = NonAdminLevelAccessProfile1.EmailAddress;
        toupdate.PhoneNumber = NonAdminLevelAccessProfile1.PhoneNumber;
        toupdate.Address1 = NonAdminLevelAccessProfile1.Address1;
        toupdate.Address2 = NonAdminLevelAccessProfile1.Address2;
        toupdate.City = NonAdminLevelAccessProfile1.City;
        toupdate.State = NonAdminLevelAccessProfile1.State;
        toupdate.ZipCode = NonAdminLevelAccessProfile1.ZipCode;
        toupdate.Country = "USA";
        toupdate.PersonStatus = (Status)OnlyAdminLevelAccessProfile1.Status;
        toupdate.PersonRole = (Role)OnlyAdminLevelAccessProfile1.PersonRole;
        toupdate.HasBeenTrained = OnlyAdminLevelAccessProfile1.HasUserBeenTrained;
        toupdate.HasClipboard = OnlyAdminLevelAccessProfile1.HasUserClipboard;

        PersonFacade.Update(toupdate);
        if (statusChanged)
        {
            toupdate.SendActivationConfirmation(Utility.SendCompleted);
        }

        this.Master.SetSuccessMessage("<p>User profile has been updated.</p>");

    }

    #endregion

    #region Protected Methods

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Master.IsAuthenticatedUser())
        {
            IbaMasterPage.ExceptionHandler(this.Master, () =>
            {
                if (!Page.IsPostBack)
                {
                    this.ItemDataPager.PreRender -= new EventHandler(ItemDataPager_PreRender);
                    //Disable this event handler so that it will not be called during page is loaded first time

                    if (this.UserState.CurrentUser.IsAdministrator)
                    {
                        loadListViewControl();
                    }
                    else
                    {
                        loadDetailUserProfile(this.UserState.CurrentUser);
                    }
                }
            });
        }
    }



    protected void lvUsers_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        this.ItemDataPager.PreRender -= new EventHandler(ItemDataPager_PreRender);
        //Disable this event handler so that it will not be called during the call of this event handler.
        IbaMasterPage.ExceptionHandler(this.Master, () =>
        {
            try
            {
                ImageButton ibEditUpdate = e.Item.FindControl("btnEdit") as ImageButton;
                ImageButton ibDeleteCancel = e.Item.FindControl("btnDelete") as ImageButton;
                HiddenField IdLabel = e.Item.FindControl("PersonId") as HiddenField;
                Guid personId = new Guid(IdLabel.Value);
                HiddenField statusLabel = e.Item.FindControl("StatusLabel") as HiddenField;
                string status = statusLabel.Value;

                if (e.CommandName.Equals("EditOrUpdateCommand"))
                {

                    if (!personId.Equals(Guid.Empty))
                    {
                        EditProfile.Visible = true;
                        loadDetailUserProfile(PersonFacade.Select(personId));
                    }
                }
                else if (e.CommandName.Equals("DeleteOrCancelCommand"))
                {
                    if (!personId.Equals(Guid.Empty))
                    {
                        // Real deletes are only allowed for pending users; others are just inactived
                        if (status.Equals("Pending"))
                        {
                            PersonFacade.Delete(new Person() { Id = personId });
                        }
                        else
                        {
                            Person toupdate = PersonFacade.Select(personId);
                            toupdate.PersonStatus = Status.Inactive;
                            PersonFacade.Update(toupdate);
                        }

                        loadListViewControl();
                        this.Master.SetSuccessMessage("<p>Successful user profile delete.</p>");
                    }
                }

            }
            catch (SqlException exc)
            {
                Utility.LogSiteError(exc);
                this.Master.SetErrorMessage("<p>A database error occurred.</p>");
            }
        });
    }

    protected void ItemDataPager_PreRender(object sender, EventArgs e)
    {

        loadListViewControl();
    }


    protected void lvUsers_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

    protected void lvUsers_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

    }

    protected void lvUsers_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
    {


    }

    protected void lvUsers_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            UpdateUserProfile();
            EditProfile.Visible = false;
        }
    }

    #endregion

    protected void ShowInactive_Click(object sender, EventArgs e)
    {
        if (ExcludeInactive.Value.Equals("true"))
        {
            ShowInactive.Text = "Hide inactive users";
            ExcludeInactive.Value = "false";
            loadListViewControl();
        }
        else
        {
            ShowInactive.Text = "Show inactive users";
            ExcludeInactive.Value = "true";
            loadListViewControl();
        }
    }
}
