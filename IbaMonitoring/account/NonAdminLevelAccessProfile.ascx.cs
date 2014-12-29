using IbaMonitoring;
using System;

public partial class account_NonAdminLevelAccessProfile : IbaUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string OpenID
        {
            get { return txtOpenID.Text; }
            set { txtOpenID.Text = value; }
        }

        public string FirstName
        {
            get { return txtFName.Text; }
            set { txtFName.Text = value; }
        }

        public string LastName
        {
            get { return txtLName.Text; }
            set { txtLName.Text = value; }
        }

        public string EmailAddress
        {
            get { return txtEmailAddress.Text; }
            set { txtEmailAddress.Text = value; }
        }

        public string Address1
        {
            get { return txtAddress1.Text; }
            set { txtAddress1.Text = value; }
        }

        public string Address2
        {
            get { return txtAddress2.Text; }
            set { txtAddress2.Text = value; }
        }

        public string City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }

        public string State
        {
            get { return txtState.Text; }
            set { txtState.Text = value; }
        }

        public string ZipCode
        {
            get { return txtZipCode.Text; }
            set { txtZipCode.Text = value; }
        }

        public string PhoneNumber
        {
            get { return txtPhoneNumber.Text; }
            set { txtPhoneNumber.Text = value; }
        }
    }