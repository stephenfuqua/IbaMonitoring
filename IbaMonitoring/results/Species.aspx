<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true"
    CodeFile="Species.aspx.cs" Inherits="SpeciesResults" %>

<%@ Register src="AvailableYears.ascx" tagname="AvailableYears" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    IBA Monitoring: Results: Species
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("/styles/iba-widepage.css");
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Results By Species
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p style="margin-top: 0">
        <a href="Default.aspx">Return to Species List</a></p>
    <p>
        <span style="font-size: 1.2em; margin-top: 20px;">Common Name: <b>
            <asp:Label ID="CommonName" runat="server" Text="Label"></asp:Label></b><br />
            Scientific Name: <b><i>
                <asp:Label ID="ScientificName" runat="server" Text="Label"></asp:Label></i></b></span><br />
        <asp:HyperLink ID="AllAboutBirds" runat="server" Text="Search Cornell&#39;s AllAboutBirds.org" />
    </p>
    <p>
        <i>The date headings refer to the
            first day of the week in which the related site visit was completed; thus 4/11/10
            refers to the entire week from Sunday the 11<sup>th</sup> through Saturday the 17<sup>th</sup>.</i>
    </p>
    <uc1:AvailableYears ID="AvailableYears" runat="server" />
    <h2>
        Data Table, By Site</h2>
    <asp:GridView runat="server" ID="SiteWeekHistogramGrid" AutoGenerateColumns="false"
        HeaderStyle-CssClass="ListViewHeader" ShowHeader="true" CssClass="ListView" 
        AlternatingRowStyle-CssClass="ListViewAlternateItem" RowStyle-CssClass="ListViewItem">
    </asp:GridView>
 
    <p>
        <a name="AdjustedCount"></a><i>Adjusted Count</i> is an statistical measurement
        of species counts, taking into account the number of point surveys conducted
        for the season at each site. By thus taking into account the difference in effort from site
        to site, this measurement is more meaningful for site-to-site comparisons than the raw data alone.
    </p>
    <h2>
        Histogram, Totals Across All Sites</h2>
    <asp:Chart runat="server" ID="SpeciesChart" ImageType="Jpeg" Width="750px">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <h2>
        Map of Total Counts per Site</h2>
    <p>
        Circles on the map represent relative counts of sightings at each sampling point,
        scaled logarithmically.</p>
    <artem:GoogleMap ID="Map" runat="server" Width="600px" Height="500px" Key="ABQIAAAAqoxYXpHiKSjB-Xn4ISSPSBTXygCqbg1esTkISxOKz_w7Vztl5RRap0tdl-nLeaX4eNBwD4mIWwxHsQ"
        Latitude="44.855382" Longitude="-93.074112" Zoom="11" ShowMapTypeControl="true"
        DefaultMapView="Satellite" />
    <!-- need to add link to Google's Terms of use, and need link to privacy policy -->
</asp:Content>
