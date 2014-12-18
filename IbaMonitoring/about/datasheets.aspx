<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="about_datasheets" Codebehind="datasheets.aspx.cs" %>

<%@ Register src="aboutnav.ascx" tagname="aboutnav" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">About: Data Sheets (Important Bird Area Monitoring Program)
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
About
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
    <uc1:aboutnav ID="aboutnav1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Data Sheets and Field References
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        PDFs of the Site Conditions and Point Count data sheets to use during your surveys:</p>
    <ul>
        <li><a href="../forms/IBA_Site_Form_08.pdf">Site Conditions Form</a></li>
        <li><a href="../forms/IBA Data Form_08.pdf">Bird Point Count Datasheet</a></li>
    </ul>
    <p>
        The following sheets may be useful to take into the field during your surveys:</p>
    <ul>
        <li><a href="../forms/IBA Code Form_08.pdf">AOU 4-Letter Codes for Commonly Seen Birds</a></li>
        <li><a href="../forms/1pg Survey Protocol 2010.pdf">Bird Survey Protocol Reminder Information</a></li>
    </ul>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
