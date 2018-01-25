<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test logo.aspx.cs" Inherits="PFEred.test_logo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox type="text" runat="server" id="txIp"  AutoPostBack="true" OnTextChanged="txIp_TextChanged" /><br />
        <br />
<asp:Label  runat="server" id="lbLogo" text="........." />
        <asp:Button Text="submit" runat="server" ID="btn"  OnClick="btn_Click"/>
    </div>
        <asp:Image ID="Image1" runat="server" />
    </form>
</body>
</html>
