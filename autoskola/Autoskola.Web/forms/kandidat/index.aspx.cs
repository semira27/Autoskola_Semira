using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoskola.Data;

namespace Autoskola.Web.forms.kandidat
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public Korisnici logirani_kandidat { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    int id = Convert.ToInt32(Session["korisnikID"]);
                    if (id > 0)
                    {
                        logirani_kandidat = DAKandidati.SelectById(id);
                    }
                }
            }
            else
                Response.Redirect("/prijava");
        }
    }
}