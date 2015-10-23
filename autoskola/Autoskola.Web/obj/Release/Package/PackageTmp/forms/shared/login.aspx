<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Autoskola.Web.forms.shared.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8"/>
        <title>Autoškola | Log in</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'/>
        <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
</head>
<body>
    <form id="form1" runat="server">
      <div class="form-box" id="login-box">
            <div class="header">Dobrodošli!</div>
            <div>
                <div class="body bg-gray">
                    <div class="form-group">
                        <asp:TextBox ID="txt_KorisnickoIme" runat="server" CssClass="form-control" placeholder="Korisničko ime"></asp:TextBox>
                   </div>
                    <div class="form-group">
                        <asp:TextBox ID="txt_Lozinka" runat="server" CssClass="form-control" placeholder="Lozinka" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="footer">
                    <asp:Button ID="btn_Prijava" runat="server" CssClass="btn bg-olive btn-block" Text="Prijava" OnClick="btn_Prijava_Click" />

                </div>
            </div>

            <div class="margin text-center">
                <span>Copyright © 2015 FIT ukoliko nije drugačije označeno.
                    <br />
                Sva prava pridržana.
                </span>
                <br/>
            </div>
        </div>

        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js" type="text/javascript"></script>


    </form>
</body>
</html>
