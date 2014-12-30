using IbaMonitoring;
using IbaMonitoring.App_Code;
using IbaMonitoring.Views;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteConditionsPage : IbaPage, ISiteConditionsView
{

    private readonly IPresenterFactory _factory;

    public SiteConditionsPage(IPresenterFactory factory)
    {
        if (factory == null)
        {
            throw new ArgumentNullException("factory");
        }

        _factory = factory;
    }



    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            Master.SetObservationActive();

            if (!Page.IsPostBack)
            {
                bindDropDownLists();
                if (Master.IsAuthenticatedUser())
                {
                    loadCurrentSiteVisit();
                }
            }
        });
    }

    private void loadCurrentSiteVisit()
    {
        if (!this.UserState.SiteVisit.Id.Equals(Guid.Empty))
        {

            this.SiteVisitedInput.Items.FindByValue(this.UserState.SiteVisit.LocationId.ToString()).Selected = true;
            this.VisitDateInput.Text = this.UserState.SiteVisit.StartTimeStamp.Value.ToShortDateString();
            this.ObserverInput.Items.FindByValue(this.UserState.SiteVisit.ObserverId.ToString()).Selected = true;
            this.RecorderInput.Items.FindByValue(this.UserState.SiteVisit.RecorderId.ToString()).Selected = true;

            this.StartSkyInput.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Sky.ToString()).Selected = true;
            if (this.UserState.SiteVisit.StartConditions.Temperature != null)
            {
                this.StartTemp_Radio.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Temperature.Units)
                    .Selected = true;
                this.StartTemperatureInput.Text = this.UserState.SiteVisit.StartConditions.Temperature.Value.ToString();
            }
            this.StartTimeInput.Text = this.UserState.SiteVisit.StartTimeStamp.Value.ToString("H:mm");
            this.StartWindInput.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Wind.ToString()).Selected = true;

            this.EndSkyInput.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Sky.ToString()).Selected = true;
            this.EndWindInput.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Wind.ToString()).Selected = true;
            if (this.UserState.SiteVisit.EndConditions.Temperature != null)
            {
                this.EndTemp_Radio.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Temperature.Units).Selected
                    = true;
                this.EndTemperatureInput.Text = this.UserState.SiteVisit.EndConditions.Temperature.Value.ToString();
            }
            this.EndTimeInput.Text = this.UserState.SiteVisit.EndTimeStamp.Value.ToString("H:mm");
        }
    }


    private void bindDropDownLists()
    {
        this.SiteVisitedInput.Items.Clear();
        this.SiteVisitedInput.DataSource = GlobalMap.SiteList;
        this.SiteVisitedInput.DataTextField = "Name";
        this.SiteVisitedInput.DataValueField = "Id";
        this.SiteVisitedInput.DataBind();

        this.ObserverInput.Items.Clear();
        this.ObserverInput.DataSource = GlobalMap.PersonList;
        this.ObserverInput.DataTextField = "FullName";
        this.ObserverInput.DataValueField = "Id";
        this.ObserverInput.DataBind();

        this.RecorderInput.Items.Clear();
        this.RecorderInput.DataSource = GlobalMap.PersonList;
        this.RecorderInput.DataTextField = "FullName";
        this.RecorderInput.DataValueField = "Id";
        this.RecorderInput.DataBind();


        this.StartSkyInput.DataBind();
        this.EndSkyInput.DataBind();
        this.StartWindInput.DataBind();
        this.EndWindInput.DataBind();

        this.StartTemp_Radio.DataSource = this.EndTemp_Radio.DataSource = new List<ListItem>()
            {
                new ListItem("F", "F"),
                new ListItem("C", "C")
            };
        this.StartTemp_Radio.DataBind();
        this.StartTemp_Radio.SelectedIndex = 0;
        this.EndTemp_Radio.DataBind();
        this.EndTemp_Radio.SelectedIndex = 0;
    }

    private void resetSession()
    {
        this.UserState.SiteVisit = null;
        this.UserState.PointSurvey = null;
        this.UserState.PointsCompleted.Clear();
        this.UserState.PointsRemaining.Clear();
        this.UserState.SamplingPoint = null;

        clearAllFields();
    }

    private void clearAllFields()
    {

        this.SiteVisitedInput.ClearSelection();
        this.VisitDateInput.Text = string.Empty;
        this.ObserverInput.ClearSelection();
        this.RecorderInput.ClearSelection();

        this.StartSkyInput.ClearSelection();
        this.StartTemp_Radio.ClearSelection();
        this.StartTemperatureInput.Text = string.Empty;
        this.StartTimeInput.Text = string.Empty;
        this.StartWindInput.ClearSelection();

        this.EndSkyInput.ClearSelection();
        this.EndWindInput.ClearSelection();
        this.EndTemp_Radio.ClearSelection();
        this.EndTemperatureInput.Text = string.Empty;
        this.EndTimeInput.Text = string.Empty;
    }

    protected void submitSiteConditions_Click(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            _factory.BuildSiteConditionsPresenter()
                    .SaveConditions(this);
        });
    }

    protected void TemperatureValidator_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        string scale = string.Empty;
        if ((sender as Control).ID.Contains("End"))
        {
            scale = this.EndTemp_Radio.SelectedValue;
        }
        else
        {
            scale = this.StartTemp_Radio.SelectedValue;
        }

        try
        {
            args.IsValid = CustomValidation.ValidateTemperature(args.Value, scale);
        }
        catch (Exception)
        {
            args.IsValid = false;
        }
    }

    protected void StartTimeValidator_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        try
        {
            args.IsValid = CustomValidation.ValidateMorning(args.Value);
        }
        catch (Exception)
        {
            args.IsValid = false;
        }
    }

    public string Observer
    {
        get { return this.ObserverInput.SelectedValue; }
    }

    public string Recorder
    {
        get { return this.RecorderInput.SelectedValue; }
    }

    public string SiteVisited
    {
        get { return this.SiteVisitedInput.SelectedValue; }
    }

    public string StartSky
    {
        get { return this.StartSkyInput.SelectedValue; }
    }

    public string StartUnit
    {
        get { return this.StartTemp_Radio.SelectedValue; }
    }

    public string StartTemp
    {
        get { return this.StartTemperatureInput.Text; }
    }

    public string StartWind
    {
        get { return this.StartWindInput.SelectedValue; }
    }

    public string StartTime
    {
        get { return this.StartTimeInput.Text; }
    }

    public string EndSky
    {
        get { return this.EndSkyInput.SelectedValue; }
    }

    public string EndUnit
    {
        get { return this.EndTemp_Radio.SelectedValue; }
    }

    public string EndTemp
    {
        get { return this.EndTemperatureInput.Text; }
    }
    public string EndWind
    {
        get { return this.EndWindInput.SelectedValue; }
    }

    public string EndTime
    {
        get { return this.EndTimeInput.Text; }
    }

    public string VisitDate
    {
        get { return this.VisitDateInput.Text; }
    }





    protected void StartNewSession_Click(object sender, EventArgs e)
    {
        this.resetSession();
    }

    protected void RetrieveIncomplete_Click(object sender, EventArgs e)
    {
        if (this.SiteVisitedInput.SelectedIndex > -1 && !string.IsNullOrEmpty(this.VisitDateInput.Text))
        {
            List<SiteVisit> visitList = SiteVisitMapper.SelectAllForSite(Guid.Parse(this.SiteVisitedInput.SelectedValue));
            SiteVisit theVisit = visitList.Find(x => !x.IsDataEntryComplete
                                                     &&
                                                     x.StartTimeStamp.Value.ToShortDateString()
                                                         .Equals(
                                                             DateTime.Parse(this.VisitDateInput.Text).ToShortDateString()));
            if (theVisit != null)
            {
                resetSession();

                this.UserState.SiteVisit = theVisit;
                this.UserState.SiteVisit.SupplementalObservations.AddRange(
                    SupplementalObservationMapper.SelectAllForEvent(theVisit.Id));
                this.UserState.SiteVisit.PointSurveys.AddRange(PointSurveyMapper.SelectAllForSiteVisit(theVisit.Id));
                this.UserState.SiteVisit.PointSurveys.ForEach(x =>
                {
                    x.Observations.AddRange(ObservationMapper.SelectAllForEvent<PointCountBeyond50>(x.Id));
                    x.Observations.AddRange(ObservationMapper.SelectAllForEvent<PointCountWithin50>(x.Id));
                });
                List<SiteCondition> conditions = ConditionsMapper.Select_BySiteVisitId(theVisit.Id);
                if (conditions != null && conditions.Count.Equals(2))
                {
                    this.UserState.SiteVisit.StartConditions =
                        conditions.Single(x => x.Id.Equals(this.UserState.SiteVisit.StartConditions.Id));
                    this.UserState.SiteVisit.EndConditions =
                        conditions.Single(x => x.Id.Equals(this.UserState.SiteVisit.EndConditions.Id));
                }

                loadCurrentSiteVisit();
            }
            else
            {
                Master.SetErrorMessage(
                    "<p>No <i>incomplete</i> site visit entry has been started for this site and date.</p>");
            }
        }
        else
        {
            Master.SetErrorMessage(
                "<p>Both site and date must be filled in to retrieve an incomplete site visit.</p>");
        }
    }
}