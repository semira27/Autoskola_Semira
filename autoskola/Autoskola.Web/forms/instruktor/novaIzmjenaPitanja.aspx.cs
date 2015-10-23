using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Autoskola.Data;
using Autoskola.Infrastructure.Misc;
using System.Drawing;

namespace Autoskola.Web.forms.instruktor
{
    public partial class novaIzmjenaPitanja : System.Web.UI.Page
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

        public Pitanja novoPitanje
        {
            get
            {
                return (Pitanja)Session["novoPitanje"] ?? new Pitanja();
            }
            set
            {
                Session["novoPitanje"] = value;
            }
        }
        public Pitanja updatePitanje
        {
            get
            {
                return (Pitanja)Session["updatePitanje"] ?? new Pitanja();
            }
            set
            {
                Session["updatePitanje"] = value;
            }
        }
        public List<Odgovori> listaOdgovora
        {
            get
            {
                return (List<Odgovori>)Session["listaOdgovora"] ?? new List<Odgovori>();
            }
            set
            {
                Session["listaOdgovora"] = value;
            }
        }

        public List<int> listaPitanjaKategorijeRemove
        {
            get
            {
                return (List<int>)Session["listaPitanjaKategorijeRemove"] ?? new List<int>();
            }
            set
            {
                Session["listaPitanjaKategorijeRemove"] = value;
            }
        }

        public int PitanjeId
        {
            get { return (int)ViewState["PitanjeId"]; }
            set { ViewState["PitanjeId"] = value; }
        }

