<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="kategorije.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.prijave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    
            <!-- Right side column. Contains the navbar and content of the page -->
            <aside class="right-side">

                  <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Prijava
                        <small id="lbldatum" runat="server"></small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a id="headerInstruktor" data-toggle="modal" data-target="#compose-modal" runat="server" href="/kandidat/instruktor">Instruktor: Ime i prezime</a></li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">

                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <asp:DataList ID="listKategorijePrijave" runat="server" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                  <div class="col-lg-3 col-xs-6" style="width:320px">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        <%#Eval ("Kategorije.Naziv") %>
                                    </h3>
                                    <p id="kategorijaInfo" runat="server" style="margin-left:3px"> 
                                        Info
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-arrows"></i>
                                </div>
                                <a href='<%# String.Format("/kandidat/prijavljena-kategorija?id={0}",Eval("KategorijaPrijavaId")) %>' class="small-box-footer">
                                    Detaljno <i class="fa fa-arrow-circle-right"></i>
                                </a>
                            </div>
                        </div><!-- ./col -->
                            </ItemTemplate>
                        </asp:DataList>
                    </div><!-- /.row -->

                    <!-- Main row -->
                    <div class="row">
                        <!-- Left col -->
                        <section class="col-lg-7 connectedSortable">



                        </section><!-- /.Left col -->
                        <!-- right col (We are only adding the ID to make the widgets sortable)-->
                        <section class="col-lg-5 connectedSortable">




                        </section><!-- right col -->
                    </div><!-- /.row (main row) -->

                </section><!-- /.content -->
            </aside><!-- /.right-side -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <!-- COMPOSE MESSAGE MODAL -->
    <div id="composemodal" runat="server">
                <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 340px;margin: 0px auto">
                    <div class="modal-header">
                        <h4 class="modal-title">Podaci instruktora</h4>
                    </div>
                    <div>
                        <div class="modal-body">
                                                <!-- general form elements disabled -->
                    <div class="">
                        <!-- /.box-header -->
                        <div role="form">
                            <div class="box-body">
                                <!-- Detalji kandidata -->
                                <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td>Ime i prezime</td>
                                        <td id="td_ImePrezime" runat="server">Ime Prezime</td>
                                    </tr>
                                    <tr>
                                        <td>Datum rođenja</td>
                                        <td id="td_DatumRodjenja" runat="server">01.01.2011</td>
                                    </tr>
                                    <tr>
                                        <td>Grad</td>
                                        <td id="td_Grad" runat="server">Mostar</td>
                                    </tr>
                                    <tr>
                                        <td>Adresa</td>
                                        <td id="td_Adresa" runat="server">Ulica 23</td>
                                    </tr>
                                    <tr>
                                        <td>Telefon</td>
                                        <td id="td_Telefon" runat="server">062/123-456</td>
                                    </tr>
                                    <tr>
                                        <td>E-mail</td>
                                        <td id="td_Email" runat="server">kandidat@autoskola.ba</td>
                                    </tr>
                                </table>
                                    </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                    </div>
                    <!-- /.box -->
                        </div>
                        
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Zatvori</button>
                            </div>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
        </div><!-- /.modal -->
    </div>


</asp:Content>

