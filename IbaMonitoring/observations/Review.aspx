<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="Review" Codebehind="Review.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Src="SurveySteps.ascx" TagName="SurveySteps" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("../styles/iba-widepage.css");
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
    <uc1:SurveySteps ID="SurveySteps1" runat="server" ReviewIsActive="True" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Observations: Review &amp; Submit
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <asp:Panel ID="SiteVisitPanel" runat="server" GroupingText="Site Visit">
        <p>
            <asp:Label runat="server" AssociatedControlID="SiteVisited" Text="Site" ID="SiteVisitedLabel" />
            <asp:Label runat="server" ID="SiteVisited" CssClass="ReadOnly" />
        </p>
        <p>
            <asp:Label ID="SiteVisitObserverLabel" runat="server" AssociatedControlID="SiteVisitObserver"
                Text="Observer"></asp:Label>
            <asp:Label ID="SiteVisitObserver" runat="server" CssClass="ReadOnly" />
        </p>
        <p>
            <asp:Label ID="SiteVisitRecorderLabel" runat="server" AssociatedControlID="SiteVisitRecorder"
                Text="Recorder"></asp:Label>
            <asp:Label ID="SiteVisitRecorder" runat="server" CssClass="ReadOnly" />
        </p>
    </asp:Panel>
    <asp:Panel ID="StartConditionsPanel" runat="server" GroupingText="Start Conditions">
        <p>
            <asp:Label ID="StartTimeLabel" runat="server" AssociatedControlID="StartTime" Text="Start Date/Time">
            </asp:Label>
            <asp:Label ID="StartTime" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
        <p>
            <asp:Label ID="StartSkyLabel" runat="server" AssociatedControlID="StartSky" Text="Sky"></asp:Label>
            <asp:Label ID="StartSky" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
        <p>
            <asp:Label ID="StartWindLabel" runat="server" AssociatedControlID="StartWind" Text="Wind"></asp:Label>
            <asp:Label ID="StartWind" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
        <p>
            <asp:Label ID="StartTempLabel" runat="server" AssociatedControlID="StartTemperature"
                Text="Temperature">
            </asp:Label>
            <asp:Label ID="StartTemperature" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="EndConditionsPanel" runat="server" GroupingText="End Conditions">
        <p>
            <asp:Label ID="EndTimeLabel" runat="server" AssociatedControlID="EndTime" Text="End Date/Time">
            </asp:Label>
            <asp:Label ID="EndTime" runat="server" CssClass="ReadOnly"></asp:Label></p>
        <p>
            <asp:Label ID="EndSkyLabel" runat="server" AssociatedControlID="EndSky" Text="Sky"></asp:Label>
            <asp:Label ID="EndSky" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
        <p>
            <asp:Label ID="EndWindLabel" runat="server" AssociatedControlID="EndWind" Text="Wind"></asp:Label>
            <asp:Label ID="EndWind" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
        <p>
            <asp:Label ID="EndTempLabel" runat="server" AssociatedControlID="EndTemperature"
                Text="Temperature">
            </asp:Label>
            <asp:Label ID="EndTemperature" runat="server" CssClass="ReadOnly"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="PointSurveyPanel" runat="server" GroupingText="Point Surveys">
        <asp:ListView ID="PointSurveyList" runat="server" InsertItemPosition="None">
            <ItemTemplate>
                <tr class="ListViewItem">
                    <td>
                        <asp:Label runat="server" ID="SurveyPointName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyStartTime"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyNoise"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyCountSpecies" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="ListViewAlternateItem">
                    <td>
                        <asp:Label runat="server" ID="SurveyPointName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyStartTime"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyNoise"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SurveyCountSpecies" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table class="ListViewEmptyData">
                    <tr>
                        <td>
                            No data to display.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table runat="server" class="ListView">
                    <tr runat="server" class="ListViewHeader">
                        <th>
                            Point Name
                        </th>
                        <th>
                            Start Time
                        </th>
                        <th>
                            Noise Code
                        </th>
                        <th>
                            Species Count
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </asp:Panel>
    <p>&nbsp;</p>
    <asp:Panel ID="PointSurveyObservationPanel" runat="server" GroupingText="Point Survey Observations">
        <asp:ListView ID="PointSurveyObservationList" runat="server" InsertItemPosition="None">
            <ItemTemplate>
                <tr class="ListViewItem">
                    <td>
                        <asp:Label runat="server" ID="ObservationPointName" Text='<%# Eval("SamplingPointName") %>'>></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' ToolTip='<%# Eval("SpeciesName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CountWithin50Label" runat="server" Text='<%# Eval("CountWithin50") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CountBeyond50Label" runat="server" Text='<%# Eval("CountBeyond50") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="WarningLabel" CssClass="Warning" Text='<%# Eval("Warning") %>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="ListViewAlternateItem">
                    <td>
                        <asp:Label runat="server" ID="ObservationPointName" Text='<%# Eval("SamplingPointName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SpeciesCodeLabel" runat="server" ToolTip='<%# Eval("SpeciesName") %>'
                            Text='<%# Eval("SpeciesCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CountWithin50Label" runat="server" Text='<%# Eval("CountWithin50") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CountBeyond50Label" runat="server" Text='<%# Eval("CountBeyond50") %>' />
                    </td>
                    <td>
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="WarningLabel" CssClass="Warning" Text='<%# Eval("Warning") %>'></asp:Label>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table class="ListViewEmptyData">
                    <tr>
                        <td>
                            No observations were recorded.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table runat="server" class="ListView">
                    <tr runat="server" class="ListViewHeader">
                        <th>
                            Point Name
                        </th>
                        <th>
                            Species<br />
                            Code
                        </th>
                        <th>
                            Within<br />
                            50m
                        </th>
                        <th>
                            Beyond<br />
                            50m
                        </th>
                        <th>
                            Comments
                        </th>
                        <th>
                            Warnings
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </asp:Panel>
    <p>&nbsp;</p>
    <asp:Panel ID="SupplementalsPanel" runat="server" GroupingText="Supplemental Observations">
        <asp:ListView ID="SupplementalList" runat="server" InsertItemPosition="None">
            <ItemTemplate>
                <tr class="ListViewItem">
                    <td>
                        <asp:Label ID="SupplementalSpeciesCodeLabel" runat="server" ToolTip='<%# Eval("SpeciesName") %>'
                            Text='<%# Eval("SpeciesCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SupplementalCommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SupplementalWarningLabel" CssClass="Warning" Text='<%# Eval("Warning") %>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="ListViewAlternateItem">
                    <td>
                        <asp:Label ID="SupplementalSpeciesCodeLabel" runat="server" ToolTip='<%# Eval("SpeciesName") %>'
                            Text='<%# Eval("SpeciesCode") %>' />
                    </td>
                    <td>
                        <asp:Label ID="SupplementalCommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" ID="SupplementalWarningLabel" CssClass="Warning" Text='<%# Eval("Warning") %>'></asp:Label>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table class="ListViewEmptyData">
                    <tr>
                        <td>
                            No supplemental observations were recorded.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="Table1" runat="server" class="ListView">
                    <tr id="Tr1" runat="server" class="ListViewHeader">
                        <th>
                            Species Code
                        </th>
                        <th>
                            Comments
                        </th>
                        <th>
                            Warnings
                        </th>
                    </tr>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </asp:Panel>
    <p>
        <asp:Button runat="server" ID="SubmitReview" Text="Save Site Visit" OnClick="submitReview_Click" />
    </p>
    <asp:ObjectDataSource ID="SupplementalObservationDataSource" runat="server" DataObjectTypeName="safnet.iba.Business.Entities.Observations.SupplementalObservation"
        DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectAllForEvent" TypeName="safnet.iba.Data.Mappers.SupplementalObservationMapper"
        UpdateMethod="Update" />
</asp:Content>
