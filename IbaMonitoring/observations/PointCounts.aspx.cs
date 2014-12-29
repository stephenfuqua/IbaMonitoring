using IbaMonitoring;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities;
using safnet.iba.Business.Entities.Observations;
using safnet.iba.Data.Mappers;
using safnet.iba.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PointCounts : IbaPage
    {

        #region Public Methods

        [WebMethod(), ScriptMethod()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            return AutoComplete.GetSpeciesList(prefixText, count, contextKey);
        }

        #endregion

        #region Protected Methods

        protected void NextPoint_Click(object sender, EventArgs e)
        {
            IbaMasterPage.ExceptionHandler(Master, () =>
            {
                saveSurvey();
                moveToNextPoint();

                this.ObservationsList.DataSource = null;
                this.PointCountObservationDataSource.Select();
            });
        }

        private void moveToNextPoint()
        {
            int index = this.UserState.PointsRemaining.IndexOf(this.UserState.SamplingPoint);
            if (index >= 0)
            {
                this.UserState.PointsRemaining.RemoveAt(index);

                this.UserState.PointsCompleted.Add(this.UserState.SamplingPoint);
            }
            if (this.UserState.PointsRemaining.Count > 0)
            {
                Supplemental.Visible = false;
                if (index == this.UserState.PointsRemaining.Count || index.Equals(-1))
                {
                    index = 0;
                }
                this.UserState.SamplingPoint = this.UserState.PointsRemaining[index];

                this.SitePointsList.DataBind();
                this.NoiseCodeDropDown.ClearSelection();
                this.Time.Text = string.Empty;
            }
            else
            {
                // All the points have been completed and we go here without validation problems, which means
                // must have clicked the "Skip this point" link. Therefore disable validation for Supplemental.
                this.Supplemental.CausesValidation = false;
            }

            if (this.UserState.PointsRemaining.Count.Equals(1))
            {
                this.NextPoint.Visible = false;
                Supplemental.Visible = true;
            }

            loadPointSurveyForm();
        }

        private void saveSurvey()
        {
            FiftyMeterPointSurvey survey = getCurrentPointSurvey();

            DateTime newStartTime =
                DateTime.Parse(this.UserState.SiteVisit.StartTimeStamp.Value.ToShortDateString() + " " + this.Time.Text);

            survey.EndTimeStamp = newStartTime.AddMinutes(5);
            survey.StartTimeStamp = newStartTime;
            survey.NoiseCode = byte.Parse(this.NoiseCodeDropDown.SelectedValue);

            PointSurveyMapper.Update(survey);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Master.ClearMessages();

            IbaMasterPage.ExceptionHandler(Master, () =>
            {
                if (Master.IsAuthenticatedUser())
                {
                    AddHandlers();

                    if (!IsPostBack)
                    {
                        this.loadSiteName();
                    }
                }
            });
        }

        protected void Supplemental_Click(object sender, EventArgs e)
        {
            IbaMasterPage.ExceptionHandler(Master, () =>
            {
                // don't need to save if the point was skipped
                if (!string.IsNullOrEmpty(this.Time.Text.Trim()))
                {
                    saveSurvey();
                }

                // next step is needed to get the point arrays in session set properly
                moveToNextPoint();

                Response.Redirect("~/observations/Supplemental.aspx", true);
            });
        }

        protected void TimeValidator_OnServerValidate(object sender, ServerValidateEventArgs args)
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


        protected void SpeciesCodeTextBox_Validate(object sender, ServerValidateEventArgs args)
        {
            args.IsValid = GlobalMap.SpeciesList.Exists(x => x.AlphaCode.Equals(args.Value.ToUpper()));
        }

        #endregion

        #region Private Methods

        private void AddHandlers()
        {
            this.Error += new EventHandler(PointCounts_Error);
            this.SitePointDataSource.Selecting +=
                new ObjectDataSourceSelectingEventHandler(SitePointDataSource_Selecting);
            this.SitePointDataSource.Selected += new ObjectDataSourceStatusEventHandler(SitePointDataSource_Selected);

            this.SitePointsList.ItemDataBound += new EventHandler<ListViewItemEventArgs>(SitePointsList_ItemDataBound);
            this.SitePointsList.ItemCommand += new EventHandler<ListViewCommandEventArgs>(SitePointsList_ItemCommand);
            this.SitePointsList.DataBound += new EventHandler(SitePointsList_DataBound);

            this.ObservationsList.ItemInserting +=
                new EventHandler<ListViewInsertEventArgs>(ObservationsList_ItemInserting);
            this.ObservationsList.ItemDataBound +=
                new EventHandler<ListViewItemEventArgs>(ObservationsList_ItemDataBound);

            this.PointCountObservationDataSource.Selecting +=
                new ObjectDataSourceSelectingEventHandler(PointCountObservationDataSource_Selecting);
            this.PointCountObservationDataSource.ObjectCreating +=
                new ObjectDataSourceObjectEventHandler(PointCountObservationDataSource_ObjectCreating);
        }

        protected void PointCountObservationDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = new FiftyMeterDataEntryFacade(UserState);
        }

        protected void ObservationsList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Control ctl = e.Item.FindControl("CommonName");
                if (ctl != null)
                {
                    FiftyMeterDataEntry rowView = e.Item.DataItem as FiftyMeterDataEntry;
                    Label commonName = (Label) ctl;
                    commonName.Text =
                        GlobalMap.SpeciesList.Find(x => x.AlphaCode.Equals(rowView.SpeciesCode.ToUpper())).CommonName;
                }
            }
        }

        private void PointCountObservationDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters.Add("eventId", UserState.PointSurvey.Id.ToString());
        }

        private void PointCounts_Error(object sender, EventArgs e)
        {
            this.Master.SetErrorMessage(e.ToString());
        }

        private FiftyMeterPointSurvey getCurrentPointSurvey()
        {
            if (this.SitePointsList.Items.Count.Equals(0))
            {
                // TODO figure out the proper response???
                return null;
            }

            if (this.UserState.SamplingPoint == null)
            {
                SamplingPoint point = this.SitePointsList.Items[0].DataItem as SamplingPoint;
                this.UserState.SamplingPoint = point;
            }

            FiftyMeterPointSurvey survey =
                this.UserState.SiteVisit.PointSurveys.Find(x => x.LocationId == this.UserState.SamplingPoint.Id);
            if (survey == null)
            {
                survey = FiftyMeterPointSurvey.CreateNewPointSurvey(this.UserState.SamplingPoint.Id);
                survey.SiteVisitId = this.UserState.SiteVisit.Id;
                this.UserState.SiteVisit.PointSurveys.Add(survey);
            }
            this.UserState.PointSurvey = survey;

            this.ObservationsList.DataBind();

            return survey;
        }


        private void loadSiteName()
        {
            Site thisSite = GlobalMap.SiteList.Find(x => x.Id == this.UserState.SiteVisit.LocationId);
            if (thisSite != null)
            {
                this.SiteName.Text = thisSite.Name;
            }
            else
            {
                Master.SetErrorMessage(
                    "Something strange happened: the web site thinks you haven't selected an IBA site yet. Please start over.");
            }
        }

        private void ObservationsList_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            if (Page.IsValid)
            {
                // Add the current survey ID to the object before it is saved
                FiftyMeterPointSurvey survey = getCurrentPointSurvey();
                if (survey != null)
                {
                    e.Values.Add("PointSurveyId", survey.Id);
                }

                saveSurvey();
            }
        }


        private void SitePointDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            IbaMasterPage.ExceptionHandler(Master, () =>
            {
                if (this.UserState.PointsRemaining.Count.Equals(0))
                {
                    List<SamplingPoint> list = ((List<SamplingPoint>) e.ReturnValue);
                    if (list == null)
                    {
                        Master.SetErrorMessage("No sampling points are defined for Site ID " +
                                               this.UserState.SiteVisit.LocationId.ToString());
                        // TODO log this as an error somehow
                        return;
                    }
                    this.UserState.PointsRemaining.AddRange(list.OrderBy(x => x.Name));
                }
            });
        }

        private void SitePointDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters.Add("siteId", this.UserState.SiteVisit.LocationId.ToString());
        }

        private void SitePointsList_DataBound(object sender, EventArgs e)
        {
            loadPointSurveyForm();
        }

        private void loadPointSurveyForm()
        {
            FiftyMeterPointSurvey survey = getCurrentPointSurvey();
            if (survey != null)
            {
                this.PointCountObservationDataSource.DataBind();

                this.NoiseCodeDropDown.DataBind();
                this.NoiseCodeDropDown.Items.FindByValue(survey.NoiseCode.ToString()).Selected = true;
                if (survey.StartTimeStamp.HasValue)
                {
                    this.Time.Text = survey.StartTimeStamp.Value.ToString("H:mm");
                }


                this.PointCountObservationDataSource.Select();
            }
        }

        private void SitePointsList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            IbaMasterPage.ExceptionHandler(Master, () =>
            {
                // User has chosen a new point for which to collect data. Set it as the current point and re-bind the list
                Guid id = new Guid(e.CommandArgument.ToString());
                this.UserState.SamplingPoint =
                    this.UserState.PointsRemaining.Union(this.UserState.PointsCompleted).SingleOrDefault(x => x.Id == id);


                SitePointsList.DataBind();
            });
        }

        private void SitePointsList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem item = (ListViewDataItem) e.Item;
            SamplingPoint point = (SamplingPoint) item.DataItem;

            // Setup the hyperlinks for the list of points
            LinkButton pointName = item.FindControl("PointName") as LinkButton;
            pointName.CommandName = "Switch";
            pointName.CommandArgument = point.Id.ToString();
            pointName.Text = point.Name;

            if (this.UserState.SamplingPoint == null)
            {
                this.UserState.SamplingPoint = point;
            }

            // Determine which type of arrow should be displayed
            if (this.UserState.SamplingPoint.Id == point.Id)
            {
                Image arrow = item.FindControl("PointArrow") as Image;
                arrow.Visible = true;
                pointName.CssClass = "CurrentPoint";
                pointName.Enabled = false;

                this.CurrentPointName.Text = point.Name;
            }
            else if (!this.UserState.PointsRemaining.Count.Equals(0) && !this.UserState.PointsRemaining.Contains(point))
            {
                Image okay = item.FindControl("PointOkay") as Image;
                okay.Visible = true;
                pointName.CssClass = "CompletedPoint";
            }
            else
            {
                Image arrow = item.FindControl("PointSilver") as Image;
                arrow.Visible = true;
            }
        }

        #endregion

        protected void SkipPoint_Click(object sender, EventArgs e)
        {

            moveToNextPoint();
        }
    }