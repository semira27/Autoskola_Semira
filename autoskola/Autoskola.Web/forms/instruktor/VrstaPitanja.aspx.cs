using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Autoskola.Data;


namespace Autoskola.Web.forms.instruktor
{
    public partial class novaVrstaPitanja1 : System.Web.UI.Page
    {
        public GrupePitanja gP { get; set; }

        public int GrupaPitanjaId
        {
            get { return (int)ViewState["GrupaPitanjaId"]; }
            set { ViewState["GrupaPitanjaId"] = value; }
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
                        GrupaPitanjaId = Convert.ToInt32(Request["id"]);
                        gP = DAVrstePitanja.Select_ById(GrupaPitanjaId);
                        if (gP != null)
                        {
                            heading_top.InnerHtml = gP.Naziv + "<small>pregled pitanja</small>";
                            BindGrid();
                            LinkButton_DodajPitanje.PostBackUrl = "/instruktor/novopitanje?id=" + GrupaPitanjaId.ToString();
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

        private void BindGrid()
        {
            if (GrupaPitanjaId > 0)
            {
                List<Pitanja> temp = DAPitanja.Select(GrupaPitanjaId);

                if (temp != null)
                {
                    for (int i = 0; i < temp.Count; i++)
                        temp[i].Slika = (i + 1).ToString();

                    Pitanja_Grid.DataSource = temp;
                    Pitanja_Grid.DataBind();
                }
            }
        }

        protected void Pitanja_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                Pitanja p = DAPitanja.Select_ById(Convert.ToInt32(e.CommandArgument));
                p.Status = 0;
                DAPitanja.Update(p);
                BindGrid();
            }
        }

        protected void Pitanja_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Pitanja_Grid.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}