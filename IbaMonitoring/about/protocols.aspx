<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="about_protocols" Codebehind="protocols.aspx.cs" %>

<%@ Register src="aboutnav.ascx" tagname="aboutnav" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    About: Protocols (Important Bird Area Monitoring Program)
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
    Sampling Protocol
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <p>
        This monitoring protocol was designed to be as standardized as possible in order
        to obtain data that can be analyzed locally and also made available to national
        databases. Collecting data using a standardized procedure is important for a variety
        of reasons. Controlling for and limiting certain biases in how data are collected
        can enhance the scientific credibility of a monitoring project. Using a well-defined
        protocol can also have quality control advantages that are key to data analysis.
        Perhaps most importantly, a clearly defined monitoring protocol can improve long-term
        monitoring studies by providing a clear system that has stability and continuity
        for both end-users of the data and the people collecting it.
    </p>
    <p>
        Since the Mississippi River Twin Cities IBA coincides with MNRRA, a National Parks
        Service unit, many of the aspects of this protocol were adopted from a basic National
        Park Service Passerine Monitoring Protocol. However, some parts of the protocol
        have been adapted in response to the urbanized nature of the Twin Cities landscape,
        working on park-reserve land, and the volunteer status of the surveyors. Ideally,
        once a unique protocol has been established for this IBA, these guidelines can be
        applied to new monitoring sites in other urban IBAs.</p>
    <p>
        Like the National Park Service land bird surveys, this protocol utilizes a 5-minute,
        50-meter radius point count methodology. The longer an observer stands at a point,
        the more species they are likely to hear; however, the longer you remain at a point,
        the more likely you are to overcount/double count the same birds. A 5-minute period
        is a frequently used time interval that allows for good detection. The 50-meter
        radius is another commonly used convention in many point counts. In this survey,
        observers will be asked to categorize birds as seen &quot;within 50 meters&quot; of the observer
        and &quot;beyond 50 meters.&quot; When dealing with many Passerine species, detection beyond
        50 meters can be difficult. Moreover, the 2 bands (0-to-50 meters and 50 meters-to-&quot;infinity&quot;)
        are useful distinctions when analyzing the data from each point.
    </p>
    <p>
        While informed by the sampling design guidelines in the National Parks Service Passerine
        Monitoring Protocol, several changes were made to accommodate sampling in urban
        greenspace. The IBA was broken into &quot;management units&quot; that followed city and regional
        park boundaries. These are designated as sites. Within each site, sampling points
        were laid out using a randomization tool in GIS software such that the sites were
        oversampled with points separated by at least 250 meters (to minimize the risk of
        double-counting birds at two different points). Points were stratified by proximity
        to trails, and a final number of points per site selected based on area of the site
        and limited to what could reasonably be sampled in a single morning.</p>
</asp:Content>
