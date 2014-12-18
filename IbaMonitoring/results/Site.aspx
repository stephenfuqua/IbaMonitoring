<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true"
    CodeFile="Site.aspx.cs" Inherits="SiteResults" %>

<%@ Register src="AvailableYears.ascx" tagname="AvailableYears" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    IBA Monitoring: Results: Site
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("/styles/iba-widepage.css");
        
        .float200
        {
            float: left;
            width: 200px;
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Results By Site
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p style="margin-top: 0">
        <a href="SiteList.aspx">Return to Site List</a></p>
    <p>
        <span style="font-size: 1.2em; margin-top: 20px;">Site Name: <b>
            <asp:Label ID="SiteName" runat="server" Text="Label"></asp:Label></b></span>
    </p>
    <p>
        <i>The date headings refer to the
            first day of the week in which the related site visit was completed; thus 4/11/10
            refers to the entire week from Sunday the 11<sup>th</sup> through Saturday the 17<sup>th</sup>.</i>
    </p>
    <uc1:AvailableYears ID="AvailableYears" runat="server" />
    <h2>
        Data Table, By Species</h2>
    <asp:GridView runat="server" ID="SpeciesWeekHistogramGrid" AutoGenerateColumns="false"
        HeaderStyle-CssClass="ListViewHeader" ShowHeader="true" CssClass="ListView" 
        AlternatingRowStyle-CssClass="ListViewAlternateItem" RowStyle-CssClass="ListViewItem">
    </asp:GridView>
    <h2>
        Supplemental Observations</h2>
    <p id="Supplemental">
        <asp:Repeater ID="SupplementalRepeater" runat="server" DataSourceID="SupplementalDataSource">
            <ItemTemplate>
                <span class="float200">
                    <%# DataBinder.Eval(Container.DataItem, "CommonName") %></span>
            </ItemTemplate>
        </asp:Repeater>
        <p runat="server" id="NoSupplementals" visible="false">
            No supplemental observations have been made at this Site in 2010.</p>
    </p>
    <p style="clear: left">
        &nbsp;</p>
    <asp:ObjectDataSource runat="server" ID="SupplementalDataSource" DataObjectTypeName="DataSet"
        SelectMethod="GetSiteSupplemental" TypeName="safnet.iba.Data.Mappers.ResultsMapper" />
</asp:Content>
