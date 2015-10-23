<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="priprema.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.priprema" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1 id="heading_top" runat="server">
                        Pitanja i odgovori urađene pripreme
                     <small>01.01.2015</small>
            </h1>
                    <ol class="breadcrumb">
                        <li id="uspjehHeader" runat="server">0%</li>
                    </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                   
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form">
                            <div class="box-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>     
                                        <asp:GridView ID="Pitanja_Grid" class="table table-striped table-bordered table-hover"  ShowHeader="false" runat="server" AutoGenerateColumns="false" PageSize="10" GridLines="None" AllowPaging="true" OnPageIndexChanging="Pitanja_Grid_PageIndexChanging" OnRowCommand="Pitanja_Grid_RowCommand" OnRowDataBound="Pitanja_Grid_RowDataBound" >
                                    <Columns>
                                        <asp:BoundField HeaderText="Pitanje" DataField="Pitanje" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a id="statusPitanjeOdg" runat="server" class="btn btn-xs btn-danger"><i class="fa fa-times"></i> </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pregled">
                                            <ItemTemplate>
                                                       <asp:LinkButton ID="btnTestiranje" runat="server" CssClass="btn btn-default" CommandArgument='<%# Eval("PitanjeId") %>' CommandName="viewPitanje"><i class="fa fa-search"></i></asp:LinkButton>
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign = "Left" CssClass = "GridPager" />
                                </asp:GridView>
                             </ContentTemplate>
                                </asp:UpdatePanel>
                           </div>
                            <!-- /.box-body -->
                        </div>
               
                    <!-- /.box -->
                    <!-- Form Element sizes -->
                </div>
                <!--/.col (left) -->
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </aside>
    <!-- /.right-side -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
            <!-- COMPOSE MESSAGE MODAL -->
        <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="txt_pitanje" runat="server" class="modal-title">Pitanje</h4>
                    </div>
                    <div>
                        <div class="modal-body">
                            <!-- glavni dio postavke -->
                                 <div class="box-body">
                                <table style="width:100%">
                                    <tr>
                                        <td>   
                                            <asp:Image ID="imageQuestion" runat="server" CssClass="imgQuestion" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg1" runat="server">
                                            <label id="lbl_tacan1" runat="server" class="control-label tacan" for="lbl_odgovor1" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor1" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                        <td id="td_Odabrano1" runat="server" class="tdOdabranoCorr" visible="false">
                                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg2" runat="server">
                                              <label id="lbl_tacan2" runat="server" class="control-label tacan" for="lbl_odgovor2" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>                                          
                                              <asp:Label ID="lbl_odgovor2" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                        <td id="td_Odabrano2" runat="server" class="tdOdabranoCorr" visible="false">
                                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg3" runat="server">
                                             <label id="lbl_tacan3" runat="server" class="control-label tacan" for="lbl_odgovor3" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor3" runat="server" CssClass="form-control"></asp:Label>
                                         </td>
                                        <td id="td_Odabrano3" runat="server" class="tdOdabranoCorr" visible="false">
                                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg4" runat="server">
                                             <label id="lbl_tacan4" runat="server" class="control-label tacan" for="lbl_odgovor4" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor4" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                        <td id="td_Odabrano4" runat="server" class="tdOdabranoCorr" visible="false">
                                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg5" runat="server">
                                             <label id="lbl_tacan5" runat="server" class="control-label tacan" for="lbl_odgovor5" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor5" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                        <td id="td_Odabrano5" runat="server" class="tdOdabranoCorr" visible="false">
                                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                     </div>
                        </div>

                        <div class="modal-footer clearfix">
                            <div class="obj">
                            <a class="btn btn-xs btn-default odabranoCorr"><i class="fa fa-check"></i></a> - odabrani odgovor/i
                            </div>
                            <button type="button" class="btn btn-danger" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Zatvori</button>
                         </div>
                    </div>
                </div><!-- /.modal-content -->
                    </ContentTemplate>
                    </asp:UpdatePanel>
            </div><!-- /.modal-dialog -->
        </div><!-- /.modal -->

    <script type="text/javascript">
        var broj = 0;
        function Test(broj) {
            if (broj == 1)
            {
                $('#compose-modal').modal('show');
            }
        }
    </script>

</asp:Content>
