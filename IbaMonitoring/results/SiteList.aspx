<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true"
    CodeFile="SiteList.aspx.cs" Inherits="SiteList" %>

<%@ Register Src="resultsnav.ascx" TagName="resultsnav" TagPrefix="uc1" %>
<%@ Register src="AvailableYears.ascx" TagName="AvailableYears" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    IBA Monitoring: Results: Site List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
    Data Analysis Results
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
    <uc1:resultsnav ID="resultsnav1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Results: Site List
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        The following tables gives prominent statistical community measures for each Site.
    </p>
    <uc2:AvailableYears ID="AvailableYears" runat="server" />
    <h3>
        Migration (through May 31)</h3>
    <asp:GridView ID="MigrationView" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="MigrationDataSource" PageSize="20" CssClass="ListView">
        <PagerSettings PageButtonCount="20" />
        <RowStyle CssClass="ListViewItem" />
        <Columns>
            <asp:HyperLinkField ItemStyle-CssClass="FirstColumn" AccessibleHeaderText="Location Name"
                DataTextField="LocationName" HeaderText="Location Name" SortExpression="LocationName"
                DataNavigateUrlFields="LocationId" DataNavigateUrlFormatString="~/results/Site.aspx?siteId={0}" />
            <asp:BoundField AccessibleHeaderText="Species Richness" DataField="Richness" HeaderText="Species Richness"
                ReadOnly="True" SortExpression="Richness" />
            <asp:BoundField AccessibleHeaderText="Shannon Index" DataField="DiversityIndex" HeaderText="Shannon Index"
                ReadOnly="true" SortExpression="DiversityIndex" />
            <asp:BoundField AccessibleHeaderText="Evenness" DataField="Evenness" HeaderText="Evenness"
                ReadOnly="true" SortExpression="Evenness" />
        </Columns>
        <FooterStyle CssClass="Footer" />
        <PagerStyle CssClass="Pager" />
        <EmptyDataTemplate>
            Insufficient data available for calculating these statistics.
        </EmptyDataTemplate>
        <HeaderStyle CssClass="ListViewHeader" />
        <AlternatingRowStyle CssClass="ListViewAlternateItem" />
    </asp:GridView>
    <h3>
        Breeding (after May 31)</h3>
    <p>
        Because species with few sightings may simply be passing through, and not actually
        setting up breeding territories, species with a relative abundance of less than
        .5% at each site have been dropped from these calculations.
    </p>
    <asp:GridView ID="BreedingGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="BreedingDataSource" PageSize="20" CssClass="ListView">
        <PagerSettings PageButtonCount="20" />
        <RowStyle CssClass="ListViewItem" />
        <Columns>
            <asp:HyperLinkField ItemStyle-CssClass="FirstColumn" AccessibleHeaderText="Location Name"
                DataTextField="LocationName" HeaderText="Location Name" SortExpression="LocationName"
                DataNavigateUrlFields="LocationId" DataNavigateUrlFormatString="~/results/Site.aspx?siteId={0}" />
            <asp:BoundField AccessibleHeaderText="Species Richness" DataField="Richness" HeaderText="Species Richness"
                ReadOnly="True" SortExpression="Richness" />
            <asp:BoundField AccessibleHeaderText="Shannon Index" DataField="DiversityIndex" HeaderText="Shannon Index"
                ReadOnly="true" SortExpression="DiversityIndex" />
            <asp:BoundField AccessibleHeaderText="Evenness" DataField="Evenness" HeaderText="Evenness"
                ReadOnly="true" SortExpression="Evenness" />
        </Columns>
        <FooterStyle CssClass="Footer" />
        <PagerStyle CssClass="Pager" />
        <EmptyDataTemplate>
            Insufficient data available for calculating these statistics.
        </EmptyDataTemplate>
        <HeaderStyle CssClass="ListViewHeader" />
        <AlternatingRowStyle CssClass="ListViewAlternateItem" />
    </asp:GridView>
    <p>
        Explanations of the statistics:
    </p>
    <ul>
        <li><em>Species Density </em>(not shown above) is a measure of the number of birds of
            a particular species per unit area (birds/hectare), based on species counts that
            have been adjusted for sampling effort. In this point count methodology, density
            is calculated using the log ratio of counts within and outside of 50 m to adjust
            for detection probability.</li>
        <li><a href="http://en.wikipedia.org/wiki/Species_richness">Species Richness</a> is
            essentially a count of distinct species observed at a given location; here, the
            count is based on species for which density could be calculated.</li>
        <li>The <a href="http://en.wikipedia.org/wiki/Shannon_index">Shannon Index</a> is a
            measure of the diversity of species present: the higher the number, the more diverse
            the population of species at the site. The calculation here is based on the densities
            of each species. </li>
        <li><a href="http://en.wikipedia.org/wiki/Species_evenness">Evenness</a> is calculated
            from the actual diversity and species richness. This measure ranges between 0 and
            1. The higher the number, the more <i>evenly distributed</i> are the counts for
            each species. So, the diversity index might be high because there are many species,
            but if only a handful of species dominate the landscape, then the evenness is said
            to be low.</li>
    </ul>
    <asp:ObjectDataSource runat="server" ID="BreedingDataSource" DataObjectTypeName="DataSet"
        SelectMethod="GetCommunityBreeding" TypeName="safnet.iba.Data.Mappers.ResultsMapper" />
    <asp:ObjectDataSource runat="server" ID="MigrationDataSource" DataObjectTypeName="DataSet"
        SelectMethod="GetCommunityMigration" TypeName="safnet.iba.Data.Mappers.ResultsMapper" />
</asp:Content>
