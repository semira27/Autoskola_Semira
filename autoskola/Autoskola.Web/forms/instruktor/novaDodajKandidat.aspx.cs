using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaDodajKonadidat : System.Web.UI.Page
    {
        public int instruktorID
        {
            get
            {
                if (Convert.ToInt32(Session["instruktorID"]) > 0 && Session["instruktorID"] != null)
                    return (int)Session["instruktorID"];
                else
                    return 0;
            }
            set { Session["instruktorID"] = value; }
        }

        public int autoskolaID
        {
            get
            {
                if (Convert.ToInt32(Session["autoskolaID"]) > 0 && Session["autoskolaID"] != null)
                    return (int)Session["autoskolaID"];
                else
                    return 0;
            }
            set { Session["autoskolaID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0 && autoskolaID > 0)
            {
                if (!IsPostBack)
                {
                    Bind();
                }
            }
            else
                Response.Redirect("/prijava");
        }

        private void Bind()
        {
            gradovidropdown.DataSource = DAGradovi.SelectAll();
            gradovidropdown.DataTextField = "Naziv";
            gradovidropdown.DataValueField = "GradId";
            gradovidropdown.DataBind();

        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btn_Registracija_Click(object sender, EventArgs e)
        {
            if (autoskolaID > 0)
            {
                if (txtIme.Text.Count() > 0 && txtPrezime.Text.Count() > 0 && txtTelefon.Text.Count() > 0 && txtEmail.Text.Count() > 0 && IsValidEmail(txtEmail.Text) == true && txtKorisnickoIme.Text.Count() > 0 && txtLozinka.Text.Count() > 0 && gradovidropdown.SelectedIndex > 0)
                {
                    Korisnici k = new Korisnici();
                    Autoskola.Data.Kandidati ka = new Autoskola.Data.Kandidati();
                    string pattern = "dd/MM/yyyy";
                    DateTime dt;
                    if (DateTime.TryParseExact(txtDatumRodjenja.Text, pattern, CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out dt))
                    {
                        k.DatumRegistracije = DateTime.Now;
                        k.DatumRodjenja = dt;
                        k.Email = txtEmail.Text;
                        k.Ime = txtIme.Text;
                        k.Telefon = txtTelefon.Text;
                        k.JMBG = txtJMBG.Text;
                        k.Adresa = txtAdresa.Text;
                        k.Prezime = txtPrezime.Text;
                        k.KorisnickoIme = txtKorisnickoIme.Text;
                        k.LozinkaHash = Infrastructure.Encryption.Helper.GenerateHash(txtLozinka.Text);
                        k.GradId = Convert.ToInt32(gradovidropdown.SelectedValue);
                        k.Aktivan = 1;
                        ka.Korisnik = k;
                        ka.AutoSkolaId = autoskolaID;
                        DAKandidati.InsertKorisnik(k, ka);
                        Danger_div.Visible = false;
                        Success_div.Visible = true;
                    }
                    else
                    {
                        txtDatumRodjenja.Attributes.Add("style", "border: 1px solid #a94442");
                        Danger_div.Visible = true;
                        Success_div.Visible = false;
                    }
                }
                else
                {
                    if (txtIme.Text.Count() == 0)
                        txtIme.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtIme.Attributes.Add("style", "");

                    if (txtPrezime.Text.Count() == 0)
                        txtPrezime.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtIme.Attributes.Add("style", "");

                    if (txtTelefon.Text.Count() == 0)
                        txtTelefon.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtTelefon.Attributes.Add("style", "");

                    if (txtKorisnickoIme.Text.Count() == 0)
                        txtKorisnickoIme.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtKorisnickoIme.Attributes.Add("style", "");

                    if (txtLozinka.Text.Count() == 0)
                        txtLozinka.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtLozinka.Attributes.Add("style", "");

                    if (txtEmail.Text.Count() > 0 && IsValidEmail(txtEmail.Text) == true)
                        txtEmail.Attributes.Add("style", "");
                    else
                        txtEmail.Attributes.Add("style", "border: 1px solid #a94442");

                    if (txtDatumRodjenja.Text.Count() == 0)
                        txtDatumRodjenja.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        txtDatumRodjenja.Attributes.Add("style", "");

                    if (gradovidropdown.SelectedIndex == 0)
                        gradovidropdown.Attributes.Add("style", "border: 1px solid #a94442");
                    else
                        gradovidropdown.Attributes.Add("style", "");

                    Danger_div.Visible = true;
                    Success_div.Visible = false;
                } 
            }
        }

    }
}