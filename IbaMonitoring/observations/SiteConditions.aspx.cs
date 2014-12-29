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

            this.SiteVisited.Items.FindByValue(this.UserState.SiteVisit.LocationId.ToString()).Selected = true;
            this.VisitDate.Text = this.UserState.SiteVisit.StartTimeStamp.Value.ToShortDateString();
            this.SiteVisitObserver.Items.FindByValue(this.UserState.SiteVisit.ObserverId.ToString()).Selected = true;
            this.SiteVisitRecorder.Items.FindByValue(this.UserState.SiteVisit.RecorderId.ToString()).Selected = true;

            this.StartSky.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Sky.ToString()).Selected = true;
            if (this.UserState.SiteVisit.StartConditions.Temperature != null)
            {
                this.StartTemp_Radio.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Temperature.Units)
                    .Selected = true;
                this.StartTemperature.Text = this.UserState.SiteVisit.StartConditions.Temperature.Value.ToString();
            }
            this.StartTime.Text = this.UserState.SiteVisit.StartTimeStamp.Value.ToString("H:mm");
            this.StartWind.Items.FindByValue(this.UserState.SiteVisit.StartConditions.Wind.ToString()).Selected = true;

            this.EndSky.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Sky.ToString()).Selected = true;
            this.EndWind.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Wind.ToString()).Selected = true;
            if (this.UserState.SiteVisit.EndConditions.Temperature != null)
            {
                this.EndTemp_Radio.Items.FindByValue(this.UserState.SiteVisit.EndConditions.Temperature.Units).Selected
                    = true;
                this.EndTemperature.Text = this.UserState.SiteVisit.EndConditions.Temperature.Value.ToString();
            }
            this.EndTime.Text = this.UserState.SiteVisit.EndTimeStamp.Value.ToString("H:mm");
        }
    }


    private void bindDropDownLists()
    {
        this.SiteVisited.Items.Clear();
        this.SiteVisited.DataSource = GlobalMap.SiteList;
        this.SiteVisited.DataTextField = "Name";
        this.SiteVisited.DataValueField = "Id";
        this.SiteVisited.DataBind();

        this.SiteVisitObserver.Items.Clear();
        this.SiteVisitObserver.DataSource = GlobalMap.PersonList;
        this.SiteVisitObserver.DataTextField = "FullName";
        this.SiteVisitObserver.DataValueField = "Id";
        this.SiteVisitObserver.DataBind();

        this.SiteVisitRecorder.Items.Clear();
        this.SiteVisitRecorder.DataSource = GlobalMap.PersonList;
        this.SiteVisitRecorder.DataTextField = "FullName";
        this.SiteVisitRecorder.DataValueField = "Id";
        this.SiteVisitRecorder.DataBind();


        this.StartSky.DataBind();
        this.EndSky.DataBind();
        this.StartWind.DataBind();
        this.EndWind.DataBind();

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

        this.SiteVisited.ClearSelection();
        this.VisitDate.Text = string.Empty;
        this.SiteVisitObserver.ClearSelection();
        this.SiteVisitRecorder.ClearSelection();

        this.StartSky.ClearSelection();
        this.StartTemp_Radio.ClearSelection();
        this.StartTemperature.Text = string.Empty;
        this.StartTime.Text = string.Empty;
        this.StartWind.ClearSelection();

        this.EndSky.ClearSelection();
        this.EndWind.ClearSelection();
        this.EndTemp_Radio.ClearSelection();
        this.EndTemperature.Text = string.Empty;
        this.EndTime.Text = string.Empty;
    }

    protected void submitSiteConditions_Click(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            _factory.BuildSiteConditionsPresenter(this)
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

    public string SiteVisitObserverAccessor
    {
        get { return this.SiteVisitObserver.SelectedValue; }
    }

    public string SiteVisitRecorderAccessor
    {
        get { return this.SiteVisitRecorder.SelectedValue; }
    }

    public string SiteVisitedAccessor
    {
        get { return this.SiteVisited.SelectedValue; }
    }

    public string StartSkyAccessor
    {
        get { return this.StartSky.SelectedValue; }
    }

    public string StartTempUnitsAccessor
    {
        get { return this.StartTemp_Radio.SelectedValue; }
    }

    public string StartTempAccessor
    {
        get { return this.StartTemperature.Text; }
    }

    public string StartWindAccessor
    {
        get { return this.StartWind.SelectedValue; }
    }

    public string StartTimeAccessor
    {
        get { return this.StartTime.Text; }
    }

    public string EndSkyAccessor
    {
        get { return this.EndSky.SelectedValue; }
    }

    public string EndTempUnitsAccessor
    {
        get { return this.EndTemp_Radio.SelectedValue; }
    }

    public string EndTempAccessor
    {
        get { return this.EndTemperature.Text; }
    }
    public string EndWindAccessor
    {
        get { return this.EndWind.SelectedValue; }
    }

    public string EndTimeAccessor
    {
        get { return this.EndTime.Text; }
    }

    public string VisitDateAccessor
    {
        get { return this.VisitDate.Text; }
    }





    protected void StartNewSession_Click(object sender, EventArgs e)
    {
        this.resetSession();
    }

    protected void RetrieveIncomplete_Click(object sender, EventArgs e)
    {
        if (this.SiteVisited.SelectedIndex > -1 && !string.IsNullOrEmpty(this.VisitDate.Text))
        {
            List<SiteVisit> visitList = SiteVisitMapper.SelectAllForSite(Guid.Parse(this.SiteVisited.SelectedValue));
            SiteVisit theVisit = visitList.Find(x => !x.IsDataEntryComplete
                                                     &&
                                                     x.StartTimeStamp.Value.ToShortDateString()
                                                         .Equals(
                                                             DateTime.Parse(this.VisitDate.Text).ToShortDateString()));
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