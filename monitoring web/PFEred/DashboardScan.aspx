<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DashboardScan.aspx.cs" Inherits="PFEred.DashboardScan" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <div id="myContainer">
    </div>

    <h4 class="page-title">DASHBOARD</h4>


    <!-- Main Widgets -->




    
    <div class="block-area">
        <div class="row">

            <div class="col-md-8" style="width: 100%;">
                <!-- Main Chart -->
                <div class="table-responsive overflow">
                            <% foreach (DataRow pRow in serveur.Tables["dsserveur"].Rows) %>
                   <%{  int id=  (int)pRow["CodeServeur"]; %>

                    <h3 class="block-title">SERVEUR <%=id %></h3>
                    <table class="table table-bordered table-hover tile">
                        <tr>  
                                   <%SqlDataAdapter serviceAdap = new SqlDataAdapter("SELECT  * from log where codeServeur="+id+"and idscan="+Convert.ToInt32(idscan) +"and codeservice>0", connection);
                                       DataSet dsService = new DataSet();
                                       serviceAdap.Fill(dsService, "dsservice");


                                       foreach ( DataRow pRow1 in dsService.Tables["dsservice"].Rows) %>
                   <%{ %>
                         <td>SERVICE  <%=pRow1["CodeService"] %> </td> 
                            <% if ((int)pRow1["reussi"] == 2)
                                {%>
                           <td runat="server"  >
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/img/vert.png" Width="25" />
                                    </td>
                            <%}
    else if ((int)pRow1["reussi"] == 1)
    { %>
                                    <td runat="server"  >
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/img/jaune.png" Width="25" />
                                    </td>
                            <%}
    else { %>
                                    <td runat="server"  >
                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/img/red.png" Width="25" />
                                    </td>
                            <%} %>

                            <%} %>
                        </tr>
                        </table>
                    <%} %>

                </div>

                </div>

             <td>
                                       
                                    <



                


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
                                        <small class="text-muted"><% = pRow["Codelog"] %> ----<% = pRow["Temps"] %> </small>
                                        <br />
                                        <a class="t-overflow" href="#"><% = pRow["Messagelog"] %></a>

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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
