using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class pregledPrijave : System.Web.UI.Page
    {
        public Prijave prijava_pregledPrijave
        {
            get
            {
                return (Prijave)Session["prijava_pregledPrijave"] ?? new Prijave();
            }
            set
            {
                Session["prijava_pregledPrijave"] = value;
            }
        }
        public int brojacUradjenePripremeGrid
        {
            get { return (int)Session["brojacUradjenePripremeGrid"]; }
            set { Session["brojacUradjenePripremeGrid"] = value; }
        }
        public Korisnici kandidat { get; set; }

        KategorijePrijave potrebna
        {
            get
            {
                return (KategorijePrijave)Session["potrebna"] ?? new KategorijePrijave();
            }
            set
            {
                Session["potrebna"] = value;
            }
        }
        public Korisnici instruktor { get; set; }
        public List<KategorijePrijave> kategorijeUPrijavi
        {
            get
            {
                return (List<KategorijePrijave>)Session["kategorijeUPrijavi"] ?? new List<KategorijePrijave>();
            }
            set
            {
                Session["kategorijeUPrijavi"] = value;
            }
        }


        public List<UradjeniTestovi> uradjenePripreme
        {
            get
            {
                return (List<UradjeniTestovi>)Session["uradjenePripreme"] ?? new List<UradjeniTestovi>();
            }
            set
            {
                Session["uradjenePripreme"] = value;
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

        public int prijavaID
        {
            get { return (int)(ViewState["prijavaID"] ?? false); }
            set { ViewState["prijavaID"] = value; }
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
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0)
            {
                if (!IsPostBack)
                {
                    if (Request["id"] != null)
                    {
                        brojacUradjenePripremeGrid = 0;
                        prijavaID = Convert.ToInt32(Request["id"]);
                        prijava_pregledPrijave = DAPrijave.SelectById(prijavaID);
                        if (prijava_pregledPrijave != null && prijava_pregledPrijave.PrijavaId > 0)
                        {
                            try
                            {
                                kandidat = DAKandidati.SelectKorisnikByKandidatId(prijava_pregledPrijave.KandidatId);
                                instruktor = DAInstruktori.SelectKorisnikByInstruktorId(prijava_pregledPrijave.InstruktorId);
                                headerKandidat.InnerHtml = "<i class='fa fa-user'></i> " + kandidat.Ime + " " + kandidat.Prezime + "<small class='pull-right'>Datum: " + prijava_pregledPrijave.DatumPrijave.ToShortDateString() + "</small>";
                                instruktorName.InnerText = instruktor.Ime + " " + instruktor.Prezime;
                                if (prijava_pregledPrijave.Zavrseno == 0)
                                    statusName.InnerText = "Aktivna";
                                else
                                    statusName.InnerText = "Završena";
                                kategorijeUPrijavi = DAKategorijePrijave.SelectByPrijavaId(prijava_pregledPrijave.PrijavaId);
                                BindKategorije();
                                pregledaj_kat_div.Visible = false;
                                addTest.Visible = false;
                                changeStatus.Attributes.Add("style", "float: left!important");
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

        private void BindUradjenePripreme(int KategorijaUPrijaviID)
        {
            if (KategorijaUPrijaviID > 0)
            {
                uradjenePripreme = DAUradjeniTestovi.SelectByKategorijePrijaveId(KategorijaUPrijaviID);
                for (int i = 0; i < uradjenePripreme.Count; i++)
                    uradjenePripreme[i].KategorijaPrijavaId = Convert.ToInt32(uradjenePripreme[i].OsvojeniProcenat);

                if (uradjenePripreme.Count > 0)
                {
                    pripreme_empty.Visible = false;
                    uradjeniTestoviGrid.Visible = true;
                    uradjeniTestoviGrid.DataSource = uradjenePripreme;
                    uradjeniTestoviGrid.DataBind();
                }
                else
                {
                    pripreme_empty.Visible = true;
                    uradjeniTestoviGrid.Visible = false;
                }

                countTestovi.InnerText = uradjenePripreme.Count.ToString();
                pripremeChart.Attributes.Add("value", uradjenePripreme.Count.ToString());
            }
        }

        private void BindKategorije()
        {
            List <Kategorije> kats = new List<Kategorije>();
            Kategorije k = new Kategorije();
            k.Naziv = "Odaberite kategoriju";
            k.KategorijaId = 0;
            kats.Insert(0, k);

            foreach (KategorijePrijave i in kategorijeUPrijavi)
            {
                kats.Add(i.Kategorije);
            }

            kategorijeList.DataTextField = "Naziv";
            kategorijeList.DataValueField = "KategorijaId";
            kategorijeList.DataSource = kats;
            kategorijeList.DataBind();
        }

        private void BindTestove(int KategorijaUPrijaviID)
        {
            if (KategorijaUPrijaviID > 0)
            {
                uradjeniTestovi = DAPolaganjeTestova.SelectByKategorijaPrijavaId(KategorijaUPrijaviID);
                testoviChart.Attributes.Add("value", uradjeniTestovi.Count.ToString());

                if (uradjeniTestovi.Count > 0)
                {
                    testovi_empty.Visible = false;
                    glavniTestoviGrid.Visible = true;
                    glavniTestoviGrid.DataSource = uradjeniTestovi;
                    glavniTestoviGrid.DataBind();
                }
                else
                {
                    testovi_empty.Visible = true;
                    glavniTestoviGrid.Visible = false;
                }
            }
        }

        protected void pregledajKategoriju_Click(object sender, EventArgs e)
        {
            int odabranaKategorija = Convert.ToInt32(kategorijeList.SelectedValue);
            potrebna = new KategorijePrijave();
            foreach (KategorijePrijave i in kategorijeUPrijavi)
            {
                if (i.KategorijaId == odabranaKategorija)
                    potrebna = i;
            }
            pregledaj_kat_div.Visible = true;
            addTest.Visible = true;
            changeStatus.Attributes.Add("style", "float: right!important");
            BindUradjenePripreme(potrebna.KategorijaPrijavaId);
            BindTestove(potrebna.KategorijaPrijavaId);
            int spremnost = Convert.ToInt32(potrebna.Spremnost);
            spremnostiTestovi.InnerHtml = "<b>" + spremnost.ToString() + "%" + "</b>";
            spremnostChart.Attributes.Add("value",spremnost.ToString());

        }

        protected void uradjeniTestoviGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && uradjenePripreme[e.Row.RowIndex].Polozeno ==true)
            {
                ((HtmlGenericControl)e.Row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-success");
                ((HtmlGenericControl)e.Row.FindControl("procenat")).Attributes.Add("class", "badge bg-green badgeCorrection");
                
            }
            else if(e.Row.RowType == DataControlRowType.DataRow)
            {
                ((HtmlGenericControl)e.Row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-danger");
                ((HtmlGenericControl)e.Row.FindControl("procenat")).Attributes.Add("class", "badge bg-red badgeCorrection");
            }
        }

        protected void finishPrijava_Click(object sender, EventArgs e)
        {
            if (prijava_pregledPrijave.Zavrseno == 0)
            {
                statusName.InnerText = "Završena";
                prijava_pregledPrijave.Zavrseno = 1;
            }
            else
            {
                prijava_pregledPrijave.Zavrseno = 0;
                statusName.InnerText = "Aktivna";
            }
            DAPrijave.Update(prijava_pregledPrijave);
        }

        protected void glavniTestoviGrid_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void uradjeniTestoviGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (uradjeniTestoviGrid.PageIndex > e.NewPageIndex)
            {
                brojacUradjenePripremeGrid--;
            }
            else
                brojacUradjenePripremeGrid++;

            int dodavanje = brojacUradjenePripremeGrid * 5;

            uradjeniTestoviGrid.PageIndex = e.NewPageIndex;
            uradjeniTestoviGrid.DataSource = uradjenePripreme;
            uradjeniTestoviGrid.DataBind();


            int broj = 0;
            foreach (GridViewRow row in uradjeniTestoviGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow && uradjenePripreme[broj + dodavanje].Polozeno == true)
                {
                    ((HtmlGenericControl)row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-success");
                    ((HtmlGenericControl)row.FindControl("procenat")).Attributes.Add("class", "badge bg-green badgeCorrection");

                }
                else if (row.RowType == DataControlRowType.DataRow)
                {
                    ((HtmlGenericControl)row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-danger");
                    ((HtmlGenericControl)row.FindControl("procenat")).Attributes.Add("class", "badge bg-red badgeCorrection");
                }
                broj++;
            }

            UpdatePanel3.Update();
        }

        protected void dodajIzlazak_Click(object sender, EventArgs e)
        {
            try
            {
                if(datumTxt.Text.Count() > 0 && (radioPolozeno.SelectedValue == "1" || radioPolozeno.SelectedValue == "0"))
                {
                    PolaganjeTestova p = new PolaganjeTestova();
                    string pattern = "dd/MM/yyyy";
                    DateTime dt;
                    if (DateTime.TryParseExact(datumTxt.Text, pattern, CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out dt))
                    {
                        p.DatumPolaganja = dt;
                        if (radioPolozeno.SelectedValue == "0")
                            p.Polozeno = 0;
                        else
                            p.Polozeno = 1;
                        p.KategorijaPrijavaId = potrebna.KategorijaPrijavaId;
                        DAPolaganjeTestova.Insert(p);

                        addTest_Danger.Visible = false;
                        addTest_Success.Visible = true;
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        addTest_Danger.Visible = true;
                        addTest_Success.Visible = false;
                    }
                }
                else
                {
                    addTest_Danger.Visible = true;
                    addTest_Success.Visible = false;
                }

            }
            catch (Exception)
            {
                addTest_Danger.Visible = true;
                addTest_Success.Visible = false;
            }
        }


    }
}