using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autoskola.Data;

namespace Autoskola.Web.forms.kandidat
{
    public partial class ucenje : System.Web.UI.Page
    {
        public Pitanja pitanje
        {
            get { return (Pitanja)Session["pitanje"]; }
            set { Session["pitanje"] = value; }
        }
        public List<int> listaIDPitanja
        {
            get { return (List<int>)Session["listaIDPitanja"]; }
            set { Session["listaIDPitanja"] = value; }
        }

        public List<Odgovori> listaOdgovora
        {
            get { return (List<Odgovori>)Session["listaOdgovora"]; }
            set { Session["listaOdgovora"] = value; }
        }

        public int brojac
        {
            get { return (int)Session["brojac"]; }
            set { Session["brojac"] = value; }
        }
        public int kandidatID
        {
            get
            {
                if (Convert.ToInt32(Session["kandidatID"]) > 0 && Session["kandidatID"] != null)
                    return (int)Session["kandidatID"];
                else
                    return 0;
            }
            set { Session["kandidatID"] = value; }
        }
        public int ucenje_KategorijaID
        {
            get { return (int)Session["ucenje_KategorijaID"]; }
            set { Session["ucenje_KategorijaID"] = value; }
        }

        public KategorijePrijave kandidat_pregledKategorijaPrijave
        {
            get { return (KategorijePrijave)Session["kandidat_pregledKategorijaPrijave"]; }
            set { Session["kandidat_pregledKategorijaPrijave"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && kandidatID > 0)
            {
                if (!IsPostBack)
                {
                    if (ucenje_KategorijaID > 0)
                    {
                        brojac = 0;
                        listaIDPitanja = DAPitanja.Select_AllPitanjaInCategory(ucenje_KategorijaID);
                        if (listaIDPitanja.Count > 0)
                            IspisiPitanje();
                    }
                    else
                        Response.Redirect("/kandidat/404");
                }
            }
            else
                Response.Redirect("/prijava");
        }

        private void BindPitanje()
        {
            if(listaIDPitanja.Count > 0 && brojac < listaIDPitanja.Count)
            {
                pitanje = DAPitanja.Select_ById(listaIDPitanja[brojac]);
                if (pitanje != null)
                {
                    listaOdgovora = DAOdgovori.Select_ByPitanje(listaIDPitanja[brojac]);
                }
                brojac++;
            }
        }

        protected void IspisiPitanje()
        {
                BindPitanje();
                txt_pitanje.InnerText = pitanje.Pitanje;

                if (pitanje.Slika != null)
                {
                    imageQuestion.ImageUrl = pitanje.Slika;
                    Image_row.Attributes.Add("style", "");
                }
                else
                {
                    Image_row.Attributes.Add("style", "display:none");
                }

                if (listaOdgovora.Count > 0)
                {
                    //prvi odgovor
                    lbl_odgovor1.Text = listaOdgovora[0].Odgovor;

                    if (listaOdgovora[0].Tacan == 1)
                    {
                        lbl_tacan1.Visible = true;
                        td_Odg1.Attributes.Add("class", "form-group has-success");
                        lbl_odgovor1.Attributes.Add("style", "margin-top:0px");
                    }
                    else
                    {
                        lbl_tacan1.Visible = false;
                        td_Odg1.Attributes.Add("class", "");
                        lbl_odgovor1.Attributes.Add("style", "margin-top:26px");
                    }


                    //drugi odgovor
                    lbl_odgovor2.Text = listaOdgovora[1].Odgovor;

                    if (listaOdgovora.Count >= 2)
                    {
                        if (listaOdgovora[1].Tacan == 1)
                        {
                            lbl_tacan2.Visible = true;
                            td_Odg2.Attributes.Add("class", "form-group has-success");
                            lbl_odgovor2.Attributes.Add("style", "margin-top:0px");
                        }
                        else
                        {
                            lbl_tacan2.Visible = false;
                            td_Odg2.Attributes.Add("class", "");
                            lbl_odgovor2.Attributes.Add("style", "margin-top:26px");
                        }
                    }

                    //treci odgovor
                    if (listaOdgovora.Count >= 3)
                    {
                        lbl_odgovor3.Text = listaOdgovora[2].Odgovor;
                        Odg3_Row.Attributes.Add("style", "");

                        if (listaOdgovora[2].Tacan == 1)
                        {
                            lbl_tacan3.Visible = true;
                            td_Odg3.Attributes.Add("class", "form-group has-success");
                            lbl_odgovor3.Attributes.Add("style", "margin-top:0px");
                        }
                        else
                        {
                            lbl_tacan3.Visible = false;
                            td_Odg3.Attributes.Add("class", "");
                            lbl_odgovor3.Attributes.Add("style", "margin-top:26px");
                        }
                    }
                    else
                    {
                        lbl_tacan3.Visible = false;
                        Odg3_Row.Attributes.Add("style", "display:none");
                    }

                    //cetvrti odgovor
                    if (listaOdgovora.Count >= 4)
                    {
                        lbl_odgovor4.Text = listaOdgovora[3].Odgovor;
                        Odg4_Row.Attributes.Add("style", "");

                        if (listaOdgovora[3].Tacan == 1)
                        {
                            lbl_tacan4.Visible = true;
                            td_Odg4.Attributes.Add("class", "form-group has-success");
                            lbl_odgovor4.Attributes.Add("style", "margin-top:0px");
                        }
                        else
                        {
                            lbl_tacan4.Visible = false;
                            td_Odg4.Attributes.Add("class", "");
                            lbl_odgovor4.Attributes.Add("style", "margin-top:26px");
                        }
                    }
                    else
                    {
                        lbl_tacan4.Visible = false;
                        Odg4_Row.Attributes.Add("style", "display:none");
                    }

                    //peti odgovor
                    if (listaOdgovora.Count >= 5)
                    {
                        lbl_odgovor5.Text = listaOdgovora[4].Odgovor;
                        Odg5_Row.Attributes.Add("style", "");

                        if (listaOdgovora[4].Tacan == 1)
                        {
                            lbl_tacan5.Visible = true;
                            td_Odg5.Attributes.Add("class", "form-group has-success");
                            lbl_odgovor5.Attributes.Add("style", "margin-top:0px; visibility:visible");
                        }
                        else
                        {
                            lbl_tacan5.Visible = false;
                            td_Odg5.Attributes.Add("class", "");
                            lbl_odgovor5.Attributes.Add("style", "margin-top:26px; visibility:visible");
                        }
                    }
                    else
                    {
                        lbl_tacan5.Visible = false;
                        Odg5_Row.Attributes.Add("style", "display:none");
                    }
                }
                UpdatePanel1.Update();
        }

        protected void btn_Next_Click(object sender, EventArgs e)
        {
            BindPitanje();

            if (brojac < listaIDPitanja.Count)
            {
                IspisiPitanje();
            }
            else
            {
                IspisiPitanje();
                btn_Next.Visible = false;
                btn_End.Visible = true;
            }

            UpdatePanel1.Update();

        }

        protected void btn_End_Click(object sender, EventArgs e)
        {
            Response.Redirect("/kandidat/prijavljena-kategorija?id=" + kandidat_pregledKategorijaPrijave.KategorijaPrijavaId.ToString());
        }


    }
}