using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.kandidat
{
    public partial class provjeraRezultat : System.Web.UI.Page
    {
        public Pitanja pitanje
        {
            get { return (Pitanja)Session["pitanje"]; }
            set { Session["pitanje"] = value; }
        }
        public int VrstaPitanjaBod
        {
            get { return (int)Session["VrstaPitanjaBod"]; }
            set { Session["VrstaPitanjaBod"] = value; }
        }
        public int brojTacnihOdgovora
        {
            get { return (int)Session["brojTacnihOdgovora"]; }
            set { Session["brojTacnihOdgovora"] = value; }
        }
        public int maxBodovi
        {
            get { return (int)Session["maxBodovi"]; }
            set { Session["maxBodovi"] = value; }
        }
        public List<int> listaIDPitanja
        {
            get { return (List<int>)Session["listaIDPitanja"]; }
            set { Session["listaIDPitanja"] = value; }
        }

        public List<Odgovori> listaOdgovora
        {
            get { return (List<Odgovori>)Session["listaOdgovora"]; }
            set { Session["listaOdgovora"] = value; }
        }
        public List<OdabraniOdgovori> listaOdabranihOdgovora
        {
            get { return (List<OdabraniOdgovori>)Session["listaOdabranihOdgovora"]; }
            set { Session["listaOdabranihOdgovora"] = value; }
        }
        public UradjeniTestovi trenutnaPriprema
        {
            get { return (UradjeniTestovi)Session["trenutnaPriprema"]; }
            set { Session["trenutnaPriprema"] = value; }
        }

        public int brojac
        {
            get { return (int)Session["brojac"]; }
            set { Session["brojac"] = value; }
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
        public int provjera_KategorijaID
        {
            get { return (int)Session["provjera_KategorijaID"]; }
            set { Session["provjera_KategorijaID"] = value; }
        }

        public KategorijePrijave kandidat_pregledKategorijaPrijave
        {
            get { return (KategorijePrijave)Session["kandidat_pregledKategorijaPrijave"]; }
            set { Session["kandidat_pregledKategorijaPrijave"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && kandidatID > 0)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        int procenat = Convert.ToInt32(trenutnaPriprema.OsvojeniProcenat);
                        uspjehChart.Attributes.Add("value", procenat.ToString());
                        pocetakPriprema.InnerText = trenutnaPriprema.PocetakTesta.ToString();
                        krajPriprema.InnerText = trenutnaPriprema.KrajTesta.ToString();
                        ukupanBrPitanja.InnerText = listaIDPitanja.Count.ToString();
                        brTacnihOdg.InnerText = brojTacnihOdgovora.ToString();
                        maxBrBodova.InnerText = maxBodovi.ToString();
                        brOsvojenihBodova.InnerText = trenutnaPriprema.OsvojeniBodovi.ToString();
                        uspjehTable.InnerText = Convert.ToInt32(trenutnaPriprema.OsvojeniProcenat).ToString() + "%";

                        if (trenutnaPriprema.Polozeno == true)
                        {
                            canvas.Attributes.Add("style", "display:block");
                            errFailed.Attributes.Add("style", "display:none");
                            uspjehChart.Attributes.Add("data-fgColor", "#00a65a");
                        }
                        else
                        {
                            canvas.Attributes.Add("style", "display:none");
                            errFailed.Attributes.Add("style", "display:block");
                            uspjehChart.Attributes.Add("data-fgColor", "#f56954");
                        }
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kandidat/prijavljena-kategorija?id=" + kandidat_pregledKategorijaPrijave.KategorijaPrijavaId.ToString());
        }
    }
}