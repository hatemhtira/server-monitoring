<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ConsulterMochard.aspx.cs" Inherits="PFEred.ConsulterMochard" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <h4 class="page-title b-0">HISTORY</h4>



    <div class="block-area" id="tableHover">
        <div class="table-responsive overflow">
            <table class="table table-bordered table-hover tile">

                <thead>
                    <tr>
                        <th>code mochard</th>
                        <th>code Employe</th>
                        <th>message</th>
                        <th>date</th>


                        <th style="text-align: center">
                            <asp:LinkButton ID="LinkButton3" title="Previous" class="tooltips" runat="server" OnClick="back_Click">
                                    <i class="sa-list-back"></i>
                            </asp:LinkButton>
                            <asp:LinkButton title="Next" class="tooltips" runat="server" ID="LinkButton1" OnClick="next_Click">
                                    <i class="sa-list-forwad"></i>
                            </asp:LinkButton></th>

                    </tr>
                </thead>




                <asp:Repeater ID="mochardList" runat="server">
                    <ItemTemplate>



                        <tr>

                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "CodeMoch") %></span>
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "codeEmploye") %></span>
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Msg") %></span>
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "date") %></span>
                            </td>




                            <td style="text-align: center">
                                <asp:Button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Codemoch") %>' OnCommand="delete_Command1" class="btn btn-sm" />
                            </td>
                            <%--   <div class="pull-right list-img"> 
                                  <asp:button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code_log") %>' OnCommand="delete_Command1"  class="btn btn-sm"/>
                            </div>--%>
                        </tr>


                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

    <div class="  text-center">
        <asp:Repeater ID="rptPages" runat="server">
            <HeaderTemplate>
                <br />
                <br />


                <table class="pagination">
                    <tr class="text">
                        <td><b>Page:</b>&nbsp;</td>

                        <td>
            </HeaderTemplate>
            <ItemTemplate>
                - &nbsp;
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

    </div>


</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
