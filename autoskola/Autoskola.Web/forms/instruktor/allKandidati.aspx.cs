using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class Kandidati : System.Web.UI.Page
    {
        public int instruktorID
        {
            get { 
                    if(Convert.ToInt32(Session["instruktorID"]) > 0 && Session["instruktorID"] != null )
                        return (int)Session["instruktorID"];
                    else
                        return 0; 
                }
            set { Session["instruktorID"] = value; }
        }

        public int brojGrid
        {
            get { return (int)Session["brojGrid"]; }
            set { Session["brojGrid"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0)
            {
                if (!Page.IsPostBack)
                {
                    brojGrid = 1;
                    BindGrid(brojGrid);
                }
            }
            else
                Response.Redirect("/prijava");
        }

        private void BindGrid(int izbor)
        {
            KandidatiGrid.Columns[1].Visible = true;
            KandidatiGrid.Columns[3].Visible = true;
            KandidatiGrid.Columns[4].Visible = true;
            KandidatiGrid.Columns[5].Visible = true;
            KandidatiGrid.Columns[6].Visible = true;
            KandidatiGrid.Columns[0].Visible = false;
            KandidatiGrid.Columns[7].Visible = false;
            KandidatiGrid.Columns[8].Visible = false;

            if(izbor == 1)
            {
                List<Korisnici> temp = DAKandidati.Select();
                if (temp != null)
                {
                    KandidatiGrid.DataSource = temp;
                    KandidatiGrid.DataBind();
                }
            }

            if(izbor == 2)
            {
                KandidatiGrid.Columns[1].Visible = false;
                KandidatiGrid.Columns[3].Visible = false;
                KandidatiGrid.Columns[4].Visible = false;
                KandidatiGrid.Columns[5].Visible = false;
                KandidatiGrid.Columns[6].Visible = false;
                KandidatiGrid.Columns[0].Visible = true;
                KandidatiGrid.Columns[7].Visible = true;
                KandidatiGrid.Columns[8].Visible = true;

                List<Korisnici> temp = DAKandidati.SelectKandidatePripreme();
                if (temp != null)
                {
                    KandidatiGrid.DataSource = temp;
                    KandidatiGrid.DataBind();
                }

            }

            if(izbor == 3)
            {
                List<Korisnici> temp = DAKandidati.SelectByImePrezime(txt_pretraga.Text);
                if (temp != null)
                {
                    KandidatiGrid.DataSource = temp;
                    KandidatiGrid.DataBind();
                }
            }
        }

        protected void sviKandidati_Click(object sender, EventArgs e)
        {
            brojGrid = 1;
            BindGrid(brojGrid);
        }


        protected void KandidatiGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                Korisnici k = DAKandidati.SelectById(Convert.ToInt32(e.CommandArgument));
                DAKandidati.PromjeniStatus(k.KorisnikId);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Uspješno ste obrisali kandidata.')", true);
                BindGrid(brojGrid);
            }
        }

        protected void KandidatiGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            KandidatiGrid.PageIndex = e.NewPageIndex;
            BindGrid(brojGrid);
        }

        protected void btn_Pretraga_Click(object sender, EventArgs e)
        {
            brojGrid = 3;
            BindGrid(brojGrid);
        }

        protected void spremnostiKandidati_Click(object sender, EventArgs e)
        {
            brojGrid = 2;
            BindGrid(brojGrid);
        }
    }
}