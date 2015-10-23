using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.kandidat
{
    public partial class profil : System.Web.UI.Page
    {
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
        public List<int> tempKategorije
        {
            get
            {
                return (List<int>)ViewState["tempKategorije"];
            }
            set
            {
                ViewState["tempKategorije"] = value;
            }
        }

        public int kandidatID
        {
            get
            {
                if (Convert.ToInt32(Session["kandidatID"]) > 0 && Session["kandidatID"] != null)
                    return (int)Session["kandidatID"];
                else
                    return 0;
            }
            set { Session["kandidatID"] = value; }
        }


        public Korisnici kandidatPregled
        {
            get
            {
                return (Korisnici)Session["kandidatPregled"] ?? new Korisnici();
            }
            set
            {
                Session["kandidatPregled"] = value;
            }
        }

        public List<PolaganjeTestova> uradjeniTestovi
        {
            get
            {
                return (List<PolaganjeTestova>)Session["uradjeniTestovi"] ?? new List<PolaganjeTestova>();
            }
            set
            {
                Session["uradjeniTestovi"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && kandidatID > 0 && autoskolaID > 0)
            {
                if(!IsPostBack)
                {
                        try
                        {
                            kandidatID = Convert.ToInt32(kandidatID);
                            kandidatPregled = DAKandidati.SelectKorisnikByKandidatId(kandidatID);
                            if (kandidatPregled != null)
                            {
                                brPrijavljenihKat.InnerText = DAKandidati.CountPrijavljeneKategorije(kandidatID).ToString();
                                brUradjenihPriprema.InnerText = DAKandidati.CountUradjenePripreme(kandidatID).ToString();
                                brPolozenihKat.InnerText = DAKandidati.CountPolozeneKategorije(kandidatID).ToString();
                                BindForm();
                                BindPrijave();
                                BindTestove();
                            }
                            else
                                Response.Redirect("/prijava");
                        }
                        catch (Exception)
                        {
                            Response.Redirect("/kandidat/404");
                        }

                }
             }
             else
                    Response.Redirect("/prijava");

        }

       private void BindForm()
        {
            if (kandidatPregled != null)
            {
                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + kandidatPregled.Ime + " " + kandidatPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja kandidata</small>";
                td_ImePrezime.InnerText = kandidatPregled.Ime + " " + kandidatPregled.Prezime;
                td_JMBG.InnerText = kandidatPregled.JMBG;
                td_Adresa.InnerText = kandidatPregled.Adresa;
                td_Grad.InnerText = kandidatPregled.Grad.Naziv;
                td_Telefon.InnerText = kandidatPregled.Telefon;
                td_DatumRodjenja.InnerText = kandidatPregled.DatumRodjenja.ToShortDateString();
                td_Email.InnerText = kandidatPregled.Email;
                td_KorisnickoIme.InnerText = kandidatPregled.KorisnickoIme;
                td_LozinkaMain.InnerText = "Datum registracije";
                td_Lozinka.InnerText = kandidatPregled.DatumRegistracije.ToShortDateString();
            }
        }

        private void BindPrijave() 
        {
            List<Prijave> temp = DAPrijave.SelectByKandidatId_AktivneZavrsene(kandidatID);
            if (temp.Count() > 0)
            {
                noPrijaveMsg.Visible = false;
                prijave_Grid.Visible = true;
                prijave_Grid.DataSource = temp;
                prijave_Grid.DataBind();
                UpdatePanel3.Update();
            }
            else
            {
                noPrijaveMsg.Visible = true;
                prijave_Grid.Visible = false;
                UpdatePanel3.Update();
            }
        }

        private void BindTestove()
        {
            if (kandidatID > 0)
            {
                uradjeniTestovi = DAPolaganjeTestova.SelectByKandidatID(kandidatID);

                if (uradjeniTestovi.Count > 0)
                {
                    emptyIzlasci.Visible = false;
                    izlasciTestoviGrid.Visible = true;
                    izlasciTestoviGrid.DataSource = uradjeniTestovi;
                    izlasciTestoviGrid.DataBind();
                }
                else
                {
                    emptyIzlasci.Visible = true;
                    izlasciTestoviGrid.Visible = false;
                }
            }
        }

        private void BindGradovi()
        {
            listGradovi.DataSource = DAGradovi.SelectAll();
            listGradovi.DataTextField = "Naziv";
            listGradovi.DataValueField = "GradId";
            listGradovi.DataBind();
        }

        protected void urediKandidata_Click(object sender, EventArgs e)
        {
            if (td_ImePrezime.Visible == true)
            {
                urediKandidata.Text = "Spasi";

                td_ImePrezime.Visible = false;
                td_ImePrezime_Edit.Visible = true;
                txtIme.Text = kandidatPregled.Ime;
                txtPrezime.Text = kandidatPregled.Prezime;

                td_JMBG.Visible = false;
                td_JMBG_Edit.Visible = true;
                txtJMBG.Text = kandidatPregled.JMBG;

                td_DatumRodjenja.Visible = false;
                td_DatumRodjenja_Edit.Visible = true;
                txtDatumRodjenja.Text = kandidatPregled.DatumRodjenja.ToShortDateString();

                td_Email.Visible = false;
                td_Email_Edit.Visible = true;
                txtEmail.Text = kandidatPregled.Email;

                td_Grad.Visible = false;
                td_Grad_Edit.Visible = true;
                BindGradovi();
                listGradovi.SelectedValue = kandidatPregled.GradId.ToString();

                td_Adresa.Visible = false;
                td_Adresa_Edit.Visible = true;
                txtAdresa.Text = kandidatPregled.Adresa;

                td_Telefon.Visible = false;
                td_Telefon_Edit.Visible = true;
                txtTelefon.Text = kandidatPregled.Telefon;

                td_KorisnickoIme.Visible = false;
                td_KorisnickoIme_Edit.Visible = true;
                txtKorisnickoIme.Text = kandidatPregled.KorisnickoIme;

                td_LozinkaMain.InnerText = "Nova lozinka";
                td_Lozinka.Visible = false;
                td_Lozinka_Edit.Visible = true;
            }
            else 
            {
                urediKandidata.Text = "Uredi";

                if (txtIme.Text.Count() > 0)
                    kandidatPregled.Ime = txtIme.Text;
                if (txtPrezime.Text.Count() > 0)
                    kandidatPregled.Prezime = txtPrezime.Text;
                if (txtJMBG.Text.Count() > 0)
                    kandidatPregled.JMBG = txtJMBG.Text;
                if (txtEmail.Text.Count() > 0)
                    kandidatPregled.Email = txtEmail.Text;
                if (txtTelefon.Text.Count() > 0)
                    kandidatPregled.Telefon = txtTelefon.Text;
                if (txtDatumRodjenja.Text.Count() > 0)
                    { 
                        string pattern1 = "dd.MM.yyyy.";
                        string pattern2 = "dd.MM.yyyy";
                        DateTime dt;
                        if (DateTime.TryParseExact(txtDatumRodjenja.Text, pattern1, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) 
                            || DateTime.TryParseExact(txtDatumRodjenja.Text, pattern2, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            kandidatPregled.DatumRodjenja = dt;
                        }
                        else
                        {
                            kandidatPregled.DatumRodjenja = kandidatPregled.DatumRodjenja;
                        }
                    }
                if (txtAdresa.Text.Count() > 0)
                    kandidatPregled.Adresa = txtAdresa.Text;
                if (listGradovi.SelectedIndex > 0)
                    kandidatPregled.GradId = Convert.ToInt32(listGradovi.SelectedValue);
                if (txtKorisnickoIme.Text.Count() > 0)
                    kandidatPregled.KorisnickoIme = txtKorisnickoIme.Text;
                if(txtLozinka.Text.Count() > 0)
                    kandidatPregled.LozinkaHash = Infrastructure.Encryption.Helper.GenerateHash(txtLozinka.Text);

                DAKandidati.Update(kandidatPregled);

                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + kandidatPregled.Ime + " " + kandidatPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja kandidata</small>";
                UpdatePanel5.Update();

                td_ImePrezime.Visible = true;
                td_ImePrezime_Edit.Visible = false;
                td_ImePrezime.InnerText = kandidatPregled.Ime + " " + kandidatPregled.Prezime;

                td_JMBG.Visible = true;
                td_JMBG_Edit.Visible = false;
                td_JMBG.InnerText = kandidatPregled.JMBG;

                td_DatumRodjenja.Visible = true;
                td_DatumRodjenja_Edit.Visible = false;
                td_DatumRodjenja.InnerText = kandidatPregled.DatumRodjenja.ToShortDateString();

                td_Email.Visible = true;
                td_Email_Edit.Visible = false;
                td_Email.InnerText = kandidatPregled.Email;

                td_Grad.Visible = true;
                td_Grad_Edit.Visible = false;
                td_Grad.InnerText = kandidatPregled.Grad.Naziv;

                td_Adresa.Visible = true;
                td_Adresa_Edit.Visible = false;
                td_Adresa.InnerText = kandidatPregled.Adresa;

                td_Telefon.Visible = true;
                td_Telefon_Edit.Visible = false;
                td_Telefon.InnerText = kandidatPregled.Telefon;

                td_KorisnickoIme.Visible = true;
                td_KorisnickoIme_Edit.Visible = false;
                td_KorisnickoIme.InnerText = kandidatPregled.KorisnickoIme;

                td_LozinkaMain.InnerText = "Datum registracije";
                td_Lozinka.Visible = true;
                td_Lozinka_Edit.Visible = false;
                td_Lozinka.InnerText = kandidatPregled.DatumRegistracije.ToShortDateString();
            }

        }

        protected void prijave_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            prijave_Grid.PageIndex = e.NewPageIndex;
            BindPrijave();
            UpdatePanel3.Update();
        }

        protected void prijave_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                Prijave p = DAPrijave.SelectById(Convert.ToInt32(e.CommandArgument));
                p.Status = 0;
                DAPrijave.Update(p);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Uspješno ste obrisali prijavu.')", true);
                BindPrijave();
                UpdatePanel3.Update();
            }
        }

        protected void izlasciTestoviGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void izlasciTestoviGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void izlasciTestoviGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && uradjeniTestovi[e.Row.RowIndex].Polozeno == 1)
            {
                ((HtmlGenericControl)e.Row.FindControl("polozenoColor")).Attributes.Add("class", "label label-success");

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HtmlGenericControl)e.Row.FindControl("polozenoColor")).Attributes.Add("class", "label label-danger");
            }
        }
    }
}