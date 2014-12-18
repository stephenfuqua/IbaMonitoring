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

public partial class SiteResults : IbaPage
{
    private Guid siteId;

    protected void Page_Load(object sender, EventArgs e)
    {

        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            addHandlers();

            GetSiteId();

            if (!Page.IsPostBack)
            {
                this.State.SiteResultsTable = ResultsFacade.SpeciesSitePivotTable(siteId,
                    int.Parse(this.State.SelectedYear));

                bindSite(siteId);
                //bindMap(siteId);
            }

            bindSiteTable();
        });
    }

    private void GetSiteId()
    {
        string siteIdString = @Request.QueryString["siteId"];
        if (!Guid.TryParse(siteIdString, out siteId) ||
            !GlobalMap.SiteList.Exists(x => x.Id.Equals(siteId)))
        {
            siteId = Guid.Empty;
        }
    }


    private void addHandlers()
    {
        this.AvailableYears.ErrorOccurred += new EventHandler<ErrorEventArgs>(AvailableYears_ErrorOccurred);
        this.AvailableYears.YearChanged += new EventHandler(AvailableYears_YearChanged);
        this.SupplementalDataSource.Selecting +=
            new ObjectDataSourceSelectingEventHandler(SupplementalDataSource_Selecting);
        this.SupplementalDataSource.Selected +=
            new ObjectDataSourceStatusEventHandler(SupplementalDataSource_Selected);
        this.SpeciesWeekHistogramGrid.RowDataBound +=
            new GridViewRowEventHandler(SpeciesWeekHistogramGrid_RowDataBound);
    }

    private void AvailableYears_YearChanged(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            //SupplementalDataSource.Select();
            SupplementalRepeater.DataBind();

            this.State.SiteResultsTable = ResultsFacade.SpeciesSitePivotTable(siteId,
                int.Parse(this.State.SelectedYear));
            bindSiteTable();
        });
    }

    private void AvailableYears_ErrorOccurred(object sender, ErrorEventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            throw e.Exception;
        });
    }

    private void SpeciesWeekHistogramGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cssClass = string.Empty;
            foreach (TableCell cell in e.Row.Cells)
            {
                if (e.Row.Cells.GetCellIndex(cell) == e.Row.Cells.Count - 1)
                {
                    break; // this is the Grand Total cell and it should be skipped
                }

                base.AssignCellColor(cssClass, cell);
            }
        }
    }

    private void SupplementalDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            if (e.ReturnValue == null)
            {
                this.NoSupplementals.Visible = true;
            }
        });
    }

    private void SupplementalDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {

        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            e.InputParameters.Add("siteId", siteId.ToString());
            e.InputParameters.Add("year", int.Parse(this.State.SelectedYear));
        });
    }

    protected void bindSite(Guid siteId)
    {
        Site sp = GlobalMap.SiteList.Single(x => x.Id.Equals(siteId));
        this.SiteName.Text = sp.Name;
    }

    protected void bindSiteTable()
    {
        SpeciesWeekHistogramGrid.Columns.Clear();

        foreach (DataColumn column in this.State.SiteResultsTable.Columns)
        {
            if (column.ColumnName != "Species Name" && column.ColumnName != "Grand Total")
            {
                BoundField newField = new BoundField()
                {
                    AccessibleHeaderText = column.ColumnName,
                    DataField = column.ColumnName,
                    SortExpression = column.ColumnName,
                    ShowHeader = true,
                    HeaderText = column.ColumnName
                };
                SpeciesWeekHistogramGrid.Columns.Add(newField);
            }
        }
        // Species Name needs to be the first column, and Grand Total as the last column
        SpeciesWeekHistogramGrid.Columns.Insert(0, new BoundField()
        {
            AccessibleHeaderText = "Species Name",
            DataField = "Species Name",
            SortExpression = "Species Name",
            ShowHeader = true,
            HeaderText = "Species Name"
        });
        SpeciesWeekHistogramGrid.Columns.Add(new BoundField()
        {
            AccessibleHeaderText = "Grand Total",
            DataField = "Grand Total",
            SortExpression = "Grand Total",
            ShowHeader = true,
            HeaderText = "Grand Total"
        });

        SpeciesWeekHistogramGrid.DataSource = this.State.SiteResultsTable;
        SpeciesWeekHistogramGrid.DataBind();

    }

    private SortedDictionary<string, int> _chartValues = new SortedDictionary<string, int>();
}
