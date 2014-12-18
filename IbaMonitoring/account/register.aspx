<%@ Page Language="C#" AutoEventWireup="true" Inherits="Register"
    MasterPageFile="~/iba.master" Codebehind="register.aspx.cs" %>

<%@ MasterType TypeName="IbaMasterPage" %>
<%@ Register Src="NonAdminLevelAccessProfile.ascx" TagName="NonAdminLevelAccessProfile"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @import url("/styles/iba-widepage.css");
    </style>
</asp:Content>
<asp:Content ID="ContentTitle" ContentPlaceHolderID="title" runat="server">
    IBA Monitoring: User Registration</asp:Content>
<asp:Content ID="ContentContent" ContentPlaceHolderID="contentBody" runat="server">
    <p>
        Once registered, your account will be in a pending state until an administrator
        approves you, at which time an e-mail will be sent confirming your registration.
    </p>
    <fieldset>
        <p>
            <uc1:NonAdminLevelAccessProfile ID="NonAdminLevelAccessProfile1" runat="server" />
        </p>
        <p class="submit">
            <asp:Button ID="btnRegister" Text="Register" runat="server" OnClick="btnRegister_Click" />
        </p>
    </fieldset>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentTitle">
    New User Registration
</asp:Content>
