<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="index.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Ionicons -->
    <link href="//code.ionicframework.com/ionicons/1.5.2/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Morris charts -->
    <link href="../../css/morris/morris.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="../../css/AdminLTE.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Naslovnica
                        <small>detalji</small>
            </h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:TextBox ID="txtSkripta" runat="server" Visible="false"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                 
                <div class="col-md-10">
                            <!-- Bar chart -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <i class="fa fa-bar-chart-o"></i>
                                    <h3 class="box-title">Godišnji grafički prikaz prijavljenih kategorija</h3>
                                       <asp:DropDownList ID="godineList" runat="server" CssClass="form-control customChartDdl" AutoPostBack="true" OnSelectedIndexChanged="godineList_SelectedIndexChanged"></asp:DropDownList>
                                 </div>
                                <div class="box-body">
                                    <div id="bar-chart" style="height: 300px;"></div>
                                </div><!-- /.box-body-->
                            </div><!-- /.box -->
                </div>

<%--                       <div class="col-md-10">
                            <!-- LINE CHART -->
                            <div class="box box-info">
                                <div class="box-header">
                                    <h3 class="box-title">Prikaz</h3>
                                 </div>
                                <div class="box-body chart-responsive">
                                    <div class="chart" id="line-chart" style="height: 300px;"></div>
                                </div><!-- /.box-body -->
                            </div><!-- /.box -->

                        </div><!-- /.col (LEFT) -->--%>

                    </div><!-- /.row -->

        </section>
        <!-- /.content -->
    </aside>
    <!-- /.right-side -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
        <!-- AdminLTE App -->
        <script src="../../js/AdminLTE/app.js" type="text/javascript"></script>
    
        <!-- FLOT CHARTS -->
        <script src="../../js/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
        <!-- FLOT RESIZE PLUGIN - allows the chart to redraw when the window is resized -->
        <script src="../../js/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
        <!-- FLOT CATEGORIES PLUGIN - Used to draw bar charts -->
        <script src="../../js/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/plugins/flot/jquery.flot.stack.js"></script>
    <script type="text/javascript" src="/js/plugins/flot/jquery.flot.barnumbers.js"></script>

        <!-- page script -->

    <script type="text/javascript">
        /*
       * BAR CHART
       * ---------
       */
        var graph_data = <%=txtSkripta.Text%>;
        var bar_data = {
            data: graph_data,
            color: "#3c8dbc"
        };
        $.plot("#bar-chart", [bar_data], {
            grid: {
                borderWidth: 1,
                borderColor: "#f3f3f3",
                tickColor: "#f3f3f3"
            },
            series: {
                bars: {
                    show: true,
                    barWidth: 0.5,
                    align: "center",
                    showNumbers: true,
                    numbers : {
                        xAlign: 80,
                    yAlign: function(y) { return y + 1; },
                    },
                }
            },
            xaxis: {
                mode: "categories",
                minTickSize: 1,
                tickLength: 0
            },
            yaxis: {
                minTickSize: 1,
                tickDecimals: 0
            }
        });
        /* END BAR CHART */
    </script>

</asp:Content>
