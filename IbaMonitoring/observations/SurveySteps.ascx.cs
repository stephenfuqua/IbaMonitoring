using System;
using System.ComponentModel;
using System.Web.UI;

public partial class SurveySteps : UserControl
{

    private const string CurrentLinkClass = "CurrentStep";

    [Browsable(true)]
    public bool SiteConditionsIsActive { get; set; }

    [Browsable(true)]
    public bool PointCountIsActive { get; set; }

    [Browsable(true)]
    public bool SupplementalIsActive { get; set; }

    [Browsable(true)]
    public bool ReviewIsActive { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Url.ToString().Contains("SiteConditions"))
        {
            siteConditions.CssClass = CurrentLinkClass;
        }

        if (Request.Url.ToString().Contains("PointCounts"))
        {
            pointCount.CssClass = CurrentLinkClass;
        }

        if (Request.Url.ToString().Contains("Supplemental"))
        {
            supplemental.CssClass = CurrentLinkClass;
        }

        if (Request.Url.ToString().Contains("Review"))
        {
            review.CssClass = CurrentLinkClass;
        }
    }
}
