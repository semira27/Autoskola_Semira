<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="ucenje.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.ucenje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
  <aside class="right-side">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <!-- left column -->
                        <div class="col-md-8">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                            <!-- general form elements -->
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 id="txt_pitanje" runat="server" class="box-title">Pitanje</h3>
                                </div><!-- /.box-header -->
                                <!-- form start -->
                                <div role="form">
                                    <div class="box-body" style="margin-top:20px">
                                         <div class="form-group">
                                            <label id="lbl_tacan1" runat="server" class="control-label" for="lbl_odgovor1" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor1" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                         <div class="form-group has-success">
                                            <label id="lbl_tacan2" runat="server" class="control-label" for="lbl_odgovor2"><i class="fa fa-check"></i> Tačan odgovor</label>                                          
                                              <asp:Label ID="lbl_odgovor2" runat="server" CssClass="form-control" Text="gdgdg"></asp:Label>
                                        </div>
                                         <div class="form-group">
                                            <label id="lbl_tacan3" runat="server" class="control-label" for="lbl_odgovor3" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor3" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label id="lbl_tacan4" runat="server" class="control-label" for="lbl_odgovor4" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor4" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label id="lbl_tacan5" runat="server" class="control-label" for="lbl_odgovor5" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor5" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                       
                                    </div><!-- /.box-body -->

                                    <div class="box-footer">
                                        <button type="submit" class="btn btn-primary">Submit</button>
                                    </div>
                                </div>
                            </div><!-- /.box -->
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        


                        </div><!--/.col (left) -->
                        <!-- right column -->
                        <div class="col-md-2">
                        </div><!--/.col (right) -->
                    </div>   <!-- /.row -->
                </section><!-- /.content -->
            </aside><!-- /.right-side -->
</asp:Content>
