<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="PFEred._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="utf-8">
    <title>jqGrid for ASP.NET WebForms - edit add and delete dialogs</title>
    <!-- The jQuery UI theme that will be used by the grid -->
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.11.4/themes/redmond/jquery-ui.css" />
    <!-- The jQuery UI theme extension jqGrid needs -->
    <link rel="stylesheet" type="text/css" media="screen" href="/themes/ui.jqgrid.css" />
    <!-- jQuery runtime minified -->
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-2.2.0.min.js" type="text/javascript"></script>
    <!-- The localization file we need, English in this case -->
    <script src="/js/trirand/i18n/grid.locale-en.js" type="text/javascript"></script>
    <!-- The jqGrid client-side javascript -->
    <script src="/js/trirand/jquery.jqGrid.min.js" type="text/javascript"></script>    
    
    <style type="text/css">
        body, html { font-size: 80%; }    
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div>       
       <div id="message"></div>
       
       <script type="text/javascript">

           function addRow() {
               var grid = jQuery("#<%= JQGrid1.ClientID %>");
               grid.editGridRow("new", grid.addDialogOptions);
           }

           function editRow() {
               var grid = jQuery("#<%= JQGrid1.ClientID %>");
               var rowKey = grid.getGridParam("selrow");
               var editOptions = grid.getGridParam('editDialogOptions');
               if (rowKey) {
                   grid.editGridRow(rowKey, editOptions);
               }
               else {
                   alert("No rows are selected");
               }
           }

           function delRow() {
               var grid = jQuery("#<%= JQGrid1.ClientID %>");
               var rowKey = grid.getGridParam("selrow");
               if (rowKey) {
                   grid.delGridRow(rowKey, grid.delDialogOptions);
               }
               else {
                   alert("No rows are selected");
               }
           }
       </script>
       
       <input type="button" onclick="addRow()" value="Add" />
       <input type="button" onclick="editRow()" value="Edit" />
       <input type="button" onclick="delRow()" value="Delete" />
       
       <asp:GridView ID="JQGrid1" runat="server" Width="600px"  
            OnRowDeleting="JQGrid1_RowDeleting"
            OnRowAdding="JQGrid1_RowAdding"
            OnRowEditing="JQGrid1_RowEditing">         
            <Columns>
                <asp:BoundField DataField="CustomerID" Editable="false" PrimaryKey="true" />
                <asp:BoundField DataField="CompanyName" Editable="true" />
                <asp:BoundField DataField="Phone" Editable="true" />
                <asp:BoundField DataField="PostalCode" Editable="true" />
                <asp:BoundField DataField="City" Editable="true" />                
            </Columns>
            <EditRowStyle ShowEditButton="true" ShowAddButton="true" ShowDeleteButton="true" />
            <EditDialogSettings CloseAfterEditing="true" Caption="The Edit Dialog"  />
            <AddDialogSettings CloseAfterAdding="true" />                   
       </asp:GridView>     

       
       <br /><br />
       <trirand:codetabs runat="server" id="DataTableCodeTabs"></trirand:codetabs>
       
    </div>

    </form>
</body>
</html>
