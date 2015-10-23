using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoskola.Data;
using System.Web.Security;

namespace Autoskola.Web.forms.kandidat
{
    public partial class Kandidat : System.Web.UI.MasterPage
    {
        public Kandidati logirani_kandidat { get; set; }

        public List<Prijave> listaPrijava_Aktivne
        {
            get { return (List<Prijave>)Session["listaPrijava_Aktivne"]; }
            set { Session["listaPrijava_Aktivne"] = value; }
        }
        public List<Prijave> listaPrijava_Zavrsene
        {
            get { return (List<Prijave>)Session["listaPrijava_Zavrsene"]; }
            set { Session["listaPrijava_Zavrsene"] = value; }
        }

        public int autoskolaID
        {
            get { 
                    if(Convert.ToInt32(Session["autoskolaID"]) > 0 && Session["autoskolaID"] != null )
                        return (int)Session["autoskolaID"];
                    else
                        return 0; 
                }
            set { Session["autoskolaID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && autoskolaID > 0)
            {
                if (!IsPostBack)
                {
                        AutoSkole trenutna = DAAutoskole.SelectById(autoskolaID);
                        if (trenutna != null)
                        {
                            mainLogo.Src = trenutna.Logo;

                            int id = Convert.ToInt32(Session["kandidatID"]);
                            logirani_kandidat = DAKandidati.SelectKandidatById(id);

                            if (logirani_kandidat != null)
                            {
                                //listaPrijava - aktivne
                                listaPrijava_Aktivne = DAPrijave.SelectByKandidatId(logirani_kandidat.KandidatId);
                                listPrijave.DataSource = listaPrijava_Aktivne;
                                listPrijave.DataBind();

                                //listaPrijava - zavrsene
                                listaPrijava_Zavrsene = DAPrijave.SelectByKandidatId_Zavrsene(logirani_kandidat.KandidatId);
                                listPrijaveZavrsene.DataSource = listaPrijava_Zavrsene;
                                listPrijaveZavrsene.DataBind();

                                lbl_imePrezime.InnerText = logirani_kandidat.Korisnik.Ime + " " + logirani_kandidat.Korisnik.Prezime;
                                lbl_ImePrezimeBig.InnerText = logirani_kandidat.Korisnik.Ime + " " + logirani_kandidat.Korisnik.Prezime;
                                lbl_PozdravnaPoruka.InnerText = "Dobrodošli, " + logirani_kandidat.Korisnik.Ime;
                            }
                        }
                        else
                            Response.Redirect("/instruktor/404");
                }
            }
            else
                Response.Redirect("/prijava");
        }

        protected void btn_Odjava_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/prijava");
        }
    }
}