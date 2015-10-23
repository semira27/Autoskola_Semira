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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    
                            <!-- glavni dio postavke -->
                            <div class="box box-primary" >
                                 <div class="box-body">
                                <table  style="width:100%">
                                    <tr>
                                        <td class="box-header">
                                            <h3 id="txt_pitanje" runat="server" class="box-title">Ovo je pitanje, ovo je pitanje. Ovo je pitanje, ovo je pitanje. Ovo je pitanje, ovo je pitanje. Ovo je pitanje, ovo je pitanje. Ovo je pitanje, ovo je pitanje. </h3>
                                        </td>
                                    </tr>
                                    <tr id="Image_row" runat="server">
                                        <td>   
                                            <asp:Image ID="imageQuestion" runat="server" CssClass="imgQuestion" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg1" runat="server">
                                            <label id="lbl_tacan1" runat="server" class="control-label tacan" for="lbl_odgovor1" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor1" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg2" runat="server">
                                              <label id="lbl_tacan2" runat="server" class="control-label tacan" for="lbl_odgovor2" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>                                          
                                              <asp:Label ID="lbl_odgovor2" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Odg3_Row" runat="server">
                                        <td id="td_Odg3" runat="server">
                                             <label id="lbl_tacan3" runat="server" class="control-label tacan" for="lbl_odgovor3" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor3" runat="server" CssClass="form-control"></asp:Label>
                                         </td>
                                    </tr>
                                    <tr id="Odg4_Row" runat="server">
                                        <td id="td_Odg4" runat="server">
                                             <label id="lbl_tacan4" runat="server" class="control-label tacan" for="lbl_odgovor4" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor4" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="Odg5_Row" runat="server">
                                        <td id="td_Odg5" runat="server">
                                             <label id="lbl_tacan5" runat="server" class="control-label tacan" for="lbl_odgovor5" visible="false"><i class="fa fa-check"></i> Tačan odgovor</label>
                                             <asp:Label ID="lbl_odgovor5" runat="server" CssClass="form-control"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                     </div>
                                <div  class="box-footer">
                                        <asp:Button ID="btn_Next" runat="server" Text="Sljedeće pitanje" CssClass="btn btn-primary" OnClick="btn_Next_Click" />
                                        <asp:Button ID="btn_End" runat="server" Text="Završi" CssClass="btn btn-danger right" OnClick="btn_End_Click" />
                                 </div>
                            </div><!-- kraj postavke -->
                                    
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
