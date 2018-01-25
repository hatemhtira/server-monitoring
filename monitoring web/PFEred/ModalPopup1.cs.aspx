<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

  protected void ServerButton_Click(object sender, EventArgs e)
  {
    ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>AJAX Control Toolkit</title>
  <script type="text/javascript">
  var launch = false;
  function launchModal() {
    launch = true;
  }
  
  function pageLoad() {
    if (launch) {
      $find("mpe").show();
    }
  }
  </script>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="asm" runat="server" />
    <div>
       
      <asp:Button ID="ServerButton" runat="server" Text="Launch Modal Popup (Server)" OnClick="ServerButton_Click" />
    </div>
    <asp:Panel ID="ModalPanel" runat="server" Width="500px">
      ASP.NET AJAX is a free framework for quickly creating a new generation of more efficient,
      more interactive and highly-personalized Web experiences that work across all the
      most popular browsers.<br />
      <asp:Button ID="OKButton" runat="server" Text="Close" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server"
      TargetControlId="ClientButton" PopupControlID="ModalPanel" OkControlID="OKButton" />
  </form>
</body>
</html>
