<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="DodajPitanje.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.novaDodajPitanje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- Right side column. Contains the navbar and content of the page -->
    <aside class="right-side">
        <!-- Content Header (Page header) -->
        <section class="content-header">
             <h1 id="heading_top" runat="server">Dodavanje pitanja
            </h1>
        </section>
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Novo pitanje</h3>
                        </div>
                        <!-- /.box-header -->
                       
                        <!-- form start -->
                        <div role="form" class="novopitanje-form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Pitanje</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Pitanje_txt" runat="server" CssClass="form-control" placeholder="Pitanje"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Kategorija/e</label>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="addList" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="removeKat" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                       <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                         <ItemTemplate>
                                            <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                                <asp:DropDownList ID="kategorijeList" runat="server" CssClass="form-control special-ddl-tb"></asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                      <asp:Button ID="addList" runat="server" Text="+" CssClass="addBtn2 btn btn-primary" OnClick="addList_Click" />
                                    <asp:Button ID="removeKat" runat="server" Text="-" CssClass="removeBtn3 btn btn-default" OnClick="removeKat_Click" Visible="false"/>
                               </ContentTemplate>
                                    </asp:UpdatePanel> 
                                </div>

                                <div class="form-group form-topfix">
                                    <label for="MasterContent_Slike">Slika</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
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
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Odgovor1_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor1_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 2</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Odgovor2_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor2_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Odgovor 3</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Odgovor3_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor3_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 4</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Odgovor4_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor4_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Odgovor 5</label>
                                    <div class="input-group col-md-9 col-lg-9 col-sm-9 col-xs-12">
                                        <asp:TextBox ID="Odgovor5_txt" runat="server" CssClass="form-control" placeholder="Tekstualni oblik odgovora (nije obavezno polje)"></asp:TextBox>
                                        <label class="checkbox-inline">
                                            <asp:CheckBox ID="Odgovor5_checkbox" runat="server" Text="Tačno" />
                                        </label>
                                    </div>
                                </div>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Spasi_btn" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>

                                <div id="Success_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 input-group">
                                        <div class="alert alert-success alert-dismissible noMarginLeft">
                                            <b>Uspješno izvršeno!</b> Uspješno ste dodali novo pitanje.
                                        </div>
                                    </div>
                                </div>

                                <div id="Danger_div" runat="server" class="form-group" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 input-group">
                                        <div id="divdanger" runat="server" class="alert alert-danger alert-dismissible noMarginLeft">
                                            <b>Upozorenje!</b> Pitanje, minimalno jedna kategorija, minimalno jedan tačan i jedan netačan odgovor su obavezna polja!
                                        </div>
                                    </div>
                                </div>
                                
                         </ContentTemplate>
                        </asp:UpdatePanel>

                                <div class="box-footer">
                                        <asp:Button ID="Spasi_btn" CssClass="btn btn-primary" runat="server" style="margin-right:5px" Text="Dodaj" OnClick="Spasi_btn_Click" />
                                        <asp:Button ID="Reset_btn" runat="server" CssClass="btn btn-default" Text="Odustani" OnClick="Reset_btn_Click" />
                                    </div>

                            </div>
                            <!-- /.box-body -->
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
