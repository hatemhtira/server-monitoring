<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="pagination.aspx.cs" Inherits="PFEred.pagination" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">

     
      <div class="message-list list-container">


       <div class="media">
      <asp:Repeater ID="rptItems" runat="server">
    
      <ItemTemplate>
    
                        <a class="media-body"  >
                            
                            <div class="pull-left list-title">
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Code_log") %></span>
                            </div>
                             <div class="pull-left list-title">
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "code_service") %></span>
                            </div>
                             <div class="pull-left list-title">
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Message_log") %></span>
                            </div>
                            <div class="pull-left list-title">
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Temps") %></span>
                            </div>
                             <div class="pull-left list-title">
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "TempsDeReponse") %></span>
                            </div>
                        <%--    <div class="pull-right list-date">   <asp:button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code_log") %>' OnCommand="delete_Command1"  class="btn btn-sm"/>
                        </div>--%>
                           
                             
                            
                           
                        </a>
      </ItemTemplate>
           
     
      </asp:Repeater>
            <asp:Repeater ID="rptPages" Runat="server">
      <HeaderTemplate>
      <table  >
      <tr class="text">
         <td><b>Page:</b>&nbsp;</td>
         <td><asp:Button ID="prvs" runat="server" OnClick="prvs_Click" /></td>
          <td> 
      </HeaderTemplate>
      <ItemTemplate>
         <asp:LinkButton ID="btnPage"
                         CommandName="Page"
                         CommandArgument="<%#
                         Container.DataItem %>"
                         CssClass="text"
                         Runat="server"><%# Container.DataItem %>
                         </asp:LinkButton>&nbsp;
      </ItemTemplate>
      <FooterTemplate>
         </td>
      </tr>
      </table>
      </FooterTemplate>
      </asp:Repeater>
    <asp:LinkButton ID="btnprv" runat="server" OnClick="prvs_Click"  Text ="<<<<"/><asp:Label ID="lbl" runat="server" />
<asp:LinkButton ID="btnsuiv" runat="server" OnClick="btnsuiv_Click"  Text =">>>>"/>

            </div>
          </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
