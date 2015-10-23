using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Autoskola.Data;
using System.Drawing;
using Autoskola.Infrastructure.Misc;
using Autoskola.Data.Model;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaDodajPitanje : System.Web.UI.Page
    {
        public GrupePitanja gP { get; set; }
        public Pitanja novoPitanje { get; set; }
        public int GrupaPitanjaId
        {
            get
            {
                if (Convert.ToInt32(ViewState["GrupaPitanjaId"]) > 0 && ViewState["GrupaPitanjaId"] != null)
                    return (int)ViewState["GrupaPitanjaId"];
                else
                    return 0;
            }
            set { ViewState["GrupaPitanjaId"] = value; }
        }

        public int kategorijeBroj
        {
            get { return (int)ViewState["kategorijeBroj"]; }
            set { ViewState["kategorijeBroj"] = value; }
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
                    kategorijeBroj = 0;
                    if (Request["id"] != null)
                    {
                        GrupaPitanjaId = Convert.ToInt32(Request["id"]);
                        gP = DAVrstePitanja.Select_ById(GrupaPitanjaId);
                        if(gP != null)
                        {
                            heading_top.InnerHtml = gP.Naziv + " <small>dodavanje pitanja</small>";
                            kategorijeBroj++;
                            BindRepeater();
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

        private void BindRepeater()
        {
            Repeater1.DataSource = RepSource();
            Repeater1.DataBind();

            if (kategorijeBroj == 1)
                removeKat.Visible = false;
            else
                removeKat.Visible = true;
        }

        protected void Spasi_btn_Click(object sender, EventArgs e)
        {
            if (GrupaPitanjaId > 0)
            {
                if (string.IsNullOrEmpty(Pitanje_txt.Text) || string.IsNullOrEmpty(Odgovor1_txt.Text) || string.IsNullOrEmpty(Odgovor2_txt.Text))
                {
                    divdanger.InnerHtml = "<b>Upozorenje!</b> Pitanje, bar jedan tačan i jedan netačan odgovor su obavezna polja!";
                    divdanger.Attributes["class"] = "alert alert-danger alert-dismissible noMarginLeft";
                    Danger_div.Visible = true;
                    Success_div.Visible = false;
                }
                else
                {
                    List<int> kategorijeValidation = new List<int>();
                    foreach (RepeaterItem dataItem in Repeater1.Items)
                    {
                        kategorijeValidation.Add(Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue));
                    }

                    if (kategorijeValidation.Count != kategorijeValidation.Distinct().Count())
                    {
                        divdanger.InnerHtml = "<b>Upozorenje!</b> Pitanje ne može pripadati više puta u istu kategoriju!";
                        divdanger.Attributes["class"] = "alert alert-danger alert-dismissible noMarginLeft";
                        Danger_div.Visible = true;
                        Success_div.Visible = false;
                    }
                    else
                    {
                    if ((string.IsNullOrEmpty(Odgovor1_txt.Text) == false && Odgovor1_checkbox.Checked) || (string.IsNullOrEmpty(Odgovor2_txt.Text) == false && Odgovor2_checkbox.Checked)
                        || (string.IsNullOrEmpty(Odgovor3_txt.Text) == false && Odgovor3_checkbox.Checked) || (string.IsNullOrEmpty(Odgovor4_txt.Text) == false && Odgovor4_checkbox.Checked))
                    {
                        
                        try
                        {
                            novoPitanje = new Pitanja();
                            novoPitanje.Pitanje = Pitanje_txt.Text;
                            novoPitanje.GrupaPitanjaId = GrupaPitanjaId;
                            novoPitanje.DatumDodavanja = DateTime.Now;
                            novoPitanje.Status = 1;

                            int brojacMultichoice = 0;

                            if (Odgovor1_checkbox.Checked)
                                brojacMultichoice++;
                            if (Odgovor2_checkbox.Checked)
                                brojacMultichoice++;
                            if (Odgovor3_checkbox.Checked)
                                brojacMultichoice++;
                            if (Odgovor4_checkbox.Checked)
                                brojacMultichoice++;
                            if (Odgovor5_checkbox.Checked)
                                brojacMultichoice++;

                            if (brojacMultichoice > 1)
                                novoPitanje.Multichoice = 1;
                            else
                                novoPitanje.Multichoice = 0;

                            //dodavanje slike
                            if (Slike.HasFiles)
                                DodajSlike();

                            //dodavanje pitanja
                            int idPitanja = DAPitanja.Insert(novoPitanje);

                            //odgovori na pitanje
                            Odgovori noviOdgovor1 = new Odgovori();
                            Odgovori noviOdgovor2 = new Odgovori();
                            Odgovori noviOdgovor3;
                            Odgovori noviOdgovor4;
                            Odgovori noviOdgovor5;

                            noviOdgovor1.Odgovor = Odgovor1_txt.Text;
                            noviOdgovor1.PitanjeId = idPitanja;
                            if (Odgovor1_checkbox.Checked)
                                noviOdgovor1.Tacan = 1;
                            else
                                noviOdgovor1.Tacan = 0;
                            DAOdgovori.Insert(noviOdgovor1);

                            noviOdgovor2.Odgovor = Odgovor2_txt.Text;
                            noviOdgovor2.PitanjeId = idPitanja;
                            if (Odgovor2_checkbox.Checked)
                                noviOdgovor2.Tacan = 1;
                            else
                                noviOdgovor2.Tacan = 0;
                            DAOdgovori.Insert(noviOdgovor2);

                            if (Odgovor3_txt.Text.Count() > 1)
                            {
                                noviOdgovor3 = new Odgovori();
                                noviOdgovor3.Odgovor = Odgovor3_txt.Text;
                                noviOdgovor3.PitanjeId = idPitanja;
                                if (Odgovor3_checkbox.Checked)
                                    noviOdgovor3.Tacan = 1;
                                else
                                    noviOdgovor3.Tacan = 0;
                                DAOdgovori.Insert(noviOdgovor3);
                            }

                            if (Odgovor4_txt.Text.Count() > 1)
                            {
                                noviOdgovor4 = new Odgovori();
                                noviOdgovor4.Odgovor = Odgovor4_txt.Text;
                                noviOdgovor4.PitanjeId = idPitanja;
                                if (Odgovor4_checkbox.Checked)
                                    noviOdgovor4.Tacan = 1;
                                else
                                    noviOdgovor4.Tacan = 0;
                                DAOdgovori.Insert(noviOdgovor4);
                            }

                            if (Odgovor5_txt.Text.Count() > 1)
                            {
                                noviOdgovor5 = new Odgovori();
                                noviOdgovor5.Odgovor = Odgovor5_txt.Text;
                                noviOdgovor5.PitanjeId = idPitanja;
                                if (Odgovor5_checkbox.Checked)
                                    noviOdgovor5.Tacan = 1;
                                else
                                    noviOdgovor5.Tacan = 0;
                                DAOdgovori.Insert(noviOdgovor5);
                            }

                            foreach (RepeaterItem dataItem in Repeater1.Items)
                            {
                                PitanjaKategorije pk = new PitanjaKategorije();
                                pk.KategorijaId = Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue);
                                pk.PitanjeId = idPitanja;
                                DAPitanjaKategorije.Insert(pk);
                            }

                            Danger_div.Visible = false;
                            Success_div.Visible = true;
                        }
                        catch (Exception)
                        {
                            divdanger.InnerHtml = "<b>Upozorenje!</b> Pitanje, bar jedan tačan i jedan netačan odgovor su obavezna polja!";
                            divdanger.Attributes["class"] = "alert alert-danger alert-dismissible noMarginLeft";
                            Success_div.Visible = false;
                            Danger_div.Visible = true;
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
            else
            {

            }
        }

        private void DodajSlike()
        {

            try
            {
                for (int i = 0; i < Slike.PostedFiles.Count; i++)
                {
                    byte[] slika = new byte[Slike.PostedFiles[i].ContentLength];
                    Slike.PostedFiles[i].InputStream.Read(slika, 0, Slike.PostedFiles[i].ContentLength);

                    System.Drawing.Image image = MyHelper.ByteArrayToImage(slika);
                    // s.Thumbnail = Slike.PostedFiles[i].FileName;

                    Bitmap bmp = image as Bitmap;
                    Graphics g = Graphics.FromImage(bmp);
                    Bitmap bmpNew = new Bitmap(bmp);
                    g.DrawImage(bmpNew, new Point(0, 0));
                    g.Dispose();
                    bmp.Dispose();
                    image.Dispose();

                    //s.Thumbnail = "/Slike/Galerija/Thumbnail/" + Slike.PostedFiles[i].FileName;
                    //MyHelper.CompressImage(MyHelper.EditImage_v2(bmpNew, new Size(622, 400)), 75, s.Thumbnail);

                    novoPitanje.Slika = "/Slike/Pitanja/" + Slike.PostedFiles[i].FileName;

                    MyHelper.CompressImage(bmpNew, 75, novoPitanje.Slika);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private List<string> RepSource()
        {
            List<string> rep = new List<string>();
            for (int i = 0; i <kategorijeBroj; i++)
            {
                rep.Add(i.ToString());
            }
            return rep;
        }

        private void FillDropdown(DropDownList ddl)
        {
            ddl.DataTextField = "Naziv";
            ddl.DataValueField = "KategorijaId";
            ddl.DataSource = DAKategorije.SelectAll();
            ddl.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem i = e.Item;
            DropDownList ddl = i.FindControl("kategorijeList") as DropDownList;
            FillDropdown(ddl);
        }

        protected void addList_Click(object sender, EventArgs e)
        {
            kategorijeBroj++;
            BindRepeater();
        }

        protected void Reset_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/instruktor/vrstapitanja?id=" + GrupaPitanjaId.ToString());
        }

        protected void removeKat_Click(object sender, EventArgs e)
        {
            if(kategorijeBroj > 1)
            {
                kategorijeBroj--;
                BindRepeater();
            }
        }

    }


}