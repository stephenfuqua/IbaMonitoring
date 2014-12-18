<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="account_OnlyAdminLevelAccessProfile" Codebehind="OnlyAdminLevelAccessProfile.ascx.cs" %>
<p>
    <asp:Label ID="lblUserID" runat="server" AssociatedControlID="tbxUserID">UserId:</asp:Label>
    <asp:TextBox ID="tbxUserID" runat="server" ReadOnly="true" />
</p>
<p>
    <asp:Label ID="HasBeenTrainedLabel" runat="server" AssociatedControlID="cbHasBeenTrained">Has Been Trained:</asp:Label>
    <asp:CheckBox ID="cbHasBeenTrained" runat="server" />
</p>
<p>
    <asp:Label ID="lblGpsSerialNumber" runat="server" AssociatedControlID="tbxGpsSerialNumber">GPS Serial Number:</asp:Label>
    <asp:TextBox ID="tbxGpsSerialNumber" runat="server" />
</p>
<p>
    <asp:Label ID="HasClipboardLabel" runat="server" AssociatedControlID="cbHasClipboard">Has Clipboard:</asp:Label>
    <asp:CheckBox ID="cbHasClipboard" runat="server" Text="" />
</p>
<p>
    <asp:Label ID="RoleLabel" runat="server" AssociatedControlID="ddlRoles">Role:</asp:Label>
    <asp:DropDownList ID="ddlRoles" runat="server" >
        <asp:ListItem Text="Administrator" Value="1" Selected="False" />
        <asp:ListItem Text="Regular User" Value="2" Selected="False" />
    </asp:DropDownList>
</p>
<p>
    <asp:Label ID="lblStatus" runat="server" AssociatedControlID="ddlStatus">Status:</asp:Label>
    <asp:DropDownList ID="ddlStatus" runat="server">
        <asp:ListItem Text="Active" Value="2" Selected="False" />
        <asp:ListItem Text="Inactive" Value="3" Selected="False" />
        <asp:ListItem Text="Pending" Value="1" Selected="False" />
    </asp:DropDownList>
</p>
