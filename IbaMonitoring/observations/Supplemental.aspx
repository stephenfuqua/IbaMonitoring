<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true" Inherits="Supplemental" Codebehind="Supplemental.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Src="SurveySteps.ascx" TagName="SurveySteps" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <uc1:SurveySteps ID="SurveySteps1" runat="server" SupplementalIsActive="True" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    Observations: Supplemental
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <asp:UpdatePanel ChildrenAsTriggers="true" UpdateMode="Always" runat="server" ID="Update1">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Panel ID="ObservationsPanel" runat="server" GroupingText="Observations">
                <asp:ListView ID="SupplementalObservationList" runat="server" DataSourceID="SupplementalObservationDataSource"
                    InsertItemPosition="FirstItem" DataKeyNames="Id, EventId">
                    <ItemTemplate>
                        <tr class="ListViewItem" runat="server">
                            <td>
                                <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' />
                                (<asp:Label ID="CommonName" runat="server" />)
                            </td>
                            <td>
                                <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                            </td>
                            <td class="NoWrap">
                                <asp:HiddenField ID="EventId" runat="server" Value='<%# Eval("EventId") %>' />
                                <asp:HiddenField ID="ID" runat="server" Value='<%# Eval("Id") %>' />
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"
                                    AlternateText="Edit" ImageUrl="~/images/pencil.png" CausesValidation="false" />
                                <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ToolTip="Delete"
                                    AlternateText="Update" ImageUrl="~/images/delete_x.png" CausesValidation="false" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="ListViewAlternateItem" runat="server">
                            <td>
                                <asp:Label ID="SpeciesCodeLabel" runat="server" Text='<%# Eval("SpeciesCode") %>' />
                                (<asp:Label ID="CommonName" runat="server" />)
                            </td>
                            <td>
                                <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                            </td>
                            <td>
                                <asp:HiddenField ID="EventId" runat="server" Value='<%# Eval("EventId") %>' />
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
                                    Width="150" />
                                <asp:CustomValidator ID="SpeciesCodeValidator" runat="server" ControlToValidate="SpeciesCodeTextBox"
                                    EnableClientScript="false" ErrorMessage="Invalid species code." Text="Invalid species code."
                                    OnServerValidate="SpeciesCodeTextBox_Validate" />
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
                                    Width="150" />
                                <asp:CustomValidator ID="SpeciesCodeValidator" runat="server" ControlToValidate="SpeciesCodeTextBox"
                                    EnableClientScript="false" ErrorMessage="Invalid species code." Text="Invalid species code."
                                    OnServerValidate="SpeciesCodeTextBox_Validate" />
                            </td>
                            <td>
                                <asp:TextBox ID="CommentsTextBox" Width="300" runat="server" Text='<%# Bind("Comments") %>'
                                    Rows="2" />
                            </td>
                            <td class="NoWrap">
                                <asp:HiddenField ID="EventId" runat="server" Value='<%# Eval("EventId") %>' />
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
                            </td>
                            <td>
                                <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                            </td>
                            <td class="NoWrap">
                                <asp:HiddenField ID="EventId" runat="server" Value='<%# Eval("EventId") %>' />
                                <asp:HiddenField ID="ID" runat="server" Value='<%# Eval("Id") %>' />
                                <asp:ImageButton ID="EditButton" runat="server" CommandName="Edit" ToolTip="Edit"
                                    AlternateText="Edit" ImageUrl="~/images/pencil.png" CausesValidation="false" />
                                <asp:ImageButton ID="DeleteButton" runat="server" CommandName="Delete" ToolTip="Delete"
                                    AlternateText="Update" ImageUrl="~/images/delete_x.png" CausesValidation="false" />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
                <p>
                    <asp:Button ID="Save" runat="server" Text="Review and Submit" OnClick="Save_Click" />
                </p>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Save" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="SupplementalObservationDataSource" runat="server" DataObjectTypeName="safnet.iba.Business.Entities.Observations.SupplementalObservation"
        DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="SelectAllForEvent"
        TypeName="iba.SupplementalObservationFacade" UpdateMethod="Update" />
    <asp:ObjectDataSource ID="NoiseCodeDataSource" runat="server" SelectMethod="Instance"
        TypeName="safnet.iba.Business.Dictionaries.NoiseCode" />
</asp:Content>
