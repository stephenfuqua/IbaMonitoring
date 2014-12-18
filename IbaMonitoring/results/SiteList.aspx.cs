using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IbaMonitoring;
using safnet.iba.Business.AppFacades;
using System.Data;
using safnet.iba.Business.Entities;
using safnet.iba.Static;
using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Web;

public partial class SiteList : IbaPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            this.AvailableYears.ErrorOccurred += new EventHandler<ErrorEventArgs>(AvailableYears_ErrorOccurred);
            this.AvailableYears.YearChanged += new EventHandler(AvailableYears_YearChanged);
            this.BreedingDataSource.Selecting +=
                new ObjectDataSourceSelectingEventHandler(BreedingDataSource_Selecting);
            this.MigrationDataSource.Selecting +=
                new ObjectDataSourceSelectingEventHandler(MigrationDataSource_Selecting);
        });
    }

    protected void MigrationDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            e.InputParameters.Add("year", int.Parse(this.State.SelectedYear));
        });
    }

    protected void BreedingDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            e.InputParameters.Add("year", int.Parse(this.State.SelectedYear));
        });
    }

    protected void AvailableYears_YearChanged(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            MigrationDataSource.Select();
            MigrationView.DataBind();

            BreedingDataSource.Select();
            BreedingGrid.DataBind();
        });
    }

    protected void AvailableYears_ErrorOccurred(object sender, ErrorEventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            throw e.Exception;
        });
    }
}
