<%@ Page Title="" Language="C#" MasterPageFile="~/forms/instruktor/Instruktor.Master" AutoEventWireup="true" CodeBehind="kategorija.aspx.cs" Inherits="Autoskola.Web.forms.instruktor.kategorija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

       <!-- Right side column. Contains the navbar and content of the page -->
            <aside class="right-side">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1 id="heading_top" runat="server">
                        <small>pregled detalja</small>
                    </h1>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSpasi" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-header">
                                    <h3 class="box-title" runat="server" id="brojPitanja">Pitanja u testu: 23/40</h3>
                                </div><!-- /.box-header -->
                                <div class="box-body">
                        <asp:GridView ID="grid_VrstePitanja" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" PageSize="5" GridLines="None" AllowPaging="true" OnRowCommand="grid_VrstePitanja_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Vrsta pitanja">
                                <ItemTemplate>
                                    <%# Eval("Naziv") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Broj pitanja" DataField="Broj" />
                              <asp:TemplateField HeaderText="Obriši podatke">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="obrisiPitanje" runat="server" CommandArgument='<%# Eval("BrojPitanjaId") %>' CommandName="deleteCommand" CssClass="btn btn-xs btn-danger" OnClientClick="return confirm('Sigurno želite obrisati pitanja?');"> <i class="fa fa-times"></i> </asp:LinkButton>
                                            </ItemTemplate>
                              </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                                    <br />
                                    <div class="alert alert-warning alert-dismissable" runat="server" id="upozorenjeDiv">
                                        <i class="fa fa-warning"></i>
                                        <b>Upozorenje!</b> Nije moguće dodati više od maksimalnog broja pitanja.
                                    </div>

                                </div>
                            </div><!-- /.box -->

                        </div><!-- /.col -->
                            </ContentTemplate>
                        </asp:UpdatePanel>

<!-- right column -->
            <div class="col-md-6">
                <!-- general form elements disabled -->
                <div class="box box-primary">
                    <br />
                    <div role="form">
                        <div class="box-body">
                            <label for="tbIme">Vrsta pitanja</label>
                            <div class="input-group">
                                <asp:DropDownList ID="ddlVrstePitanja" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Morate upisati cijenu kursa"
                                ControlToValidate="ddlVrstePitanja" CssClass="callout callout-danger" ValidationGroup="sacuvaj"></asp:RequiredFieldValidator>
                            <!-- /.input group -->

                            <div class="form-group">
                                <label for="tbIme">Broj pitanja</label>
                                <div class="input-group">
                                <asp:DropDownList ID="ddlBrojPitanja" runat="server" CssClass="form-control">
                                       <asp:ListItem Value="- Odaberite broj -"></asp:ListItem>
                                                    <asp:ListItem Value="1"></asp:ListItem>
                                                    <asp:ListItem Value="2"></asp:ListItem>
                                                    <asp:ListItem Value="3"></asp:ListItem>
                                                    <asp:ListItem Value="4"></asp:ListItem>
                                                    <asp:ListItem Value="5"></asp:ListItem>
                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <asp:ListItem Value="8"></asp:ListItem>
                                                    <asp:ListItem Value="9"></asp:ListItem>
                                                    <asp:ListItem Value="10"></asp:ListItem>
                                                    <asp:ListItem Value="11"></asp:ListItem>
                                                    <asp:ListItem Value="12"></asp:ListItem>
                                                    <asp:ListItem Value="13"></asp:ListItem>
                                                    <asp:ListItem Value="14"></asp:ListItem>
                                                    <asp:ListItem Value="15"></asp:ListItem>
                                                    <asp:ListItem Value="16"></asp:ListItem>
                                                    <asp:ListItem Value="17"></asp:ListItem>
                                                    <asp:ListItem Value="18"></asp:ListItem>
                                                    <asp:ListItem Value="19"></asp:ListItem>
                                                    <asp:ListItem Value="20"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                                <!-- /.input group -->
                            </div>

                            <div class="form-group">
                                <asp:Button ID="btnSpasi" runat="server" Text="Dodaj" CssClass="btn btn-sm btn-primary" OnClick="btnSpasi_Click" />
                                <!-- /.input group -->
                            </div>
                            <!-- /.box-body -->
                        </div>

                    </div>
                </div>
                <!-- /.box -->
            </div>
            <!--/.col (right) -->
                    </div><!-- /.row -->
                   
                </section><!-- /.content -->
            </aside><!-- /.right-side -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="outsidewrapper" runat="server">
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
</asp:Content>
