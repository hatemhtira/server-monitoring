<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Accueil.aspx.cs" Inherits="PFEred.Accueil" %>

<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
   
  <div class="form-group" >
       <asp:LinkButton  id="ref" OnClick="refresh_Click"  runat="server" style="margin-right: 16px;float: right;padding: 7px; margin-top: 3px;"   ><i class="sa-list-refresh"></i></asp:LinkButton>
              
    </div>
    <h4 class="page-title">Home Page</h4>
   
            
    <!-- Shortcuts -->
    <!-- Quick Stats -->
    <div class="block-area">
        <div class="row">
            <div class="col-md-3 col-xs-6">
                <div class="tile quick-stats">
                    <div id="stats-line-2" class="pull-left"></div>
                    <div class="data">
                        <h2  ><%=test%></h2>
                        <small>ETAT</small>
                    </div>
                </div>
            </div>


            <div class="col-md-3 col-xs-6">
                <div class="tile quick-stats media">
                    <div id="stats-line-3" class="pull-left"></div>
                    <div class="media-body">

                        <h2 style="margin-left: 32px;"><%=lastDown.Hour.ToString()%> :<%=lastDown.Minute.ToString()%>:<%=lastDown.Second.ToString()%></h2>

                        <small style="font-size: 13px">Last Down    <%= lastDown.ToString()%></small>
                    </div>
                </div>

            </div>

            <div class="col-md-3 col-xs-6">
                <div class="tile quick-stats media">
                    <div id="stats-line-4" class="pull-left"></div>
                    <div class="media-body">

                        <h2 style="margin-left: 32px;"><%=lastUp.Hour.ToString()%> :<%=lastUp.Minute.ToString()%>:<%=lastUp.Second.ToString()%></h2>

                        <small style="font-size: 13px">Last Up    <%= lastUp.ToString()%></small>
                    </div>
                </div>

            </div>

            <div class="col-md-3 col-xs-6">
                <div class="tile quick-stats media">
                    <div id="stats-line" class="pull-left"></div>
                    <div class="media-body">

                        <h2 style="margin-left: 32px;"><%=lastScan.Hour.ToString()%> :<%=lastScan.Minute.ToString()%>:<%=lastScan.Second.ToString()%></h2>

                        <small style="font-size: 13px">Last Scan    <%= lastScan.ToString()%></small>
                    </div>
                </div>

            </div>

        </div>

    </div>

    <hr class="whiter" />

    <!-- Main Widgets -->





    <div class="block-area">
        <div class="row">

            <div class="col-md-8" style="width: 100%;">
                <!-- Main Chart -->
                <div class="tile">
                    <h2 class="tile-title">LAST SCANS</h2>

                    <div class="p-10">
                        <div id="line-charta" class="main-chart" style="height: 250px"></div>
                        
                    </div>
                </div>
                
 
                <!-- Dynamic Chart -->
      
                    <h3 class="block-title">ETAT DES SERVEURS</h3>
                    <div class="table-responsive overflow">
                        <table class="table table-bordered table-hover tile">
                            <thead>
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                              
                                    <th>Serveur</th>
                                    <th>Etat</th>
                                   
                            
                                    </ItemTemplate>

                                </asp:Repeater>
                            </thead>

                            <tbody>

                                <asp:Repeater ID="ProductList" runat="server">
                                    <ItemTemplate>
                                 
                                    <td>
                                         <%# DataBinder.Eval(Container.DataItem, "CodeServeur") %> 
                                    </td>
                                        <asp:Label runat="server" Visible="false"><%# a = DataBinder.Eval(Container.DataItem, "reussi") %></asp:Label>
                                    <td runat="server" visible="<%# Convert.ToInt32(a) ==2 %>">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/vert.png" Width="25" />
                                        </td>
                                    <td runat="server" visible="<%# Convert.ToInt32(a) ==1 %>">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/img/jaune.png" Width="25" />
                                    </td>
                                    <td runat="server" visible="<%# Convert.ToInt32(a) ==0 %>">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/img/red.png" Width="25" />
                                    </td>
                                    <%--<td><%# DataBinder.Eval(Container.DataItem, "CodeServeur") %></td>
                                    <td>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/img/vert.png" Width="25" />
                                    </td>
                                </tr>--%>
                                 
                                    </ItemTemplate>

                                </asp:Repeater>
                              
                            </tbody>
                        </table>
                    </div>
               

                <!--  Recent Postings -->
                <div class="row">
                    <div class="col-md-6" style="width: 100%;">
                        <div class="tile">
                            <h2 class="tile-title">notification</h2>


                            <div class="listview narrow">
                                <% foreach (DataRow pRow in log.Tables["log"].Rows) %>
                                <%{ %>

                                <div class="media p-l-5">

                                    <div class="media-body">
                                        <small class="text-muted">IDScan:<% = pRow["idscan"] %> &nbsp;&nbsp;&nbsp;&nbsp;CodeServeur:<% = pRow["CodeServeur"] %>&nbsp;&nbsp;&nbsp;&nbsp;CodeService:<% = pRow["CodeService"] %>&nbsp;&nbsp;&nbsp;&nbsp;Temps:<% = pRow["Temps"] %> </small>
                                        <br />
                                        <a class="t-overflow" href="#">&nbsp;&nbsp;<% = pRow["Messagelog"] %></a>

                                    </div>
                                </div>
                                <%} %>

                                     <div class="media p-5 text-center l-100">
                                    <a href="#"><small>VIEW ALL</small></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
