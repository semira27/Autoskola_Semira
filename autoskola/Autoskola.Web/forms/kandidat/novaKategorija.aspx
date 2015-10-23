<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="novaKategorija.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.novaKategorija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
         <!-- Right side column. Contains the navbar and content of the page -->
            <aside class="right-side">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1 id="headerKat" runat="server">
                        A
                        <small>prijavljena kategorija</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a id="headerInstruktor" data-toggle="modal" data-target="#compose-modal"  runat="server" href="/kandidat/instruktor">Instruktor: Ime i prezime</a></li>
                    </ol>
                </section>
                
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <!-- Main content -->
                <section class="content invoice">
                    
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box box-solid">
                                <div class="box-body">
                                    <div class="row">
                                        <div id="divCharts">
                                        <div class="col-lg-4 col-md-5 col-sm-5 col-xs-12 text-center" style="display: inline-block;float: none">
                                            <input id="spremnostChart" runat="server" type="text" class="knobspecial" value="30" data-width="210" data-height="210" data-fgColor="#f56954" data-readonly="true"/>
                                            <div class="knob-label" style="font-size:15px">Spremnost za izlazak na testove</div>
                                        </div><!-- ./col -->
                                       <div class="col-lg-8 col-md-7 col-xs-12 col-sm-7">
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div id="boxPripreme" runat="server" class="small-box bg-green">
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
                                <a href="#" class="small-box-footer boxBottom">
                                </a>
                            </div>
                        </div><!-- ./col -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div id="boxPolozeno" runat="server" class="small-box bg-red">
                                <div class="inner">
                                    <h3 id="imgPolozeno" runat="server">
                                        <i class='fa fa-exclamation-triangle'></i>
                                    </h3>
                                    <p id="txtPolozeno" runat="server">
                                       Testovi nisu položeni.
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-exclamation-triangle"></i>
                                </div>
                                <a href="#" class="small-box-footer boxBottom">
                                </a>
                            </div>
                        </div><!-- ./col -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h3>
                                        Učenje
                                    </h3>
                                    <p>
                                        Učenje teorijskog dijela vozačkog ispita
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-pencil"></i>
                                </div>
<asp:LinkButton ID="btn_ucenje" runat="server" class="small-box-footer" OnClick="btn_ucenje_Click" >Počni učenje! <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                               
                            </div>
                        </div><!-- ./col -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                            <!-- small box -->
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <h3>
                                        Priprema
                                    </h3>
                                    <p>
                                        Provjera znanja
                                    </p>
                                </div>
                                <div class="icon">
                                    <i class="fa fa-pencil"></i>
                                </div>
<asp:LinkButton ID="btn_provjera" runat="server" class="small-box-footer" OnClick="btn_provjera_Click" >Počni pripremu! <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

                            </div>
                        </div><!-- ./col -->
                                        </div>
                                        </div>
                                    </div><!-- /.row -->
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                        </div><!-- /.col -->
                    </div><!-- /.row -->

                    <div class="row">
                         <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <asp:TextBox ID="txtSkripta" runat="server" Visible="false"></asp:TextBox>
                            <!-- LINE CHART -->
                            <div class="box box-default">
                                <div class="box-header">
                                    <h3 class="box-title">Grafički prikaz uspjeha urađenih priprema</h3>
                                 </div>
                                <div class="box-body chart-responsive">
                                    <div class="chart" id="line-chart" style="height: 300px;"></div>
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                                </ContentTemplate>
                        </asp:UpdatePanel>
                         </div><!-- /.col (LEFT) -->
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                          <p class="lead">Urađene pripreme</p>
                            <span id="pripreme_empty" runat="server" visible="false" style="display:block">Kandidat nije uradio još niti jednu pripremu za testove.</span>
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                 <asp:GridView ID="uradjeniTestoviGrid" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" PageSize="5" GridLines="None" AllowPaging="true" OnRowDataBound="uradjeniTestoviGrid_RowDataBound" OnPageIndexChanging="uradjeniTestoviGrid_PageIndexChanging">
                                     <Columns>
                                         <asp:TemplateField HeaderStyle-Width="100" >
                                           <ItemTemplate>
                                              <asp:HyperLink ID="linkPripreme" CssClass="btn btn-default btn-sm" NavigateUrl='<%# string.Format("/kandidat/priprema?id={0}",Eval("UradjeniTestId"))%>' runat="server"><i class="fa fa-search"></i></asp:HyperLink>
                                           </ItemTemplate>
                                         </asp:TemplateField>
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
                    </div>
                    <br /> <br />
                     <div class="row">
                        <!-- glavni testovi -->
                        <div class="col-md-6 col-lg-6 col-sm-6 col-xs-12">
                          <p class="lead"><b>Izlazak na testove</b></p>
                            <span id="testovi_empty" runat="server" visible="false">Kandidat još nije izašao na testove.</span>
                        <div class="table-responsive">
                        <asp:GridView ID="glavniTestoviGrid" runat="server" ShowHeader="false" CssClass="table" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="glavniTestoviGrid_RowDataBound">
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
                                        <td id="countPripreme" runat="server">0</td>
                                    </tr>
                                    <tr>
                                        <th>Spremnost za testove:</th>
                                        <td id="spremnostiTestovi" runat="server">0%</td>
                                    </tr>
                                </table>
                            </div>
                        </div><!-- /.col -->

                    </div><!-- /.row -->
                    
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

        <!-- FLOT CHARTS -->
        <script src="../../js/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
        <!-- FLOT RESIZE PLUGIN - allows the chart to redraw when the window is resized -->
        <script src="../../js/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
        <!-- FLOT CATEGORIES PLUGIN - Used to draw bar charts -->
        <script src="../../js/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    
    <script src="/js/jquery.js"></script> 

    <script src="/js/jquery.maskedinput.js" type="text/javascript"></script>
    
        <!-- jQuery Knob -->
        <script src="../../js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>

            <!-- charts -->
            <!-- page script -->

    <script type="text/javascript">
        //Morris chart
        var graph_data = <%=txtSkripta.Text%>;

        var shoreham_g = Morris.Line({
            element: 'line-chart',
            data: graph_data,
            yLabelFormat: function (y) { return y + "%"; },
            axes: 'y',
            xkey: 'y',
            ykeys: ['Uspjeh'],
            labels: ['Uspjeh'],
            gridEnabled: false
        });

        var pusher = new Pusher('xxxx');
        var channel_weather = pusher.subscribe('weather-channel');

        channel_weather.bind('new_shoreham_event', function(data){
            graph_data.push({
                time: data.observation_time,
                value: data.windspeedMiles
            });

                shoreham_g.setData(graph_data);
        });
    </script>
        <script type="text/javascript">
            function pageLoad() {
                $(function () {
                    /* jQueryKnob */
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
