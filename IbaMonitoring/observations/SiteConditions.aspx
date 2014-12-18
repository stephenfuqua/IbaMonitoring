<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="SiteConditionsPage" Codebehind="SiteConditions.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="SurveySteps.ascx" TagName="SiteVisitStepsControl" TagPrefix="uc1" %>
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
<asp:Content ID="Content5" ContentPlaceHolderID="contentTitle" runat="Server">
    Observations: Site Conditions
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentBody" runat="Server">
    <p><asp:LinkButton runat="server" ID="StartNewSession" 
            Text="Start New Data Entry Session" CausesValidation="False" 
            onclick="StartNewSession_Click" /></p>
    <p>
        All form fields are required.
    </p>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="The following errors need to be corrected in order to submit the form:" />
    <asp:Panel ID="SiteVisitPanel" runat="server" GroupingText="Visit">
        <p>
            <asp:Label runat="server" AssociatedControlID="SiteVisited" Text="Site" ID="SiteVisitSiteLabel" />
            <asp:DropDownList ID="SiteVisited" runat="server" />
        </p>
        <p>
            <asp:Label ID="SiteVisitDateLabel" runat="server" AssociatedControlID="VisitDate"
                Text="Date">
                <asp:RequiredFieldValidator ID="SiteVisitDateRequired" runat="server" ControlToValidate="VisitDate"
                    ErrorMessage="Site Visit Date: required"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="VisitDate" runat="server" Width="75px"></asp:TextBox>
            <cc1:CalendarExtender ID="SiteVisitDateCalExt" runat="server" Enabled="True" TargetControlID="VisitDate">
            </cc1:CalendarExtender>
            &nbsp;<asp:CompareValidator ID="SiteVisitDateValidator" runat="server" ControlToValidate="VisitDate"
                ErrorMessage="Site Visit date: invalid format; ex: 11/22/2009, 2009-11-22, 11-22-2009"
                Operator="DataTypeCheck" Type="Date" Display="Dynamic">Invalid date format; ex: 11/22/2009, 2009-11-22, 11-22-2009</asp:CompareValidator>
            &nbsp;<asp:LinkButton ID="RetrieveIncomplete" runat="server" 
                CausesValidation="False" onclick="RetrieveIncomplete_Click">Retrieve incomplete site visit</asp:LinkButton>
        </p>
        <p>
            <asp:Label ID="SiteVisitObserverLabel" runat="server" AssociatedControlID="SiteVisitObserver"
                Text="Observer"></asp:Label>
            <asp:DropDownList ID="SiteVisitObserver" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="SiteVisitRecorderLabel" runat="server" AssociatedControlID="SiteVisitRecorder"
                Text="Recorder"></asp:Label>
            <asp:DropDownList ID="SiteVisitRecorder" runat="server" />
        </p>
    </asp:Panel>
    <asp:Panel ID="StartConditions" GroupingText="Starting Conditions" runat="server">
        <p>
            <asp:Label ID="StartTimeLabel" runat="server" AssociatedControlID="StartTime" Text="Time">
                <asp:RequiredFieldValidator ID="StartTimeRequired" runat="server" ErrorMessage="StartTime: required"
                    ControlToValidate="StartTime"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="StartTime" runat="server" Width="75px"></asp:TextBox>
            &nbsp;AM
            <asp:CustomValidator ID="StartTimeValidator" runat="server" OnServerValidate="StartTimeValidator_OnServerValidate"
                ClientValidationFunction="ValidMorningTime" ControlToValidate="StartTime" ErrorMessage="Start Time: invalid time format; ex: 6:30, 10:00">Invalid time format; ex: 6:30, 10:00</asp:CustomValidator>
        </p>
        <p>
            <asp:Label ID="StartSkyLabel" runat="server" AssociatedControlID="StartSky" Text="Sky"></asp:Label>
            <asp:DropDownList ID="StartSky" runat="server" DataSourceID="SkyCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="StartWindLabel" runat="server" AssociatedControlID="StartWind" Text="Wind"></asp:Label>
            <asp:DropDownList ID="StartWind" runat="server" DataSourceID="WindCodeDataSource"
                DataTextField="value" DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="StartTempLabel" runat="server" AssociatedControlID="StartTemperature"
                Text="Temperature">
                <asp:RequiredFieldValidator ID="StartTemperatureRequired" runat="server" ErrorMessage="Start Temperature: required"
                    ControlToValidate="StartTemperature"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="StartTemperature" runat="server" MaxLength="3" Width="40px"></asp:TextBox>
            <asp:RadioButtonList ID="StartTemp_Radio" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" CssClass="radiolist">
            </asp:RadioButtonList>
            &nbsp;<asp:CustomValidator ID="StartTemperatureValidator" runat="server" OnServerValidate="TemperatureValidator_OnServerValidate"
                ClientValidationFunction="ValidTemperature" ControlToValidate="StartTemperature"
                ErrorMessage="Start Temperature: valid range is 0 to 99 F or -18 to 37 C">valid 
            range is 0 to 99 F or -18 to 37 C</asp:CustomValidator>
        </p>
    </asp:Panel>
    <asp:Panel ID="EndConditions" GroupingText="Ending Conditions" runat="server">
        <p>
            <asp:Label ID="EndTimeLabel" runat="server" AssociatedControlID="EndTime" Text="Time">
                <asp:RequiredFieldValidator ID="EndTimeRequired" runat="server" ErrorMessage="End Time: required"
                    ControlToValidate="EndTime"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="EndTime" runat="server" Width="75px"></asp:TextBox>
            &nbsp;AM
            <asp:CustomValidator ID="EndTimeValidator" runat="server" ClientValidationFunction="ValidMorningTime"
                ControlToValidate="EndTime" ErrorMessage="End Time: invalid time format; ex: 6:30, 10:00"
                OnServerValidate="StartTimeValidator_OnServerValidate">Invalid time format; ex: 6:30, 10:00</asp:CustomValidator>
        </p>
        <p>
            <asp:Label ID="EndSkyLabel" runat="server" AssociatedControlID="EndSky" Text="Sky"></asp:Label>
            <asp:DropDownList ID="EndSky" runat="server" DataSourceID="SkyCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="EndWindLabel" runat="server" AssociatedControlID="EndWind" Text="Wind"></asp:Label>
            <asp:DropDownList ID="EndWind" runat="server" DataSourceID="WindCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="EndTempLabel" runat="server" AssociatedControlID="EndTemperature"
                Text="Temperature">
                <asp:RequiredFieldValidator ID="EndTemperatureRequired" runat="server" ErrorMessage="End Temperature: required"
                    ControlToValidate="EndTemperature"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="EndTemperature" runat="server" MaxLength="3" Width="40px"></asp:TextBox>
            <asp:RadioButtonList ID="EndTemp_Radio" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" CssClass="radiolist">
                <asp:ListItem Selected="True">F</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
            </asp:RadioButtonList>
            &nbsp;<asp:CustomValidator ID="EndTemperatureValidator" runat="server" OnServerValidate="TemperatureValidator_OnServerValidate"
                ClientValidationFunction="ValidTemperature" ControlToValidate="EndTemperature"
                ErrorMessage="End Temperature: valid range is 0 to 99 F or -18 to 37 C">valid 
            range is 0 to 99 F or -18 to 37 C</asp:CustomValidator>
        </p>
    </asp:Panel>
    <p class="submit">
        <asp:ImageButton runat="server" ID="SubmitSiteConditions" ImageUrl="~/images/next.png"
            ToolTip="Next" OnClick="submitSiteConditions_Click" />
    </p>
    <asp:ObjectDataSource ID="SkyCodeDataSource" runat="server" SelectMethod="GetDropdownValues"
        EnableCaching="false" TypeName="safnet.iba.Business.Dictionaries.SkyCode" />
    <asp:ObjectDataSource ID="WindCodeDataSource" runat="server" SelectMethod="GetDropdownValues"
        EnableCaching="false" TypeName="safnet.iba.Business.Dictionaries.WindCode" />
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="contentNav">
    <uc1:SiteVisitStepsControl ID="SiteVisitStepsControl1" runat="server" SiteConditionsIsActive="True" />
</asp:Content>
