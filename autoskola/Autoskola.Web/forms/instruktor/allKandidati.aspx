<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="allKandidati.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.Kandidati" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
      <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Kandidati
               <small>pregled kandidata</small>
            </h1>
        </section>

                <!-- Main content -->
                <section class="content">
                    <!-- MAILBOX BEGIN -->
                    <div class="mailbox row">
                        <div class="col-xs-12">
                            <div class="box box-solid">
                                <div class="box-body" style="min-height:300px">
                                                       <div class="row">
                                        <div class="col-md-3 col-sm-4">
                                            <!-- BOXES are complex enough to move the .box-header around.
                                                 This is an example of having the box header within the box body -->
                                            <div class="box-header">
                                                <i class="fa fa-inbox"></i>
                                                <h3 class="box-title">KANDIDATI</h3>
                                            </div>
                                            <!-- compose message btn -->
                                            <a href="/instruktor/novikandidat" class="btn btn-block btn-primary"><i class="fa fa-pencil"></i> Novi kandidat</a>
                                            <!-- Navigation - folders-->
                                            <div style="margin-top: 15px;">
                                                <ul class="nav nav-pills nav-stacked">
                                                    <li class="header">  </li> 
                                                    <li class="active"><asp:LinkButton ID="sviKandidati" runat="server" OnClick="sviKandidati_Click"><i class="fa fa-inbox"></i> Svi kandidati</asp:LinkButton></li>
                                                    <li><asp:LinkButton ID="spremnostiKandidati" runat="server" OnClick="spremnostiKandidati_Click"><i class="fa fa-bar-chart"></i> Spremnosti za testove</asp:LinkButton></li>
                                                </ul>
                                            </div>
                                        </div><!-- /.col (LEFT) -->
                                        <div class="col-md-9 col-sm-8" style="min-height:371px">
                                            <div class="row pad">
                                                <div class="col-sm-6">
                                                    <!-- Action button -->
                                                     
                                                </div>
                                                <div class="col-sm-6 search-form">
                                                    <div class="text-right">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txt_pretraga" runat="server" class="form-control input-sm" placeholder="Pretraga po imenu i/ili prezimenu"></asp:TextBox>
                                                            <div class="input-group-btn">
                                                                <asp:LinkButton ID="btn_Pretraga" runat="server" class="btn btn-sm btn-primary" OnClick="btn_Pretraga_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div><!-- /.row -->

                                            <div class="table-responsive">
                                                <!-- THE MESSAGES -->
                                                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                                    <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btn_Pretraga" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="sviKandidati" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="spremnostiKandidati" EventName="Click" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                              <asp:GridView ID="KandidatiGrid" class="table table-mailbox table-prijave" runat="server" EnableViewState="true" AutoGenerateColumns="false" PageSize="5" GridLines="None" AllowPaging="true" OnRowCommand="KandidatiGrid_RowCommand" OnPageIndexChanging="KandidatiGrid_PageIndexChanging" >
                                                    <Columns>
                                         <asp:TemplateField HeaderStyle-Width="120" >
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="linkPrijave" CssClass="btn btn-default btn-sm" NavigateUrl='<%# string.Format("/instruktor/prijava?id={0}",Eval("KorisnikId"))%>' runat="server"><i class="fa fa-search"></i> Detalji</asp:HyperLink>
                                           </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-Width="120" >
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="linkKandidata" CssClass="btn btn-default btn-sm" NavigateUrl='<%# string.Format("/instruktor/kandidat?id={0}",Eval("KorisnikId"))%>' runat="server"><i class="fa fa-search"></i> Detalji</asp:HyperLink>
                                           </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Kandidat" ControlStyle-CssClass="name">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="kandidatLink" runat="server"><%#Eval ("Ime") %>&#32;<%#Eval ("Prezime") %></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Email" DataField="Email" />
                                        <asp:BoundField HeaderText="Telefon" DataField="Telefon" />
                                        <asp:BoundField HeaderText="Grad" DataField="Grad.Naziv" />
                                         <asp:TemplateField HeaderText="Obriši" HeaderStyle-Width="80">
                                                        <ItemTemplate>     
                                                            <asp:LinkButton ID="obrisiKandidata" CommandArgument='<%# Eval("KorisnikId") %>' CommandName="deleteCommand" runat="server" class="btn btn-xs btn-danger"> <i class="fa fa-times"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Spremnost">
                                            <ItemTemplate>
                                                <span class="label label-default" style="font-size:15px"><%# Eval("Email") %></span>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                        <asp:BoundField HeaderText="Kategorija" DataField="Telefon" />
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
