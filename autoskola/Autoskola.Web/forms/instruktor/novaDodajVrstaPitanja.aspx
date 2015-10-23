<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="novaDodajVrstaPitanja.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaVrstaPitanja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
             <h1>Vrsta pitanja
                 <small>nova vrsta pitanja</small>
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
                            <h3 class="box-title">Nova vrsta pitanja</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Naziv</label>
                                    <asp:TextBox ID="txt_Naziv" runat="server" CssClass="form-control" placeholder="Unesite naziv vrste pitanja"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Opis</label>
                                        <asp:TextBox ID="txt_Opis" runat="server" TextMode="MultiLine" CssClass="cleditor form-control" placeholder="Unesite opis vrste pitanja" style="resize:none"></asp:TextBox>
                                 
                                </div>
                                  <div class="form-group">
                                    <label for="txt_Broj">Broj bodova</label>
                                    <asp:TextBox ID="txt_Broj" runat="server" CssClass="form-control" TextMode="Number" placeholder="Unesite broj bodova koje nosi jedno pitanje"></asp:TextBox>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Add" EventName="Click" />
                            </Triggers>
                                    <ContentTemplate>
                                <div id="Success_div" runat="server" class="form-group" visible="false">
                                    <div class="alert alert-success alert-dismissible" role="alert" style="margin-left: -1px">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <strong>Uspješno izvršeno! </strong> Uspješno ste dodali novu vrstu pitanja.
                                    </div>
                                </div>
                                <div id="Danger_div" runat="server" class="form-group" visible="false">
                                    <div class="alert alert-danger alert-dismissible" role="alert" style="margin-left: -1px">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <strong>Upozorenje!</strong> Naziv grupe pitanja i broj bodova su obavezna polja!
                                    </div>
                                </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                               


                            </div>
                            <!-- /.box-body -->

                            <div class="box-footer">
                                <asp:Button ID="btn_Add" runat="server" CssClass="btn btn-primary" Text="Dodaj" OnClick="btn_Add_Click" />
                            </div>
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
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
</asp:Content>