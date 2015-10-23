<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="novaDodajKandidat.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaDodajKonadidat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Novi kandidat
                                <small>dodavanje kandidata</small>
            </h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Podaci o novom kandidatu
                            </h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form">
                            <div class="box-body">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="btn_Registracija" EventName="Click"/>
                                </Triggers>
                                    <ContentTemplate>
                                <div class="form-group">
                                    <label for="name">Ime *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtIme" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="name">Prezime *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtPrezime" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- Email -->
                                <div class="form-group">
                                    <label for="email">Email *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtEmail" class="form-control" TextMode="Email" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- Telefon -->
                                <div class="form-group">
                                    <label for="telefon">Telefon *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtTelefon" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- Datum rodjenja -->
                                <div class="form-group">
                                    <label for="email">Datum rođenja *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtDatumRodjenja" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- JMBG -->
                                <div class="form-group">
                                    <label for="JMBG">JMBG</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtJMBG" class="form-control" MaxLength="13" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- Adresa -->
                                <div class="form-group">
                                    <label for="adresa">Adresa</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtAdresa" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- Select box -->
                                <div class="form-group">
                                    <label>Grad *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:DropDownList ID="gradovidropdown" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <!-- Username -->
                                <div class="form-group">
                                    <label for="username">Korisničko ime *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtKorisnickoIme" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <!-- Password -->
                                <div class="form-group">
                                    <label for="email">Lozinka *</label>
                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 input-group">
                                        <asp:TextBox ID="txtLozinka" TextMode="Password" class="form-control" runat="server"></asp:TextBox>

                                    </div>
                                </div>
                                <br />
                                <div id="Success_div" runat="server" class="form-group" visible="false">
                                    <div class="alert alert-success alert-dismissible" role="alert" style="margin-left: -1px">
                                        <strong>Uspješno! </strong> Dodali ste novog kandidata.
                                    </div>
                                </div>
                                <div id="Danger_div" runat="server" class="form-group" visible="false">
                                    <div class="alert alert-danger alert-dismissible" role="alert" style="margin-left: -1px">
                                        <strong>Upozorenje!</strong> Potrebno je popuniti obavezna polja!
                                    </div>
                                </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <!-- Accept box and button s-->
                                 <div class="box-footer">
                                        <asp:Button ID="btn_Registracija" class="btn btn-primary" runat="server" style="margin-right:10px" Text="Dodaj" OnClick="btn_Registracija_Click" ValidationGroup="dodavanjeKandidata" />
                                        <asp:HyperLink ID="btnReset" runat="server" NavigateUrl="/instruktor/kandidati" CssClass="btn btn-default">Odustani</asp:HyperLink>
                            </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </div>
                    <!-- /.box -->


                    <!-- Form Element sizes -->





                </div>
                <!--/.col (left) -->
                <!-- right column -->
                <div class="col-md-6">
                </div>
                <!--/.col (right) -->
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </aside>
    <!-- /.right-side -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <script src="/js/jquery.js"></script> 

    <script src="/js/jquery.maskedinput.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(
            $("#<%=txtDatumRodjenja.ClientID%>").mask("99/99/9999", { placeholder: "DD/MM/GGGG" }));

        $(document).ready(function () {
            $("#<%=txtTelefon.ClientID%>").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: Ctrl+C
                    (e.keyCode == 67 && e.ctrlKey === true) ||
                    // Allow: Ctrl+X
                    (e.keyCode == 88 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });

        $(document).ready(function () {
            $("#<%=txtJMBG.ClientID%>").keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    (e.keyCode == 65 && e.ctrlKey === true) ||
                    // Allow: Ctrl+C
                    (e.keyCode == 67 && e.ctrlKey === true) ||
                    // Allow: Ctrl+X
                    (e.keyCode == 88 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right
                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            });
        });

  
    </script>
</asp:Content>