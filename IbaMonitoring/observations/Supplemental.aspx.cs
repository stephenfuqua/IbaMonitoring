using System;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using IbaMonitoring;
using safnet.iba;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.Entities.Observations;

public partial class Supplemental : IbaPage
{


    #region Public Methods

    [WebMethod(), ScriptMethod()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        return AutoComplete.GetSpeciesList(prefixText, count, contextKey);
    }

    #endregion

    #region Protected Methods

    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            if (Master.IsAuthenticatedUser())
            {
                Master.SetObservationActive();

                AddHandlers();
            }
        });
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            Response.Redirect("Review.aspx", true);
        });
    }

    #endregion

    #region Private Methods

    protected void SpeciesCodeTextBox_Validate(object sender, ServerValidateEventArgs args)
    {
        args.IsValid = GlobalMap.SpeciesList.Exists(x => x.AlphaCode.Equals(args.Value.ToUpper()));
    }

    private void AddHandlers()
    {
        this.SupplementalObservationList.ItemInserting +=
            new EventHandler<ListViewInsertEventArgs>(SupplementalObservationList_ItemInserting);
        this.SupplementalObservationList.ItemDataBound +=
            new EventHandler<ListViewItemEventArgs>(SupplementalObservationList_ItemDataBound);

        this.SupplementalObservationDataSource.ObjectCreating +=
            new ObjectDataSourceObjectEventHandler(SupplementalObservationDataSource_ObjectCreating);
        this.SupplementalObservationDataSource.Selecting +=
            new ObjectDataSourceSelectingEventHandler(SupplementalObservationDataSource_Selecting);
    }

    private void SupplementalObservationDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = new SupplementalObservationFacade(UserState);
    }


    private void SupplementalObservationList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Control ctl = e.Item.FindControl("CommonName");
            if (ctl != null)
            {
                SupplementalObservation rowView = e.Item.DataItem as SupplementalObservation;
                Label commonName = (Label)ctl;
                commonName.Text =
                    GlobalMap.SpeciesList.Find(x => x.AlphaCode.Equals(rowView.SpeciesCode.ToUpper())).CommonName;
            }
        }
    }


    private void SupplementalObservationDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters.Add("eventId", this.UserState.SiteVisit.Id.ToString());
    }

    private void SupplementalObservationList_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            e.Values.Add("EventId", this.UserState.SiteVisit.Id);
        });
    }

    #endregion

}
