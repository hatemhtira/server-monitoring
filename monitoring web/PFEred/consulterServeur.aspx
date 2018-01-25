<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="consulterServeur.aspx.cs" Inherits="PFEred.consulterServeur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <h4 class="page-title b-0">Servers List</h4>

     

    <div class="block-area" id="tableHover">
        <div class="table-responsive overflow">
            <table class="table table-bordered table-hover tile">

               <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Name</th><th>adress</th><th>login</th><th>password</th>
                                    
                                    
                                     <th style="text-align:center"> <asp:LinkButton ID="LinkButton3" title="Previous" class="tooltips" runat="server" OnClick="back_Click">
                                    <i class="sa-list-back"></i>
                    </asp:LinkButton> <asp:LinkButton title="Next" class="tooltips" runat="server" ID="LinkButton1" OnClick="next_Click">
                                    <i class="sa-list-forwad"></i>
                    </asp:LinkButton></th>
                                   
                                </tr>
                            </thead>
               
                


                <asp:Repeater ID="ProductList" runat="server">
                    <ItemTemplate>



                        <tr>

                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "CodeServeur") %> 
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "nom") %> 
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "adresseServeur") %></span>
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "login") %></span>
                            </td>


                            <td>
                                <span class="t-overflow"><%# DataBinder.Eval(Container.DataItem, "pwd") %></span>

                            </td>
                            <td style="text-align:center">
                                <asp:Button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Codeserveur") %>' OnCommand="delete_Command1" class="btn btn-sm" />
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <asp:Repeater ID="rptPages" runat="server">

        <HeaderTemplate>
            <br />
            <br />
            <table style="">
                <tr class="text">
                    <td><b>Page:</b>&nbsp;</td>

                    <td>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="btnPage"
                CommandName="Page"
                CommandArgument="<%#
                         Container.DataItem %>"
                CssClass="text"
                runat="server"><%# Container.DataItem %>
            </asp:LinkButton>&nbsp;
        </ItemTemplate>
        <FooterTemplate>
            </td>
      </tr>
      </table>
         
        </FooterTemplate>
    </asp:Repeater>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
