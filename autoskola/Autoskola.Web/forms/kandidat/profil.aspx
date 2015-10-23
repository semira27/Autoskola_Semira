<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="profil.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.profil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <h1 id="imePrezimeHeader" runat="server" style="font-size: 30px;margin-top: 0px;margin-bottom: 0px"><i style="font-size: 40px; margin-right: 10px" class="fa fa-user"></i>Ime i prezime
                        <small style="font-size: 16px;margin-left: 5px">pregled detalja kandidata</small>
            </h1>
            </ContentTemplate>
        </asp:UpdatePanel>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <div class="box-body" style="padding-top: 40px">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <h3 id="brPrijavljenihKat" runat="server">
                                            0
                                            </h3>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <p>
                                        prijavljenih kategorija
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <a class="small-box-footer" style="height:26px">
                                </a>
                            </div>
                        </div><!-- ./col -->
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <h3 id="brUradjenihPriprema" runat="server">
                                        0
                                    </h3>
                                    <p>
                                        urađenih priprema
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <a href="#" class="small-box-footer" style="height:26px">
                                </a>
                            </div>
                        </div><!-- ./col -->
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner" style="height: 140px">
                                    <h3 id="brPolozenihKat" runat="server" style="font-size: 50px">
                                        0
                                    </h3>
                                    <p style="font-size: 20px">
                                        položenih kategorija
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-check-square-o" style="font-size:135px"></i>
                                </div>
                                <a href="#" class="small-box-footer" style="height:26px">
                                </a>
                            </div>
                        </div><!-- ./col -->
                        </div>
                    <!-- /.box -->

                </div>
                <!-- /.col -->


                <!-- right column -->
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <!-- general form elements disabled -->
                    <div class="box box-primary">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="urediKandidata" EventName="Click" />
                             </Triggers>
                            <ContentTemplate>
                        <div class="box-header">
                            <h3 class="box-title"><i class="fa fa-edit">&nbsp;&nbsp;</i>Podaci kandidata</h3>
                            <asp:Button ID="urediKandidata" runat="server" Text="Uredi" CssClass="btn btn-sm btn-default" OnClick="urediKandidata_Click" style="margin-top: 7px;margin-right: 10px;float: right" />
                        </div>
                        <!-- /.box-header -->
                        <div role="form">
                            <div class="box-body">
                                <!-- Detalji kandidata -->
                                <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover">
                                    <tr>
                                        <td>Ime i prezime</td>
                                        <td id="td_ImePrezime" runat="server">Semira Čehić</td>
                                        <td id="td_ImePrezime_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtIme" runat="server" CssClass="col-lg-4" style="margin-right: 13px;width: 127px"></asp:TextBox>
                                            <asp:TextBox ID="txtPrezime" runat="server" CssClass="col-lg-4" style="width: 127px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>JMBG</td>
                                        <td id="td_JMBG" runat="server">1234567891234</td>
                                        <td id="td_JMBG_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtJMBG" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Datum rođenja</td>
                                        <td id="td_DatumRodjenja" runat="server">01.01.2011</td>
                                        <td id="td_DatumRodjenja_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtDatumRodjenja" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Grad</td>
                                        <td id="td_Grad" runat="server">Mostar</td>
                                        <td id="td_Grad_Edit" runat="server" visible="false">
                                            <asp:DropDownList ID="listGradovi" class="form-control" style="width:268px" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Adresa</td>
                                        <td id="td_Adresa" runat="server">Ulica 23</td>
                                        <td id="td_Adresa_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtAdresa" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Telefon</td>
                                        <td id="td_Telefon" runat="server">062/123-456</td>
                                        <td id="td_Telefon_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtTelefon" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>E-mail</td>
                                        <td id="td_Email" runat="server">kandidat@autoskola.ba</td>
                                        <td id="td_Email_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Korisničko ime</td>
                                        <td id="td_KorisnickoIme" runat="server">ime.prezime</td>
                                        <td id="td_KorisnickoIme_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtKorisnickoIme" runat="server" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_LozinkaMain" runat="server">Datum registracije</td>
                                        <td id="td_Lozinka" runat="server">01.01.2014</td>
                                        <td id="td_Lozinka_Edit" runat="server" visible="false">
                                            <asp:TextBox ID="txtLozinka" runat="server" TextMode="Password" CssClass="col-lg-9"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                    </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- /.box -->
                </div>
                <!--/.col (right) -->

                <!-- right column -->
                <div class="col-lg-6 col-md-7 col-sm-12 col-xs-12">
                    <!-- general form elements disabled -->
                    <div class="box box-primary"  style="min-height:373px">
                        <div class="box-header">
                            <h3 class="box-title"><i class="fa fa-calendar">&nbsp;&nbsp;</i>Prijave</h3>
                        </div>
                        <!-- /.box-header -->
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                        <div role="form">
                            <div class="box-body" style="min-height: 298px">
                                <h5 id="noPrijaveMsg" runat="server" visible="false">Kandidat još nema niti jednu prijavu.</h5>
                                <div class="table-responsive">
                                <asp:GridView ID="prijave_Grid" runat="server" CssClass="table table-mailbox table-prijave" EnableViewState="true" AutoGenerateColumns="false" PageSize="4" GridLines="None" AllowPaging="true" OnPageIndexChanging="prijave_Grid_PageIndexChanging" OnRowCommand="prijave_Grid_RowCommand">
                                    <Columns>
                                         <asp:TemplateField HeaderText="Pregled">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="linkPrijave" CssClass="btn btn-default btn-sm" NavigateUrl='<%# string.Format("/kandidat/prijavljene-kategorije?id={0}",Eval("PrijavaId"))%>' runat="server"><i class="fa fa-search"></i> Detalji</asp:HyperLink>
                                           </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kategorija/e" ControlStyle-CssClass="time time-left">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="kategorijeLink" runat="server"><%# Autoskola.Data.Prijave.Join(", ",(List<Autoskola.Data.KategorijePrijave>)DataBinder.Eval(Container.DataItem, "KategorijeUPrijavi")) %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kandidat" ControlStyle-CssClass="name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="kandidatLink" runat="server"  NavigateUrl='<%# string.Format("/kandidat/instruktor?id={0}",Eval("InstruktorId"))%>'><%#Eval ("Instruktor.Korisnik.Ime") %>&#32;<%#Eval ("Instruktor.Korisnik.Prezime") %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Datum" DataField="DatumPrijave" dataformatstring="{0:dd/MM/yyyy}"  />
                                         <asp:TemplateField HeaderText="Obriši">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="obrisiPrijavu" CommandArgument='<%# Eval("PrijavaId") %>' CommandName="deleteCommand" runat="server" class="btn btn-xs btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                                </asp:GridView>
                             </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </div>
                    <!-- /.box -->
                </div>
                <!--/.col (right) -->

                <!-- right column -->
                <div class="col-lg-6 col-md-5 col-sm-12 col-xs-12">
                    <!-- general form elements disabled -->
                    <div class="box box-primary"  style="min-height:373px">
                        <div class="box-header">
                            <h3 class="box-title"><i class="fa fa-calendar">&nbsp;&nbsp;</i>Izlasci na testove</h3>
                        </div>
                        <!-- /.box-header -->
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                        <div role="form">
                            <div class="box-body" style="min-height: 298px">
                                <h5 id="emptyIzlasci" runat="server" visible="false">Kandidat još nije nijednom izašao na testove.</h5>
                                <div class="table-responsive">
                                <asp:GridView ID="izlasciTestoviGrid" runat="server" CssClass="table table-mailbox table-prijave" EnableViewState="true" AutoGenerateColumns="false" PageSize="4" GridLines="None" AllowPaging="true" OnRowDataBound="izlasciTestoviGrid_RowDataBound" OnPageIndexChanging="izlasciTestoviGrid_PageIndexChanging" OnRowCommand="izlasciTestoviGrid_RowCommand">
                                    <Columns>
                                 <asp:BoundField HeaderText="Datum polaganja" DataField="DatumPolaganja"  dataformatstring="{0:MM/dd/yyyy}"/>
                                 <asp:BoundField HeaderText="Kategorija" DataField="KategorijaIzPrijave.Kategorije.Naziv"  dataformatstring="{0:MM/dd/yyyy}"/>
                                  <asp:TemplateField HeaderText="Uspjeh">
                                      <ItemTemplate>
                                         <span id="polozenoColor" runat="server" class="label label-danger"><%# Convert.ToBoolean(Eval("Polozeno")) ? "Položeno" : "Nepoloženo" %></span>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                                </asp:GridView>
                             </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    </div>
                    <!-- /.box -->
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

</asp:Content>
