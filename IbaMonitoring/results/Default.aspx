<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="ResultsDefault" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register src="resultsnav.ascx" tagname="resultsnav" tagprefix="uc1" %>
<%@ Register src="AvailableYears.ascx" tagname="AvailableYears" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    IBA Monitoring: Results: Master Species List
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
    Results: Master Species List
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        The following table summarizes all point count observations at all Mississippi River
        IBA sites across all dates. Click on a species name to see more information about
        where and when that species has been observed.</p>
    
    <uc2:AvailableYears ID="AvailableYears" runat="server" />
    
    <p>
        <asp:GridView ID="MasterSpeciesCountGrid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataSourceID="MasterSpeciesListDataSource" PageSize="20" CssClass="Results">
            <PagerSettings PageButtonCount="20" />
            <RowStyle CssClass="RegularRow" />
            <Columns>
                <asp:HyperLinkField ItemStyle-CssClass="FirstColumn" AccessibleHeaderText="Common Name" DataTextField="CommonName"
                    HeaderText="Common Name" SortExpression="CommonName" DataNavigateUrlFields="SpeciesId"
                    DataNavigateUrlFormatString="~/results/Species.aspx?speciesId={0}" />
                <asp:BoundField ItemStyle-CssClass="FirstColumn" ItemStyle-Font-Italic="true" AccessibleHeaderText="Scientific Name" DataField="ScientificName"
                    HeaderText="Scientific Name" SortExpression="ScientificName" />
                <asp:BoundField AccessibleHeaderText="Count" DataField="SpeciesCount" HeaderText="Count"
                    ReadOnly="True" SortExpression="SpeciesCount" />
            </Columns>
            <FooterStyle CssClass="Footer" />
            <PagerStyle CssClass="Pager" />
            <EmptyDataTemplate>
                No observations have been submitted yet.
            </EmptyDataTemplate>
            <HeaderStyle CssClass="Header"/>
            <AlternatingRowStyle CssClass="AlternateRow" />
        </asp:GridView>
        <asp:ObjectDataSource ID="MasterSpeciesListDataSource" runat="server" SelectMethod="MasterCount"
            TypeName="safnet.iba.Business.AppFacades.ResultsFacade"></asp:ObjectDataSource>
    </p>
</asp:Content>
