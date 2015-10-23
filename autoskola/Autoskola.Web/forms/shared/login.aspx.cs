using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoskola.Data;
using System.Web.Security;
using System.Windows.Forms;

namespace Autoskola.Web.forms.shared
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Prijava_Click(object sender, EventArgs e)
        {
            if (txt_KorisnickoIme.Text != "" && txt_Lozinka.Text != "")
            {
                Korisnici k = DAKorisnici.Login_Check(txt_KorisnickoIme.Text, txt_Lozinka.Text);

                if (k != null)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(k.KorisnikId.ToString(), false, 30);
                    string encryptTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie loginCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                    Response.Cookies.Add(loginCookie);

                    if (k.Kandidat != null)
                    {
                        Session.Add("kandidatID", k.Kandidat.KandidatId);
                        Session.Add("korisnikID", k.KorisnikId);
                        Session.Add("autoskolaID", k.Kandidat.AutoSkolaId);
                        Response.Redirect("/kandidat/naslovnica");
                    }
                    if (k.Instruktor != null)
                    {
                        Session.Add("instruktorID", k.Instruktor.InstruktorId);
                        Session.Add("korisnikID", k.KorisnikId);
                        Session.Add("autoskolaID", k.Instruktor.AutoSkolaId);
                        Response.Redirect("/instruktor/naslovnica");
                    }
                }
                else
                {
                    txt_KorisnickoIme.Text = "";
                    txt_Lozinka.Text = "";
                    CustomValidator err = new CustomValidator();
                    err.ValidationGroup = "UserUniqueness";
                    err.IsValid = false;
                    err.ErrorMessage = "The combination of email and password is not correct.";
                    Page.Validators.Add(err);
                    MessageBox.Show("E-mail i/ili lozinka su pogrešni.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}