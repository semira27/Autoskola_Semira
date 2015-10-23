using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaVrstaPitanja : System.Web.UI.Page
    {
        public GrupePitanja novaVrsta { get; set; }
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

            }
            else
                Response.Redirect("/prijava");
        }


        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_Naziv.Text) || !string.IsNullOrWhiteSpace(txt_Broj.Text))
            {
                try
                {
                    novaVrsta = new GrupePitanja();
                    novaVrsta.Naziv = txt_Naziv.Text;
                    if (txt_Opis.Text.Count() > 0)
                        novaVrsta.Opis = txt_Opis.Text;
                    novaVrsta.PitanjeBod = Convert.ToInt32(txt_Broj.Text);
                    DAVrstePitanja.VrstePitanja_Insert(novaVrsta);
                    Success_div.Visible = true;
                    Danger_div.Visible = false;
                }
                catch (Exception)
                {
                    Danger_div.InnerText = "Desila se greška!";
                    Danger_div.Visible = true;
                    Success_div.Visible = false;
                }
            }
            else
            {
                Danger_div.Visible = true;
                Success_div.Visible = false;
            }
        }
    }
}