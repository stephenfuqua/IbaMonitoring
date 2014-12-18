<%@ Control Language="C#" AutoEventWireup="true" Inherits="SurveySteps" Codebehind="SurveySteps.ascx.cs" %>
    <p>
        <asp:HyperLink runat="server" ID="siteConditions" NavigateUrl="SiteConditions.aspx">1. Site Conditions</asp:HyperLink> &rarr;
        <asp:HyperLink runat="server" ID="pointCount" NavigateUrl="PointCounts.aspx">2. Point Count Results</asp:HyperLink> &rarr;
        <asp:HyperLink runat="server" ID="supplemental" NavigateUrl="Supplemental.aspx">3. Supplemental</asp:HyperLink> &rarr;
        <asp:HyperLink runat="server" ID="review" NavigateUrl="Review.aspx">4. Review &amp; Submit</asp:HyperLink></p>
