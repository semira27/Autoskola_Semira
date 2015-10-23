using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaPrijave : System.Web.UI.Page
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
        public int currentPrijave
        {
            get { return (int)ViewState["currentPrijave"]; }
            set { ViewState["currentPrijave"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0)
            {
                if (!Page.IsPostBack)
                {
                    currentPrijave = 1;
                    BindGrid(1);
                }
            }
            else
                Response.Redirect("/prijava");

        }


        private void BindGrid(int select)
        {
            List<Prijave> temp = null;
            PrijaveGrid.Columns[0].Visible = true;

            if (select == 1)
            {
                temp = DAPrijave.SelectAll();
            }
            else if (select == 2)
            {
                temp = DAPrijave.SelectActive();
            }
            else if (select == 3)
            {
                temp = DAPrijave.SelectUnactive();
            }
            else if (select == 4)
            {
                PrijaveGrid.Columns[0].Visible = false;
                temp = DAPrijave.SelectDeleted();
            }
            
            if (temp != null)
            {
                PrijaveGrid.DataSource = temp;
                PrijaveGrid.DataBind();
            }

        }


        protected void PrijaveGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                Prijave p = DAPrijave.SelectById(Convert.ToInt32(e.CommandArgument));
                p.Status = 0;
                DAPrijave.Update(p);
                BindGrid(2);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Uspješno ste obrisali prijavu.')", true);
                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        protected void aktivnePrijave_Click(object sender, EventArgs e)
        {
            currentPrijave = 2;
            BindGrid(2);

        }

        protected void pretragaBtn_Click(object sender, EventArgs e)
        {
            currentPrijave = 2;
            PrijaveGrid.Columns[0].Visible = true;
            string imePrezime = txt_pretraga.Text.ToLower();
            List<Prijave> temp = DAPrijave.SelectByKandidatOrInstruktor(imePrezime);

            if (temp != null)
            {
                PrijaveGrid.DataSource = temp;
                PrijaveGrid.DataBind();
            }
        }

        protected void svePrijave_Click(object sender, EventArgs e)
        {
            currentPrijave = 1;
            BindGrid(1);
        }

        protected void zavrsenePrijave_Click(object sender, EventArgs e)
        {
            currentPrijave = 3;
            BindGrid(3);
        }

        protected void obrisanePrijave_Click(object sender, EventArgs e)
        {
            currentPrijave = 4;
            BindGrid(4);
        }

        protected void PrijaveGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PrijaveGrid.PageIndex = e.NewPageIndex;
            BindGrid(currentPrijave);
        }

        protected void PrijaveGrid_DataBound(object sender, EventArgs e)
        {
            //PrijaveGrid.BottomPagerRow.Visible = true;            
        }


    }
}