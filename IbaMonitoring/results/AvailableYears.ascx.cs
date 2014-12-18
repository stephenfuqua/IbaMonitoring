using System;
using safnet.iba.Web;

public partial class results_AvailableYears : IbaUserControl
{

    public event EventHandler YearChanged;
    public event EventHandler<ErrorEventArgs> ErrorOccurred;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                // set the "current" year to the largest available year
                this.CurrentYear.Text = GlobalMap.AvailableYears.Max.ToString();
                if (string.IsNullOrEmpty(State.SelectedYear))
                {
                    State.SelectedYear = this.CurrentYear.Text;
                }

                // Load the available years
                this.AvailableYears.DataSource = GlobalMap.AvailableYears;
                this.AvailableYears.DataBind();

                this.AvailableYears.SelectedValue = this.CurrentYear.Text;
            }
        }
        catch (Exception ex)
        {
            OnErrorOccurred(new ErrorEventArgs(ex));
        }
    }

    protected void ChangeYear_Click(object sender, EventArgs e)
    {
        try
        {
            this.CurrentYear.Text = State.SelectedYear = AvailableYears.SelectedValue;
            OnYearChanged(e);
        }
        catch (Exception ex)
        {
            OnErrorOccurred(new ErrorEventArgs(ex));
        }
    }

    protected virtual void OnYearChanged(EventArgs e)
    {
        if (YearChanged != null)
        {
            YearChanged(this, e);
        }
    }

    protected virtual void OnErrorOccurred(ErrorEventArgs e)
    {
        if (ErrorOccurred != null)
        {
            ErrorOccurred(this, e);
        }
    }
}

public class ErrorEventArgs : EventArgs
{
    /// <summary>
    /// Gets or sets the exception.
    /// </summary>
    /// <value>The exception.</value>
    public Exception Exception { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorEventArgs"/> class.
    /// </summary>
    /// <param name="ex">The ex.</param>
    public ErrorEventArgs(Exception ex)
    {
        Exception = ex;
    }
}