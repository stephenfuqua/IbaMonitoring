using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IbaMonitoring;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.Web;

public partial class ResultsDefault : IbaPage
{


    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler(Master, () =>
        {

            Master.SetViewActive();

            this.AvailableYears.YearChanged += new EventHandler(AvailableYears_YearChanged);
            this.AvailableYears.ErrorOccurred += new EventHandler<ErrorEventArgs>(AvailableYears_ErrorOccurred);
            this.MasterSpeciesListDataSource.Selecting +=
                new ObjectDataSourceSelectingEventHandler(MasterSpeciesListDataSource_Selecting);
        });
    }

    protected void AvailableYears_ErrorOccurred(object sender, ErrorEventArgs args)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            throw args.Exception;
        });
    }

    protected void AvailableYears_YearChanged(object sender, EventArgs args)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            MasterSpeciesListDataSource.Select();
            MasterSpeciesCountGrid.DataBind();
        });
    }

    private void MasterSpeciesListDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        IbaMasterPage.ExceptionHandler(Master, () =>
        {
            e.InputParameters.Add("year", int.Parse(this.State.SelectedYear));
        });
    }
}