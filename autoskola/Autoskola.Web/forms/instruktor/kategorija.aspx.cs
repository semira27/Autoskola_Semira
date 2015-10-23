using Autoskola.Data;
using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class kategorija : System.Web.UI.Page
    {
        public Kategorije kat { get; set; }

        public int kategorijaID
        {
            get { return (int)(ViewState["kategorijaID"] ?? false); }
            set { ViewState["kategorijaID"] = value; }
        }

        public int dodanihSesija
        {
            get { return (int)(ViewState["dodanihSesija"] ?? false); }
            set { ViewState["dodanihSesija"] = value; }
        }

        public int maxPitanja
        {
            get { return (int)(ViewState["maxPitanja"] ?? false); }
            set { ViewState["maxPitanja"] = value; }
        }

        public int brojDodanih { get; set; }

        BrojPitanja noviBrojPitanja { get; set; }

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
                        upozorenjeDiv.Visible = false;

                        kategorijaID = Convert.ToInt32(Request["id"]);
                        kat = DAKategorije.Select_ById(kategorijaID);

                        if (kat != null)
                        {
                            try
                            {
                                heading_top.InnerHtml = "Kategorija " + kat.Naziv + " <small>pregled detalja</small>";

                                ddlVrstePitanja.DataSource = DAVrstePitanja.Select_All();
                                ddlVrstePitanja.DataValueField = "GrupaPitanjaId";
                                ddlVrstePitanja.DataTextField = "Naziv";
                                ddlVrstePitanja.DataBind();

                                maxPitanja = DAKategorije.getBrojPitanja(kategorijaID);

                                BindTable();
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

        private void BindTable()
        {
            List<BrojPitanja> tempPitanja = DABrojPitanja.Select_BrojByKategorija(kategorijaID);


            if(tempPitanja!=null)
            {
                for (int i = 0; i < tempPitanja.Count; i++)
                {
                    brojDodanih += tempPitanja[i].Broj;
                }


                brojPitanja.InnerText = "Pitanja u testu: " + brojDodanih + "/ " + maxPitanja;
                dodanihSesija = brojDodanih;
            }
            else
                brojPitanja.InnerText = "Pitanja u testu: " + 0 + "/ " + maxPitanja;

            grid_VrstePitanja.DataSource = DABrojPitanja.Select_ByKategorija(kategorijaID);
            grid_VrstePitanja.DataBind();
        }

        protected void btnSpasi_Click(object sender, EventArgs e)
        {
            int provjera = dodanihSesija + Convert.ToInt32(ddlBrojPitanja.SelectedValue);
            if (kategorijaID > 0 && provjera <= maxPitanja)
            {
                upozorenjeDiv.Visible = false;

                noviBrojPitanja = new BrojPitanja();
                noviBrojPitanja.KategorijaId = kategorijaID;
                noviBrojPitanja.Broj = Convert.ToInt32(ddlBrojPitanja.SelectedValue);
                noviBrojPitanja.GrupaPitanjaId = Convert.ToInt32(ddlVrstePitanja.SelectedValue);
                DABrojPitanja.Insert(noviBrojPitanja);
                BindTable();

            }
            else
            {
                upozorenjeDiv.Visible = true;
            }
        }

        protected void grid_VrstePitanja_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                    DABrojPitanja.DeleteBroj(Convert.ToInt32(e.CommandArgument));
                    BindTable();
            }
        }


    }
}