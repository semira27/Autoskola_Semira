<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="Registracija.aspx.cs" Inherits="Autoskola.Web.Forms.Guest.Registracija" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page heading starts -->

<div class="page-head">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2>Register</h2>
          <h4>something goes here</h4>
      </div>
    </div>
  </div>
    </div>

    <!-- Page Heading ends -->

    <!-- Page content starts -->

    <div class="content">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <!-- Some content -->
                    <h3 class="title">Register Today <span class="color">!!!</span></h3>
                    <h4>Morbi tincidunt posuere turpis eu laoreet</h4>
                    <p>Nullam in est urna. In vitae adipiscing enim. Curabitur rhoncus condimentum lorem, non convallis dolor faucibus eget. In vitae adipiscing enim. Curabitur rhoncus condimentum lorem, non convallis dolor faucibus eget. In ut nulla est. </p>
                    <h5>Maecenas hendrerit neque id</h5>
                    <ul>
                        <li>Etiam adipiscing posuere justo, nec iaculis justo dictum non</li>
                        <li>Cras tincidunt mi non arcu hendrerit eleifend</li>
                        <li>Aenean ullamcorper justo tincidunt justo aliquet et lobortis diam faucibus</li>
                        <li>Maecenas hendrerit neque id ante dictum mattis</li>
                        <li>Vivamus non neque lacus, et cursus tortor</li>
                    </ul>

                    <p>Nullam in est urna. In vitae adipiscing enim. In ut nulla est. Nullam in est urna. In vitae adipiscing enim. Curabitur rhoncus condimentum lorem, non convallis dolor faucibus eget. In ut nulla est. </p>

                </div>

                <!-- Form -->
                <div class="col-md-6">
                    <div class="formy well">
                        <!-- Title -->
                        <h4 class="title">Novi korisnički račun</h4>
                        <p>&nbsp;</p>
                        <div class="form">
                            <!-- Register form (not working)-->

                            <div class="form-horizontal">
                            <div class="form-group">
                                <label for="inputName" class="col-lg-2 control-label">Ime i prezime</label>
                                <div class="col-lg-8">
                                    <asp:TextBox ID="txtIme" class="form-control" placeholder="Ime i prezime" runat="server"></asp:TextBox>
                                    <%--<input type="text" class="form-control" id="inputName" placeholder="Name" />--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputEmail1" class="col-lg-2 control-label">Email</label>
                                <div class="col-lg-8">
                                    <%--<input type="email" class="form-control" id="inputEmail1" placeholder="Email" />--%>
                                    <asp:TextBox ID="txtEmail" class="form-control" placeholder="Email adresa" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputPassword1" class="col-lg-2 control-label">Lozinka</label>
                                <div class="col-lg-8">
                                    <%--<input type="password" class="form-control" id="inputPassword1" placeholder="Password" />--%>
                                    <asp:TextBox ID="txtLozinka" class="form-control" placeholder="Lozinka" runat="server"></asp:TextBox>
                                
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="dropdown" class="col-lg-2 control-label">Dropdown</label>
                                <div class="col-lg-4">
                                    <select class="form-control">
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="inputComment" class="col-lg-2 control-label">Comment</label>
                                <div class="col-lg-8">
                                    <textarea class="form-control" rows="3"></textarea>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-8">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" />
                                            I Agree Terms & Conditions 
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-8">
                                    <button type="submit" class="btn btn-default">Register</button>
                                    <button type="reset" class="btn btn-default">Reset</button>
                                </div>
                            </div>
                            <hr />

                            Already have an Account? <a href="login.html">Login</a>
                        </div>
                            </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

<!-- Page content ends -->

</asp:Content>
