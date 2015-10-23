<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404r.aspx.cs" Inherits="Autoskola.Web.forms.kandidat._4042" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <link rel="stylesheet" href="/css/bootstrap.min.css"/>
        <link href="/css/AdminLTE.css" rel="stylesheet" type="text/css" />
        <link href="/css/custom-style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <div class="wrapper row-offcanvas row-offcanvas-left">
      <!-- Right side column. Contains the navbar and content of the page -->
            <aside>
                <!-- Content Header (Page header) -->
                <section class="content-header" style="height:100px">
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="col-lg-4 col-md-4 col-sm-3 col-xs-12"></div>
                    <div class="error-page col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <h2 class="headline text-info alignLeft"> 404</h2>
                        <div class="error-content">
                            <h3 class="alignLeft"><i class="fa fa-warning text-yellow"></i> Ups! Stranica nije pronađena.</h3>
                            <p>
                                Nismo mogli pronaći stranicu koju ste tražili. <br />
                                Vratite se na <a href='/kandidat/naslovnica'>naslovnicu</a>.
                             </p>
                        </div><!-- /.error-content -->
                    </div><!-- /.error-page -->
                    <div class="col-lg-4 col-md-4 col-sm-3 col-xs-12"></div>
                </section><!-- /.content -->
            </aside><!-- /.right-side -->
             </div>
    </form>
</body>
</html>
