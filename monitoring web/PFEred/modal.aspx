<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modal.aspx.cs" Inherits="PFEred.modal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
        <link href="css/animate.min.css" rel="stylesheet"/>
        <link href="css/font-awesome.min.css" rel="stylesheet"/>
        <link href="css/form.css" rel="stylesheet"/>
        <link href="css/calendar.css" rel="stylesheet"/>
        <link href="css/style.css" rel="stylesheet"/>
</head>
<body>
      <form id="form1" runat="server"  class="form-horizontal">
    <button    data-toggle="modal" data-target="#form-modal"  class="btn btn-sm">dddddd</button>
     <asp:HyperLink  text="tes2" UseSubmitBehavior="false" data-dismiss="modal" runat="server" data-toggle="modal" href="#form-modal" class="btn btn-sm"  value="<% 5 %>" OnInit="Unnamed_Click1" /> 

     
    <%--<asp:Repeater ID="ProductList" runat="server">
    <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem, "Name") %> for only <%# DataBinder.Eval(Container.DataItem, "Price", "{0:c}") %>
        <br />
        <a href='<%# DataBinder.Eval(Container.DataItem, "ProductID", "details.asp?id={0}") %>'>See Details</a>
        <br />
        <br />
    </ItemTemplate>
</asp:Repeater>--%>

        

    <div>
    <div class="modal fade" id="form-modal" tabindex="-1" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button class="close" type="button" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Modal title</h4>
                                </div>
                                <div class="modal-body">
                            
                                        <div class="form-group">
                                            <asp:Label for="inputName4"  id="lbl1" runat="server"  class="col-md-2 control-label">test</asp:Label>
                                            <div class="col-md-9">
                                                <input type="text" runat="server" class="form-control input-sm" id="inputName4" value="" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="inputEmail4" class="col-md-2 control-label">Email</label>
                                            <div class="col-md-9">
                                                <input type="email" runat="server"  class="form-control input-sm" id="inputEmail4" value="" />
                                            </div>
                                        </div>
                                      
                                   
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-sm btn-alt">Save changes</button>
                                    <button type="button" class="btn btn-sm btn-alt" data-dismiss="modal">Close</button>
                                    <a data-toggle="modal" href="#myModal5" class="btn btn-info btn-lg">Putting a Modal in a Modal</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
    </div>
        <div class="modal fade" id="myModal5" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
          <h3 class="modal-title">Modal5 - Modal in A Modal</h3>
        </div>
        <div class="modal-body">
          <asp:Label id="lbl" runat="server">Tsss</asp:Label>
                                                <asp:TextBox type="text" runat="server" class="form-control input-sm" id="txIp"    /><br />
        <br /> 
        </div>
        <div class="modal-footer">
                          <!-- Button trigger modal 4-->
          <a data-toggle="modal" href="#form-modal">openmodal</a>
            <asp:Button runat="server" ID="btnn"   href="#form-modal" Text="ttt" />
         <!-- Button trigger modal 6-->
         
          <button type="button" class="btn btn-info" data-dismiss="modal">Close</button>
          
        </div>
      </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
  </div>
    </form>
            <!-- Javascript Libraries -->
        <!-- jQuery -->
        <script src="js/jquery.min.js"></script> <!-- jQuery Library -->
        
        <!-- Bootstrap -->
        <script src="js/bootstrap.min.js"></script>
        
        <!--  Form Related -->
        <script src="js/select.min.js"></script> <!-- Custom Select -->
        <script src="js/icheck.js"></script> <!-- Custom Checkbox + Radio -->
        <script src="js/fileupload.min.js"></script> <!-- File Upload -->
        <script src="js/autosize.min.js"></script> <!-- Textare autosize -->
        
        <!-- UX -->
        <script src="js/scroll.min.js"></script> <!-- Custom Scrollbar -->
        
        <!-- Other -->
        <script src="js/calendar.min.js"></script> <!-- Calendar -->
        <script src="js/feeds.min.js"></script> <!-- News Feeds -->
        
        
        <!-- All JS functions -->
        <script src="js/functions.js"></script>

</body>
</html>
