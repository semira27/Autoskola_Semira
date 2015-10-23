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
    public partial class dodajPrijavu : System.Web.UI.Page
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
        public int kategorijeBroj
        {
            get { return (int)ViewState["kategorijeBroj"]; }
            set { ViewState["kategorijeBroj"] = value; }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0 && autoskolaID > 0)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BindDdls();
                        kategorijeBroj = 0;
                        kategorijeBroj++;
                        BindRepeater();
                    }
                    catch (Exception)
                    {
                        Response.Redirect("/instruktor/404");
                    }
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

        private void BindDdls()
        {
            List<Korisnici> kandidati = DAKandidati.SelectAllActive();
            foreach (Korisnici k in kandidati)
            {
                k.Ime = k.Ime + " " + k.Prezime;
            }

            kandidatList.DataTextField = "Ime";
            kandidatList.DataValueField = "KorisnikId";
            kandidatList.DataSource = kandidati;
            kandidatList.DataBind();

            List<Korisnici> instruktori = DAInstruktori.SelectAllActive();
            foreach (Korisnici i in instruktori)
            {
                i.Ime = i.Ime + " " + i.Prezime;
            }


            instruktorList.DataTextField = "Ime";
            instruktorList.DataValueField = "KorisnikId";
            instruktorList.DataSource = instruktori;
            instruktorList.DataBind();
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
        
        protected void addList_Click(object sender, EventArgs e)
        {
            kategorijeBroj++;
            BindRepeater();
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem i = e.Item;
            DropDownList ddl = i.FindControl("kategorijeList") as DropDownList;
            FillDropdown(ddl);
        }

        protected void Reset_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/instruktor/prijave");
        }

        protected void Spasi_btn_Click(object sender, EventArgs e)
        {
            if(kandidatList.SelectedIndex > 0 && instruktorList.SelectedIndex > 0 && txtDatum.Text.Count() > 0)
            {
                List<int> kategorijeValidation = new List<int>();
                foreach (RepeaterItem dataItem in Repeater2.Items)
                {
                    kategorijeValidation.Add(Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue));
                }

                if (kategorijeValidation.Count == kategorijeValidation.Distinct().Count())
                {
                    string pattern = "dd/MM/yyyy";
                    DateTime dt;
                    if (DateTime.TryParseExact(txtDatum.Text, pattern, CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out dt))
                    {
                        try
                        {
                            Prijave p = new Prijave();
                            p.KandidatId = DAKandidati.SelectKandidatIdByKorisnikId(Convert.ToInt32(kandidatList.SelectedValue));
                            p.InstruktorId = DAInstruktori.SelectInstruktorIdByKorisnikId(Convert.ToInt32(instruktorList.SelectedValue));
                            p.DatumPrijave = dt;
                            p.Zavrseno = 0;
                            p.Status = 1;

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
                        }
                        catch (Exception)
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

        protected void removeKat_Click(object sender, EventArgs e)
        {
            kategorijeBroj--;
            BindRepeater();
        }
    }
}