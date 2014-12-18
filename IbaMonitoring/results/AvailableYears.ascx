<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AvailableYears.ascx.cs" Inherits="results_AvailableYears" %>
<p>
        Currently displaying data for
        <strong><asp:Label ID="CurrentYear" runat="server"></asp:Label></strong>. Change to
        <asp:DropDownList ID="AvailableYears" runat="server">
        </asp:DropDownList>
&nbsp;<asp:Button ID="ChangeYear" runat="server" onclick="ChangeYear_Click" Text="Go" /></p>