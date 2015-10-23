<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="provjeraRezultat.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.provjeraRezultat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
       
      <aside class="right-side">
                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                        </div>
                        <!-- left column -->
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                            <!-- glavni dio postavke -->
                            <div class="box box-primary" >
                                <div class="box-body" style="min-height:577px; padding-bottom:20px">
                                     <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 text-center">
                                     <canvas id="canvas" runat="server" height='160'></canvas>
                                     <div id="errFailed" runat="server">
                                         <label class='col-lg-12 col-md-12 col-sm-12 col-xs-12 failedLbl'></label>
                                         <img class="col-lg-offset-6 col-md-offset-4 col-sm-offset-0 col-xs-offset-0" src='../../img/error-icon.png' />
                                         <label class='col-lg-12 col-md-12 col-sm-12 col-xs-12 failedLbl'></label>
                                     </div>
                                     </div>
                                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 text-center" style="display: inline-block;float: none">
                                            <input id="uspjehChart" runat="server" type="text" class="knobspecial" value="30" data-width="210" data-height="210" data-fgColor="#00a65a" data-readonly="true"/>
                                            <div class="knob-label" style="font-size:15px">Uspjeh</div>
                                        </div><!-- ./col -->
                                     <br /><br />
                                     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="box">
                                <div class="box-header">
                                    <h3 class="box-title">Detalji pripreme</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                                    <table class="table table-bordered">
                                        <tr>
                                            <td>Početak pripreme:</td>
                                            <td id="pocetakPriprema" runat="server">12.10.2015 00:00</td>
                                        </tr>
                                        <tr>
                                            <td>Kraj testa:</td>
                                            <td id="krajPriprema" runat="server">12.10.2015 00:00</td>
                                        </tr>
                                        <tr>
                                            <td>Ukupan broj pitanja:</td>
                                            <td id="ukupanBrPitanja" runat="server">0</td>
                                        </tr>
                                        <tr>
                                            <td>Maksimalan broj bodova:</td>
                                            <td id="maxBrBodova" runat="server">0</td>
                                        </tr>
                                        <tr>
                                            <td>Tačno odgovorenih pitanja:</td>
                                            <td id="brTacnihOdg" runat="server">50</td>
                                        </tr>
                                        <tr>
                                            <td>Osvojeni broj bodova:</td>
                                            <td id="brOsvojenihBodova" runat="server">50</td>
                                        </tr>
                                        <tr>
                                            <td>Uspjeh:</td>
                                            <td id="uspjehTable" runat="server">0%</td>
                                        </tr>
                                    </table>
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->
                                     </div>
                                </div>
                                <div  class="box-footer">
                                        <asp:Button ID="btnClose" runat="server" Text="Zatvori" CssClass="btn btn-default" OnClick="btnClose_Click" />
                                 </div>
                            </div><!-- kraj postavke -->
                                    
                        </div><!--/.col (left) -->
                        <!-- right column -->
                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                        </div><!--/.col (right) -->
                    </div>   <!-- /.row -->
                </section><!-- /.content -->
            </aside><!-- /.right-side -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
    <script src="/js/jquery.js"></script> 
    <script src="/js/jquery.maskedinput.js" type="text/javascript"></script>
    <!-- jQuery Knob -->
    <script src="../../js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>

        <script type="text/javascript">
                $(function () {
                    /*check*/
                    var start = 100;
                    var mid = 145;
                    var end = 250;
                    var width = 20;
                    var leftX = start;
                    var leftY = start;
                    var rightX = mid - (width / 2.7);
                    var rightY = mid + (width / 2.7);
                    var animationSpeed = 20;

                    var ctx = document.getElementsByTagName('canvas')[0].getContext('2d');
                    ctx.lineWidth = width;
                    ctx.strokeStyle = '#00a65a';

                    for (i = start; i < mid; i++) {
                        var drawLeft = window.setTimeout(function () {
                            ctx.beginPath();
                            ctx.moveTo(start, start);
                            ctx.lineTo(leftX, leftY);
                            ctx.stroke();
                            leftX++;
                            leftY++;
                        }, 1 + (i * animationSpeed) / 3);
                    }

                    for (i = mid; i < end; i++) {
                        var drawRight = window.setTimeout(function () {
                            ctx.beginPath();
                            ctx.moveTo(leftX, leftY);
                            ctx.lineTo(rightX, rightY);
                            ctx.stroke();
                            rightX++;
                            rightY--;
                        }, 1 + (i * animationSpeed) / 3);
                    }

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

        </script>
</asp:Content>
