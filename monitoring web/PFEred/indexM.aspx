<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="indexM.aspx.cs" Inherits="PFEred.indexM" %>
<%@ Import Namespace="System.Data"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">
    <div id="myContainer">
        
    </div>

    <h4 class="page-title">DASHBOARD SERVER : <%=id%></h4>

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

            <div class="col-md-8">
                <!-- Main Chart -->
                <div class="tile">
                    <h2 class="tile-title">LAST SCANS</h2>

                    <div class="p-10">
                        <div id="line-chart" class="main-chart" style="height: 250px"></div>
                       

                    </div>
                </div>

                <!-- Pies -->
                <div class="tile text-center">
                    <div class="tile-dark p-10">
                        <div class="pie-chart-tiny" data-percent="<%=ram %>">
                            <span class="percent"></span>
                            <span class="pie-title">ram  </span>
                        </div>
                        <div class="pie-chart-tiny" data-percent="<%=cpu %>">
                            <span class="percent"></span>
                            <span class="pie-title">cpu  </span>
                        </div>
                        <div class="pie-chart-tiny" data-percent="<%=ds1 %>">
                            <span class="percent"></span>
                            <span class="pie-title">Emails Sent </span>
                        </div>
                        <div class="pie-chart-tiny" data-percent="<%=ds2 %>">
                            <span class="percent"></span>
                            <span class="pie-title">Sales Rate </span>
                        </div>
                       
                    </div>
                </div>

                

                <!-- Activity -->
                <div class="row">
                    <div class="col-md-6" style="width: 100%;">
                        <div class="tile">
                            <h2 class="tile-title">notifications</h2>


                            <div class="listview narrow">
                                <% foreach (DataRow pRow in logSr.Tables["log"].Rows) %>
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
            <%  int i = 1; foreach (DataRow p in dsServiceM.Tables[0].Rows)
                {
                 %>
            <a href="DashboardService.aspx?id1=<%= p["codeservice"] %>&id2=<%=id %>">
            <div class="col-md-3 col-xs-6" style="text-align: center; margin-left: 24px;">
                <div id="g<%=i %>" class="gauge"></div>
                <h3>service <%=  p["codeservice"] %></h3>
            </div>
                </a>
            <% i++;
                } %>
           <%-- <div class="col-md-3 col-xs-6" style="text-align: center; margin-left: 24px;">
                <div id="g1" class="gauge"></div>
                <h3>service 1</h3>
            </div>--%>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/raphael-2.1.4.min.js"></script>
    <script src="js/justgage.js"></script>
</asp:Content>
