<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Guest/Guest.Master" AutoEventWireup="true" CodeBehind="Kontakt.aspx.cs" Inherits="Autoskola.Web.Forms.Guest.Kontakt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
<!-- Page heading starts -->

<div class="page-head">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h2>Kontaktirajte nas</h2>
        <%--<h4>something goes here</h4>--%>
      </div>
    </div>
  </div>
</div>

<!-- Page Heading ends -->

<!-- Page content starts -->

<div class="content contact">
  <div class="container">
    <div class="row">
      <div class="col-md-12">  
                     <!-- Contact -->
                     
                              <!-- Google maps -->
                              <div class="gmap">
                                 <!-- Google Maps. Replace the below iframe with your Google Maps embed code -->
                                 <iframe height="300" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.co.in/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=Google+India+Bangalore,+Bennigana+Halli,+Bangalore,+Karnataka&amp;aq=0&amp;oq=google+&amp;sll=9.930582,78.12303&amp;sspn=0.192085,0.308647&amp;ie=UTF8&amp;hq=Google&amp;hnear=Bennigana+Halli,+Bangalore,+Bengaluru+Urban,+Karnataka&amp;t=m&amp;ll=12.993518,77.660294&amp;spn=0.012545,0.036006&amp;z=15&amp;output=embed"></iframe>
                              </div>
                              

                        <div class="bor"></div>
                        <div class="row">
                           <div class="col-md-6">
                              <div class="cwell">
                                 <!-- Contact form -->
                                    <h4 class="title">Kontakt forma</h4>
                                    <form role="form">
                                      <div class="form-group">
                                        <label for="name">Vaše ime</label>
                                          <asp:TextBox ID="txtIme" class="form-control" placeholder="Unesite ime" runat="server"></asp:TextBox>
                                     <%--   <input type="text" class="form-control" id="name" placeholder="Enter Name">--%>
                                      </div>                                    
                                      <div class="form-group">
                                        <label for="exampleInputEmail1">Email addresa</label>
                                          <asp:TextBox ID="txtEmail" class="form-control" placeholder="Unesite email adresu" runat="server"></asp:TextBox>

                                        <%--<input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">--%>
                                      </div>
                                      <div class="form-group">
                                        <label for="exampleInputEmail1">Poruka</label>
                                        <asp:TextBox ID="txtPoruka" class="form-control" placeholder="Napišite nešto ovdje..." TextMode="MultiLine" Rows="5"  runat="server"></asp:TextBox>

                                       <%-- <textarea class="form-control" rows="5"></textarea>--%>
                                      </div>                                      
                                      <%--<div class="checkbox">
                                        <label>
                                          <input type="checkbox" /> Important?
                                        </label>
                                      </div>--%>
                                     <%-- <button type="submit" class="btn btn-default">Send</button>
                                      <button type="reset" class="btn btn-default">Reset</button>--%>
                                        <asp:Button ID="btnPosalji" class="btn btn-default" runat="server" Text="Pošalji" />
                                        <asp:Button ID="btnPonist" class="btn btn-default" runat="server" Text="Poništi" />
                                        
                                    </form>
                                    <hr />
                                 </div>
                           </div>
                           <div class="col-md-6">
                                 <div class="cwell">
                                    <!-- Address section -->
                                       <h4 class="title">Address</h4>
                                       <div class="address">
                                           <address>
                                              <!-- Company name -->
                                              <strong>Responsive Web, Inc.</strong><br>
                                              <!-- Address -->
                                              795 Folsom Ave, Suite 600<br>
                                              San Francisco, CA 94107<br>
                                              <!-- Phone number -->
                                              <abbr title="Phone">P:</abbr> (123) 456-7890.
                                           </address>
                                            
                                           <address>
                                              <!-- Name -->
                                              <strong>Full Name</strong><br>
                                              <!-- Email -->
                                              <a href="mailto:#">first.last@gmail.com</a>
                                           </address>
                                           
                                           <!-- Social media icons -->
                                           <strong>Get in touch:</strong>
                                           <div class="social">
                                                <a href="#"><i class="fa fa-facebook facebook"></i></a>
                                                <a href="#"><i class="fa fa-twitter twitter"></i></a>
                                                <a href="#"><i class="fa fa-linkedin linkedin"></i></a>
                                                <a href="#"><i class="fa fa-google-plus google-plus"></i></a>
                                                <a href="#"><i class="fa fa-pinterest pinterest"></i></a>
                                           </div>
                                       </div>
                                       <hr />
                                 </div>
                           </div>
                        </div>
                        

      </div>
    </div>
  </div>
</div>

<!-- Page content ends -->


</asp:Content>
