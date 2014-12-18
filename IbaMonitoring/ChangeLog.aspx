<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
	</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" Runat="Server">
	 IBA Monitoring: Change History</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" Runat="Server">
	<ul>
        <li><a href="#Aug232010">August 23, 2010</a></li>
        <li><a href="#June292010">June 29, 2010</a></li>
        <li><a href="#June202010">June 20, 2010</a></li>
        <li><a href="#June62010">June 6, 2010</a></li>
		<li><a href="#May232010">May 23, 2010</a></li>
        <li><a href="#May162010">May 16, 2010</a></li>
		<li><a href="#May92010">May 9, 2010</a></li>
	</ul>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" Runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" Runat="Server">
    Web Application Change Log 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" Runat="Server">
    <h3><a name="Aug232010"></a>Monday, August 23, 2010</h3>
    <ul>
        <li>Corrections to formulas used in calculating densities and adjusted counts in the Results pages.</li>
        <li>Removed hard-coding of dates on the result pages, which caused the page to blow up when a site visit was made outside of the hard-coded date range.</li>
        <li>Split main site list community measures results into breeding and migration seasons.</li>
    </ul>
    <h3><a name="June292010"></a>Tuesday, June 29, 2010</h3>
    <ul>
        <li>Species results now includes relative abundance</li>
        <li>Site results include new community measures - Shannon Diversity Index as well as Evenness</li>
        <li>Flaw in Site results, richness calculation, fixed</li>
    </ul>
    <h3><a name="June202010"></a>Sunday, June 20, 2010</h3>
    <ul>
        <li>Results - Site List with Species Richness.</li>
        <li>Results - Site with species list and count per week and list of supplemental observations.</li>
        <li>Results - Species histogram fixed X-coordinate (Date) sorting.</li>
    </ul>
    <h3><a name="June62010"></a>Sunday, June 6, 2010</h3>
    <ul>
        <li>Results display!</li>
        <li>Master species list now links to individual species page.</li>
        <li>Individual species page includes three views of the data:
            <ol>
               <li>Crosstab chart showing number of individuals sited per week at each site.</li> 
               <li>Histogram showing cumulative count of individuals in all sites per week.</li>
               <li>Google map with total number of individuals sited at each sampling point, cumulative to all weeks.</li>
            </ol>
        </li>
    </ul>
    <h3>Sunday, May 23, 2010</h3>
    <ul>
        <li>Updated copyright and funding notices.</li>
        <li>Result counts now exclude supplemental observations.</li>
        <li>The &quot;quirk&quot; mentioned on 5/9 about the &quot;red X is supposed to delete the given 
            entry&quot; has been fixed: it is now possible to delete an observation entry on the 
            Point Count page by clicking on the red X icon.</li>
    </ul>
    <h3><a name="May162010"></a>Sunday, May 16, 2010</h3>
    <ul>
        <li>Added: ability to retrieve an incomplete Site Visit&nbsp;
            <ul>
                <li>On the <em>Submit Observations</em> page, choose a Location and enter a start 
                    date, then click the &quot;Retrieve incomplete site visit&quot; link next to the date.</li>
                <li>&nbsp;This will load into memory all conditions and observations that have 
                    already been saved, allowing the user to make corrections or additions.</li>
                <li>The Site Visit will remain incomplete until the user clicks the final Save button 
                    on the <em>Review</em> page.</li>
            </ul>
        </li>
        <li>Fixed: site conditions were not saving properly for a Site Visit.</li>
        <li>Modified: the review page now sorts survey results by the point survey time.</li>
        <li>Added: free-form comments textbox in the <em>Supplemental Observations</em> 
            page.</li>
        <li>Added: ability to skip a point survey.
            <ul>
                <li>For example, if weather conditions cause the observer(s) to leave and come back 
                    the next day, then these two trips should be treated as separate Site Visits, 
                    with separate data entry. Each point not surveyed on a particular day can be 
                    skipped, so that noise codes and times are not required.</li>
            </ul>
        </li>
    </ul>
	<h3><a name="May92010"></a>Sunday, May 9, 2010</h3>
	<ul>
		<li>Site launched!</li>
		<li>Known quirks:<ul>
			<li>On observation data entry page, the red X is supposed to delete 
			the given entry. It does not work. Instead zero out both within and 
			beyond 50 m to delete the given entry.</li>
			<li>Only a generic login for now, contact Tania Homayoun for 
			credentials.</li>
		</ul>
		</li>
	</ul>
</asp:Content>

