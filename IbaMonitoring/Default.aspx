<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default"
    MasterPageFile="~/iba.master" Codebehind="Default.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="ContentTitle" runat="server" ContentPlaceHolderID="title">
    Mississippi River Twin Cities Landbird Monitoring Program</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentBody">
    <div style="float: right; margin: 0 0 20px 20px; width: 300px;">
        <img src="images/GC_river_view.jpg" alt="A view of the Mississippi River at Grey Cloud SNA"
            width="300" height="225" />
        <p style="font-size: .8em;">
            A view of the Mississippi River at Grey Cloud SNA<br />
            &copy; <a href="http://www.safnet.com">Stephen A. Fuqua</a>.</p>
    </div>
    <p>
       This project is a citizen science monitoring initiative designed to inventory and monitor the abundance of landbirds in the 
       <a href="http://iba.audubon.org/iba/viewSiteProfile.do?siteId=2421&navSite=state ">Mississippi River Twin Cities IBA</a>. 
       Since our first season in 2004, participants have recorded 139 landbird species on 10 sites within 
       the IBA during spring migration and summer breeding seasons. Thank you to all our volunteers who make the program a success! 
    </p>
   <p>
   Learn more about <a href="http://mn.audubon.org/birds-science-education/important-bird-areas/ibas-minnesota">Minnesota's 
   Important Bird Areas</a> and how this <a href="about/">citizen science program</a> is working to evaluate the state of landbird 
   species in the Minneapolis-Saint Paul metro. For more information on how you can get involved, 
   <a href="http://mn.audubon.org/about-us">contact</a> Mark Martell, Audubon Minnesota’s Director of Bird Conservation.
   </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <strong>Special thanks to those whose support has made this program possible:</strong></p>
    <p>
        <a href="http://mn.audubon.org/">Audubon Minnesota</a><br />
        <a href="http://www.missriverfund.org/">The Mississippi River Fund</a>: <em>Providing
            funding to develop the monitoring protocols.<br />
        </em><a href="http://www.fws.gov/">U.S. Fish and Wildlife Service</a>:<em> Funding to
            develop website/database interface. Partial funding for this program is supported
            by a Grant Agreement from the U.S. Department of the Interior, Fish and Wildlife
            Service. Mention of trade names or commercial products does not constitute their
            endorsement by the U.S. government</em>.<br />
        <a href="http://www.nps.gov">U.S. National Park Service</a></p>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="contentTitle">
    Welcome to the Mississippi River Twin Cities Important Bird Area Landbird Monitoring
    Program!
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="X-XRDS-Location" content="http://www.ibamonitoring.org/ibamonitoring.xrds"/>
    <style type="text/css">
        @import url("styles/iba-widepage.css");
    </style>
    <!--meta http-equiv="refresh" content="0;url=./maintenance.aspx" /-->
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="subnavTitle">
</asp:Content>
