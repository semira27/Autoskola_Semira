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

        public int kategorijeBroj
        {
            get { return (int)ViewState["kategorijeBroj"]; }
            set { ViewState["kategorijeBroj"] = value; }
        }

        public Korisnici instruktorPregled
        {
            get
            {
                return (Korisnici)Session["instruktorPregled"] ?? new Korisnici();
            }
            set
            {
                Session["instruktorPregled"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0 && autoskolaID > 0)
            {
                if(!IsPostBack)
                {
                    kategorijeBroj = 0;
                    instruktorID = Convert.ToInt32(instruktorID);
                    if (instruktorPregled != null)
                    {
                        instruktorPregled = DAInstruktori.SelectKorisnikByInstruktorId(instruktorID);
                        brPrijavljenihKat.InnerText = DAInstruktori.CountPrijavljeneKategorije(instruktorID).ToString();
                        brKandidata.InnerText = DAInstruktori.CountKandidate(instruktorID).ToString();
                        brPolozenihKat.InnerText = DAInstruktori.CountPolozeneKategorije(instruktorID).ToString();
                        BindForm();
                        BindGrid();
                        BindNovaPrijava();
                        kategorijeBroj++;
                        BindRepeater();
                    }
                    else
                        Response.Redirect("/instruktor/404");
                }
            }
            else
                Response.Redirect("/prijava");

        }


        private void BindRepeater()
        {
            Repeater2.DataSource = RepSource();
            Repeater2.DataBind();

            if (kategorijeBroj == 1)
                removeKat.Visible = false;
            else
                removeKat.Visible = true;
        }

        private void BindNovaPrijava()
        {

            List<Korisnici> kandidati = DAKandidati.SelectAllActive();
            foreach (Korisnici k in kandidati)
            {
                k.Ime = k.Ime + " " + k.Prezime;
            }

            list_Kandidati.DataTextField = "Ime";
            list_Kandidati.DataValueField = "KorisnikId";
            list_Kandidati.DataSource = kandidati;
            list_Kandidati.DataBind();
        }

        private void BindForm()
        {
            if (instruktorPregled != null)
            {
                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + instruktorPregled.Ime + " " + instruktorPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja instruktora</small>";
                td_ImePrezime.InnerText = instruktorPregled.Ime + " " + instruktorPregled.Prezime;
                td_JMBG.InnerText = instruktorPregled.JMBG;
                td_Adresa.InnerText = instruktorPregled.Adresa;
                td_Grad.InnerText = instruktorPregled.Grad.Naziv;
                td_Telefon.InnerText = instruktorPregled.Telefon;
                td_DatumRodjenja.InnerText = instruktorPregled.DatumRodjenja.ToShortDateString();
                td_Email.InnerText = instruktorPregled.Email;
                td_KorisnickoIme.InnerText = instruktorPregled.KorisnickoIme;
                td_LozinkaMain.InnerText = "Datum registracije";
                td_Lozinka.InnerText = instruktorPregled.DatumRegistracije.ToShortDateString();
            }
        }

        private void BindGrid()
        {
            List<Prijave> temp = DAPrijave.SelectByInstruktorId(instruktorID);
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

        private void BindGradovi()
        {
            listGradovi.DataSource = DAGradovi.SelectAll();
            listGradovi.DataTextField = "Naziv";
            listGradovi.DataValueField = "GradId";
            listGradovi.DataBind();
        }


        private List<string> RepSource()
        {
            List<string> rep = new List<string>();
            for (int i = 0; i < kategorijeBroj; i++)
            {
                rep.Add(i.ToString());
            }
            return rep;
        }

        private void FillDropdown(DropDownList ddl)
        {
            ddl.DataTextField = "Naziv";
            ddl.DataValueField = "KategorijaId";
            ddl.DataSource = DAKategorije.SelectAll();
            ddl.DataBind();
        }


        protected void urediKandidata_Click(object sender, EventArgs e)
        {
            if (td_ImePrezime.Visible == true)
            {
                urediKandidata.Text = "Spasi";

                td_ImePrezime.Visible = false;
                td_ImePrezime_Edit.Visible = true;
                txtIme.Text = instruktorPregled.Ime;
                txtPrezime.Text = instruktorPregled.Prezime;

                td_JMBG.Visible = false;
                td_JMBG_Edit.Visible = true;
                txtJMBG.Text = instruktorPregled.JMBG;

                td_DatumRodjenja.Visible = false;
                td_DatumRodjenja_Edit.Visible = true;
                txtDatumRodjenja.Text = instruktorPregled.DatumRodjenja.ToShortDateString();

                td_Email.Visible = false;
                td_Email_Edit.Visible = true;
                txtEmail.Text = instruktorPregled.Email;

                td_Grad.Visible = false;
                td_Grad_Edit.Visible = true;
                BindGradovi();
                listGradovi.SelectedValue = instruktorPregled.GradId.ToString();

                td_Adresa.Visible = false;
                td_Adresa_Edit.Visible = true;
                txtAdresa.Text = instruktorPregled.Adresa;

                td_Telefon.Visible = false;
                td_Telefon_Edit.Visible = true;
                txtTelefon.Text = instruktorPregled.Telefon;

                td_KorisnickoIme.Visible = false;
                td_KorisnickoIme_Edit.Visible = true;
                txtKorisnickoIme.Text = instruktorPregled.KorisnickoIme;

                td_LozinkaMain.InnerText = "Nova lozinka";
                td_Lozinka.Visible = false;
                td_Lozinka_Edit.Visible = true;
            }
            else
            {
                urediKandidata.Text = "Uredi";

                if (txtIme.Text.Count() > 0)
                    instruktorPregled.Ime = txtIme.Text;
                if (txtPrezime.Text.Count() > 0)
                    instruktorPregled.Prezime = txtPrezime.Text;
                if (txtJMBG.Text.Count() > 0)
                    instruktorPregled.JMBG = txtJMBG.Text;
                if (txtEmail.Text.Count() > 0)
                    instruktorPregled.Email = txtEmail.Text;
                if (txtTelefon.Text.Count() > 0)
                    instruktorPregled.Telefon = txtTelefon.Text;
                if (txtDatumRodjenja.Text.Count() > 0)
                {
                    string pattern1 = "dd.MM.yyyy.";
                    string pattern2 = "dd.MM.yyyy";
                    DateTime dt;
                    if (DateTime.TryParseExact(txtDatumRodjenja.Text, pattern1, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)
                        || DateTime.TryParseExact(txtDatumRodjenja.Text, pattern2, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    {
                        instruktorPregled.DatumRodjenja = dt;
                    }
                    else
                    {
                        instruktorPregled.DatumRodjenja = instruktorPregled.DatumRodjenja;
                    }
                }
                if (txtAdresa.Text.Count() > 0)
                    instruktorPregled.Adresa = txtAdresa.Text;
                if (listGradovi.SelectedIndex > 0)
                    instruktorPregled.GradId = Convert.ToInt32(listGradovi.SelectedValue);
                if (txtKorisnickoIme.Text.Count() > 0)
                    instruktorPregled.KorisnickoIme = txtKorisnickoIme.Text;
                if (txtLozinka.Text.Count() > 0)
                    instruktorPregled.LozinkaHash = Infrastructure.Encryption.Helper.GenerateHash(txtLozinka.Text);

                DAKandidati.Update(instruktorPregled);

                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + instruktorPregled.Ime + " " + instruktorPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja instruktora</small>";
                UpdatePanel5.Update();

                td_ImePrezime.Visible = true;
                td_ImePrezime_Edit.Visible = false;
                td_ImePrezime.InnerText = instruktorPregled.Ime + " " + instruktorPregled.Prezime;

                td_JMBG.Visible = true;
                td_JMBG_Edit.Visible = false;
                td_JMBG.InnerText = instruktorPregled.JMBG;

                td_DatumRodjenja.Visible = true;
                td_DatumRodjenja_Edit.Visible = false;
                td_DatumRodjenja.InnerText = instruktorPregled.DatumRodjenja.ToShortDateString();

                td_Email.Visible = true;
                td_Email_Edit.Visible = false;
                td_Email.InnerText = instruktorPregled.Email;

                td_Grad.Visible = true;
                td_Grad_Edit.Visible = false;
                td_Grad.InnerText = instruktorPregled.Grad.Naziv;

                td_Adresa.Visible = true;
                td_Adresa_Edit.Visible = false;
                td_Adresa.InnerText = instruktorPregled.Adresa;

                td_Telefon.Visible = true;
                td_Telefon_Edit.Visible = false;
                td_Telefon.InnerText = instruktorPregled.Telefon;

                td_KorisnickoIme.Visible = true;
                td_KorisnickoIme_Edit.Visible = false;
                td_KorisnickoIme.InnerText = instruktorPregled.KorisnickoIme;

                td_LozinkaMain.InnerText = "Datum registracije";
                td_Lozinka.Visible = true;
                td_Lozinka_Edit.Visible = false;
                td_Lozinka.InnerText = instruktorPregled.DatumRegistracije.ToShortDateString();
            }

        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem i = e.Item;
            DropDownList ddl = i.FindControl("kategorijeList") as DropDownList;
            FillDropdown(ddl);
        }

        protected void addList_Click(object sender, EventArgs e)
        {
            kategorijeBroj++;
            BindRepeater();
        }

        protected void btnAddPrijava_Click(object sender, EventArgs e)
        {
            if (txtDatum.Text.Count() > 0 && list_Kandidati.SelectedIndex > 0)
            {
                List<int> kategorijeValidation = new List<int>();
                foreach (RepeaterItem dataItem in Repeater2.Items)
                {
                    kategorijeValidation.Add(Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue));
                }

                if (kategorijeValidation.Count == kategorijeValidation.Distinct().Count())
                {
                    Prijave p = new Prijave();
                    string pattern = "dd/MM/yyyy";
                    DateTime dt;
                    if (DateTime.TryParseExact(txtDatum.Text, pattern, CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out dt))
                    {
                        try
                        {
                            p.InstruktorId = instruktorID;
                            p.KandidatId = DAKandidati.SelectKandidatIdByKorisnikId(Convert.ToInt32(list_Kandidati.SelectedValue));
                            p.Zavrseno = 0;
                            p.Status = 1;
                            p.DatumPrijave = dt;

                            int prijavaID = DAPrijave.Insert(p);

                            foreach (RepeaterItem dataItem in Repeater2.Items)
                            {
                                KategorijePrijave kp = new KategorijePrijave();
                                kp.KategorijaId = Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue);
                                kp.PrijavaId = prijavaID;
                                DAKategorijePrijave.Insert(kp);
                            }

                            Danger_div.Visible = false;
                            Success_div.Visible = true;
                            BindGrid();
                            UpdatePanel3.Update();

                            brPrijavljenihKat.InnerText = DAKandidati.CountPrijavljeneKategorije(instruktorID).ToString();
                            UpdatePanel6.Update();
                        }
                        catch (Exception)
                        {
                            Danger_div.Visible = true;
                            Success_div.Visible = false;
                        }

                    }
                }
                else
                {
                    Danger_div.Visible = true;
                    Success_div.Visible = false;
                }

            }
            else
            {
                Danger_div.Visible = true;
                Success_div.Visible = false;
            }

        }

        protected void prijave_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            prijave_Grid.PageIndex = e.NewPageIndex;
            BindGrid();
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
                BindGrid();
                UpdatePanel3.Update();
            }

        }

        protected void removeKat_Click(object sender, EventArgs e)
        {
            if (kategorijeBroj > 1)
            {
                kategorijeBroj--;
                BindRepeater();
            }
        }
    }
}