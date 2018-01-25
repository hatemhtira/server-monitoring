<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inscription.aspx.cs" Inherits="registre.inscription" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8"/>
    <!-- Set the viewport width to device width for mobile -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="description" content="Coming soon, Bootstrap, Bootstrap 3.0, Free Coming Soon, free coming soon, free template, coming soon template, Html template, html template, html5, Code lab, codelab, codelab coming soon template, bootstrap coming soon template">
    <title>Banque de Tunisie</title>
    <!-- ============ Google fonts ============ -->
    <link href='http://fonts.googleapis.com/css?family=EB+Garamond' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700,300,800'
        rel='stylesheet' type='text/css' />
    <!-- ============ Add custom CSS here ============ -->
    <link href="css/bootstrapR.min.css" rel="stylesheet" type="text/css" />
    <link href="css/styleR.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
         @font-face {
             font-family: myFirstFont;
             src: url('../fonts/fontawesome/BFantezy.ttf');
         }
     </style>
</head>
<body>
    <form id="form1" runat="server" class="form-validation-2">
     <div id="custom-bootstrap-menu" class="navbar navbar-default " role="navigation">
        <div class="container">
            <div class="navbar-header">
                
                <a class="navbar-brand"  style="font-family:myFirstFont;font-size: 36px;position: absolute;">البنك التونسي</a>
       </div>
            <div class="collapse navbar-collapse navbar-menubuilder">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="seConnecter.aspx">Se Connecter</a> </li>
                    <li><a href="inscription.aspx">S'Inscrire</a> </li>
                 
                </ul>
            </div>
        </div>
    </div>
    <div class="container">

       

        <br />
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <div class="registrationform">
                <div class="form-horizontal">
                    <fieldset>
                        <legend>SIGN UP <i class="fa fa-pencil pull-right"></i></legend>
                        <br />
                      
                         <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="Firstname" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="txNom" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="Nom"   /> 
                              </div> 
                             </div>
                          <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Text="Lastname" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="TxtPrenom" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="Prenom"   /> 
                             </div>
                        </div>
                       <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Login" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="txLogin" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="login"   /> 
                             </div>
                        </div>
                      
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Password" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="txPwd" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="Password"   /> 
                             </div>
                        </div>
                       <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="Email" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="txEmail" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="Email"   /> 
                             </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="Phone" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                 <input ID="txTel" type="text" runat="server"  class="input-sm validate[required] form-control" placeholder="Tel"   /> 
                             </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" Text="Date" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <input id="Txtbirth" type="text" runat="server" class="input-sm validate[required] form-control" placeholder="Date de naissance" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Adress" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <input id="TxtAdresse" type="text" runat="server" class="input-sm validate[required] form-control" placeholder="Adresse" />
                            </div>
                        </div>
                       
                        <br />
                        <div class="form-group">
                            <div class="col-lg-10 col-lg-offset-2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                                          
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>

         <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 text-center">
     
             
                 
            <div id="banner" runat="server">
                 <h1 ">Authenticator Code</h1>

                <div id="aDesparaitre" runat="server" style ="color:white;">
                    <br /><br /><br /><br /><br />
                   Your Google Authenticator<strong>QR code </strong> will be displayed here after successful Signup!
               </div> 
              <div> <h4 style="color:greenyellow"> <asp:Label runat="server" ID="lblpsk"> </asp:Label></h4></div> 
                  <img id="iimmgg" runat="server" src="**"  alt="" height="350" width="350"/>
              <div> <asp:Label runat="server" ID="LblMail">  </asp:Label></div>
            </div>
             
        </div>
    </div>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/jquery.backstretch.js" type="text/javascript"></script>
            <script src="js/validation/validate.min.js" type="text/javascript"></script> <!-- jQuery Form Validation Library -->
        <script src="js/validation/validationEngine.min.js" type="text/javascript"></script> <!-- jQuery Form Validation Library - requirred with above js -->
        <script src="js/icheck.js" type="text/javascript"></script> <!-- Custom Checkbox + Radio -->
       
       
        <!-- All JS functions -->
        <script src="js/functions.js" type="text/javascript"></script>

    <script type="text/javascript">
        'use strict';

        /* ========================== */
        /* ::::::: Backstrech ::::::: */
        /* ========================== */
        // You may also attach Backstretch to a block-level element
        $.backstretch(
        [

            "img/34.jpg",

        ],

        {
            duration: 4500,
            fade: 1500
        }
    );
    </script>
    </form>
</body>
</html>

