<%@ Control Language="C#" AutoEventWireup="true" Inherits="observations_NewRecorder" Codebehind="NewRecorder.ascx.cs" %>
<asp:Panel ID="newRecorder_Div" runat="server" CssClass="ModalPopup">
    <asp:Panel ID="newRecorder_Group" runat="server" GroupingText="Add New Recorder">
    <p>
        <asp:Label ID="newRecorder_Label" runat="server" 
            AssociatedControlID="newRecorder" Text="New Recorder Name"></asp:Label>
        <asp:TextBox ID="newRecorder" runat="server"></asp:TextBox>
    </p>
        <p>
            <asp:Button ID="add" runat="server" Text="Add" />
        </p>
    </asp:Panel>
</asp:Panel>
