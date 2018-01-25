<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="consulterService.aspx.cs" Inherits="PFEred.consulterService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">

    <h4 class="page-title b-0">Services List</h4>
 <div class="media">
            <div class="block-area" id="tableHover">
                <div class="table-responsive overflow">
                    <table class="table table-bordered table-hover tile">

                        <thead>
                            <tr>
                                <th>No.</th>
                                <th>type</th>
                                <th>operation1</th>
                                <th>opeartion2</th>
                                <th>resultat</th>


                                <th style="text-align: center">
                                    <asp:LinkButton ID="LinkButton3" title="Previous" class="tooltips" runat="server" OnClick="back_Click">
                                    <i class="sa-list-back"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton title="Next" class="tooltips" runat="server" ID="LinkButton1" OnClick="next_Click">
                                    <i class="sa-list-forwad"></i>
                                    </asp:LinkButton></th>

                            </tr>
                        </thead>
                        <asp:Repeater ID="ProductList" runat="server">
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "CodeService") %> 
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "type") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "operation1") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "operation2") %> 
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "resultatAttendu") %>
                                    </td>

                                   <td style="text-align:center">
                                        <asp:Button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Codeservice") %>' OnCommand="delete_Command1" class="btn btn-sm" />
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

        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