        public int kategorijeBroj
        {
            get { return (int)ViewState["kategorijeBroj"]; }
            set { ViewState["kategorijeBroj"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated && instruktorID > 0)
            {
                if (!IsPostBack)
                {
                    kategorijeBroj = 0;
                    listaPitanjaKategorijeRemove = new List<int>();
                    if (Request["id"] != null)
                    {
                        PitanjeId = Convert.ToInt32(Request["id"]);
                        novoPitanje = DAPitanja.Select_ById(PitanjeId);
                        if (novoPitanje != null && novoPitanje.PitanjeId > 0)
                        {
                            try
                            {
                                Pitanje_txt.Text = novoPitanje.Pitanje;

                                listaOdgovora = DAOdgovori.Select_ByPitanje(novoPitanje.PitanjeId);

                                int count = listaOdgovora.Count;

                                if (count >= 1)
                                {
                                    Odgovor1_txt.Text = listaOdgovora[0].Odgovor;
                                    if (listaOdgovora[0].Tacan == 1)
                                        Odgovor1_checkbox.Checked = true;
                                }
                                if (count >= 2)
                                {
                                    Odgovor2_txt.Text = listaOdgovora[1].Odgovor;
                                    if (listaOdgovora[1].Tacan == 1)
                                        Odgovor2_checkbox.Checked = true;
                                }
                                if (count >= 3)
                                {
                                    Odgovor3_txt.Text = listaOdgovora[2].Odgovor;
                                    if (listaOdgovora[2].Tacan == 1)
                                        Odgovor3_checkbox.Checked = true;
                                }
                                if (count >= 4)
                                {
                                    Odgovor4_txt.Text = listaOdgovora[3].Odgovor;
                                    if (listaOdgovora[3].Tacan == 1)
                                        Odgovor4_checkbox.Checked = true;
                                }
                                if (count >= 5)
                                {
                                    Odgovor5_txt.Text = listaOdgovora[4].Odgovor;
                                    if (listaOdgovora[4].Tacan == 1)
                                        Odgovor5_checkbox.Checked = true;
                                }

                                kategorijeBroj = novoPitanje.PitanjaKategorije.Count;
                                BindDdlKategorije();
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

        private void BindDdlKategorije()
        {
            int i = 0;
            Repeater2.DataSource = RepSource();
            Repeater2.DataBind();

            if(novoPitanje.PitanjaKategorije.Count() > 0)
            {
                foreach (RepeaterItem dataItem in Repeater2.Items)
                {
                    if (i < novoPitanje.PitanjaKategorije.Count())
                    {
                        ((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue = novoPitanje.PitanjaKategorije[i].KategorijaId.ToString();
                        ((Button)dataItem.FindControl("removeKat")).CommandArgument = novoPitanje.PitanjaKategorije[i].PitanjaKategorijaId.ToString();
                        i++;
                    }
                }
            }

            UpdatePanel1.Update();
        }

        protected void Spasi_btn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Pitanje_txt.Text) || string.IsNullOrEmpty(Odgovor1_txt.Text) || string.IsNullOrEmpty(Odgovor2_txt.Text))
            {
                dangerdiv.InnerHtml = "<b>Upozorenje!</b> Pitanje, bar jedan tačan i jedan netačan odgovor su obavezna polja!";
                dangerdiv.Attributes["class"] = "alert alert-danger";
                Danger_div.Visible = true;
                Success_div.Visible = false;
            }
            else
            {
                List<int> kategorijeValidation = new List<int>();
                foreach (RepeaterItem dataItem in Repeater2.Items)
                {
                    kategorijeValidation.Add(Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue));
                }

                if (kategorijeValidation.Count != kategorijeValidation.Distinct().Count())
                {
                    dangerdiv.InnerHtml = "<b>Upozorenje!</b> Pitanje ne može pripadati više puta u istu kategoriju!";
                    dangerdiv.Attributes["class"] = "alert alert-danger";
                    Danger_div.Visible = true;
                    Success_div.Visible = false;
                }
                else
                {
                    if ((string.IsNullOrEmpty(Odgovor1_txt.Text) == false && Odgovor1_checkbox.Checked) || (string.IsNullOrEmpty(Odgovor2_txt.Text) == false && Odgovor2_checkbox.Checked)
                        || (string.IsNullOrEmpty(Odgovor3_txt.Text) == false && Odgovor3_checkbox.Checked) || (string.IsNullOrEmpty(Odgovor4_txt.Text) == false && Odgovor4_checkbox.Checked) || (string.IsNullOrEmpty(Odgovor5_txt.Text) == false && Odgovor5_checkbox.Checked))
                    {

                        try
                        {
                            int brojac = 0;

                            novoPitanje.Pitanje = Pitanje_txt.Text;

                            if (novoPitanje.Multichoice == 0)
                            { 
                                if (string.IsNullOrEmpty(Odgovor3_txt.Text) == false || string.IsNullOrEmpty(Odgovor4_txt.Text) == false || string.IsNullOrEmpty(Odgovor5_txt.Text) == false)
                                novoPitanje.Multichoice = 1;
                            }


                            //dodavanje slike
                            if (Slike.HasFiles)
                                DodajSlike();

                            //update pitanja
                            DAPitanja.Update(novoPitanje);

                            //odgovori na pitanje
                            Odgovori noviOdgovor3;
                            Odgovori noviOdgovor4;
                            Odgovori noviOdgovor5;

                            listaOdgovora[0].Odgovor = Odgovor1_txt.Text;
                            if (Odgovor1_checkbox.Checked)
                                listaOdgovora[0].Tacan = 1;
                            else
                                listaOdgovora[0].Tacan = 0;
                            DAOdgovori.Update(listaOdgovora[0]);

                            listaOdgovora[1].Odgovor = Odgovor2_txt.Text;
                            if (Odgovor2_checkbox.Checked)
                                listaOdgovora[1].Tacan = 1;
                            else
                                listaOdgovora[1].Tacan = 0;
                            DAOdgovori.Update(listaOdgovora[1]);

                            if (Odgovor3_txt.Text.Count() > 1)
                            {
                                if(listaOdgovora.Count() >= 3)
                                {
                                    listaOdgovora[2].Odgovor = Odgovor3_txt.Text;
                                    if (Odgovor3_checkbox.Checked)
                                        listaOdgovora[2].Tacan = 1;
                                    else
                                        listaOdgovora[2].Tacan = 0;
                                    DAOdgovori.Update(listaOdgovora[2]);
                                }
                                else
                                {
                                    noviOdgovor3 = new Odgovori();
                                    noviOdgovor3.Odgovor = Odgovor3_txt.Text;
                                    noviOdgovor3.PitanjeId = novoPitanje.PitanjeId;
                                    if (Odgovor3_checkbox.Checked)
                                        noviOdgovor3.Tacan = 1;
                                    else
                                        noviOdgovor3.Tacan = 0;
                                    DAOdgovori.Insert(noviOdgovor3);
                                }
                            }
                            else
                            {
                                if (listaOdgovora.Count() >= 3)
                                {
                                    Odgovor3_txt.Text = listaOdgovora[2].Odgovor;
                                    if (listaOdgovora[2].Tacan == 1)
                                        Odgovor3_checkbox.Checked = true;
                                    else
                                        Odgovor3_checkbox.Checked = false;

                                    dangerdiv.InnerHtml = "<b>Upozorenje!</b> Nije moguće brisati već postojeće odgovore na pitanje!";
                                    dangerdiv.Attributes["class"] = "alert alert-danger";
                                    Success_div.Visible = false;
                                    Danger_div.Visible = true;
                                    brojac++;
                                }
                            }

                            if (Odgovor4_txt.Text.Count() > 1)
                            {
                                if (listaOdgovora.Count() >= 4)
                                {
                                    listaOdgovora[3].Odgovor = Odgovor4_txt.Text;
                                    if (Odgovor4_checkbox.Checked)
                                        listaOdgovora[3].Tacan = 1;
                                    else
                                        listaOdgovora[3].Tacan = 0;
                                    DAOdgovori.Update(listaOdgovora[3]);
                                }
                                else
                                {
                                    noviOdgovor4 = new Odgovori();
                                    noviOdgovor4.Odgovor = Odgovor4_txt.Text;
                                    noviOdgovor4.PitanjeId = novoPitanje.PitanjeId;
                                    if (Odgovor4_checkbox.Checked)
                                        noviOdgovor4.Tacan = 1;
                                    else
                                        noviOdgovor4.Tacan = 0;
                                    DAOdgovori.Insert(noviOdgovor4);
                                }
                            }
                            else
                            {
                                if (listaOdgovora.Count() >= 4)
                                {
                                    Odgovor4_txt.Text = listaOdgovora[3].Odgovor;
                                    if (listaOdgovora[3].Tacan == 1)
                                        Odgovor4_checkbox.Checked = true;
                                    else
                                        Odgovor4_checkbox.Checked = false;

                                    dangerdiv.InnerHtml = "<b>Upozorenje!</b> Nije moguće brisati već postojeće odgovore na pitanje!";
                                    dangerdiv.Attributes["class"] = "alert alert-danger";
                                    Success_div.Visible = false;
                                    Danger_div.Visible = true;
                                    brojac++;
                                }
                            }

                            if (Odgovor5_txt.Text.Count() > 1)
                            {
                                if (listaOdgovora.Count() >= 5)
                                {
                                    listaOdgovora[4].Odgovor = Odgovor5_txt.Text;
                                    if (Odgovor5_checkbox.Checked)
                                        listaOdgovora[4].Tacan = 1;
                                    else
                                        listaOdgovora[4].Tacan = 0;
                                    DAOdgovori.Update(listaOdgovora[4]);
                                }
                                else
                                {
                                    noviOdgovor5 = new Odgovori();
                                    noviOdgovor5.Odgovor = Odgovor5_txt.Text;
                                    noviOdgovor5.PitanjeId = novoPitanje.PitanjeId;
                                    if (Odgovor5_checkbox.Checked)
                                        noviOdgovor5.Tacan = 1;
                                    else
                                        noviOdgovor5.Tacan = 0;
                                    DAOdgovori.Insert(noviOdgovor5);
                                }
                            }
                            else
                            {
                                if (listaOdgovora.Count() >= 5)
                                {
                                    Odgovor5_txt.Text = listaOdgovora[4].Odgovor;
                                    if (listaOdgovora[4].Tacan == 1)
                                        Odgovor5_checkbox.Checked = true;
                                    else
                                        Odgovor5_checkbox.Checked = false;

                                    dangerdiv.InnerHtml = "<b>Upozorenje!</b> Nije moguće brisati već postojeće odgovore na pitanje!";
                                    dangerdiv.Attributes["class"] = "alert alert-danger";
                                    Success_div.Visible = false;
                                    Danger_div.Visible = true;
                                    brojac++;
                                }
                                
                            }

                            //brisanje kategorija

                            foreach (int pitkat in listaPitanjaKategorijeRemove)
                            {
                                DAPitanjaKategorije.Delete(pitkat);
                            }

                            listaPitanjaKategorijeRemove.Clear();

                            //kategorije
                            foreach (RepeaterItem dataItem in Repeater2.Items)
                            {
                                bool pronadjeno = false;

                                foreach (PitanjaKategorije p in novoPitanje.PitanjaKategorije)
                                {
                                    if(p.KategorijaId == Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue))
                                        pronadjeno = true;
                                }

                                if(pronadjeno == false)
                                {
                                    PitanjaKategorije pk = new PitanjaKategorije();
                                    pk.KategorijaId = Convert.ToInt32(((DropDownList)dataItem.FindControl("kategorijeList")).SelectedValue);
                                    pk.PitanjeId = novoPitanje.PitanjeId;
                                    DAPitanjaKategorije.Insert(pk);
                                    novoPitanje.PitanjaKategorije.Add(pk);
                                }
                            }

                            if(brojac == 0)
                            {
                                Danger_div.Visible = false;
                                Success_div.Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                            dangerdiv.InnerHtml = "<b>Upozorenje!</b> Desila se greška!";
                            dangerdiv.Attributes["class"] = "alert alert-danger";
                            Success_div.Visible = false;
                            Danger_div.Visible = true;
                        }

                    }
                    else
                    {
                        dangerdiv.InnerHtml = "<b>Upozorenje!</b> Pitanje, minimalno jedna kategorija, minimalno jedan tačan i jedan netačan odgovor su obavezna polja!";
                        dangerdiv.Attributes["class"] = "alert alert-danger";
                        Danger_div.Visible = true;
                        Success_div.Visible = false;
                    }
                }
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
                    //novoPitanje.Thumbnail = Slike.PostedFiles[i].FileName;

                    Bitmap bmp = image as Bitmap;
                    Graphics g = Graphics.FromImage(bmp);
                    Bitmap bmpNew = new Bitmap(bmp);
                    g.DrawImage(bmpNew, new Point(0, 0));
                    g.Dispose();
                    bmp.Dispose();
                    image.Dispose();
                    //MyHelper.CompressImage(MyHelper.EditImage_v2(bmpNew, new Size(400, 300)), 75, novoPitanje.Thumbnail);
                    updatePitanje.Slika = "/Slike/Pitanja/" + Slike.PostedFiles[i].FileName;
                    MyHelper.CompressImage(bmpNew, 75, updatePitanje.Slika);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private List<string> RepSource()
        {
            List<string> rep = new List<string>();
            for (int i = 0; i < kategorijeBroj; i++)
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
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem i = e.Item;
            DropDownList ddl = i.FindControl("kategorijeList") as DropDownList;
            FillDropdown(ddl);
        }

        protected void addList_Click(object sender, EventArgs e)
        {
            kategorijeBroj++;
            BindDdlKategorije();
        }

        protected void removeKat_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;

            if (kategorijeBroj > 1)
            {
                if (id.Count() > 0)
                {
                    PitanjaKategorije removeItem = new PitanjaKategorije();

                    foreach (PitanjaKategorije p in novoPitanje.PitanjaKategorije)
                    {
                        if (id == p.PitanjaKategorijaId.ToString())
                        {
                            removeItem = p;
                        }
                    }

                    novoPitanje.PitanjaKategorije.Remove(removeItem);
                    listaPitanjaKategorijeRemove.Add(removeItem.PitanjaKategorijaId);
                    kategorijeBroj--;
                }
                else
                    kategorijeBroj--;

                BindDdlKategorije();
            }
            else if (kategorijeBroj == 1) 
            {
                if (id.Count() > 0)
                {
                    PitanjaKategorije removeItem = new PitanjaKategorije();

                    foreach (PitanjaKategorije p in novoPitanje.PitanjaKategorije)
                    {
                        if (id == p.PitanjaKategorijaId.ToString())
                        {
                            removeItem = p;
                        }
                    }

                    novoPitanje.PitanjaKategorije.Remove(removeItem);
                    listaPitanjaKategorijeRemove.Add(removeItem.PitanjaKategorijaId);

                    BindDdlKategorije();
                }
            }
        }
    }
}