<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="dodajPrijavu.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.dodajPrijavu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
      <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
             <h1 id="heading_top" runat="server">
                 Prijave <small>dodavanje prijava</small>
            </h1>
        </section>
        
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-7 col-lg-6 col-sm-8 col-xs-10">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Nova prijava</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="Spasi_btn" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                        <div role="form" class="novopitanje-form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Datum</label>
                                    <div class="input-group col-lg-9 col-md-9 col-xs-12 col-sm-6">
                                        <asp:TextBox ID="txtDatum" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Kandidat</label>
                                    <div class="input-group col-lg-9 col-md-9 col-xs-12 col-sm-6">
                                        <asp:DropDownList id="kandidatList" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label>Instruktor</label>
                                    <div class="input-group col-lg-9 col-md-9 col-xs-12 col-sm-6">
                                        <asp:DropDownList id="instruktorList" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Kategorija/e</label>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="addList" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                            <ItemTemplate>
                                            <div class="input-group col-lg-9 col-md-9 col-xs-12 col-sm-6">
                                                <asp:DropDownList ID="kategorijeList" runat="server" CssClass="form-control special-ddl-tb"></asp:DropDownList>
                                            </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                      <asp:Button ID="addList" runat="server" Text="+" CssClass="addBtn2 btn btn-primary" OnClick="addList_Click"/>
                                       <asp:Button ID="removeKat" runat="server" Text="-" CssClass="removeBtn3 btn btn-default" OnClick="removeKat_Click"/>
                                       
                                        </ContentTemplate>
                                    </asp:UpdatePanel>    
                                </div>
                                <br /> <br />

                                <div id="Success_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 input-group">
                                        <div class="alert alert-success alert-dismissible noMarginLeft">
                                          <b>Uspješno izvršeno!</b> Uspješno ste dodali novu prijavu.
                                        </div>
                                    </div>
                                </div>

                                <div id="Danger_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 input-group">
                                        <div id="divdanger" runat="server" class="alert alert-danger alert-dismissible noMarginLeft">
                                           <b>Upozorenje!</b> Svako polje je obavezno!
                                        </div>
                                    </div>
                                </div>


                                <div class="box-footer">
                                        <asp:Button ID="Spasi_btn" CssClass="btn btn-sm btn-primary" style="margin-right:5px" runat="server" Text="Dodaj" OnClick="Spasi_btn_Click" />
                                        <asp:Button ID="Reset_btn" runat="server" CssClass="btn btn-sm btn-default" Text="Odustani" OnClick="Reset_btn_Click"/>
                                    </div>

                            </div>
                            <!-- /.box-body -->
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- /.box -->


                    <!-- Form Element sizes -->





                </div>
                <!--/.col (left) -->
                <!-- right column -->
                <div class="col-md-6">
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
    <script src="/js/jquery.js"></script> 

    <script src="/js/jquery.maskedinput.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(
            $("#<%=txtDatum.ClientID%>").mask("99/99/9999", { placeholder: "DD/MM/GGGG" }));
    </script>
</asp:Content>
