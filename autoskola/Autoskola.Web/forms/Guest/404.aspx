<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Autoskola.Web.Forms.Guest._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page content starts -->

<div class="content error-page">
  <div class="container">
    <div class="row">
      <div class="col-md-4 col-sm-6">  
        <div class="big-text">404</div>
        <hr />
      </div>
      <div class="col-md-7 col-md-offset-1 col-sm-6">
        <h2>Oops<span class="color">!!!</span></h2>
        <h4>Page Not Found</h4>
        <hr />
        <!-- Some links -->
        <div class="horizontal-links">
          <h5>Take a look around our site</h5>
          <a href="#">Home</a> <a href="#">About us</a> <a href="#">Support</a> <a href="#">Contact Us</a> <a href="#">Disclaimer</a>
        </div>
        <hr />
        <!-- Search form -->
            <form class="form-inline" role="form">
              <div class="form-group">
                <input type="email" class="form-control" id="search" placeholder="Type Something...">
              </div>
              <button type="submit" class="btn btn-default">Search</button>
            </form>
      </div>

    </div>
  </div>
</div>

<!-- Page content ends -->

</asp:Content>
