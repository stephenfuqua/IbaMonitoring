using IbaMonitoring;
using System;

public partial class account_OnlyAdminLevelAccessProfile : IbaUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string UserID
        {
            get { return tbxUserID.Text; }
            set { tbxUserID.Text = value; }
        }

        public bool HasUserBeenTrained
        {
            get { return cbHasBeenTrained.Checked; }
            set { cbHasBeenTrained.Checked = value; }
        }

        public bool HasUserClipboard
        {
            get { return cbHasClipboard.Checked; }
            set { cbHasClipboard.Checked = value; }
        }

        public string GpsSerialNumber
        {
            get { return tbxGpsSerialNumber.Text; }
            set { tbxGpsSerialNumber.Text = value; }
        }

        public short Status
        {
            get
            {
                short result = 0;
                Int16.TryParse(ddlStatus.SelectedValue, out result);
                return result;
            }
            set
            {
                if (value != 0)
                {
                    ddlStatus.SelectedValue = value.ToString();
                }
            }
        }

        public short PersonRole
        {
            get
            {
                short result = 0;
                Int16.TryParse(ddlRoles.SelectedValue, out result);
                return result;
            }
            set
            {
                if (value != 0)
                {
                    ddlRoles.SelectedValue = value.ToString();
                }
            }
        }
    }