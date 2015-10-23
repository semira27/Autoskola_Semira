using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAKandidati
    {
        public static List<Korisnici> Select()
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Korisnici.Include("Grad").Where(x => x.Kandidat != null).ToList();

            }
        }

        public static int Count()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Kandidati.Count();

            }
        }



        public static void PromjeniStatus(int korisnikId)
        {
            using (dataContext dt = new dataContext())
            {
                Korisnici k = dt.Korisnici.Where(x => x.KorisnikId == korisnikId).FirstOrDefault();
                if (k.Aktivan == 0)
                    k.Aktivan = 1;
                else
                    k.Aktivan = 0;
                dt.SaveChanges();
            }
        }


        public static void PromjeniStatusPrijava(int prijavaId)
        {
            using (dataContext dt = new dataContext())
            {
                Prijave k = dt.Prijave.Where(x => x.PrijavaId == prijavaId).FirstOrDefault();
                if (k.Zavrseno == 0)
                    k.Zavrseno = 1;
                else
                    k.Zavrseno = 0;
                dt.SaveChanges();
            }
        }




        public static int CountPrijavljeneKategorije(int kandidatId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == kandidatId && x.Status == 1)
                    .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp).Count();
            }
        }

        public static int CountUradjenePripreme(int kandidatId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == kandidatId && x.Status == 1)
                    .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp)
                    .Join(dt.UradjeniTestovi, kp => kp.KategorijaPrijavaId, u => u.KategorijaPrijavaId, (kp, u) => u).Count();
            }
        }

        public static int CountPolozeneKategorije(int kandidatId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == kandidatId && x.Status == 1)
                    .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp)
                    .Join(dt.PolaganjeTestova, kp => kp.KategorijaPrijavaId, p => p.KategorijaPrijavaId, (kp, p) => p).Where(p => p.Polozeno == 1).Count();
            }
        }

        public static Korisnici SelectById(int korisnikId)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Korisnici.Include("Administrator").Include("Kandidat").Where(x => x.KorisnikId == korisnikId).FirstOrDefault();
            }
        }

        public static Kandidati SelectKandidatById(int kId)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Kandidati.Include("Korisnik").Where(x => x.KandidatId == kId).FirstOrDefault();
            }
        }

        public class Person
        {
            public Person()
            {
            }

            public String FullName { get; set; }
            public int InstruktorId { get; set; }
        }

        public static List<Person> SelectInstruktori()
        {



            using (dataContext dt = new dataContext())
            {

                var instruktori = (from i in dt.Korisnici
                                   where i.Instruktor != null
                                   select new Person()
                                   {
                                       FullName = i.Ime + " " + i.Prezime,
                                       InstruktorId = i.Instruktor.InstruktorId
                                   }).ToList();

                return instruktori;

                // return dt.Korisnici.Include("Instruktor").Where(x => x.Instruktor != null).OrderBy(x => x.Ime).ToList();
            }


        }

        public static Korisnici SelectInstruktoriByName(string name)
        {

            using (dataContext dt = new dataContext())
            {
                string[] rijeci = name.Split(' ');
                string tmp1 = rijeci[0];
                string tmp2 = rijeci[1];

                return dt.Korisnici.Include("Instruktor").Where(x => x.Ime == tmp1 && x.Prezime == tmp2).FirstOrDefault();
            }
        }




        public static Gradovi SelectGradByName(string name)
        {

            using (dataContext dt = new dataContext())
            {

                return dt.Gradovi.Where(x => x.Naziv == name).FirstOrDefault();
            }


        }


        public static Gradovi SelectGradById(int id)
        {

            using (dataContext dt = new dataContext())
            {

                return dt.Gradovi.Where(x => x.GradId == id).FirstOrDefault();
            }


        }
        public static int SelectMaxId()
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Kandidati.Max(x => x.KandidatId);
            }
        }

        public static int SelectPrijavaMax(int kandidatId)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Prijave.Where(x => x.KandidatId == kandidatId && x.Zavrseno == 0 ).Max(x => x.PrijavaId);
            }
        }


        public static void InsertKorisnik(Korisnici korisnik, Kandidati kandidat)
        {
            using (dataContext dt = new dataContext())
            {
                dt.Korisnici.Add(korisnik);
                dt.Kandidati.Add(kandidat);
                dt.SaveChanges();
            }
        }

        public static void InsertPrijava(Prijave p)
        {

            using (dataContext dt = new dataContext())
            {
                dt.Prijave.Add(p);
                dt.SaveChanges();
            }
        }


        public static Korisnici SelectKorisnikByKandidatId(int kandidatID)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Korisnici.Include("Kandidat").Include("Grad").Where(x => x.Kandidat.KandidatId == kandidatID).FirstOrDefault();
            }
        }

        public static void Update(Korisnici k)
        {
            using (dataContext dt = new dataContext())
            {
                Korisnici kor = dt.Korisnici.Where(x => x.KorisnikId == k.KorisnikId).FirstOrDefault();
                kor.Ime = k.Ime;
                kor.Prezime = k.Prezime;
                kor.Email = k.Email;
                kor.Adresa = k.Adresa;
                kor.DatumRodjenja = k.DatumRodjenja;
                kor.JMBG = k.JMBG;
                kor.KorisnickoIme = k.KorisnickoIme;
                kor.Telefon = k.Telefon;
                kor.LozinkaHash = k.LozinkaHash;

                dt.SaveChanges();

            }
        }



        public static List<Korisnici> Pretraga(string p1, string p2, string p3)
        {
            using (dataContext dt = new dataContext())
            {
                List<Korisnici> temp = new List<Korisnici>();
                Korisnici k = null;
                if (p1 != "" && p2 != "" && p3 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Ime == p1 && x.Prezime == p2 && x.Grad.Naziv == p3 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);

                    }
                    catch (Exception e)
                    { }
                }
                else if (p1 != "" && p2 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Ime == p1 && x.Prezime == p2 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);


                    }
                    catch (Exception e)
                    { }

                }
                else if (p1 != "" && p3 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Ime == p1 && x.Grad.Naziv == p3 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);
                    }
                    catch (Exception e)
                    { }

                }
                else if (p2 != "" && p3 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Grad.Naziv == p3 && x.Prezime == p2 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);

                    }
                    catch (Exception e)
                    { }

                }
                else if (p1 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Ime == p1 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);


                    }
                    catch (Exception e)
                    { }

                }
                else if (p2 != "")
                {
                    try
                    {
                        k = dt.Korisnici.Where(x => x.Prezime == p2 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);

                    }
                    catch (Exception e)
                    {
                        temp.Add(null);
                    }

                }
                else if (p3 != "")
                {


                    try
                    {
                        k = dt.Korisnici.Where(x => x.Grad.Naziv == p3 && x.Kandidat != null).FirstOrDefault();
                        if (k != null)
                            temp.Add(k);
                    }
                    catch (Exception e)
                    { }

                }


                if (temp == null)
                    temp = dt.Korisnici.Where(x => x.Kandidat != null).ToList();

                return temp;
            }
        }

        public static Prijave SelectPrijavaByKandidat(int korisnikId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == korisnikId && x.Zavrseno == 0).FirstOrDefault();
            }
        }

        


        public class PrijaveKandidati
        {
            public Prijave Prijava { get; set; }
            public Korisnici Korisnik { get; set; }
        }

        public static List<PrijaveKandidati> SelectPrijaveByKandidat(int id)
        {
            using (dataContext dt = new dataContext())
            {
                try
                {


                    var q = from p in dt.Prijave
                            join i in dt.Instruktori on p.InstruktorId equals i.InstruktorId
                            join k in dt.Korisnici on i.InstruktorId equals k.Instruktor.InstruktorId
                            where p.KandidatId == id
                            select new PrijaveKandidati()
                            {
                                Prijava = p,
                                Korisnik = k
                            };
                    return q.ToList();

                }
                catch (Exception)
                {
                    return null;
                }
            }

        }



        public static List<Korisnici> SelectByJMBG(string JMBG)
        {
            using (dataContext dt = new dataContext())
            {
                return (from k in dt.Korisnici
                        where k.JMBG == JMBG
                        select k).ToList();

            }
        }

        public static List<Korisnici> SelectByImePrezime(string tekst)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Korisnici.Include("Grad").Where(x => x.Ime == tekst || x.Prezime == tekst).ToList();

            }
        }

        public static List<Korisnici> SelectByKorisnickoIme(string korisnicko)
        {
            using (dataContext dt = new dataContext())
            {
                return (from k in dt.Korisnici
                        where k.KorisnickoIme == korisnicko
                        select k).ToList();

            }
        }

        public static List<Korisnici> SelectAllActive()
        {
            using (dataContext dt = new dataContext())
            {
                List < Korisnici > temp = dt.Korisnici.Where(x => x.Aktivan == 1 && x.Kandidat != null).ToList();

                Korisnici k = new Korisnici();
                k.Ime = "Odaberite kandidata";
                k.KorisnikId = 0;
                temp.Insert(0, k);

                return temp;
            }
        }

        public static int SelectKandidatIdByKorisnikId(int id)
        {
            using (dataContext dt = new dataContext())
            {
                Kandidati temp = dt.Kandidati.Where(x => x.Korisnik.KorisnikId == id && x.Korisnik.Aktivan == 1).FirstOrDefault();

                return temp.KandidatId;
            }
        }

        public static List<Korisnici> SelectKandidatePripreme()
        {
            using (dataContext dt = new dataContext())
            {
                List<Prijave> lista = dt.Prijave.Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1 && x.Zavrseno == 0).ToList();

                List<Korisnici> temp = new List<Korisnici>();

                foreach (Prijave p in lista)
                {
                    p.KategorijeUPrijavi.OrderByDescending(x => x.Spremnost);

                    for (int i = 0; i < p.KategorijeUPrijavi.Count; i++)
			        {
                            Korisnici k = DAKandidati.SelectById(p.KandidatId);
                            k.KorisnikId = p.KategorijeUPrijavi[i].KategorijaPrijavaId;
                            k.Email = Convert.ToInt32(p.KategorijeUPrijavi[i].Spremnost).ToString() + "%";
                            k.Telefon = p.KategorijeUPrijavi[i].Kategorije.Naziv;
                            temp.Add(k);
			        }
                }

                return temp;
            }
            
        }
    }

}

