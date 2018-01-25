<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="consulterEmployee.aspx.cs" Inherits="PFEred.consulterEmployeP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
     <link href="css/style.css" rel="stylesheet"/>
    <style>
        .GridViewEditRow input[type=text] {width:100%;} /* size textboxes */
  /* size drop down lists */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
     
         <div class="block-area" id="tableHover">
                    <h3 class="block-title">LIST OF EMPLOYEES</h3>
     <div class="table-responsive overflow">
                         
<asp:GridView ID="gvDetails" DataKeyNames="UserId,Nom,Prenom" runat="server"
AutoGenerateColumns="false"   CellPadding="4" PageSize="5" AllowPaging="True" CssClass="table table-bordered table-hover tile"  
 HeaderStyle-Font-Bold="true" 
onrowcancelingedit="gvDetails_RowCancelingEdit"
onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing"
onrowupdating="gvDetails_RowUpdating"  OnPageIndexChanging="grdData_PageIndexChanging">
      
<Columns>
<asp:TemplateField>
<EditItemTemplate>
<asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/img/AddUser.png" ToolTip="Update" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/img/RemoveUser.png" ToolTip="Cancel" Height="20px" Width="20px" />
</EditItemTemplate>

<ItemTemplate>
<asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/EditUser.png" ToolTip="Edit" Height="20px" Width="20px" />
<asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server" ImageUrl="~/img/RemoveUser.png" ToolTip="Delete" Height="20px" Width="20px" />
</ItemTemplate>
</asp:TemplateField>

     <asp:TemplateField HeaderText="ID">
<EditItemTemplate>
<asp:Label ID="lbledit" runat="server" Text='<%#Eval("Userid") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitem" runat="server" Text='<%#Eval("Userid") %>'/>
</ItemTemplate>
 </asp:TemplateField>

<asp:TemplateField HeaderText="Lastname">
<EditItemTemplate>
<asp:Label ID="lbleditusr" runat="server" Text='<%#Eval("Nom") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemUsr" runat="server" Text='<%#Eval("Nom") %>'/>
</ItemTemplate>
 </asp:TemplateField>

    <asp:TemplateField HeaderText="Firstname">
<EditItemTemplate>
<asp:Label ID="lbleditr" runat="server" Text='<%#Eval("Prenom") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemr" runat="server" Text='<%#Eval("Prenom") %>'/>
</ItemTemplate>
 </asp:TemplateField>
 

<asp:TemplateField HeaderText="Adress">
<EditItemTemplate>
<asp:TextBox ID="txtadress" runat="server" Text='<%#Eval("Adresse") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lbladress" runat="server" Text='<%#Eval("Adresse") %>'/>
</ItemTemplate>
 </asp:TemplateField>

    <asp:TemplateField HeaderText="Phone">
<EditItemTemplate>
<asp:TextBox ID="txttel" runat="server" Text='<%#Eval("Tel") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lbltel" runat="server" Text='<%#Eval("Tel") %>'/>
</ItemTemplate>
 </asp:TemplateField>

       <asp:TemplateField HeaderText="Birthdate">
<EditItemTemplate>
<asp:TextBox ID="txtne" runat="server" Text='<%#Eval("DateDeNaissance") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblne" runat="server" Text='<%#Eval("DateDeNaissance") %>'/>
</ItemTemplate>
 </asp:TemplateField>

    <asp:TemplateField HeaderText="Login">
<EditItemTemplate>
<asp:TextBox ID="txtlogin" runat="server" Text='<%#Eval("Login") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lbllogin" runat="server" Text='<%#Eval("Login") %>'/>
</ItemTemplate>
 </asp:TemplateField>

      <asp:TemplateField HeaderText="Password">
<EditItemTemplate>
<asp:TextBox ID="txtpwd" runat="server" Text='<%#Eval("Pwd") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblpwd" runat="server" Text='<%#Eval("Pwd") %>'/>
</ItemTemplate>
 </asp:TemplateField>

      <asp:TemplateField HeaderText="PSK">
<EditItemTemplate>
<asp:TextBox ID="txtpsk" runat="server" Text='<%#Eval("Psk") %>' CssClass="wide"/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblpsk" runat="server" Text='<%#Eval("Psk") %>'/>
</ItemTemplate>
 </asp:TemplateField>

        <asp:TemplateField HeaderText="Access">
<EditItemTemplate>
   <asp:DropDownList ID="DropDownaccess" runat="server"    CssClass="wide">
                        <asp:ListItem Text="locked" Value="0" />
                        <asp:ListItem Text="unlocked" Value="1" />

                            </asp:DropDownList>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblaccess" runat="server" Text='<%# (Boolean.Parse(Eval("Access").ToString())) ? "unlocked" : "locked" %>'/>
</ItemTemplate>
 </asp:TemplateField>

 
</Columns>
  <pagerstyle   ForeColor="White" 
            HorizontalAlign="Center"></pagerstyle>
     <EditRowStyle CssClass="GridViewEditRow" />
</asp:GridView> 
</div>
        </div> 
<div>
<asp:Label ID="lblresult" runat="server"></asp:Label>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
