<%@ Page Title="Banque de Tunisie" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Extras.aspx.cs" Inherits="PFEred.Extras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenu" runat="server">


    <div class="block-area" id="custom-select">

        <br />
        <br />

        <div class="row">
            <div class="col-md-2 m-b-15" style="text-align: center">
                <select runat="server" id="cmb" class="select">
                    <option value="1">check ping</option>
                    <option value="2">check port</option>
                    <option value="3">my Adress IP</option>

                </select>
            </div>
            <div class="col-md-2 m-b-15">
                <asp:Button runat="server" ID="btnTest" class="btn btn-gr-gray btn-sm" Text="test it" OnClick="btnTest_Click" />
            </div>
        </div>
        <br />
        <br />
        <hr class="whiter" />
        <div class="row">

            <div id="ping" runat="server" visible="false" style="text-align: center">
                <br />
                <h3 class="block-title" style="text-align: center">Ping</h3>


                <br />
                <br />
                <br />
                <div class="col-md-3 m-b-15">
                    <label>IP Adress</label>


                    <input type="text" id="txtping" runat="server" class="input-sm form-control " placeholder="..." />

                </div>

                <div class="col-md-2 m-b-15">
                    <br />
                    <asp:Button runat="server" ID="btnPing" class="btn btn-gr-gray btn-sm" Text="Ping" Style="height: 40px; width: 67px;"
                        OnClick="btnPing_Click" />
                </div>

                <div class="col-md-2 m-b-15" style="font-size: 18px; left: 115px; width: 259px;">
                    <asp:Label ID="ping1" runat="server" />
                    <br />
                    <asp:Label ID="ping2" runat="server" />
                    <br />
                    <asp:Label ID="ping3" runat="server" />
                    <br />
                    <asp:Label ID="ping4" runat="server" />
                    <br />
                    <asp:Label ID="ping5" runat="server" />
                </div>
            </div>

            <div id="port" runat="server" visible="false" style="text-align: center">
                <br />
                <h3 class="block-title" style="text-align: center">Port</h3>

                <br />
                <br />
                <br />
                <div class="col-md-3 m-b-15">
                    <label>IP Adress</label>
                    <input type="text" runat="server" id="txtIpPort" class="input-sm form-control mask-ip_address " placeholder="xxx.xxxx.xxxx.xxxx" />
                </div>
                <div class="col-md-3 m-b-15">
                    <label>Port</label>
                    <input type="text" runat="server" id="txtPort" class="input-sm form-control  " placeholder="..." />
                </div>
                <div class="col-md-2 m-b-15">
                    <br />
                    <asp:Button runat="server" ID="btnPort" class="btn btn-gr-gray btn-sm" Text="check port" Style="height: 37px; width: 103px;" OnClick="btnPort_Click" />
                </div>
                <div class="col-md-3 m-b-15 block-title" runat="server" id="cp">

                    <asp:Label ID="Label2" runat="server" Text="Common Ports" />
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="FTP: 21" />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="SMTP:	25" />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="TelNet: 23" />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="DNS: 53" />
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="HTTP 80" />
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="POP3: 110" />
                    <br />

                </div>

                 <div class="col-md-3 m-b-15 " style="font-size: 30px;" > 
 
                     <asp:Label id="resultat" runat="server"  text="resultat"  Visible="false" />  <br />
                         <asp:Label id="aport" runat="server"   Visible="false"/>  <br />
                      
                                            </div>
            </div>
            <div id="my" runat="server" visible="false" style="text-align: center">
                <br />
                <h3 class="block-title">My IP</h3>

                <br />
                <br />
                <br />
                <div class="col-md-3 m-b-15">
                    <h3>
                        <asp:Label ID="lblIp" runat="server" Text="my ip" Style="margin-left: 418px;" /></h3>
                </div>

            </div>

        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
