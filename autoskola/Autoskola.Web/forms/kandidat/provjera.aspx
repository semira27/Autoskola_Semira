<%@ Page Title="" Language="C#" MasterPageFile="~/forms/kandidat/Kandidat.Master" AutoEventWireup="true" CodeBehind="provjera.aspx.cs" Inherits="Autoskola.Web.forms.kandidat.TestiranjeFrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function reload_js(src) {
            $('script[src="' + src + '"]').remove();
            $('<script>').attr('src', src).appendTo('head');
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
      <aside class="right-side">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                        </div>
                        <!-- left column -->
                        <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                            <!-- glavni dio postavke -->
                            <div class="box box-primary" >
                                 <div class="box-body" style="padding-bottom:20px">
                                <table id="tabela" runat="server" style="width:100%">
                                    <tr>
                                        <td class="box-header">
                                            <h3 id="txt_pitanje" runat="server" class="box-title" style="padding-bottom:20px">Ovo je pitanje.</h3>
                                        </td>
                                    </tr>
                                    <tr id="row_Image">
                                        <td>   
                                            <asp:Image ID="imageQuestion" runat="server" CssClass="imgQuestion imgQ" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg1" runat="server" class="padding10">
                                            <asp:RadioButton ID="radio_Odg1" runat="server" Text=" Odgovor 1" CssClass="checkRadio" AutoPostBack="true" Visible="false" OnCheckedChanged="radio_Odg1_CheckedChanged"/>
                                            <asp:CheckBox ID="check_Odg1" runat="server" Text="Odgovor 1" CssClass="checkAnswer" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="td_Odg2" runat="server" class="padding10">
                                            <asp:RadioButton ID="radio_Odg2" runat="server" CssClass="radioAnswer" Text=" Odgovor 2" AutoPostBack="true" OnCheckedChanged="radio_Odg2_CheckedChanged" Visible="false" />
                                            <asp:CheckBox ID="check_Odg2" runat="server" Text="Odgovor 2" CssClass="checkAnswer" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr id="row_Odg3" runat="server">
                                        <td id="td_Odg3" runat="server" class="padding10">
                                            <asp:CheckBox ID="check_Odg3" runat="server" Text="Odgovor 3" CssClass="checkAnswer" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr id="row_Odg4" runat="server">
                                        <td id="td_Odg4" runat="server" class="padding10">
                                            <asp:CheckBox ID="check_Odg4" runat="server" Text="Odgovor 4" CssClass="checkAnswer" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr id="row_Odg5" runat="server">
                                        <td id="td_Odg5" runat="server" class="padding10">
                                            <asp:CheckBox ID="check_Odg5" runat="server" Text="Odgovor 5" CssClass="checkAnswer" Visible="false" />
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
                                        <asp:Button ID="btn_End" runat="server" Text="Završi" CssClass="btn btn-danger" Visible="false" OnClick="btn_End_Click"  />
                                 </div>
                            </div><!-- kraj postavke -->
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div><!--/.col (left) -->
                        <!-- right column -->
                        <div class="col-lg-2 col-md-2 col-sm-0 col-xs-0">
                        </div><!--/.col (right) -->
                    </div>   <!-- /.row -->
                </section><!-- /.content -->
            </aside><!-- /.right-side -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
    <script>
        
        function pageLoad() {
            $('#<%=radio_Odg1.ClientID%>').button();
        }
    </script>

</asp:Content>
