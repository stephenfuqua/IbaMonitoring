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
            <asp:Label runat="server" AssociatedControlID="SiteVisitedInput" Text="Site" ID="SiteVisitSiteLabel" />
            <asp:DropDownList ID="SiteVisitedInput" runat="server" />
        </p>
        <p>
            <asp:Label ID="SiteVisitDateLabel" runat="server" AssociatedControlID="VisitDateInput"
                Text="Date">
                <asp:RequiredFieldValidator ID="SiteVisitDateRequired" runat="server" ControlToValidate="VisitDateInput"
                    ErrorMessage="Site Visit Date: required"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="VisitDateInput" runat="server" Width="75px"></asp:TextBox>
            <cc1:CalendarExtender ID="SiteVisitDateCalExt" runat="server" Enabled="True" TargetControlID="VisitDateInput">
            </cc1:CalendarExtender>
            &nbsp;<asp:CompareValidator ID="SiteVisitDateValidator" runat="server" ControlToValidate="VisitDateInput"
                ErrorMessage="Site Visit date: invalid format; ex: 11/22/2009, 2009-11-22, 11-22-2009"
                Operator="DataTypeCheck" Type="Date" Display="Dynamic">Invalid date format; ex: 11/22/2009, 2009-11-22, 11-22-2009</asp:CompareValidator>
            &nbsp;<asp:LinkButton ID="RetrieveIncomplete" runat="server" 
                CausesValidation="False" onclick="RetrieveIncomplete_Click">Retrieve incomplete site visit</asp:LinkButton>
        </p>
        <p>
            <asp:Label ID="SiteVisitObserverLabel" runat="server" AssociatedControlID="ObserverInput"
                Text="Observer"></asp:Label>
            <asp:DropDownList ID="ObserverInput" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="SiteVisitRecorderLabel" runat="server" AssociatedControlID="RecorderInput"
                Text="Recorder"></asp:Label>
            <asp:DropDownList ID="RecorderInput" runat="server" />
        </p>
    </asp:Panel>
    <asp:Panel ID="StartConditions" GroupingText="Starting Conditions" runat="server">
        <p>
            <asp:Label ID="StartTimeLabel" runat="server" AssociatedControlID="StartTimeInput" Text="Time">
                <asp:RequiredFieldValidator ID="StartTimeRequired" runat="server" ErrorMessage="StartTime: required"
                    ControlToValidate="StartTimeInput"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="StartTimeInput" runat="server" Width="75px"></asp:TextBox>
            &nbsp;AM
            <asp:CustomValidator ID="StartTimeValidator" runat="server" OnServerValidate="StartTimeValidator_OnServerValidate"
                ClientValidationFunction="ValidMorningTime" ControlToValidate="StartTimeInput" ErrorMessage="Start Time: invalid time format; ex: 6:30, 10:00">Invalid time format; ex: 6:30, 10:00</asp:CustomValidator>
        </p>
        <p>
            <asp:Label ID="StartSkyLabel" runat="server" AssociatedControlID="StartSkyInput" Text="Sky"></asp:Label>
            <asp:DropDownList ID="StartSkyInput" runat="server" DataSourceID="SkyCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="StartWindLabel" runat="server" AssociatedControlID="StartWindInput" Text="Wind"></asp:Label>
            <asp:DropDownList ID="StartWindInput" runat="server" DataSourceID="WindCodeDataSource"
                DataTextField="value" DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="StartTempLabel" runat="server" AssociatedControlID="StartTemperatureInput"
                Text="Temperature">
                <asp:RequiredFieldValidator ID="StartTemperatureRequired" runat="server" ErrorMessage="Start Temperature: required"
                    ControlToValidate="StartTemperatureInput"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="StartTemperatureInput" runat="server" MaxLength="3" Width="40px"></asp:TextBox>
            <asp:RadioButtonList ID="StartTemp_Radio" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" CssClass="radiolist">
            </asp:RadioButtonList>
            &nbsp;<asp:CustomValidator ID="StartTemperatureValidator" runat="server" OnServerValidate="TemperatureValidator_OnServerValidate"
                ClientValidationFunction="ValidTemperature" ControlToValidate="StartTemperatureInput"
                ErrorMessage="Start Temperature: valid range is 0 to 99 F or -18 to 37 C">valid 
            range is 0 to 99 F or -18 to 37 C</asp:CustomValidator>
        </p>
    </asp:Panel>
    <asp:Panel ID="EndConditions" GroupingText="Ending Conditions" runat="server">
        <p>
            <asp:Label ID="EndTimeLabel" runat="server" AssociatedControlID="EndTimeInput" Text="Time">
                <asp:RequiredFieldValidator ID="EndTimeRequired" runat="server" ErrorMessage="End Time: required"
                    ControlToValidate="EndTimeInput"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="EndTimeInput" runat="server" Width="75px"></asp:TextBox>
            &nbsp;AM
            <asp:CustomValidator ID="EndTimeValidator" runat="server" ClientValidationFunction="ValidMorningTime"
                ControlToValidate="EndTimeInput" ErrorMessage="End Time: invalid time format; ex: 6:30, 10:00"
                OnServerValidate="StartTimeValidator_OnServerValidate">Invalid time format; ex: 6:30, 10:00</asp:CustomValidator>
        </p>
        <p>
            <asp:Label ID="EndSkyLabel" runat="server" AssociatedControlID="EndSkyInput" Text="Sky"></asp:Label>
            <asp:DropDownList ID="EndSkyInput" runat="server" DataSourceID="SkyCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="EndWindLabel" runat="server" AssociatedControlID="EndWindInput" Text="Wind"></asp:Label>
            <asp:DropDownList ID="EndWindInput" runat="server" DataSourceID="WindCodeDataSource" DataTextField="value"
                DataValueField="key">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Label ID="EndTempLabel" runat="server" AssociatedControlID="EndTemperatureInput"
                Text="Temperature">
                <asp:RequiredFieldValidator ID="EndTemperatureRequired" runat="server" ErrorMessage="End Temperature: required"
                    ControlToValidate="EndTemperatureInput"> - required</asp:RequiredFieldValidator></asp:Label>
            <asp:TextBox ID="EndTemperatureInput" runat="server" MaxLength="3" Width="40px"></asp:TextBox>
            <asp:RadioButtonList ID="EndTemp_Radio" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" CssClass="radiolist">
                <asp:ListItem Selected="True">F</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
            </asp:RadioButtonList>
            &nbsp;<asp:CustomValidator ID="EndTemperatureValidator" runat="server" OnServerValidate="TemperatureValidator_OnServerValidate"
                ClientValidationFunction="ValidTemperature" ControlToValidate="EndTemperatureInput"
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
