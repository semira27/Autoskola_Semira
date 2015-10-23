using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.instruktor
{
    public partial class index : System.Web.UI.Page
    {
        public List<int> listaPrijava
        {
            get { return (List<int>)ViewState["listaPrijava"]; }
            set { ViewState["listaPrijava"] = value; }
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

        public List<int> listaGodina
        {
            get { return (List<int>)ViewState["listaGodina"]; }
            set { ViewState["listaGodina"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        string godina = "2015";
                        listaPrijava = new List<int>();
                        listaGodina = new List<int>();
                        BindGodine(godina);
                        BindMonths(DateTime.Now.Year);
                        BindSkripta();
                    }
                    catch (Exception)
                    {
                        Response.Redirect("instruktor/404");
                    }
                }
            }
            else
                Response.Redirect("/prijava");

        }

        private void BindSkripta()
        {
            string skripta2 = "[['Januar', " + listaPrijava[0].ToString() + "],";
            skripta2 += "['Februar', " + listaPrijava[1].ToString() + "],";
            skripta2 += "['Mart', " + listaPrijava[2].ToString() + "],";
            skripta2 += "['April', " + listaPrijava[3].ToString() + "],";
            skripta2 += "['Maj', " + listaPrijava[4].ToString() + "],";
            skripta2 += "['Juni', " + listaPrijava[5].ToString() + "],";
            skripta2 += "['Juli', " + listaPrijava[6].ToString() + "],";
            skripta2 += "['August', " + listaPrijava[7].ToString() + "],";
            skripta2 += "['Septembar', " + listaPrijava[8].ToString() + "],";
            skripta2 += "['Oktobar', " + listaPrijava[9].ToString() + "],";
            skripta2 += "['Novembar', " + listaPrijava[10].ToString() + "],";
            skripta2 += "['Decembar', " + listaPrijava[11].ToString() + "]]";

            txtSkripta.Text = skripta2;

            UpdatePanel1.Update();

        }

        private void BindGodine(string g)
        {
            for (int i = DateTime.Now.Year; i >= Convert.ToInt32(g); i--)
            {
                listaGodina.Add(i);
                ListItem li = new ListItem();
                li.Text = i.ToString();
                li.Value = i.ToString();
                godineList.Items.Add(li);
            }
        }

        private void BindMonths(int year)
        {
            listaPrijava.Clear();
            for (int i = 1; i <= 12; i++)
            {
                listaPrijava.Add(DAPrijave.CountPrijaveIzMjesecaIGodine(i, year));
            }
        }


        protected void godineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMonths(Convert.ToInt32(godineList.SelectedValue));
            BindSkripta();
        }

    }
}