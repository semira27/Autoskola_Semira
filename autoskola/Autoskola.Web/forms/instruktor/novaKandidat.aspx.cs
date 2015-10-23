using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoskola.Data;
using Autoskola.Data.Model;
using System.Globalization;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaKandidat : System.Web.UI.Page
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
                return (int)ViewState["kandidatID"];
            }
            set
            {
                ViewState["kandidatID"] = value;
            }
        }

        public int kategorijeBroj
        {
            get { return (int)ViewState["kategorijeBroj"]; }
            set { ViewState["kategorijeBroj"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0 && autoskolaID > 0)
            {
                if (!IsPostBack)
                {
                    kategorijeBroj = 0;
                    if (Request["id"] != null)
                    {
                        kandidatID = Convert.ToInt32(Request["id"]);
                        kandidatPregled = DAKandidati.SelectKorisnikByKandidatId(kandidatID);
                        if (kandidatPregled != null && kandidatPregled.KorisnikId > 0)
                        {
                            try
                            {
                                brPrijavljenihKat.InnerText = DAKandidati.CountPrijavljeneKategorije(kandidatID).ToString();
                                brUradjenihPriprema.InnerText = DAKandidati.CountUradjenePripreme(kandidatID).ToString();
                                brPolozenihKat.InnerText = DAKandidati.CountPolozeneKategorije(kandidatID).ToString();
                                BindForm();
                                BindGrid();
                                BindNovaPrijava();
                                kategorijeBroj++;
                                BindRepeater();
                            }
                            catch (Exception)
                            {
                                Response.Redirect("/instruktor/404");
                            }
                        }
                        else
                            Response.Redirect("/instruktor/404");
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

            List<Korisnici> instruktori = DAInstruktori.SelectAllActive();
            foreach (Korisnici i in instruktori)
            {
                i.Ime = i.Ime + " " + i.Prezime;
            }

            list_Instruktori.DataTextField = "Ime";
            list_Instruktori.DataValueField = "KorisnikId";
            list_Instruktori.DataSource = instruktori;
            list_Instruktori.DataBind();
        }

        private void BindForm()
        {
            if (kandidatPregled != null)
            {
                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + kandidatPregled.Ime + " " + kandidatPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja</small>";
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

        private void BindGrid() 
        {
            List<Prijave> temp = DAPrijave.SelectByKandidatId(kandidatID);
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
            if (txtDatum.Text.Count() > 0 && list_Instruktori.SelectedIndex > 0)
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
                            p.InstruktorId = DAInstruktori.SelectInstruktorIdByKorisnikId(Convert.ToInt32(list_Instruktori.SelectedValue));
                            p.KandidatId = kandidatID;
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

                            brPrijavljenihKat.InnerText = DAKandidati.CountPrijavljeneKategorije(kandidatID).ToString();
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

                imePrezimeHeader.InnerHtml = "<i style='font-size: 40px; margin-right: 10px' class='fa fa-user'></i>" + kandidatPregled.Ime + " " + kandidatPregled.Prezime + "<small style='font-size: 16px;margin-left: 5px'>pregled detalja</small>";
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