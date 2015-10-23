<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="pregledPrijave.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.pregledPrijave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <!-- Right side column. Contains the navbar and content of the page -->
            <aside class="right-side">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Prijava
                        <small>pregled detalja prijave</small>
                    </h1>
                </section>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="pregledajKategoriju" EventName="Click"/>
                                </Triggers>
                                <ContentTemplate>
                <!-- Main content -->
                <section class="content invoice">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 id="headerKandidat" runat="server" class="page-header">
                                <i class="fa fa-user"></i> Ime i prezime
                                <small class="pull-right">Datum: 2/10/2014</small>
                            </h2>
                        </div><!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <div class="col-sm-4 col-lg-4 col-xs-12 col-md-4 invoice-col">
                            Instruktor
                            <address>
                                <strong id="instruktorName" runat="server">Ime i prezime</strong>
                            </address>
                        </div><!-- /.col -->
                        <div class="col-sm-3 col-lg-4 col-xs-12 col-md-4 invoice-col">
                            Status prijave
                            <address>
                                <strong id="statusName" runat="server">Aktivna</strong>
                            </address>
                        </div><!-- /.col -->
                        <div class="col-sm-5 col-lg-4 col-xs-12 col-md-4 invoice-col">
                            <b>Prijavljene kategorije</b><br/>
                            <br/> 
                             <div class="input-group col-md-6 col-lg-7 col-sm-7 col-xs-5" style="display:inherit">
                                  <asp:DropDownList ID="kategorijeList" runat="server" CssClass="form-control"></asp:DropDownList>
                             </div>
                            <asp:LinkButton ID="pregledajKategoriju" runat="server" CssClass="btn btn-primary btn-sm" style="margin-left:20px" OnClick="pregledajKategoriju_Click">Pregledaj</asp:LinkButton>
                             <br /><br />
                        </div><!-- /.col -->
                       
                    </div><!-- /.row -->
                    
                    <div id="pregledaj_kat_div" runat="server">
                        
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-solid">
                                <div class="box-header">
                                    <i class="fa fa-bar-chart-o"></i>
                                    <h3 class="box-title">Analiza trenutnog stanja prijavljene kategorije</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div id="divCharts" style="margin: 0px auto;width: 100%;text-align: center">
                                        <div class="col-md-3 col-sm-6 col-xs-12 text-center" style="display: inline-block;float: none">
                                            <input id="spremnostChart" runat="server" type="text" class="knobspecial" value="30" data-width="120" data-height="120" data-fgColor="#f56954" data-readonly="true"/>
                                            <div class="knob-label">Spremnost za izlazak na testove</div>
                                        </div><!-- ./col -->
                                        <div class="col-md-3 col-sm-6 col-xs-12 text-center" style="display: inline-block;float: none">
                                            <input id="pripremeChart" runat="server" type="text" class="knob knobBlack" value="30" data-width="120" data-height="120" data-fgColor="#EDEDED"  data-readonly="true"/>
                                            <div class="knob-label">Broj urađenih priprema</div>
                                        </div><!-- ./col -->
                                        <div class="col-md-3 col-sm-6 col-xs-12 text-center" style="display: inline-block;float: none">
                                            <input id="testoviChart" runat="server" type="text" class="knob knobBlack" value="30" data-width="120" data-height="120" data-fgColor="#EDEDED" data-readonly="true"/>
                                            <div class="knob-label">Broj pokušaja izlaska na testove</div>
                                        </div><!-- ./col -->
                                        </div>
                                    </div><!-- /.row -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                        </div><!-- /.col -->
                    </div><!-- /.row -->


                    
                        <br />
                    <!-- Table row -->
                    <div class="row">
                        <div class="col-xs-12">
                          <p class="lead">Urađene pripreme</p>
                            <span id="pripreme_empty" runat="server" visible="false" style="display:block">Kandidat nije uradio još niti jednu pripremu za testove.</span>
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                 <asp:GridView ID="uradjeniTestoviGrid" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" PageSize="5" GridLines="None" AllowPaging="true" OnRowDataBound="uradjeniTestoviGrid_RowDataBound" OnPageIndexChanging="uradjeniTestoviGrid_PageIndexChanging">
                                     <Columns>
                                        <asp:BoundField HeaderText="Početak" DataField="PocetakTesta" ItemStyle-Width="140" />
                                        <asp:BoundField HeaderText="Kraj" DataField="KrajTesta" ItemStyle-Width="140" />
                                         <asp:TemplateField HeaderText="Uspjeh" ItemStyle-Width="330">
                                            <ItemTemplate>
                                             <div class="progress xs uspjehCorrection">
                                                    <div id="grafUspjeh" runat="server" style='<%# Eval("KategorijaPrijavaId", "width:{0}%") %>'></div>
                                             </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Procenat" ItemStyle-Width="110">
                                            <ItemTemplate>
                                                <span runat="server" id="procenat" class="badge bg-red badgeCorrection"><%# Eval("KategorijaPrijavaId") %>%</span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Osvojeni bodovi" ItemStyle-Width="110">
                                            <ItemTemplate>
                                                <span runat="server" id="osvojeniBodoviGrid" class="badge bg-default badgeCorrection"><%# Eval("OsvojeniBodovi") %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mogući bodovi" ItemStyle-Width="110">
                                            <ItemTemplate>
                                                <span runat="server" id="moguciBodoviGrid" class="badge bg-default badgeCorrection"><%# Eval("MaxBodovi") %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     </Columns>
                                     <PagerStyle HorizontalAlign = "Right" CssClass = "GridPager" />
                                 </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div><!-- /.col -->
                    </div><!-- /.row -->
                        <br /><br />
                    <div class="row">
                        <!-- glavni testovi -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                          <p class="lead"><b>Izlazak na testove</b></p>
                            <span id="testovi_empty" runat="server" visible="false">Kandidat još nije izašao na testove.</span>
                        <div class="table-responsive">
                        <asp:GridView ID="glavniTestoviGrid" runat="server" ShowHeader="false" CssClass="table" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="glavniTestoviGrid_RowDataBound" >
                            <Columns>
                                 <asp:BoundField DataField="DatumPolaganja"  dataformatstring="{0:MM/dd/yyyy}"/>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                         <span id="polozenoColor" runat="server" class="label label-danger"><%# Convert.ToBoolean(Eval("Polozeno")) ? "Položeno" : "Nepoloženo" %></span>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                        </div>
                        <!-- /glavni testovi -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                            <p class="lead">Sažetak pripremanja za testove</p>
                            <div class="table-responsive">
                                <table class="table">
                                    <tr>
                                        <th style="width:50%">Ukupno urađenih priprema:</th>
                                        <td id="countTestovi" runat="server">0</td>
                                    </tr>
                                    <tr>
                                        <th>Spremnost za testove:</th>
                                        <td id="spremnostiTestovi" runat="server">0%</td>
                                    </tr>
                                </table>
                            </div>
                        </div><!-- /.col -->

                    </div><!-- /.row -->
                    </div>
                    <br /><br />
                    <!-- this row will not appear when printing -->
                    <div class="row no-print">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <a id="addTest" runat="server" class="btn btn-default col-xs-12 col-md-4 col-lg-3 col-sm-4" data-toggle="modal" data-target="#compose-modal"><i class="fa fa-plus-circle"></i> Dodaj izlazak na testove</a>
                            <asp:LinkButton ID="changeStatus" runat="server" CssClass="btn btn-success pull-right col-xs-12 col-md-4 col-lg-3 col-sm-4" OnClick="finishPrijava_Click"><i class="fa fa-check"></i> Promijeni status prijave</asp:LinkButton>
                        </div>
                    </div> 
                </section><!-- /.content -->
                                     </ContentTemplate>
                            </asp:UpdatePanel>
            </aside><!-- /.right-side -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <!-- COMPOSE MESSAGE MODAL -->
        <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 340px;margin: 0px auto">
                    <div class="modal-header">
                        <h4 class="modal-title">Izlazak na testove</h4>
                    </div>
                    <div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="input-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <label>Datum polaganja</label>
                                    <asp:TextBox ID="datumTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                    <asp:RadioButtonList ID="radioPolozeno" runat="server">
                                        <asp:ListItem Value="1">&nbsp; Položeno</asp:ListItem>
                                        <asp:ListItem Value="0">&nbsp; Nepoloženo</asp:ListItem>
                                    </asp:RadioButtonList>
                                        </div>
                        </div>
                        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="dodajIzlazak" EventName="Click"/>
                                </Triggers>
                            <ContentTemplate>
                               <div id="addTest_Success" runat="server" class="form-group" visible="false">
                                    <label class="control-label"></label>
                                    <div class="col-lg-11 input-group">
                                        <div class="alert alert-success alert-dismissible">
                                            <b>Uspješno izvršeno!</b> Uspješno ste dodali izlazak na test.
                                        </div>
                                    </div>
                                </div>

                                <div id="addTest_Danger" runat="server" class="form-group" visible="false">
                                    <label class="control-label"></label>
                                    <div class="col-lg-11 input-group">
                                        <div id="divdanger" runat="server" class="alert alert-danger alert-dismissible">
                                            <b>Upozorenje!</b> Sva polja su obavezna!
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer clearfix">

                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Zatvori</button>
                            <asp:LinkButton ID="dodajIzlazak" runat="server" CssClass="btn btn-primary pull-left" OnClick="dodajIzlazak_Click"><i class="fa fa-plus-circle"></i>&nbsp;Dodaj</asp:LinkButton>
                            
                        </div>
                    </div>
                </div><!-- /.modal-content -->
            </div><!-- /.modal-dialog -->
        </div><!-- /.modal -->


    <script src="/js/jquery.js"></script> 

    <script src="/js/jquery.maskedinput.js" type="text/javascript"></script>
    
        <!-- jQuery Knob -->
        <script src="/js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(
            $("#<%=datumTxt.ClientID%>").mask("99/99/9999", { placeholder: "DD/MM/GGGG" }));
    </script>
            <!-- charts -->
        <script type="text/javascript">
            function pageLoad() {
                $(function () {
                    /* jQueryKnob */

                    $(".knob").knob({
                        /*change : function (value) {
                         //console.log("change : " + value);
                         },
                         release : function (value) {
                         console.log("release : " + value);
                         },
                         cancel : function () {
                         console.log("cancel : " + this.value);
                         },*/
                        draw: function () {
                            // "tron" case
                            if (this.$.data('skin') == 'tron') {
                                var a = this.angle(this.cv)  // Angle
                                        , sa = this.startAngle          // Previous start angle
                                        , sat = this.startAngle         // Start angle
                                        , ea                            // Previous end angle
                                        , eat = sat + a                 // End angle
                                        , r = true;

                                this.g.lineWidth = this.lineWidth;

                                this.o.cursor
                                        && (sat = eat - 0.3)
                                        && (eat = eat + 0.3);

                                if (this.o.displayPrevious) {
                                    ea = this.startAngle + this.angle(this.value);
                                    this.o.cursor
                                            && (sa = ea - 0.3)
                                            && (ea = ea + 0.3);
                                    this.g.beginPath();
                                    this.g.strokeStyle = this.previousColor;
                                    this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false);
                                    this.g.stroke();
                                }

                                this.g.beginPath();
                                this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
                                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sat, eat, false);
                                this.g.stroke();

                                this.g.lineWidth = 2;
                                this.g.beginPath();
                                this.g.strokeStyle = this.o.fgColor;
                                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth + 1 + this.lineWidth * 2 / 3, 0, 2 * Math.PI, false);
                                this.g.stroke();

                                return false;
                            }
                        }


                    });
                    /* END JQUERY KNOB */




                    $(".knobspecial").knob({
                        /*change : function (value) {
                         //console.log("change : " + value);
                         },
                         release : function (value) {
                         console.log("release : " + value);
                         },
                         cancel : function () {
                         console.log("cancel : " + this.value);
                         },*/
                        draw: function () {
                            $(this.i).val(this.cv + '%')
                            // "tron" case
                            if (this.$.data('skin') == 'tron') {

                                var a = this.angle(this.cv)  // Angle
                                        , sa = this.startAngle          // Previous start angle
                                        , sat = this.startAngle         // Start angle
                                        , ea                            // Previous end angle
                                        , eat = sat + a                 // End angle
                                        , r = true;

                                this.g.lineWidth = this.lineWidth;

                                this.o.cursor
                                        && (sat = eat - 0.3)
                                        && (eat = eat + 0.3);

                                if (this.o.displayPrevious) {
                                    ea = this.startAngle + this.angle(this.value);
                                    this.o.cursor
                                            && (sa = ea - 0.3)
                                            && (ea = ea + 0.3);
                                    this.g.beginPath();
                                    this.g.strokeStyle = this.previousColor;
                                    this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sa, ea, false);
                                    this.g.stroke();
                                }

                                this.g.beginPath();
                                this.g.strokeStyle = r ? this.o.fgColor : this.fgColor;
                                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth, sat, eat, false);
                                this.g.stroke();

                                this.g.lineWidth = 2;
                                this.g.beginPath();
                                this.g.strokeStyle = this.o.fgColor;
                                this.g.arc(this.xy, this.xy, this.radius - this.lineWidth + 1 + this.lineWidth * 2 / 3, 0, 2 * Math.PI, false);
                                this.g.stroke();

                                return false;
                            }
                        }


                    });
                    /* END JQUERY KNOB */

                });

            }
        </script>


</asp:Content>
