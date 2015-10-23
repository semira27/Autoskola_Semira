<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="VrstaPitanja.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaVrstaPitanja1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1 id="heading_top" runat="server">
                     <small>pregled detalja</small>
            </h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Pitanja</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form">
                            <div class="box-body">

                                <asp:GridView ID="Pitanja_Grid" class="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="false" PageSize="10" GridLines="None" AllowPaging="true" OnPageIndexChanging="Pitanja_Grid_PageIndexChanging" OnRowCommand="Pitanja_Grid_RowCommand">
                                    <Columns>

                                        <asp:BoundField HeaderText="Br." DataField="Slika" />
                                        <asp:BoundField HeaderText="Pitanje" DataField="Pitanje" />
                                        <asp:TemplateField HeaderText="Datum dodavanja">
                                            <ItemTemplate>
                                                <asp:Label ID="Datum_label" runat="server"><%#Eval ("DatumDodavanja") %></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Uredi podatke">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="urediPitanje" runat="server" CssClass="btn btn-xs btn-warning" NavigateUrl='<%# string.Format("/instruktor/izmjena-pitanja?id={0}",Eval("PitanjeId")) %>'> <i class="fa fa-pencil"></i></asp:HyperLink>
                                                <asp:LinkButton ID="obrisiPitanje" runat="server" CommandArgument='<%# Eval("PitanjeId") %>' CommandName="deleteCommand" CssClass="btn btn-xs btn-danger"> <i class="fa fa-times"></i> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign = "Left" CssClass = "GridPager" />
                                </asp:GridView>

                                <div style='float: right; margin-right: 12px; margin-top: 20px'>
                                    <asp:LinkButton ID="LinkButton_DodajPitanje" runat="server" Text="Dodaj pitanje" CssClass="btn btn-sm btn-success"></asp:LinkButton>

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
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
</asp:Content>