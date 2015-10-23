using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Autoskola.Web.forms.kandidat
{
    public partial class TestiranjeFrm : System.Web.UI.Page
    {
        public Pitanja pitanje
        {
            get { return (Pitanja)Session["pitanje"]; }
            set { Session["pitanje"] = value; }
        }
        public int VrstaPitanjaBod
        {
            get { return (int)Session["VrstaPitanjaBod"]; }
            set { Session["VrstaPitanjaBod"] = value; }
        }
        public int brojTacnihOdgovora
        {
            get { return (int)Session["brojTacnihOdgovora"]; }
            set { Session["brojTacnihOdgovora"] = value; }
        }
        public int maxBodovi
        {
            get { return (int)Session["maxBodovi"]; }
            set { Session["maxBodovi"] = value; }
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
        public List<OdabraniOdgovori> listaOdabranihOdgovora
        {
            get { return (List<OdabraniOdgovori>)Session["listaOdabranihOdgovora"]; }
            set { Session["listaOdabranihOdgovora"] = value; }
        }
        public UradjeniTestovi trenutnaPriprema
        {
            get { return (UradjeniTestovi)Session["trenutnaPriprema"]; }
            set { Session["trenutnaPriprema"] = value; }
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
        public int provjera_KategorijaID
        {
            get { return (int)Session["provjera_KategorijaID"]; }
            set { Session["provjera_KategorijaID"] = value; }
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
                    if (provjera_KategorijaID > 0)
                    {
                        try
                        {
                            brojac = 0;
                            brojTacnihOdgovora = 0;
                            VrstaPitanjaBod = 0;
                            listaOdabranihOdgovora = new List<OdabraniOdgovori>();
                            maxBodovi = 0;
                            trenutnaPriprema = new UradjeniTestovi();
                            listaIDPitanja = DAPitanja.Select_PitanjaTestByCategory(provjera_KategorijaID);
                            if (listaIDPitanja.Count > 0)
                                IspisiPitanje();
                            trenutnaPriprema.KategorijaPrijavaId = kandidat_pregledKategorijaPrijave.KategorijaPrijavaId;
                            trenutnaPriprema.MaxBodovi = 0;
                            trenutnaPriprema.OsvojeniBodovi = 0;
                            trenutnaPriprema.OsvojeniProcenat = 0;
                            trenutnaPriprema.Polozeno = false;
                            trenutnaPriprema.PocetakTesta = DateTime.Now;
                            trenutnaPriprema.KrajTesta = DateTime.Now;
                            DAUradjeniTestovi.Insert(trenutnaPriprema);
                        }
                        catch (Exception)
                        {
                            Response.Redirect("/kandidat/404");
                        }
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
            if (listaIDPitanja.Count > 0 && brojac < listaIDPitanja.Count)
            {
                pitanje = DAPitanja.Select_ById(listaIDPitanja[brojac]);
                if (pitanje != null)
                {
                    listaOdgovora = DAOdgovori.Select_ByPitanje(listaIDPitanja[brojac]);
                    VrstaPitanjaBod = DAPitanja.GetBodove(pitanje.PitanjeId);
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
                row_Image.Attributes.Add("style", "");
            }
            else
            {
                row_Image.Attributes.Add("style", "display:none");
            }

            if(listaOdgovora.Count == 2)
            {
                check_Odg1.Visible = false; check_Odg1.Checked = false;
                check_Odg2.Visible = false; check_Odg2.Checked = false;
                check_Odg3.Visible = false; check_Odg3.Checked = false;
                check_Odg4.Visible = false; check_Odg4.Checked = false;
                check_Odg5.Visible = false; check_Odg5.Checked = false;
                radio_Odg1.Visible = true;  radio_Odg1.Checked = false;
                radio_Odg2.Visible = true;  radio_Odg2.Checked = false;
                row_Odg3.Attributes.Add("style", "display:none");
                row_Odg4.Attributes.Add("style", "display:none");
                row_Odg5.Attributes.Add("style", "display:none");

                if(listaOdgovora.Count > 0)
                {
                    //prvi odgovor radio/singlechoice
                    radio_Odg1.Text = "&nbsp;" + listaOdgovora[0].Odgovor;

                    //drugi odgovor radio/singlechoice
                    if(listaOdgovora.Count >= 1)
                        radio_Odg2.Text = "&nbsp;" + listaOdgovora[1].Odgovor;
                }
            }
            else if (listaOdgovora.Count > 2)
            {
                check_Odg1.Visible = true; check_Odg1.Checked = false;
                check_Odg2.Visible = true; check_Odg2.Checked = false;
                check_Odg3.Visible = true; check_Odg3.Checked = false;
                check_Odg4.Visible = true; check_Odg4.Checked = false;
                check_Odg5.Visible = true; check_Odg5.Checked = false;
                radio_Odg1.Visible = false; radio_Odg1.Checked = false;
                radio_Odg2.Visible = false; radio_Odg2.Checked = false;


                    //prvi odgovor multichoice
                check_Odg1.Text = "&nbsp;" + listaOdgovora[0].Odgovor;

                    //drugi odgovor multichoice
                    if (listaOdgovora.Count >= 2)
                        check_Odg2.Text = "&nbsp;" + listaOdgovora[1].Odgovor;

                    //treći odgovor multichoice
                    if (listaOdgovora.Count >= 3)
                    {
                        check_Odg3.Text = "&nbsp;" + listaOdgovora[2].Odgovor;
                        row_Odg3.Attributes.Add("style", "");
                    }
                    else
                        row_Odg3.Attributes.Add("style", "display:none");

                    //četvrti odgovor multichoice
                    if (listaOdgovora.Count >= 4)
                    {
                        check_Odg4.Text = "&nbsp;" + listaOdgovora[3].Odgovor;
                        row_Odg4.Attributes.Add("style", "");
                    }
                    else
                        row_Odg4.Attributes.Add("style", "display:none");

                    //peti odgovor multichoice
                    if (listaOdgovora.Count >= 5)
                    {
                        check_Odg5.Text = "&nbsp;" + listaOdgovora[4].Odgovor;
                        row_Odg5.Attributes.Add("style", "");
                    }
                    else
                        row_Odg5.Attributes.Add("style", "display:none");

            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reload_js('/js/plugins/iCheck/icheck.min.js');", true);
            UpdatePanel1.Update();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "reload_js('/js/plugins/iCheck/icheck.min.js');", true);
        }

        protected void btn_Next_Click(object sender, EventArgs e)
        {
            if((listaOdgovora.Count == 2 && (radio_Odg1.Checked || radio_Odg2.Checked)) || (listaOdgovora.Count > 2 && (check_Odg1.Checked || check_Odg2.Checked || check_Odg3.Checked || check_Odg4.Checked || check_Odg5.Checked)))
            {
                OdabraniOdgovori provjera = listaOdabranihOdgovora.Where(x => x.PitanjeId == pitanje.PitanjeId).FirstOrDefault();
                if(provjera == null)
                {
                    ProvjeriOdgovore();

                    if (brojac == listaIDPitanja.Count - 1)
                    {
                        IspisiPitanje();
                        btn_Next.Visible = false;
                        btn_End.Visible = true;
                    }
                    else if (brojac < listaIDPitanja.Count)
                    {
                        IspisiPitanje();
                    }

                    UpdatePanel1.Update();
                }
            }
        }

        private void ProvjeriOdgovore()
        {
            if (pitanje.Multichoice == 0 && listaOdgovora.Count == 2)
            {
                OdabraniOdgovori temp = new OdabraniOdgovori();
                temp.PitanjeId = pitanje.PitanjeId;
                temp.UradjeniTestId = trenutnaPriprema.UradjeniTestId;

                if (radio_Odg1.Checked)
                {
                    temp.OdgovorId = listaOdgovora[0].OdgovorId;
                    if (listaOdgovora[0].Tacan == 1)
                    {
                        temp.Bodovi = VrstaPitanjaBod;
                        brojTacnihOdgovora++;
                    }
                    else
                        temp.Bodovi = 0;
                }
                else if (radio_Odg2.Checked)
                {
                    temp.OdgovorId = listaOdgovora[1].OdgovorId;
                    if (listaOdgovora[1].Tacan == 1)
                    {
                        temp.Bodovi = VrstaPitanjaBod;
                        brojTacnihOdgovora++;
                    }
                    else
                        temp.Bodovi = 0;
                }

                maxBodovi += VrstaPitanjaBod;
                listaOdabranihOdgovora.Add(temp);
                DAOdabraniOdgovori.Insert(temp);
            }

            else if (pitanje.Multichoice == 0 && listaOdgovora.Count > 2)
            {
                List<OdabraniOdgovori> temp = new List<OdabraniOdgovori>();

                bool checkall = false;
                if ((listaOdgovora[0].Tacan == 1 && check_Odg1.Checked) || (listaOdgovora[0].Tacan == 0 && check_Odg1.Checked == false))
                    if ((listaOdgovora[1].Tacan == 1 && check_Odg2.Checked) || (listaOdgovora[1].Tacan == 0 && check_Odg2.Checked == false))
                        if ((listaOdgovora[2].Tacan == 1 && check_Odg3.Checked) || (listaOdgovora[2].Tacan == 0 && check_Odg3.Checked == false))
                        {
                            if (listaOdgovora.Count >= 4)
                            {
                                if ((listaOdgovora[3].Tacan == 1 && check_Odg4.Checked) || (listaOdgovora[3].Tacan == 0 && check_Odg4.Checked == false))
                                {
                                    if (listaOdgovora.Count >= 5)
                                    {
                                        if ((listaOdgovora[4].Tacan == 1 && check_Odg5.Checked) || (listaOdgovora[4].Tacan == 0 && check_Odg5.Checked == false))
                                            checkall = true;
                                    }
                                    else
                                        checkall = true;
                                }

                            }
                            else
                                checkall = true;
                        }

                if (checkall == true)
                {
                    brojTacnihOdgovora++;
                    if (check_Odg1.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[0].OdgovorId;
                        o.Bodovi = VrstaPitanjaBod;
                        temp.Add(o);
                    }
                    else if (check_Odg2.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[1].OdgovorId;
                        o.Bodovi = VrstaPitanjaBod;
                        temp.Add(o);
                    }
                    else if (check_Odg3.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[2].OdgovorId;
                        o.Bodovi = VrstaPitanjaBod;
                        temp.Add(o);
                    }
                    else if (check_Odg4.Checked && listaOdgovora.Count >= 4)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[3].OdgovorId;
                        o.Bodovi = VrstaPitanjaBod;
                        temp.Add(o);
                    }
                    else if (check_Odg5.Checked && listaOdgovora.Count >= 5)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[4].OdgovorId;
                        o.Bodovi = VrstaPitanjaBod;
                        temp.Add(o);
                    }
                }
                else
                {
                    if (check_Odg1.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[0].OdgovorId;
                        o.Bodovi = 0;
                        temp.Add(o);
                    }
                    if (check_Odg2.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[1].OdgovorId;
                        o.Bodovi = 0;
                        temp.Add(o);
                    }
                    if (check_Odg3.Checked)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[2].OdgovorId;
                        o.Bodovi = 0;
                        temp.Add(o);
                    }
                    if (check_Odg4.Checked && listaOdgovora.Count >= 4)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[3].OdgovorId;
                        o.Bodovi = 0;
                        temp.Add(o);
                    }
                    if (check_Odg5.Checked && listaOdgovora.Count >= 5)
                    {
                        OdabraniOdgovori o = new OdabraniOdgovori();
                        o.OdgovorId = listaOdgovora[4].OdgovorId;
                        o.Bodovi = 0;
                        temp.Add(o);
                    }

                }


                maxBodovi += VrstaPitanjaBod;
                foreach (OdabraniOdgovori o in temp)
                {
                    o.UradjeniTestId = trenutnaPriprema.UradjeniTestId;
                    o.PitanjeId = pitanje.PitanjeId;
                    listaOdabranihOdgovora.Add(o);
                    DAOdabraniOdgovori.Insert(o);
                }

            }


            else if (pitanje.Multichoice == 1 && listaOdgovora.Count > 2)
            {
                List<OdabraniOdgovori> temp = new List<OdabraniOdgovori>();

                bool checkall = false;
                if ((listaOdgovora[0].Tacan == 1 && check_Odg1.Checked) || (listaOdgovora[0].Tacan == 0 && check_Odg1.Checked == false))
                    if ((listaOdgovora[1].Tacan == 1 && check_Odg2.Checked) || (listaOdgovora[1].Tacan == 0 && check_Odg2.Checked == false))
                        if ((listaOdgovora[2].Tacan == 1 && check_Odg3.Checked) || (listaOdgovora[2].Tacan == 0 && check_Odg3.Checked == false))
                        {
                            if (listaOdgovora.Count >= 4)
                            {
                                if ((listaOdgovora[3].Tacan == 1 && check_Odg4.Checked) || (listaOdgovora[3].Tacan == 0 && check_Odg4.Checked == false))
                                {
                                    if (listaOdgovora.Count >= 5)
                                    {
                                        if ((listaOdgovora[4].Tacan == 1 && check_Odg5.Checked) || (listaOdgovora[4].Tacan == 0 && check_Odg5.Checked == false))
                                            checkall = true;
                                    }
                                    else
                                        checkall = true;
                                }

                            }
                            else
                                checkall = true;
                        }

                float bodovi = 0;
                if (checkall == true)
                {
                    brojTacnihOdgovora++;
                    int brTacnih = 0;
                    foreach (Odgovori o in listaOdgovora)
                    {
                        if (o.Tacan == 1)
                            brTacnih++;
                    }
                    bodovi = (float)VrstaPitanjaBod / (float)brTacnih;
                }

                if (check_Odg1.Checked)
                {
                    OdabraniOdgovori o = new OdabraniOdgovori();
                    o.OdgovorId = listaOdgovora[0].OdgovorId;
                    if (listaOdgovora[0].Tacan == 1)
                        o.Bodovi = bodovi;
                    else
                        o.Bodovi = 0;
                    temp.Add(o);
                }
                if (check_Odg2.Checked)
                {
                    OdabraniOdgovori o = new OdabraniOdgovori();
                    o.OdgovorId = listaOdgovora[1].OdgovorId;
                    if (listaOdgovora[1].Tacan == 1)
                        o.Bodovi = bodovi;
                    else
                        o.Bodovi = 0;
                    temp.Add(o);
                }
                if (check_Odg3.Checked)
                {
                    OdabraniOdgovori o = new OdabraniOdgovori();
                    o.OdgovorId = listaOdgovora[2].OdgovorId;
                    if (listaOdgovora[2].Tacan == 1)
                        o.Bodovi = bodovi;
                    else
                        o.Bodovi = 0;
                    temp.Add(o);
                }
                if (check_Odg4.Checked && listaOdgovora.Count >= 4)
                {
                    OdabraniOdgovori o = new OdabraniOdgovori();
                    o.OdgovorId = listaOdgovora[3].OdgovorId;
                    if (listaOdgovora[3].Tacan == 1)
                        o.Bodovi = bodovi;
                    else
                        o.Bodovi = 0;
                    temp.Add(o);
                }
                if (check_Odg5.Checked && listaOdgovora.Count >= 5)
                {
                    OdabraniOdgovori o = new OdabraniOdgovori();
                    o.OdgovorId = listaOdgovora[4].OdgovorId;
                    if (listaOdgovora[4].Tacan == 1)
                        o.Bodovi = bodovi;
                    else
                        o.Bodovi = 0;
                    temp.Add(o);
                }

                maxBodovi += VrstaPitanjaBod;
                foreach (OdabraniOdgovori o in temp)
                {
                    o.UradjeniTestId = trenutnaPriprema.UradjeniTestId;
                    o.PitanjeId = pitanje.PitanjeId;
                    listaOdabranihOdgovora.Add(o);
                    DAOdabraniOdgovori.Insert(o);
                }
            }

        }

        protected void btn_End_Click(object sender, EventArgs e)
        {
            //Posljedni odgovor
            ProvjeriOdgovore();

            //Update uradjene pripreme
            float osvojeniBodovi = 0;
            foreach (OdabraniOdgovori o in listaOdabranihOdgovora)
            {
                osvojeniBodovi += Convert.ToInt32(o.Bodovi);
            }

            if (osvojeniBodovi == (maxBodovi - 1))
                osvojeniBodovi++;

            trenutnaPriprema.KrajTesta = DateTime.Now;
            trenutnaPriprema.OsvojeniProcenat = ((float)osvojeniBodovi / (float)maxBodovi) * 100;
            trenutnaPriprema.MaxBodovi = maxBodovi;
            trenutnaPriprema.OsvojeniBodovi = Convert.ToInt32(osvojeniBodovi);

            if (trenutnaPriprema.OsvojeniProcenat >= 90)
                trenutnaPriprema.Polozeno = true;
            else
                trenutnaPriprema.Polozeno = false;

            DAUradjeniTestovi.Update(trenutnaPriprema);

            //Update spremnosti
            List<UradjeniTestovi> testovi = DAUradjeniTestovi.SelectByKategorijePrijaveId(kandidat_pregledKategorijaPrijave.KategorijaPrijavaId);

            double brojnik = 0, nazivnik = 0;
            foreach (UradjeniTestovi u in testovi)
            {
                brojnik = brojnik + (u.OsvojeniProcenat / 100) * u.MaxBodovi;
                nazivnik = nazivnik + u.MaxBodovi;
            }

            kandidat_pregledKategorijaPrijave.Spremnost = (brojnik / nazivnik) * 100;
            DAKategorijePrijave.UpdateSpremnost(kandidat_pregledKategorijaPrijave);

            //Prikaz rezultata
            Response.Redirect("/kandidat/provjera-znanja/rezultat");
        }

        protected void radio_Odg1_CheckedChanged(object sender, EventArgs e)
        {
            radio_Odg2.Checked = false;
            UpdatePanel1.Update();
        }

        protected void radio_Odg2_CheckedChanged(object sender, EventArgs e)
        {
            radio_Odg1.Checked = false;
            UpdatePanel1.Update();
        }


    }
}