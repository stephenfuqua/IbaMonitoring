<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="account_NonAdminLevelAccessProfile" Codebehind="NonAdminLevelAccessProfile.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ValidationSummary ID="vs" runat="server" ShowSummary="true" HeaderText="The following errors are preventing you from submitting the form:" />
<p>
    <asp:Label ID="OpenIDLabel" runat="server" AssociatedControlID="txtOpenID">OpenID:</asp:Label>
    <asp:TextBox ID="txtOpenID" runat="server"  CssClass="ReadOnly" Enabled="false"
        ToolTip="You are kindly requested not to use your email account as your openid." 
        Width="300px" ReadOnly="True"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvOpenID" runat="server" ErrorMessage="OpenID is required field"
        ControlToValidate="txtOpenID">*</asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="OpenIdLength" runat="server" 
        ControlToValidate="txtOpenID" ErrorMessage="Max 250 characters in OpenID" 
        ValidationExpression=".{0,250}" Display="Dynamic">Max 250 characters in OpenID</asp:RegularExpressionValidator>
</p>
<p class="Required">
    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtFName">First Name:</asp:Label>
    <asp:TextBox ID="txtFName" runat="server" />
    <strong>required</strong>
    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Text=" *" ErrorMessage="First Name is required field"
        ControlToValidate="txtFName" />
    <asp:RegularExpressionValidator ID="FirstNameLength" runat="server" 
        ControlToValidate="txtFName" ErrorMessage="Max 50 characters in first name" 
        ValidationExpression="[A-Za-z \'\-]{0,50}" Display="Dynamic">Max 50 <i>letters</i> in first name</asp:RegularExpressionValidator>
</p>
<p class="Required">
    <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="txtLName">Last Name:</asp:Label>
    <asp:TextBox ID="txtLName" runat="server" />
    <strong>required</strong><asp:RequiredFieldValidator ID="rfvLName" runat="server" Text=" *" ErrorMessage="Last Name is required field"
        ControlToValidate="txtLName" />
    <asp:RegularExpressionValidator ID="LastNameLength" runat="server" 
        ControlToValidate="txtLName" ErrorMessage="Max 50 characters in last name" 
        ValidationExpression="[A-Za-z \'\-]{0,50}" Display="Dynamic">Max 50 <i>letters</i> in last name</asp:RegularExpressionValidator>
</p>
<p class="Required">
    <asp:Label ID="EmailAddressLabel" runat="server" AssociatedControlID="txtEmailAddress">E-mail Address:</asp:Label>
    <asp:TextBox ID="txtEmailAddress" runat="server" MaxLength="50" />
    <strong>required</strong><asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" Text=" *" ErrorMessage="Email Address is required field"
        ControlToValidate="txtEmailAddress" />
    <asp:RegularExpressionValidator ID="EmailAddressRegEx" runat="server" 
        ControlToValidate="txtEmailAddress" ErrorMessage="Invalid e-mail address" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic">Invalid e-mail address</asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="EmailLength" runat="server" 
        ControlToValidate="txtEmailAddress" ErrorMessage="Max 250 characters in e-mail" 
        ValidationExpression=".{0,250}" Display="Dynamic">Max 250 characters in e-mail</asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="PhoneNumberLabel" runat="server" AssociatedControlID="txtPhoneNumber">Phone Number:</asp:Label>
    <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="20" />
    <asp:RegularExpressionValidator ID="PhoneNumberRegEx" runat="server" 
        ControlToValidate="txtPhoneNumber" ErrorMessage="Invalid phone number" 
        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}">Invalid phone number</asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="PhoneNumberLength" runat="server" 
        ControlToValidate="txtPhoneNumber"  Display="Dynamic"
        ErrorMessage="Max 20 characters in phone number" ValidationExpression=".{0,20}"></asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="Address1Label" runat="server" AssociatedControlID="txtAddress1">Address 1:</asp:Label>
    <asp:TextBox ID="txtAddress1" runat="server" />
    <asp:RegularExpressionValidator ID="Address1Length" runat="server" 
        ControlToValidate="txtAddress1" ErrorMessage="Max 50 characters in address 1"  Display="Dynamic"
        ValidationExpression="[0-9A-Za-z \'-\.\#]{0,50}">Max 50 characters in address 1</asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="Address2Label" runat="server" AssociatedControlID="txtAddress2">Address 2:</asp:Label>
    <asp:TextBox ID="txtAddress2" runat="server" />
    <asp:RegularExpressionValidator ID="Address2length" runat="server" 
        ControlToValidate="txtAddress2" ErrorMessage="Max 50 characters in address 2"  Display="Dynamic"
        ValidationExpression="[0-9A-Za-z \'-\.\#]{0,50}">Max 50 characters in address 2</asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="CityLabel" runat="server" AssociatedControlID="txtCity">City:</asp:Label>
    <asp:TextBox ID="txtCity" runat="server" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtCity" ErrorMessage="Max 50 characters in city"  Display="Dynamic"
        ValidationExpression="[0-9A-Za-z \'-\.\#]{0,50}">Max 50 characters in city</asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="StateLabe" runat="server" AssociatedControlID="txtState">UserState:</asp:Label>
    <asp:TextBox ID="txtState" runat="server" Columns="2" Width="30px" 
        MaxLength="2"></asp:TextBox>
    <asp:RegularExpressionValidator ID="StateRegEx" runat="server" 
        ControlToValidate="txtState" ErrorMessage="Invalid UserState"  Display="Dynamic"
        ValidationExpression="A[LKSZRAP]|C[AOT]|D[EC]|F[LM]|G[AU]|HI|I[ADL N]|K[SY]|LA|M[ADEHINOPST]|N[CDEHJMVY]|O[HKR]|P[ARW]|RI|S[CD] |T[NX]|UT|V[AIT]|W[AIVY]">Invalid UserState</asp:RegularExpressionValidator>
</p>
<p>
    <asp:Label ID="ZipCodeLabel" runat="server" AssociatedControlID="txtZipCode">Zip Code:</asp:Label>
    <asp:TextBox ID="txtZipCode" runat="server" Width="50px" MaxLength="10"></asp:TextBox>
    <asp:RegularExpressionValidator ID="ZipRegEx" runat="server"  Display="Dynamic"
        ControlToValidate="txtZipCode" ErrorMessage="Invalid U.S. zip code" 
        ValidationExpression="\d{5}(-\d{4})?">Invalid U.S. zip code</asp:RegularExpressionValidator>
</p>

