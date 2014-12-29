using IbaMonitoring;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Review : IbaPage
{

    #region Protected Methods

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler(this.Master, () =>
        {
            if (Master.IsAuthenticatedUser())
            {
                Master.SetObservationActive();

                AddHandlers();

                if (!Page.IsPostBack)
                {
                    bindControls();
                }
            }
        });
    }


    private void AddHandlers()
    {
        this.PointSurveyList.ItemDataBound += new EventHandler<ListViewItemEventArgs>(PointSurveyList_ItemDataBound);
    }

    private void PointSurveyList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        FiftyMeterPointSurvey survey = (FiftyMeterPointSurvey)item.DataItem;


        // find the SamplingPoint in order to get the right text for the PointName label
        SamplingPoint point =
            this.UserState.PointsRemaining.Union(this.UserState.PointsCompleted)
                .SingleOrDefault(x => x.Id.Equals(survey.SamplingPointId));
        if (point == null)
        {
            Master.SetErrorMessage(
                "<p>Apologies for the inconvenience, but there seems to have been an unexpected problem. Please close this browser window and start your data entry session over.</p>");
            return;
        }
        Label pointName = item.FindControl("SurveyPointName") as Label;
        pointName.Text = point.Name;

        Label startTime = item.FindControl("SurveyStartTime") as Label;
        Label noise = item.FindControl("SurveyNoise") as Label;
        Label count = item.FindControl("SurveyCountSpecies") as Label;


        if (survey.StartTimeStamp.HasValue)
        {
            startTime.Text = survey.StartTimeStamp.Value.ToString("yyyy-MM-dd HH:mm");
            noise.Text = survey.NoiseCode + " - " + survey.NoiseCodeText;
            count.Text = survey.Observations.GroupBy(x => x.SpeciesCode).Count().ToString();
        }
        else
        {
            startTime.Text = noise.Text = count.Text = "n/a";
        }
    }



    private void PointSurveyDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters.Add("siteVisitId", this.UserState.SiteVisit.Id.ToString());
        e.InputParameters.Add("state", this.UserState);
    }

    private void fakeDataSaveObservations(FiftyMeterPointSurvey survey)
    {
        foreach (FiftyMeterPointObservation observation in survey.Observations)
        {
            ObservationMapper.Insert(observation);
        }
    }


    protected void submitReview_Click(object sender, EventArgs e)
    {
        if (this.UserState.SiteVisit.PointSurveys.Count.Equals(0))
        {
            Master.SetErrorMessage(
                "<p>No point surveys have been entered yet. Please return to step 2 and complete at least one point survey for the site visit.</p>");
            this.SubmitReview.Enabled = false;
        }
        else
        {
            IbaMasterPage.ExceptionHandler(this.Master, () =>
            {
                this.UserState.SiteVisit.IsDataEntryComplete = true;
                SiteVisitMapper.Update(this.UserState.SiteVisit);

                this.UserState.SiteVisit = null;
                this.UserState.PointSurvey = null;
                this.UserState.PointsCompleted.Clear();
                this.UserState.PointsRemaining.Clear();
                this.UserState.SamplingPoint = null;

                Response.Redirect("../Default.aspx", true);
            });
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Binds the controls.
    /// </summary>
    private void bindControls()
    {
        SiteVisit siteVisit = this.UserState.SiteVisit;
        this.SiteVisited.Text = GlobalMap.SiteList.First(x => x.Id == siteVisit.LocationId).Name;
        this.SiteVisitObserver.Text = GlobalMap.PersonList.First(x => x.Id == siteVisit.ObserverId).FullName;
        this.SiteVisitRecorder.Text = GlobalMap.PersonList.First(x => x.Id == siteVisit.RecorderId).FullName;

        SiteCondition startConditions = siteVisit.StartConditions;
        this.StartTime.Text = siteVisit.StartTimeStamp.ToString();
        this.StartSky.Text = startConditions.SkyText;
        this.StartWind.Text = startConditions.WindText;
        this.StartTemperature.Text = startConditions.Temperature.ToString();

        SiteCondition endConditions = siteVisit.EndConditions;
        this.EndTime.Text = siteVisit.EndTimeStamp.ToString();
        this.EndSky.Text = endConditions.SkyText;
        this.EndTemperature.Text = endConditions.Temperature.ToString();
        this.EndWind.Text = endConditions.WindText;

        this.PointSurveyList.DataSource = siteVisit.PointSurveys.OrderBy(x => x.StartTimeStamp);
        this.PointSurveyList.DataBind();

        this.PointSurveyObservationList.DataSource = ReviewObservation.GetReviewList(this.UserState, this.GlobalMap);
        this.PointSurveyObservationList.DataBind();

        this.SupplementalList.DataSource = ReviewSupplemental.GetReviewSupplementalList(this.UserState, this.GlobalMap);
        this.SupplementalList.DataBind();
    }

    #endregion

}