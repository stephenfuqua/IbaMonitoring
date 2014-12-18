using System;
using System.Data.SqlClient;
using IbaMonitoring;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;

public partial class Register : IbaPage
    {

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            // don't use Master.IsAuthenticatedUser() because that also checks to make sure the user already exists in the system, which doesn't make sense in this context.
            //if (this.UserState.OpenIdResponse != null && this.UserState.OpenIdResponse.IsAuthenticated)
            //{
            this.Master.ClearMessages();
            IbaMasterPage.ExceptionHandler(this.Master, () =>
            {
                if (!Page.IsPostBack)
                {
                    initializePage();
                }
            });
            //}
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    IbaMasterPage.ExceptionHandler(this.Master, () =>
                    {
                        RegisterUser();
                        this.btnRegister.Visible = false;
                    });
                    this.Master.SetSuccessMessage(
                        "<p>Your account registration has been submitted. You will receive an e-mail confirmation when your account has been activated by a site administrator.</p>");
                }

                catch (SqlException sqlEx)
                {
                    string message = "A database error has occurred";
                    if (sqlEx.Message.Contains("UNIQUE KEY"))
                    {
                        message =
                            "The OpenID you have chosen is already in use for another user. Perhaps you have already registered previously?";
                    }
                    else
                    {
                        Utility.LogSiteError(sqlEx);
                    }

                    this.Master.SetErrorMessage("<p>" + message + "</p>");
                }
            }
        }

        #endregion

        #region Private Methods

        private void initializePage()
        {

            NonAdminLevelAccessProfile1.FirstName = string.Empty;
            NonAdminLevelAccessProfile1.LastName = string.Empty;
            NonAdminLevelAccessProfile1.OpenID = this.UserState.OpenIdResponse.DatabaseIdentifier;
            NonAdminLevelAccessProfile1.PhoneNumber = string.Empty;
            NonAdminLevelAccessProfile1.Address1 = string.Empty;
            NonAdminLevelAccessProfile1.Address2 = string.Empty;
            NonAdminLevelAccessProfile1.City = string.Empty;
            NonAdminLevelAccessProfile1.State = string.Empty;
            NonAdminLevelAccessProfile1.ZipCode = string.Empty;
            NonAdminLevelAccessProfile1.EmailAddress = string.Empty;

        }

        private void RegisterUser()
        {
            Person newPerson = Person.CreatePerson();
            newPerson.PersonStatus = Status.Pending;
            newPerson.FirstName = NonAdminLevelAccessProfile1.FirstName;
            newPerson.LastName = NonAdminLevelAccessProfile1.LastName;
            newPerson.EmailAddress = NonAdminLevelAccessProfile1.EmailAddress;
            newPerson.PhoneNumber = NonAdminLevelAccessProfile1.PhoneNumber;
            newPerson.Address1 = NonAdminLevelAccessProfile1.Address1;
            newPerson.Address2 = NonAdminLevelAccessProfile1.Address2;
            newPerson.City = NonAdminLevelAccessProfile1.City;
            newPerson.State = NonAdminLevelAccessProfile1.State;
            newPerson.ZipCode = NonAdminLevelAccessProfile1.ZipCode;
            newPerson.Country = "USA";
            newPerson.PersonStatus = Status.Pending;
            newPerson.HasBeenTrained = false;
            newPerson.HasClipboard = false;
            newPerson.OpenId = NonAdminLevelAccessProfile1.OpenID;
            newPerson.PersonRole = Role.RegularUser;
            PersonFacade.Insert(newPerson);

            newPerson.SendRegistrationConfirmation(Utility.SendCompleted);
        }

        #endregion
    }