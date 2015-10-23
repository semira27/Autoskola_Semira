using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class Instruktor : System.Web.UI.MasterPage
    {
        public Instruktori logirani_instruktor
        {
            get { return (Instruktori)Session["logirani_instruktor"]; }
            set { Session["logirani_instruktor"] = value; }
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
            if (Page.User.Identity.IsAuthenticated)
            {
                try
                {
                    int id = Convert.ToInt32(Session["instruktorID"]);
                    logirani_instruktor = DAInstruktori.SelectByInstruktorId(id);

                    if (autoskolaID > 0 && logirani_instruktor != null && logirani_instruktor.InstruktorId > 0)
                    {
                        AutoSkole trenutna = DAAutoskole.SelectById(autoskolaID);
                        if (trenutna != null)
                            mainLogo.Src = trenutna.Logo;

                        List<GrupePitanja> grupePitanjaList = DAVrstePitanja.Select_All_Instruktor();
                        listPitanja.DataSource = grupePitanjaList;
                        listPitanja.DataBind();
                        listKategorije.DataSource = DAKategorije.Select();
                        listKategorije.DataBind();


                        lnkKorisnickiProfil.NavigateUrl = "/instruktor/profil";
                        lbl_imePrezime.InnerText = logirani_instruktor.Korisnik.Ime + " " + logirani_instruktor.Korisnik.Prezime;
                        lbl_ImePrezimeBig.InnerText = logirani_instruktor.Korisnik.Ime + " " + logirani_instruktor.Korisnik.Prezime;
                        lbl_PozdravnaPoruka.InnerText = "Dobrodošli, " + logirani_instruktor.Korisnik.Ime;
                    }
                    else
                        Response.Redirect("/prijava");
                }
                catch (Exception)
                {
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