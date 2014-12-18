<%@ Page Title="" Language="C#" MasterPageFile="~/iba.master" AutoEventWireup="true"  Async="true" Inherits="Account" Codebehind="account.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Src="NonAdminLevelAccessProfile.ascx" TagName="NonAdminLevelAccessProfile"
    TagPrefix="uc1" %>
<%@ Register Src="OnlyAdminLevelAccessProfile.ascx" TagName="OnlyAdminLevelAccessProfile"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    IBA Monitoring: User Profile Management</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("/styles/iba-widepage.css");
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="subnavTitle" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="subnavLinks" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentNav" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="contentTitle" runat="Server">
    User Profile Management
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="contentBody" runat="Server">
    <script type="text/C#" runat="server">
        string SetPending(object status, string defaultClass)
        {
            if (status.ToString().Equals("Pending"))
            {
                return "ListViewHighlighted";
            }
            else
            {
                return defaultClass;
            }
        }
        string SetPendingMouseOut(object status, string defaultClass)
        {
            if (status.ToString().Equals("Pending"))
            {
                defaultClass = "ListViewHighlighted";
            }

            return "this.className='" + defaultClass + "'";
        }
    </script>
    <p>
        Highlighted accounts are in the pending state and need to be either deleted or activated.</p>
    <asp:Panel ID="UserProfilesPanel" runat="server" GroupingText="User Profiles">
        <asp:LinkButton ID="ShowInactive" Text="Show inactive users" runat="server" OnClick="ShowInactive_Click" />
        <asp:HiddenField ID="ExcludeInactive" Value="true" runat="server" />
        <asp:ListView ID="lvUsers" runat="server" OnItemCommand="lvUsers_ItemCommand" OnItemEditing="lvUsers_ItemEditing"
            DataKeyNames="OpenId" OnItemUpdating="lvUsers_ItemUpdating" OnItemCanceling="lvUsers_ItemCanceling"
            OnItemUpdated="lvUsers_ItemUpdated">
            <LayoutTemplate>
                <table border="0" cellpadding="3" class="ListView">
                    <thead class="ListViewHeader">
                        <tr>
                            <td>
                                First Name
                            </td>
                            <td>
                                Last Name
                            </td>
                            <td>
                                Phone Number
                            </td>
                            <td>
                                Email Address
                            </td>
                            <td>
                                Action
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="ItemPlaceHolder" runat="server" />
                    </tbody>
                    <tfoot>
                        <tr>
                        </tr>
                    </tfoot>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr runat="server" id="UserRow" class='<%# SetPending(Eval("PersonStatus"), "ListViewItem")  %>'
                    onmouseover="this.className='ListViewSelectedItem'" onmouseout='<%# SetPendingMouseOut(Eval("PersonStatus"), "ListViewItem")  %>'>
                    <td>
                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label>
                    </td>
                    <td>
                        <a href='<%# "mailto:" + Eval("EmailAddress") %>' runat="server" id="EmailLink">
                            <asp:Label ID="EmailAddressLabel" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label></a>
                    </td>
                    <td>
                        <asp:HiddenField ID="PersonId" runat="server" Value='<%# Eval("Id") %>' />
                        <asp:HiddenField ID="StatusLabel" runat="server" Value='<%# Eval("PersonStatus") %>' />
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/pencil.png" ToolTip="Edit Record"
                            AlternateText="Edit" CommandName="EditOrUpdateCommand" />
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete_x.png" ToolTip="Delete Record"
                            AlternateText="Delete" CommandName="DeleteOrCancelCommand" />
                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you certain you want to delete this user? Pending users are permanently deleted, but active users are changed to an inactive state in order to preserve data."
                            Enabled="True" TargetControlID="btnDelete" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr runat="server" id="Tr1" class='<%# SetPending(Eval("PersonStatus"), "ListViewAlternateItem")  %>'
                    onmouseover="this.className='ListViewSelectedItem'" onmouseout='<%# SetPendingMouseOut(Eval("PersonStatus"), "ListViewAlternateItem")  %>'>
                    <td>
                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PhoneNumberLabel" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label>
                    </td>
                    <td>
                        <a href='<%# "mailto:" + Eval("EmailAddress") %>' runat="server" id="EmailLink">
                            <asp:Label ID="EmailAddressLabel" runat="server" Text='<%# Eval("EmailAddress") %>'></asp:Label></a>
                    </td>
                    <td>
                        <asp:HiddenField ID="PersonId" runat="server" Value='<%# Eval("Id") %>' />
                        <asp:HiddenField ID="StatusLabel" runat="server" Value='<%# Eval("PersonStatus") %>' />
                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/pencil.png" ToolTip="Edit Record"
                            AlternateText="Edit" CommandName="EditOrUpdateCommand" />
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete_x.png" ToolTip="Delete Record"
                            AlternateText="Delete" CommandName="DeleteOrCancelCommand" />
                        <asp:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server" ConfirmText="Are you certain you want to delete this user? Pending users are permanently deleted, but active users are changed to an inactive state in order to preserve data."
                            Enabled="True" TargetControlID="btnDelete" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>
        <asp:DataPager runat="server" ID="ItemDataPager" PageSize="10" PagedControlID="lvUsers"
            OnPreRender="ItemDataPager_PreRender">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                    ShowLastPageButton="true" ShowNextPageButton="true" />
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        <b>showing
                            <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.StartRowIndex %>" />
                            to
                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.StartRowIndex+Container.PageSize %>" />
                            ( of
                            <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" />
                            records) </b>
                    </PagerTemplate>
                </asp:TemplatePagerField>
            </Fields>
        </asp:DataPager>
    </asp:Panel>
    <fieldset id="EditProfile" runat="server" visible="false">
        <legend>Edit User Profile</legend>
        <uc1:NonAdminLevelAccessProfile ID="NonAdminLevelAccessProfile1" runat="server" />
        <uc2:OnlyAdminLevelAccessProfile ID="OnlyAdminLevelAccessProfile1" runat="server" />
        <p class="submit">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
        </p>
    </fieldset>
</asp:Content>
