<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AssignerPrivilege.aspx.cs" Inherits="PFEred.AssignerPrivilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <style>
        .GridViewEditRow input[type=text] {
            width: 100%;
        }
        /* size textboxes */
        /* size drop down lists */
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <div class="block-area" id="tableHover">
        <h3 class="block-title">affect privilege</h3>
        <div class="table-responsive overflow">
           
            <asp:GridView ID="gvDetails" DataKeyNames="IdDroit" runat="server"
                AutoGenerateColumns="false" CellPadding="4" PageSize="10" AllowPaging="True" CssClass="table table-bordered table-hover tile"
                HeaderStyle-Font-Bold="true"
                OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                OnRowDeleting="gvDetails_RowDeleting" OnRowEditing="gvDetails_RowEditing"
                OnRowUpdating="gvDetails_RowUpdating" OnPageIndexChanging="grdData_PageIndexChanging">

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
                            <asp:Label ID="lbledit" runat="server" Text='<%#Eval("idDroit") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblitem" runat="server" Text='<%#Eval("idDroit") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="code Employee">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownEmploye" runat="server" AppendDataBoundItems="True"
                                DataSourceID="SqlDataSource1" DataTextField="conemploy" DataValueField="userid"
                                SelectedValue='<%# Eval("userid") %>' CssClass="wide">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUSER" runat="server" Text='<%#Eval("conemploy") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="code Server">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownServeur" runat="server" AppendDataBoundItems="True"
                                DataSourceID="SqlDataSource2" DataTextField="tt" DataValueField="codeserveur"
                                SelectedValue='<%# Eval("CodeServeur") %>' CssClass="wide">
                                <asp:ListItem Text="" Value="" />
                            </asp:DropDownList>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblsERVEUR" runat="server" Text='<%#Eval("conServr") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Privilege">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownprivilege" runat="server" AppendDataBoundItems="True" CssClass="wide">
                                <asp:ListItem Text="edit" Value="edit" />
                                <asp:ListItem Text="check" Value="check" />

                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblprivilege" runat="server" Text='<%#Eval("privilege") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>





                </Columns>
                <PagerStyle ForeColor="White"
                    HorizontalAlign="Center"></PagerStyle>
                <EditRowStyle CssClass="GridViewEditRow" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:Test %>"
                SelectCommand="select DISTINCT [UserId] ,CONCAT([employe].[userId], '', [Employe].nom, '',[Employe].Prenom) as conemploy from employe;"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:Test %>"
                SelectCommand="SELECT DISTINCT CodeServeur,CONCAT(CodeServeur, ' ', nom)  as tt  FROM [serveur]"></asp:SqlDataSource>


        </div>
        <div class="form-group" style="text-align:center">
         <asp:Button Text="Add Privilege" ID="btnAjout" runat="server" OnClick="btnAjout_Click"   class="btn btn-sm" />
             
    </div>
    <div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
