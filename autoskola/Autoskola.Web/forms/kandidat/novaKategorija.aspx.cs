using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.kandidat
{
    public partial class novaKategorija : System.Web.UI.Page
    {
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

        public int brojacUradjenePripremeGrid
        {
            get { return (int)Session["brojacUradjenePripremeGrid"]; }
            set { Session["brojacUradjenePripremeGrid"] = value; }
        }
        public Prijave kandidat_pregledPrijave
        {
            get
            {
                return (Prijave)Session["kandidat_pregledPrijave"] ?? new Prijave();
            }
            set
            {
                Session["kandidat_pregledPrijave"] = value;
            }
        }

        public KategorijePrijave kandidat_pregledKategorijaPrijave
        {
            get { return (KategorijePrijave)Session["kandidat_pregledKategorijaPrijave"]; }
            set { Session["kandidat_pregledKategorijaPrijave"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.User.Identity.IsAuthenticated && kandidatID > 0)
            {
                if (!IsPostBack )
                {
                    if (Request["id"] != null && kandidat_pregledPrijave != null)
                    {
                        brojacUradjenePripremeGrid = 0;
                        int id = Convert.ToInt32(Request["id"]);
                        kandidat_pregledKategorijaPrijave = DAKategorijePrijave.SelectById_Kandidat(kandidat_pregledPrijave.PrijavaId, id, kandidatID);

                        if (kandidat_pregledKategorijaPrijave != null)
                        {
                            try
                            {

                                int spremnost = Convert.ToInt32(kandidat_pregledKategorijaPrijave.Spremnost);
                                spremnostiTestovi.InnerText = spremnost.ToString() + "%";
                                spremnostChart.Attributes.Add("value", spremnost.ToString());
                                if (spremnost > 90)
                                {
                                    spremnostChart.Attributes.Add("data-fgColor", "#00a65a");
                                }
                                else
                                    spremnostChart.Attributes.Add("data-fgColor", "#f56954");

                                BindUradjenePripreme();
                                BindSkripta();
                                BindIzlaskeNaTestove();

                                if (kandidat_pregledPrijave.Instruktor != null)
                                {
                                    headerInstruktor.Visible = true;
                                    composemodal.Visible = true;
                                    headerInstruktor.HRef = "/kandidat/instruktor?id=" + kandidat_pregledPrijave.Instruktor.InstruktorId;
                                    headerInstruktor.InnerText = "Instruktor: " + kandidat_pregledPrijave.Instruktor.Korisnik.Ime + " " + kandidat_pregledPrijave.Instruktor.Korisnik.Prezime;
                                    td_ImePrezime.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.Ime + " " + kandidat_pregledPrijave.Instruktor.Korisnik.Prezime;
                                    td_DatumRodjenja.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.DatumRodjenja.ToShortDateString();
                                    td_Adresa.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.Adresa;
                                    td_Email.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.Email;
                                    td_Grad.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.Grad.Naziv;
                                    td_Telefon.InnerText = kandidat_pregledPrijave.Instruktor.Korisnik.Telefon;
                                }
                                else
                                {
                                    headerInstruktor.Visible = false;
                                    composemodal.Visible = false;
                                }

                                if (DAPolaganjeTestova.CheckPolozeno(kandidat_pregledKategorijaPrijave.KategorijaPrijavaId) == true)
                                {
                                    txtPolozeno.InnerText = "Testovi su položeni.";
                                    imgPolozeno.InnerHtml = "<i class='fa fa-check'></i>";
                                    boxPolozeno.Attributes.Add("class", "small-box bg-green");
                                }
                                else
                                {
                                    txtPolozeno.InnerText = "Testovi nisu položeni.";
                                    imgPolozeno.InnerHtml = "<i class='fa fa-exclamation-triangle'></i>";
                                    boxPolozeno.Attributes.Add("class", "small-box bg-red");
                                }
                            }
                            catch (Exception)
                            {
                                Response.Redirect("/kandidat/404");
                            }
                        }
                        else
                        {
                            Response.Redirect("/kandidat/404");
                        }
                    }
                    else
                        Response.Redirect("/kandidat/404");
                }
            }
            else
            {
                Response.Redirect("/prijava");
            }
            
        }

        private void BindIzlaskeNaTestove()
        {
            if (kandidat_pregledKategorijaPrijave != null)
            {
                uradjeniTestovi = DAPolaganjeTestova.SelectByKategorijaPrijavaId(kandidat_pregledKategorijaPrijave.KategorijaPrijavaId);

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
        private void BindUradjenePripreme()
        {
            if (kandidat_pregledKategorijaPrijave.KategorijaPrijavaId > 0)
            {
                uradjenePripreme = DAUradjeniTestovi.SelectByKategorijePrijaveId(kandidat_pregledKategorijaPrijave.KategorijaPrijavaId);
                for (int i = 0; i < uradjenePripreme.Count; i++)
                    uradjenePripreme[i].KategorijaPrijavaId = Convert.ToInt32(uradjenePripreme[i].OsvojeniProcenat);

                brUradjenihPriprema.InnerText = uradjenePripreme.Count.ToString();
                countPripreme.InnerText = uradjenePripreme.Count.ToString();

                if (uradjenePripreme.Count == 0)
                    boxPripreme.Attributes.Add("class", "small-box bg-red");
                else
                    boxPripreme.Attributes.Add("class", "small-box bg-green");

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

            }
        }
        private void BindSkripta()
        {
            string skripta = "[";
            for (int i = uradjenePripreme.Count-1; i >= 0; i--)
            {
                if(i != 0)
                    skripta += "{ y: '" + (uradjenePripreme.Count - i) + ". test', Uspjeh: " + uradjenePripreme[i].KategorijaPrijavaId + "},";
                else
                    skripta += "{ y: '" + (uradjenePripreme.Count - i) + ". test', Uspjeh: " + uradjenePripreme[i].KategorijaPrijavaId + "}]";
            }

            txtSkripta.Text = skripta;
            UpdatePanel2.Update();
        }

        protected void btn_ucenje_Click(object sender, EventArgs e)
        {
            Session.Add("ucenje_KategorijaID", kandidat_pregledKategorijaPrijave.KategorijaId);
            Response.Redirect("/kandidat/ucenje");
        }

        protected void btn_provjera_Click(object sender, EventArgs e)
        {
            Session.Add("provjera_KategorijaID", kandidat_pregledKategorijaPrijave.KategorijaId);
            Response.Redirect("/kandidat/provjera-znanja");
        }

        protected void uradjeniTestoviGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                if (e.Row.RowType == DataControlRowType.DataRow && uradjenePripreme[e.Row.RowIndex].Polozeno == true)
                {
                    ((HtmlGenericControl)e.Row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-success");
                    ((HtmlGenericControl)e.Row.FindControl("procenat")).Attributes.Add("class", "badge bg-green badgeCorrection");

                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ((HtmlGenericControl)e.Row.FindControl("grafUspjeh")).Attributes.Add("class", "progress-bar progress-bar-danger");
                    ((HtmlGenericControl)e.Row.FindControl("procenat")).Attributes.Add("class", "badge bg-red badgeCorrection");
                }
        }

        protected void uradjeniTestoviGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if(uradjeniTestoviGrid.PageIndex > e.NewPageIndex)
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

    }
}