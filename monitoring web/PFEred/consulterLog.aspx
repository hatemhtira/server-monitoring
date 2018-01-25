<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="consulterLog.aspx.cs" Inherits="PFEred.consulterLog" %>
 
<%@ Import Namespace="System.Data"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
     <h4 class="page-title b-0">HISTORY</h4>
                
 
   
                     <div class="block-area" id="tableHover">
        <div class="table-responsive overflow">
            <table class="table table-bordered table-hover tile">

               <thead>
                                <tr>
                                    <th>code log</th>
                                    <th>codeService</th><th>message</th><th>temps de reponse</th><th>time</th>
                                    
                                    
                                     <th style="text-align:center"> <asp:LinkButton ID="LinkButton3" title="Previous" class="tooltips" runat="server" OnClick="back_Click">
                                    <i class="sa-list-back"></i>
                    </asp:LinkButton> 
                                         </th><th><asp:LinkButton title="Next" class="tooltips" runat="server" ID="LinkButton1" OnClick="next_Click">
                                    <i class="sa-list-forwad"></i>
                    </asp:LinkButton></th>
                                   
                                </tr>
                            </thead>
               
                

                        
                  <asp:Repeater ID="ProductList" runat="server">
                            <ItemTemplate>
                                
                             

                        <tr  >
                            
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Codelog") %></span>
                            </td>
                             <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "codeService") %></span>
                            </td>
                             <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Messagelog") %></span>
                            </td>
                            <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "Temps") %></span>
                            </td>
                             <td>
                                <span class="t-overflow f-bold"><%# DataBinder.Eval(Container.DataItem, "TempsDeReponse") %></span>
                            </td>
                          
                                 <asp:Label runat="server"  Visible="false"><%# a = DataBinder.Eval(Container.DataItem, "reussi") %></asp:Label>
                            <td  style="width: 50px;text-align:center"   runat="server" visible="<%# Convert.ToInt32(a) ==2 %>">
                                <asp:Image ID="imgtest" runat="server" ImageUrl="~/img/vert.png" Width="31px" Height="31px" />
                            </td>
                            <td  style="width: 50px;text-align:center"   runat="server" visible="<%# Convert.ToInt32(a) ==1 %>">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/img/jaune.png" Width="31px" Height="31px" />
                            </td>
                            <td style="width: 50px;text-align:center"   runat="server" visible="<%# Convert.ToInt32(a) ==0 %>">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/img/red.png" Width="31px" Height="31px" />
                            </td>
                            
                            
                            
                            
                              <td style="text-align:center"> 
                                  <asp:button runat="server" Text="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Codelog") %>' OnCommand="delete_Command1"  class="btn btn-sm"/>
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
                 <asp:Repeater ID="rptPages" Runat="server">
      <HeaderTemplate>
          <br /><br />
      <table  style="">
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
                         Runat="server"><%# Container.DataItem %>
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
