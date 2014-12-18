<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="about_maps" Codebehind="maps.aspx.cs" %>

<%@ Register src="aboutnav.ascx" tagname="aboutnav" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    About: Maps (Important Bird Area Monitoring Program)
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
    Maps
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        These color maps show the sampling points and marked trails on an aerial photograph
        of each site in the IBA. Click the link to download a PDF to use during your surveys:</p>
    <ul>
        <li><a href="../maps/MissGorge_MPG.pdf">Mississippi River Gorge</a></li>
        <li><a href="../maps/Minnehaha_MHA.pdf">Minnehaha</a></li>
        <li><a href="../maps/HiddenFalls_HF.pdf">Hidden Falls</a></li>
        <li><a href="../maps/Crosby_CF.pdf">Crosby</a></li>
        <li><a href="../maps/Lilydale_LD_2008.pdf">Lilydale</a></li>
        <li><a href="../maps/BattleCreekE_BCE.pdf">Battle Creek East</a></li>
        <li><a href="../maps/BattleCreekW_BCW.pdf">Battle Creek West</a></li>
        <li><a href="../maps/GreyCloudSNA_GC.pdf">Grey Cloud SNA</a></li>
        <li><a href="../maps/LSpringLake_LSL.pdf">Lower Spring Lake</a></li>
        <li><a href="../maps/SchaarsBluff_SB.pdf">Schaars Bluff</a></li>
    </ul>
</asp:Content>
