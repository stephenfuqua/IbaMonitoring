<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="about_Default" Codebehind="Default.aspx.cs" %>

<%@ Register src="aboutnav.ascx" tagname="aboutnav" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    About: Program Background  (Important Bird Area Monitoring Program)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">About
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">

    <uc1:aboutnav ID="aboutnav1" runat="server" />

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Program Background
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        As human-dominated land uses replace native landscapes across North America, there
        is growing concern about the impacts this habitat loss will have on native bird
        populations. With many migratory bird species in decline, it is essential to assess
        the effectiveness of our conservation initiatives.&nbsp; The goal of Audubon’s Important
        Bird Area (IBA) program is to identify areas that are particularly valuable habitat
        to one or more species of bird, during migration, breeding, or winter seasons.
    </p>
    <p>
        The Mississippi River Twin Cities IBA stretches from downtown Minneapolis to Hastings,
        MN, roughly following the bounds of the Mississippi National River and Recreation
        Area (MNRRA) and covering over 37,000 acres.&nbsp; Located on the Mississippi Flyway,
        much of the value of this IBA lies in its function as migratory habitat. While ongoing
        surveys of waterbirds indicate large numbers of these birds use this IBA, less is
        known about its potential value to migrating landbirds.&nbsp;
    </p>
    <p>
        This monitoring project seeks to evaluate trends in land bird species in the Mississippi
        River Twin Cities IBA in order to gain a fuller understanding of how this habitat
        benefits this group of birds.
    </p>
    <h2>
        Goals</h2>
    <p>
        The primary objective of this project is to monitor land birds in the Mississippi
        River Twin Cities IBA using a standardized survey methodology carried out by teams
        of volunteers during migration, breeding season, and winter. With this project,
        we hope to address the following:</p>
    <ol>
        <li>Identify land bird species present in the IBA </li>
        <li>Determine which species are using the IBA habitats during what time periods
            (i.e., spring migration, breeding, fall migration, over-wintering)</li>
        <li>Estimate relative abundances of various land bird species in the IBA across
            time</li>
        <li>In the long-term, monitor trends in land bird species in the IBA</li>
    </ol>
</asp:Content>
