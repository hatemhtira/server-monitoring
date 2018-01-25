<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DashboardService.aspx.cs" Inherits="PFEred.DashboardService" %>
<%@ Import Namespace="System.Data"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <div id="myContainer">
        
    </div>

    <h4 class="page-title">DASHBOARD SERVICE</h4>

    <!-- Shortcuts -->
    <!-- Quick Stats -->
    <div class="block-area">
        <div class="row">
            <div class="col-md-3 col-xs-6">
                <div class="tile quick-stats">
                    <div id="stats-line-2" class="pull-left"></div>
                    <div class="data">
                        <h2 data-value="<%=etat%>">0</h2>
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

            <div class="col-md-8"  style=" width: 100%;">
                <!-- Main Chart -->
                <div class="tile">
                    <h2 class="tile-title">Statistics</h2>

                    <div class="p-10">
                        <div id="line-chart" class="main-chart" style="height: 250px"></div>
                        

                    </div>
                </div>

                

                

                <!-- Activity -->
                 <!-- Activity -->
                <div class="row">
                    <div class="col-md-6" style="width: 100%;">
                        <div class="tile">
                            <h2 class="tile-title">notification</h2>


                            <div class="listview narrow">
                                <% foreach (DataRow pRow in logSc.Tables["log"].Rows) %>
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
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/charts.js"></script>
</asp:Content>
