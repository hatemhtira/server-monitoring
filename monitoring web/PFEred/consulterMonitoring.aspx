<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="consulterMonitoring.aspx.cs" Inherits="PFEred.consulterMonitoring" %>

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
        <h3 class="block-title">Monitoring Table</h3>
        <div class="table-responsive overflow">

            <asp:GridView ID="gvDetails" DataKeyNames="codeM" runat="server"
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

                    <asp:TemplateField HeaderText="Code ">
                        <EditItemTemplate>
                            <asp:Label ID="lbledit" runat="server" Text='<%#Eval("codeM") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblitem" runat="server" Text='<%#Eval("codeM") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Code Server">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownServeur" runat="server" AppendDataBoundItems="True"
                                DataSourceID="SqlDataSource2" DataTextField="tt" DataValueField="CodeServeur"
                                SelectedValue='<%# Eval("CodeServeur") %>' CssClass="wide">
                                <asp:ListItem Text="" value="" />
                            </asp:DropDownList>

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbladress" runat="server" Text='<%#Eval("conServr") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Code Service">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownService"  runat="server" AppendDataBoundItems="True"
                                DataSourceID="SqlDataSource1" DataTextField="tt1" DataValueField="CodeService" 
                                SelectedValue='<%# Eval("CodeService") %>' CssClass="wide">
                                <asp:ListItem Text="" value="" />
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txttel" runat="server" Text='<%#Eval("codeService") %>' CssClass="wide"/>--%>
                         
                            

                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbltel" runat="server" Text='<%#Eval("conServc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Priority">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPriorite" runat="server" Text='<%#Eval("Priorite") %>' CssClass="wide" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblpriorite" runat="server" Text='<%#Eval("Priorite") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                 <%--   <asp:TemplateField HeaderText="Dependance">
                        <EditItemTemplate>
                             <asp:DropDownList ID="DropDownDependance" runat="server"  AppendDataBoundItems="True"
                                DataSourceID="SqlDataSource3" DataTextField="codeM" DataValueField="codeM"
                                SelectedValue='<%# Eval("Dependance") %>' CssClass="wide">
                                 <asp:ListItem Text="0" value="0" />
                            </asp:DropDownList>
                           
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblpwd" runat="server" Text='<%#Eval("Dependance") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>



                </Columns>
                <PagerStyle ForeColor="White"
                    HorizontalAlign="Center"></PagerStyle>
                <EditRowStyle CssClass="GridViewEditRow" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:Test %>"
                SelectCommand="SELECT DISTINCT [CodeService],CONCAT(CodeService, ' ', type)  as tt1 FROM [service]">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                ConnectionString="<%$ ConnectionStrings:Test %>"
                SelectCommand=" SELECT DISTINCT CodeServeur,CONCAT(CodeServeur, ' ', nom)  as tt  FROM [serveur]">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                ConnectionString="<%$ ConnectionStrings:Test %>"
                SelectCommand="SELECT DISTINCT [CodeM] FROM [monitoring]">
            </asp:SqlDataSource>

        </div>
    </div>
      <div class="form-group" style="text-align:center">
        <asp:Button Text="Add Line" ID="btnAjout" runat="server" OnClick="btnAjout_Click"   class="btn btn-sm" />
             
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
