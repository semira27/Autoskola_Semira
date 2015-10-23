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
    public partial class prijave : System.Web.UI.Page
    {
        public int PrijavaId { get; set; }

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

        public List<KategorijePrijave> kandidat_kategorijeUPrijavi
        {
            get
            {
                return (List<KategorijePrijave>)Session["kandidat_kategorijeUPrijavi"] ?? new List<KategorijePrijave>();
            }
            set
            {
                Session["kandidat_kategorijeUPrijavi"] = value;
            }
        }

 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                if (Request["id"] != null)
                {
                    PrijavaId = Convert.ToInt32(Request["id"]);
                    kandidat_pregledPrijave = DAPrijave.SelectById(PrijavaId);
                    if(kandidat_pregledPrijave != null)
                    {
                        kandidat_kategorijeUPrijavi = DAKategorijePrijave.SelectByPrijavaId(PrijavaId);

                        listKategorijePrijave.DataSource = kandidat_kategorijeUPrijavi;
                        listKategorijePrijave.DataBind();

                        listKategorijePrijaveIspis();

                        if (kandidat_pregledPrijave != null)
                            lbldatum.InnerText = "Datum: " + kandidat_pregledPrijave.DatumPrijave.ToShortDateString().ToString();
                        if (kandidat_pregledPrijave.Instruktor != null)
                        {
                            composemodal.Visible = true;
                            headerInstruktor.Visible = true;
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
                    }
                    else
                        Response.Redirect("/kandidat/404");
                    
                }
                else
                    Response.Redirect("/kandidat/404");
            }
            else
                Response.Redirect("/prijava");
            
        }

        private void listKategorijePrijaveIspis()
        {
            int i = 0;

            foreach (DataListItem dli in listKategorijePrijave.Items)
            {
                if (dli.ItemType == ListItemType.Item || dli.ItemType == ListItemType.AlternatingItem)
                {
                    bool pronadjeno = false;
                    foreach (PolaganjeTestova kp in kandidat_kategorijeUPrijavi[i].PolaganjeTestova)
                    {
                        if (kp.Polozeno == 1)
                            pronadjeno = true;
                    }

                    HtmlGenericControl label = (HtmlGenericControl)dli.FindControl("kategorijaInfo");
                    if (pronadjeno == true)
                        label.InnerHtml = "<i class='fa fa-check'></i> Položeni testovi";
                    else
                        label.InnerHtml = "<i class='fa fa-exclamation-triangle'></i> Nepoloženi testovi";

                    i++;
                }
            }
        }

    }
}