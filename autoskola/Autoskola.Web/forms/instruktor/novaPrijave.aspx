<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="novaPrijave.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaPrijave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
     <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Prijave
               <small>pregled prijava</small>
            </h1>
        </section>

                <!-- Main content -->
                <section class="content">
                    <!-- MAILBOX BEGIN -->
                    <div class="mailbox row">
                        <div class="col-xs-12">
                            <div class="box box-solid">
                                <div class="box-body">
                                                       <div class="row">
                                        <div class="col-md-3 col-sm-4">
                                            <!-- BOXES are complex enough to move the .box-header around.
                                                 This is an example of having the box header within the box body -->
                                            <div class="box-header">
                                                <i class="fa fa-inbox"></i>
                                                <h3 class="box-title">PRIJAVE</h3>
                                            </div>
                                            <!-- compose message btn -->
                                            <a href="/instruktor/novaprijava" class="btn btn-block btn-primary"><i class="fa fa-pencil"></i> Nova prijava</a>
                                            <!-- Navigation - folders-->
                                            <div style="margin-top: 15px;">
                                                <ul class="nav nav-pills nav-stacked">
                                                    <li class="header">  </li> 
                                                    <li class="active"><asp:LinkButton ID="svePrijave" runat="server" OnClick="svePrijave_Click"><i class="fa fa-inbox"></i> Sve prijave</asp:LinkButton></li>
                                                    <li><asp:LinkButton ID="aktivnePrijave" runat="server" OnClick="aktivnePrijave_Click"><i class="fa fa-pencil-square-o"></i> Aktivne prijave</asp:LinkButton></li>
                                                    <li><asp:LinkButton ID="zavrsenePrijave" runat="server" OnClick="zavrsenePrijave_Click"><i class="fa fa-mail-forward"></i> Završene prijave</asp:LinkButton></li>
                                                    <li><asp:LinkButton ID="obrisanePrijave" runat="server" OnClick="obrisanePrijave_Click"><i class="fa fa-trash-o"></i> Obrisane prijave</asp:LinkButton></li>
                                                </ul>
                                            </div>
                                        </div><!-- /.col (LEFT) -->
                                        <div class="col-md-9 col-sm-8" style="min-height: 371px">
                                            <div class="row pad">
                                                <div class="col-sm-6">
                                                    <!-- Action button -->
                                                     
                                                </div>
                                                <div class="col-sm-6 search-form">
                                                    <div class="text-right">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txt_pretraga" runat="server" class="form-control input-sm" placeholder="Pretraga po kandidatu ili instruktoru"></asp:TextBox>
                                                            <div class="input-group-btn">
                                                                <asp:LinkButton ID="pretragaBtn" runat="server" class="btn btn-sm btn-primary" OnClick="pretragaBtn_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><!-- /.row -->

                                            <div class="table-responsive">
                                                <!-- THE MESSAGES -->
                                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                          <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="pretragaBtn" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="aktivnePrijave" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="zavrsenePrijave" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="obrisanePrijave" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="svePrijave" EventName="Click" />

                                                         </Triggers>
                                                    <ContentTemplate>
                                              <asp:GridView ID="PrijaveGrid" class="table table-mailbox table-prijave" runat="server" EnableViewState="true" AutoGenerateColumns="false" PageSize="5" GridLines="None" AllowPaging="true" OnDataBound="PrijaveGrid_DataBound" OnRowCommand="PrijaveGrid_RowCommand" OnPageIndexChanging="PrijaveGrid_PageIndexChanging">
                                                    <Columns>
                                         <asp:TemplateField HeaderStyle-Width="100" >
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="linkPrijave" CssClass="btn btn-default btn-sm" NavigateUrl='<%# string.Format("/instruktor/prijava?id={0}",Eval("PrijavaId"))%>' runat="server"><i class="fa fa-search"></i> Detalji</asp:HyperLink>
                                           </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kandidat" ControlStyle-CssClass="name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="kandidatLink" runat="server"><%#Eval ("Kandidat.Korisnik.Ime") %>&#32;<%#Eval ("Kandidat.Korisnik.Prezime") %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kategorija/e" ControlStyle-CssClass="time time-left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="kategorijeLink" runat="server"><%# Autoskola.Data.Prijave.Join(", ",(List<Autoskola.Data.KategorijePrijave>)DataBinder.Eval(Container.DataItem, "KategorijeUPrijavi")) %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruktor" ControlStyle-CssClass="name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="instruktorLink" runat="server"><%#Eval ("Instruktor.Korisnik.Ime") %>&#32;<%#Eval ("Instruktor.Korisnik.Prezime") %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Datum" DataField="DatumPrijave" dataformatstring="{0:dd/MM/yyyy}"  />
                                         <asp:TemplateField HeaderText="Obriši" HeaderStyle-Width="70">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="obrisiPrijavu" CommandArgument='<%# Eval("PrijavaId") %>' CommandName="deleteCommand" runat="server" class="btn btn-xs btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                         </asp:TemplateField>
                                                    </Columns>
                                    <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                                                </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div><!-- /.table-responsive -->
                                        </div><!-- /.col (RIGHT) -->
                                    </div><!-- /.row -->

                         
                                </div><!-- /.box-body -->
                                <div class="box-footer clearfix">
                                    <div class="pull-right">
                                    </div>
                                </div><!-- box-footer -->
                            </div><!-- /.box -->
                        </div><!-- /.col (MAIN) -->
                    </div>
                    <!-- MAILBOX END -->

                </section><!-- /.content -->
    </aside>
    <!-- /.right-side -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
</asp:Content>
