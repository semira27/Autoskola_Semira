<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Autoskola.Web.Forms.Guest.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
<!-- Page heading starts -->

<div class="page-head">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2>Login</h2>
      <%--  <h4>something goes here</h4>--%>
      </div>
    </div>
  </div>
</div>

<!-- Page Heading ends -->

<!-- Page content starts -->

<div class="content">
  <div class="container">
    <div class="row">
     

                <!-- Login form -->
                <div class="col-md-6">
                  <div class="formy well">
                    <!-- Title -->
                     <h4 class="title">Login to Your Account</h4>
					 <p>&nbsp;</p>
                                  <div class="form">

                                      <!-- Login form (not working)-->
                                      
                                      <div class="form-horizontal" >
                                         <div class="form-group">
                                           <label for="inputEmail1" class="col-lg-2 control-label">Email</label>
                                           <div class="col-lg-8">
                                             <%--<input type="email" class="form-control" id="inputEmail1" placeholder="Email"/>--%>
                                               <asp:TextBox ID="txtEmail" class="form-control" placeholder="Email" runat="server"></asp:TextBox>

                                           </div>
                                         </div>
                                         <div class="form-group">
                                           <label for="inputPassword1" class="col-lg-2 control-label">Lozinka</label>
                                           <div class="col-lg-8">
                                            <%-- <input type="password" class="form-control" id="inputPassword1" placeholder="Password"/>--%>
                                               <asp:TextBox ID="txtLozinka" class="form-control" placeholder="Lozinka" runat="server"></asp:TextBox>
                                           </div>
                                         </div>
                                         <div class="form-group">
                                           <div class="col-lg-offset-2 col-lg-8">
                                             <div class="checkbox">
                                               <label>
                                                 <%--<input type="checkbox"/> Remember me--%>
                                                   <asp:CheckBox ID="CheckBox1" runat="server" /> Remember me
                                               </label>
                                             </div>
                                           </div>
                                         </div>
                                         <div class="form-group">
                                           <div class="col-lg-offset-2 col-lg-10">
                                             <%--<button type="submit" class="btn btn-default">Sign in</button>
                                             <button type="reset" class="btn btn-default">Reset</button>--%>
                                                <asp:Button ID="btnPrijaviSe" class="btn btn-default" runat="server" Text="Prijavi se" />
                                        <asp:Button ID="btnPonist" class="btn btn-default" runat="server" Text="Poništi" />
                                           </div>
                                         </div>
                                       </div>
                                      
                                      <hr />

                                      <h5>Novi korisnički račun?</h5>
                                      <!-- Register link -->
                                             Nemate korisnički račun? <a href="Registracija.aspx">Registracija</a>
                                    </div> 
                                  </div>

                </div>
    </div>
  </div>
</div>

<!-- Page content ends -->


</asp:Content>
