<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="novaIzmjenaPitanja.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaIzmjenaPitanja" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Ionicons -->
        <link href="//code.ionicframework.com/ionicons/1.5.2/css/ionicons.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="/css/AdminLTE.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <h1>Izmjena pitanja
            </h1>
        </section>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6 col-lg-7 col-sm-7 col-xs-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Izmjena pitanja</h3>
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div role="form" class="novopitanje-form">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Spasi_btn" EventName="Click" />
                            </Triggers>
                                <ContentTemplate>
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Pitanje</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Pitanje_txt" runat="server" CssClass="form-control" placeholder="Pitanje"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label>Kategorija/e</label>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                                <ItemTemplate>
                                            <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                                <asp:DropDownList ID="kategorijeList" runat="server" CssClass="form-control special-ddl-tb"></asp:DropDownList>
                                                <asp:Button ID="removeKat" runat="server" Text="-" CssClass="addBtn removeBtn btn btn-default" OnClick="removeKat_Click"/>
                                            </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                      <asp:Button ID="addList" runat="server" Text="+" CssClass="addBtn2 btn btn-primary" OnClick="addList_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="form-group form-topfix">
                                    <label for="MasterContent_Slike">Slika</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <div class="sky-form">
                                            <label for="file" class="input input-file">
                                                <div class="button">
                                                    <asp:FileUpload ID="Slike" runat="server" onchange="document.getElementById('broj').value = 'Odabrana ' + body_Slike.files.length + ' slika.'" AllowMultiple="false" />
                                                    Odaberi sliku
                                                </div>
                                                <input type="text" readonly="true" id="broj" value="Možete odabrati samo jednu sliku." />
                                            </label>
                                        </div>
                                    </div>
                                </div>

                                   <div class="form-group">
                                    <label>Odgovor 1</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Odgovor1_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor1_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 2</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Odgovor2_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor2_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Odgovor 3</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Odgovor3_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor3_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 4</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Odgovor4_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor4_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 5</label>
                                    <div class="input-group col-lg-9 col-md-10 col-sm-10 col-xs-10">
                                        <asp:TextBox ID="Odgovor5_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor5_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                <div id="Success_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 input-group">
                                        <div class="alert alert-success noMarginLeft">
                                            <b>Uspješno izvršeno!</b> Uspješno ste izmijenili pitanje.
                                        </div>
                                    </div>
                                </div>

                                <div id="Danger_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 input-group">
                                        <div id="dangerdiv" runat="server" class="alert alert-danger noMarginLeft">
                                            <b>Upozorenje!</b> Pitanje, minimalno jedna kategorija, minimalno jedan tačan i jedan netačan odgovor su obavezna polja!
                                        </div>
                                    </div>
                                </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div class="box-footer">
                                        <asp:Button ID="Spasi_btn" CssClass="btn btn-primary" runat="server" Text="Spasi" style="margin-right:10px" OnClick="Spasi_btn_Click" />
                                        <a href="/instruktor/naslovnica" class="btn btn-default">Odustani</a>
                                </div>

                            </div>
                            <!-- /.box-body -->
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
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
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
</asp:Content>