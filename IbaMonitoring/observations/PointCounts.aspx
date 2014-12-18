<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="PointCounts" Codebehind="PointCounts.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Src="SurveySteps.ascx" TagName="SurveySteps" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("../styles/iba-widepage.css");
        
        .SitePoints
        {
            vertical-align: top;
            width: 120px;
            padding: 0 5px 5px 5px;
            margin: 0 10px 0 10px;
        }
       .SitePoints a
        {
            display: inline;
            margin: 0;
            padding: 0;
        }
        .SitePoints h3
        {
            margin: 0 0 20px 0;
        }
        .CurrentPoint
        {
            font-weight: bold;
        }
        .CompletedPoint
        {
            color: Gray;
        }
        .SitePointListUl
        {
            list-style-type: none;
            padding: 0px;
            margin: 0px;
        }
        .SitePointListUl img
        {
            float: left;
            clear: left;
            margin-bottom: 4px;
            margin-right: 5px;
        }
        .SitePointListUl a
        {
            display: block;
            margin-bottom: 4px;
        }
        .DataEntry
        {
            vertical-align: top;
            padding: 0 10px 0 10px;
            margin: 0;
        }
        .DataEntry h3
        {
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
    <uc1:SurveySteps ID="SurveySteps1" runat="server" PointCountIsActive="True" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Observations: Point Counts
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
        <ContentTemplate>
            <table runat="server" id="SitePointsTable">
                <tr>
                    <td id="SitePointsPanel" runat="server" class="SitePoints">
                        <h3>
                            <asp:Literal runat="server" ID="SiteName" /></h3>
                        <asp:ListView ID="SitePointsList" runat="server" EnableModelValidation="True" DataSourceID="SitePointDataSource"
                            DataKeyNames="Id, Name">
                            <LayoutTemplate>
                                <ul class="SitePointListUl">
                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:Image runat="server" ID="PointSilver" Visible="false" AlternateText="*  " ImageUrl="~/images/arrow_right_silver.png" /><asp:Image
                                        runat="server" ID="PointArrow" Visible="false" AlternateText="-&gt;" ImageUrl="~/images/arrow_right_blue.png" /><asp:Image
                                            runat="server" ID="PointOkay" Visible="false" AlternateText="-&gt;" ImageUrl="~/images/check_blue.png" />
                                    <asp:LinkButton runat="server" ID="PointName" CausesValidation="false" /></li>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                    <td id="DataEntryPanel" runat="server" class="DataEntry">
                        <h3>
                            Current Point:
                            <asp:Label runat="server" ID="CurrentPointName" /></h3>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                        <asp:Panel ID="PointConditionsPanel" runat="server" GroupingText="Point Conditions">
                            <p>
                                <asp:Label ID="NoiseCodeLabel" runat="server" Text="Noise Code" AssociatedControlID="NoiseCodeDropDown"></asp:Label>
                                <asp:DropDownList ID="NoiseCodeDropDown" runat="server" DataSourceID="NoiseCodeDataSource"
                                    DataTextField="value" DataValueField="key">
                                </asp:DropDownList>
                            </p>
                            <p>
                                <asp:Label ID="TimeLabel" runat="server" AssociatedControlID="Time">Time<asp:RequiredFieldValidator
                                    ID="StartTimeRequired" runat="server" ErrorMessage="StartTime: required" ControlToValidate="Time"> - required</asp:RequiredFieldValidator></asp:Label>
                                <asp:TextBox ID="Time" runat="server" Width="75px"></asp:TextBox>
                                &nbsp;AM
                                <asp:CustomValidator ID="TimeValidator" runat="server" OnServerValidate="TimeValidator_OnServerValidate"
                                    ClientValidationFunction="ValidMorningTime" ControlToValidate="Time" ErrorMessage="Start Time: invalid time format; ex: 6:30, 10:00">Invalid time format; ex: 6:30, 10:00</asp:CustomValidator>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="ObservationsPanel" runat="server" GroupingText="Observations">
                            <asp:ListView ID="ObservationsList" runat="server" DataSourceID="PointCountObservationDataSource"
                                InsertItemPosition="FirstItem" DataKeyNames="PointSurveyId, CountWithin50, CountBeyond50, SpeciesCode, Comments">
                                <ItemTemplate>
                                    <tr runat="server" class="ListViewItem">
                                        <td>
                                            <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' />
                                            (<asp:Label ID="CommonName" runat="server"  />)
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
                                        <td class="NoWrap">
                                            <asp:HiddenField ID="PointSurveyId" runat="server" Value='<%# Eval("PointSurveyId") %>' />
                                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"
                                                AlternateText="Edit" ImageUrl="~/images/pencil.png" CausesValidation="false" />
                                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ToolTip="Delete"
                                                AlternateText="Update" ImageUrl="~/images/delete_x.png" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr runat="server" class="ListViewAlternateItem">
                                        <td>
                                            <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' />
                                            (<asp:Label ID="CommonName" runat="server"  />)
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
                                        <td class="NoWrap">
                                            <asp:HiddenField ID="PointSurveyId" runat="server" Value='<%# Eval("PointSurveyId") %>' />
                                            <asp:HiddenField ID="ID" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"
                                                AlternateText="Edit" ImageUrl="~/images/pencil.png" CausesValidation="false" />
                                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ToolTip="Delete"
                                                AlternateText="Update" ImageUrl="~/images/delete_x.png" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table class="ListViewEmptyData">
                                        <tr>
                                            <td>
                                                No data were returned.
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <InsertItemTemplate>
                                    <tr class="ListViewInsertItem" runat="server">
                                        <td>
                                            <asp:TextBox ID="SpeciesCodeTextBox" runat="server" Text='<%# Bind("SpeciesCode") %>'
                                                Width="120" />
                                            <asp:CustomValidator ID="SpeciesCodeValidator" runat="server" ControlToValidate="SpeciesCodeTextBox"
                                                EnableClientScript="false" ErrorMessage="Invalid species code." Text="Invalid species code."
                                                OnServerValidate="SpeciesCodeTextBox_Validate" />
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="Number" ID="CountWithin50TextBox" runat="server" Text='<%# Bind("CountWithin50") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="Number" ID="CountBeyond50TextBox" runat="server" Text='<%# Bind("CountBeyond50") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CommentsTextBox" Width="300" runat="server" Text='<%# Bind("Comments") %>'
                                                Rows="2" />
                                        </td>
                                        <td class="NoWrap">
                                            <asp:ImageButton ID="InsertButton" runat="server" CommandName="Insert" ToolTip="Insert"
                                                AlternateText="Insert" ImageUrl="~/images/check_blue.png" />
                                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ToolTip="Clear"
                                                AlternateText="Clear" ImageUrl="~/images/refresh_blue.png" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </InsertItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server" class="ListView">
                                        <tr runat="server" class="ListViewHeader">
                                            <th>
                                                Species Code
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
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <EditItemTemplate>
                                    <tr class="ListViewEditItem" runat="server">
                                        <td>
                                            <asp:TextBox ID="SpeciesCodeTextBox" runat="server" Text='<%# Bind("SpeciesCode") %>'
                                                Width="120" />
                                            <asp:CustomValidator ID="SpeciesCodeValidator" runat="server" ControlToValidate="SpeciesCodeTextBox"
                                                EnableClientScript="false" ErrorMessage="Invalid species code." Text="Invalid species code."
                                                OnServerValidate="SpeciesCodeTextBox_Validate" />
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="Number" ID="CountWithin50TextBox" runat="server" Text='<%# Bind("CountWithin50") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="Number" ID="CountBeyond50TextBox" runat="server" Text='<%# Bind("CountBeyond50") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CommentsTextBox" Width="300" runat="server" Text='<%# Bind("Comments") %>'
                                                Rows="2" />
                                        </td>
                                        <td class="NoWrap">
                                            <asp:HiddenField ID="PointSurveyId" runat="server" Value='<%# Eval("PointSurveyId") %>' />
                                            <asp:HiddenField ID="ID" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:ImageButton ID="UpdateButton" runat="server" CommandName="Update" ToolTip="Update"
                                                AlternateText="Update" ImageUrl="~/images/check_blue.png" />
                                            <asp:ImageButton ID="CancelButton" runat="server" CommandName="Cancel" ToolTip="Cancel"
                                                AlternateText="Cancel" ImageUrl="~/images/refresh_blue.png" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </EditItemTemplate>
                                <SelectedItemTemplate>
                                    <tr class="ListViewSelectedItem" runat="server">
                                        <td>
                                            <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' />
                                            (<asp:Label ID="CommonName" runat="server"  />)
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
                                        <td class="NoWrap">
                                            <asp:HiddenField ID="PointSurveyId" runat="server" Value='<%# Eval("PointSurveyId") %>' />
                                            <asp:HiddenField ID="ID" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"
                                                AlternateText="Edit" ImageUrl="~/images/pencil.png" CausesValidation="false" />
                                            <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ToolTip="Delete"
                                                AlternateText="Update" ImageUrl="~/images/delete_x.png" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                            <p><!-- ImageUrl="~/images/next.png"-->
                                <asp:Button ID="NextPoint" runat="server" Text="Save/Next Point" OnClick="NextPoint_Click" CssClass="RightMargin" />
                                <asp:Button ID="Supplemental" runat="server" Text="Enter Supplemental" OnClick="Supplemental_Click" Visible="false" />
                                <asp:LinkButton ID="SkipPoint" runat="server" CausesValidation="False" 
                                    onclick="SkipPoint_Click">Point not surveyed during visit</asp:LinkButton>
                            </p>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="NextPoint" />
            <asp:PostBackTrigger ControlID="Supplemental" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <a href="http://www.birdpop.org/alphacodes.htm">English and Scientific Alpha 
    Codes for North American Birds </a>
    <asp:ObjectDataSource ID="PointCountObservationDataSource" runat="server"
     DataObjectTypeName="safnet.iba.Business.Entities.Observations.FiftyMeterDataEntry"
        DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" SelectMethod="SelectAllForEvent"
        TypeName="iba.FiftyMeterDataEntryFacade" />
    <asp:ObjectDataSource ID="SitePointDataSource" runat="server" SelectMethod="SelectAllForSite"
        DataObjectTypeName="safnet.iba.Business.Entities.SamplingPoint" TypeName="safnet.iba.Data.Mappers.SamplingPointMapper" />
    <asp:ObjectDataSource ID="NoiseCodeDataSource" runat="server" SelectMethod="GetDropdownValues"
        TypeName="safnet.iba.Business.Dictionaries.NoiseCode" />
</asp:Content>
