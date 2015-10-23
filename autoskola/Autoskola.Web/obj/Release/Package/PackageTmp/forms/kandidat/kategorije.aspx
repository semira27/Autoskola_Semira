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
                        <small>A + B + C</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li class="active">Prijava</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">

                    <!-- Small boxes (Stat box) -->
                    <div class="row">
                        <asp:DataList ID="listPrijave" runat="server" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                  <div class="col-lg-3 col-xs-6" style="width:320px">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <h3>
                                        <%#Eval ("KategorijePitanja.Naziv") %>
                                    </h3>
                                    <p>
                                        Info Info Info
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
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
