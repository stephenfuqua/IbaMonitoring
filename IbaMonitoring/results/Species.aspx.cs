using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using safnet.iba.Business.AppFacades;
using safnet.iba.Business.DataTransferObjects;
using safnet.iba.Business.Entities;
using safnet.iba.Web;

public partial class SpeciesResults : IbaPage
{
    private const string Column_AdjustedCount = "Adjusted Count";
    private const string Column_GrandTotal = "Grand Total";
    private const string Column_SiteName = "Site Name";

    private Guid speciesId;

    protected void Page_Load(object sender, EventArgs e)
    {

        addHandlers();
        GetSpeciesID();

        if (!Page.IsPostBack)
        {
            this.State.SpeciesResultsTable = ResultsFacade.SiteBySpeciesPivotTable(speciesId, _chartValues,
                int.Parse(this.State.SelectedYear));

            bindSpecies();
            getMapDataAndBind();
        }

        bindSiteTable();
    }

    private void GetSpeciesID()
    {
        string speciesIdString = @Request.QueryString["speciesId"];

        // Validate that this is a real SpeciesId
        if (!Guid.TryParse(speciesIdString, out speciesId) ||
            !GlobalMap.SpeciesList.Exists(x => x.Id.Equals(speciesId)))
        {
            speciesId = Guid.Empty;
        }

    }

    private void addHandlers()
    {
        this.AvailableYears.ErrorOccurred += new EventHandler<ErrorEventArgs>(AvailableYears_ErrorOccurred);
        this.AvailableYears.YearChanged += new EventHandler(AvailableYears_YearChanged);
        this.SiteWeekHistogramGrid.RowDataBound += new GridViewRowEventHandler(SiteWeekHistogramGrid_RowDataBound);
        
        // TODO: restore mapping
        // this.Map.ZoomEnd += new EventHandler<Artem.Web.UI.Controls.GoogleZoomEventArgs>(Map_ZoomEnd);
        
    }

    protected void AvailableYears_YearChanged(object sender, EventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            this.State.SpeciesResultsTable = ResultsFacade.SiteBySpeciesPivotTable(speciesId, _chartValues,
                int.Parse(this.State.SelectedYear));

            bindSpecies();
            getMapDataAndBind();
        });
    }

    protected void AvailableYears_ErrorOccurred(object sender, ErrorEventArgs e)
    {
        IbaMasterPage.ExceptionHandler((IbaMasterPage)Master, () =>
        {
            throw e.Exception;
        });
    }

    private void SiteWeekHistogramGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string cssClass = string.Empty;
            foreach (TableCell cell in e.Row.Cells)
            {
                // skip the last two columns - grand total and adjusted count
                if (e.Row.Cells.GetCellIndex(cell) >= e.Row.Cells.Count - 2)
                {
                    break;
                }

                AssignCellColor(cssClass, cell);
            }
        }
    }


    //private void Map_ZoomEnd(object sender, Artem.Web.UI.Controls.GoogleZoomEventArgs e)
    //{
    //    bindMap();
    //}

    protected void bindSpecies()
    {
        Species sp = GlobalMap.SpeciesList.Single(x => x.Id.Equals(speciesId));
        this.CommonName.Text = sp.CommonName;
        this.ScientificName.Text = sp.ScientificName;

        this.AllAboutBirds.NavigateUrl =
            string.Format("http://www.google.com/search?hl=en&q={0}+site%3Awww.allaboutbirds.org",
                HttpUtility.UrlEncode(sp.CommonName));
    }

    protected void bindSiteTable()
    {
        this.SiteWeekHistogramGrid.Columns.Clear();

        foreach (DataColumn column in this.State.SpeciesResultsTable.Columns)
        {
            if (column.ColumnName != Column_SiteName
                && column.ColumnName != Column_GrandTotal
                && column.ColumnName != Column_AdjustedCount)
            {
                BoundField newField = new BoundField()
                {
                    AccessibleHeaderText = column.ColumnName,
                    DataField = column.ColumnName,
                    SortExpression = column.ColumnName,
                    ShowHeader = true,
                    HeaderText = column.ColumnName
                };

                // Must be added to the grid with date growing larger from left to right,
                // which is not guaranteed 
                bool found = false;
                DateTime columnDate = DateTime.Parse(column.ColumnName + "/10");

                for (int i = 0; i < SiteWeekHistogramGrid.Columns.Count; i++)
                {
                    DateTime iDate = DateTime.Parse(SiteWeekHistogramGrid.Columns[i].HeaderText + "/10");
                    if (columnDate < iDate)
                    {
                        SiteWeekHistogramGrid.Columns.Insert(i, newField);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    // This is the largest date yet, so add to end of list
                    SiteWeekHistogramGrid.Columns.Add(newField);
                }
            }
        }

        // Species Name needs to be the first column, and Grand Total as the last column
        SiteWeekHistogramGrid.Columns.Insert(0, new BoundField()
        {
            AccessibleHeaderText = Column_SiteName,
            DataField = Column_SiteName,
            SortExpression = Column_SiteName,
            ShowHeader = true,
            HeaderText = Column_SiteName
        });
        SiteWeekHistogramGrid.Columns.Add(new BoundField()
        {
            AccessibleHeaderText = Column_GrandTotal,
            DataField = Column_GrandTotal,
            SortExpression = Column_GrandTotal,
            ShowHeader = true,
            HeaderText = Column_GrandTotal
        });
        SiteWeekHistogramGrid.Columns.Add(new BoundField()
        {
            AccessibleHeaderText = Column_AdjustedCount,
            DataField = Column_AdjustedCount,
            SortExpression = Column_AdjustedCount,
            ShowHeader = true,
            HeaderText = "Adjusted Countl"
        });

        SiteWeekHistogramGrid.DataSource = this.State.SpeciesResultsTable;
        SiteWeekHistogramGrid.DataBind();

        // bind the histogram
        this.SpeciesChart.Series["Series1"].Points.DataBind(_chartValues, "Key", "Value", "Tooltip=Value");
        this.SpeciesChart.Series["Series1"].Sort(System.Web.UI.DataVisualization.Charting.PointSortOrder.Ascending,
            "X");
    }

    private SortedDictionary<DateTime, int> _chartValues = new SortedDictionary<DateTime, int>();


    protected void getMapDataAndBind()
    {
        this.State.MapList = ResultsFacade.GetSpeciesMap(speciesId, int.Parse(this.State.SelectedYear));
        bindMap();
    }

    private void bindMap()
    {
        this.Map.Polygons.Clear();
        foreach (MapCount count in this.State.MapList)
        {
            this.Map.Polygons.Add(new Artem.Web.UI.Controls.GoogleCirclePolygon()
            {
                Latitude = double.Parse(count.Latitude.ToString()),
                Longitude = double.Parse(count.Longitude.ToString()),
                StrokeColor = System.Drawing.Color.Goldenrod,
                StrokeOpacity = 0.5f,
                StrokeWeight = 1,
                FillColor = System.Drawing.Color.Gold,
                Radius = 0.25 + Math.Log(count.Count, 20)
            });
        }
    }


}