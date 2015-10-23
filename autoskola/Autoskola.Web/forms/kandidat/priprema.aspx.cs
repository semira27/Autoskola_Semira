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
    public partial class priprema : System.Web.UI.Page
    {
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

        public List<Pitanja> pregled_listaPitanja_uradjenaPriprema
        {
            get
            {
                return (List<Pitanja>)Session["pregled_listaPitanja_uradjenaPriprema"] ?? new List<Pitanja>();
            }
            set
            {
                Session["pregled_listaPitanja_uradjenaPriprema"] = value;
            }
        }
        public List<OdabraniOdgovori> pregled_listaOdabranihOdgovora_uradjenaPriprema
        {
            get
            {
                return (List<OdabraniOdgovori>)Session["pregled_listaOdabranihOdgovora_uradjenaPriprema"] ?? new List<OdabraniOdgovori>();
            }
            set
            {
                Session["pregled_listaOdabranihOdgovora_uradjenaPriprema"] = value;
            }
        }

        public UradjeniTestovi pregled_uradjenaPriprema
        {
            get
            {
                return (UradjeniTestovi)Session["pregled_uradjenaPriprema"] ?? new UradjeniTestovi();
            }
            set
            {
                Session["pregled_uradjenaPriprema"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.User.Identity.IsAuthenticated && kandidatID >  0)
            {
                if (!IsPostBack)
                {
                    if (Request["id"] != null)
                    {
                        int id = Convert.ToInt32(Request["id"]);
                        pregled_uradjenaPriprema = DAUradjeniTestovi.SelectByIdAndKandidatID(id, kandidatID);
                        if (pregled_uradjenaPriprema != null)
                        {
                            try
                            {
                                uspjehHeader.InnerText = "Uspjeh: " + (Convert.ToInt32(pregled_uradjenaPriprema.OsvojeniProcenat)).ToString() + "%";
                                pregled_listaOdabranihOdgovora_uradjenaPriprema = DAOdabraniOdgovori.SelectByUradjeniTestId(pregled_uradjenaPriprema.UradjeniTestId);
                                pregled_listaPitanja_uradjenaPriprema = new List<Pitanja>();

                                if (pregled_listaOdabranihOdgovora_uradjenaPriprema != null)
                                {
                                    for (int i = 0; i < pregled_listaOdabranihOdgovora_uradjenaPriprema.Count; i++)
                                    {
                                        if (pregled_listaOdabranihOdgovora_uradjenaPriprema[i].Pitanje != null)
                                        {
                                            if (!pregled_listaPitanja_uradjenaPriprema.Contains(pregled_listaOdabranihOdgovora_uradjenaPriprema[i].Pitanje))
                                                pregled_listaPitanja_uradjenaPriprema.Add(pregled_listaOdabranihOdgovora_uradjenaPriprema[i].Pitanje);
                                        }
                                    }

                                    BindGrid();
                                }
                            }
                            catch (Exception)
                            {
                                Response.Redirect("/kandidat/404");
                            }
                        }
                        else
                            Response.Redirect("/kandidat/404");
                    }
                    else
                        Response.Redirect("/kandidat/404");
                }
            }
            else
                Response.Redirect("/prijava");
        }

        private void BindGrid()
        {
            Pitanja_Grid.DataSource = pregled_listaPitanja_uradjenaPriprema;
            Pitanja_Grid.DataBind();
        }
        protected void Pitanja_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Pitanja_Grid.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Pitanja_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewPitanje")
            {
                int id_pitanje = Convert.ToInt32((e.CommandArgument));
                Pitanja p = pregled_listaPitanja_uradjenaPriprema.Where(x => x.PitanjeId == id_pitanje).FirstOrDefault();
                List<OdabraniOdgovori> odabraniOdg = pregled_listaOdabranihOdgovora_uradjenaPriprema.Where(x => x.PitanjeId == id_pitanje).ToList();

                txt_pitanje.InnerText = p.Pitanje;

                if (p.Slika != null)
                {
                    imageQuestion.Visible = true;
                    imageQuestion.ImageUrl = p.Slika;
                }
                else
                    imageQuestion.Visible = false;

                int brojac = 0;

                //odgovor 1
                lbl_odgovor1.Text = p.Odgovori[0].Odgovor;

                for (int i = 0; i < odabraniOdg.Count; i++)
                {
                    if (p.Odgovori[0].OdgovorId == odabraniOdg[i].OdgovorId)
                        brojac++;
                }
                if (brojac > 0)
                    td_Odabrano1.Visible = true;
                else
                    td_Odabrano1.Visible = false;

                if (p.Odgovori[0].Tacan == 1)
                {
                    lbl_tacan1.Visible = true;
                    td_Odg1.Attributes.Add("class", "form-group has-success");
                    lbl_odgovor1.Attributes.Add("style", "margin-top:0px");
                }
                else
                {
                    lbl_tacan1.Visible = false;
                    td_Odg1.Attributes.Add("class", "");
                    lbl_odgovor1.Attributes.Add("style", "margin-top:24px");
                }

                //drugi odgovor
                brojac = 0;
                lbl_odgovor2.Text = p.Odgovori[1].Odgovor;

                for (int i = 0; i < odabraniOdg.Count; i++)
                {
                    if (p.Odgovori[1].OdgovorId == odabraniOdg[i].OdgovorId)
                        brojac++;
                }
                if (brojac > 0)
                    td_Odabrano2.Visible = true;
                else
                    td_Odabrano2.Visible = false;

                if (p.Odgovori[1].Tacan == 1)
                {
                    lbl_tacan2.Visible = true;
                    td_Odg2.Attributes.Add("class", "form-group has-success");
                    lbl_odgovor2.Attributes.Add("style", "margin-top:0px");
                }
                else
                {
                    lbl_tacan2.Visible = false;
                    td_Odg2.Attributes.Add("class", "");
                    lbl_odgovor2.Attributes.Add("style", "margin-top:24px");
                }

                //treci odgovor

                if (p.Odgovori.Count >= 3)
                {
                    td_Odg3.Visible = true;
                    lbl_odgovor3.Text = p.Odgovori[2].Odgovor;

                    brojac = 0;
                    for (int i = 0; i < odabraniOdg.Count; i++)
                    {
                        if (p.Odgovori[2].OdgovorId == odabraniOdg[i].OdgovorId)
                            brojac++;
                    }
                    if (brojac > 0)
                        td_Odabrano3.Visible = true;
                    else
                        td_Odabrano3.Visible = false;

                    if (p.Odgovori[2].Tacan == 1)
                    {
                        lbl_tacan3.Visible = true;
                        td_Odg3.Attributes.Add("class", "form-group has-success");
                        lbl_odgovor3.Attributes.Add("style", "margin-top:0px");
                    }
                    else
                    {
                        lbl_tacan3.Visible = false;
                        td_Odg3.Attributes.Add("class", "");
                        lbl_odgovor3.Attributes.Add("style", "margin-top:24px");
                    }
                }
                else
                    td_Odg3.Visible = false;

                //cetvrti odgovor
                if (p.Odgovori.Count >= 4)
                {
                    td_Odg4.Visible = true;
                    lbl_odgovor4.Text = p.Odgovori[3].Odgovor;
                    brojac = 0;
                    for (int i = 0; i < odabraniOdg.Count; i++)
                    {
                        if (p.Odgovori[3].OdgovorId == odabraniOdg[i].OdgovorId)
                            brojac++;
                    }
                    if (brojac > 0)
                        td_Odabrano4.Visible = true;
                    else
                        td_Odabrano4.Visible = false;

                    if (p.Odgovori[3].Tacan == 1)
                    {
                        lbl_tacan4.Visible = true;
                        td_Odg4.Attributes.Add("class", "form-group has-success");
                        lbl_odgovor4.Attributes.Add("style", "margin-top:0px");
                    }
                    else
                    {
                        lbl_tacan4.Visible = false;
                        td_Odg4.Attributes.Add("class", "");
                        lbl_odgovor4.Attributes.Add("style", "margin-top:24px");
                    }
                }
                else
                    td_Odg4.Visible = false;

                //peti odgovor
                if (p.Odgovori.Count >= 5)
                {
                    td_Odg5.Visible = true;
                    lbl_odgovor5.Text = p.Odgovori[4].Odgovor;

                    brojac = 0;
                    for (int i = 0; i < odabraniOdg.Count; i++)
                    {
                        if (p.Odgovori[4].OdgovorId == odabraniOdg[i].OdgovorId)
                            brojac++;
                    }
                    if (brojac > 0)
                        td_Odabrano5.Visible = true;
                    else
                        td_Odabrano5.Visible = false;

                    if (p.Odgovori[4].Tacan == 1)
                    {
                        lbl_tacan5.Visible = true;
                        td_Odg5.Attributes.Add("class", "form-group has-success");
                        lbl_odgovor5.Attributes.Add("style", "margin-top:0px");
                    }
                    else
                    {
                        lbl_tacan5.Visible = false;
                        td_Odg5.Attributes.Add("class", "");
                        lbl_odgovor5.Attributes.Add("style", "margin-top:24px");
                    }
                }
                else
                    td_Odg5.Visible = false;

                UpdatePanel1.Update();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Test(1);", true);
            }
        }

        protected void Pitanja_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowIndex >= 0 && e.Row.RowIndex < pregled_listaPitanja_uradjenaPriprema.Count)
            {
                int pitanjeid = pregled_listaPitanja_uradjenaPriprema[e.Row.RowIndex].PitanjeId;
                float tacan = pregled_listaOdabranihOdgovora_uradjenaPriprema.Where(x => x.PitanjeId == pitanjeid && x.Bodovi > 0).Select(x => x.Bodovi).FirstOrDefault();

                if (e.Row.RowType == DataControlRowType.DataRow && tacan > 0)
                {
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).Attributes.Add("class", "btn btn-xs btn-success");
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).Attributes.Add("style", "width:auto");
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).InnerHtml = "<i class='fa fa-check'></i>";

                }
                else if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).Attributes.Add("class", "btn btn-xs btn-danger");
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).Attributes.Add("style", "width:24px");
                    ((System.Web.UI.HtmlControls.HtmlAnchor)e.Row.FindControl("statusPitanjeOdg")).InnerHtml = "<i class='fa fa-times'></i>";
                }
            }
        }
    }
}