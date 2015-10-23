using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoskola.Data;
namespace Autoskola.Data
{
   public class DAInstruktori
    {

        public static List<Korisnici> Select()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Korisnici.Include("Instruktor").Where(x => x.Instruktor != null && x.Aktivan == 1).ToList();
            }
        }


        public static void Insert(Korisnici korisnik, Instruktori i)
        {

            using (dataContext dt = new dataContext())
            {
                dt.Korisnici.Add(korisnik);
                dt.Instruktori.Add(i);
                dt.SaveChanges();
            }
        }

        public static void Update(Korisnici k, Instruktori i)
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


                Instruktori inst = dt.Instruktori.Where(x => x.InstruktorId == i.InstruktorId).FirstOrDefault();
                //inst.KategorijeObuke = i.KategorijeObuke;
                //inst.SifraLicense = i.SifraLicense;
                dt.SaveChanges();

           }
        }

        public static Instruktori SelectByInstruktorId(int instruktorID)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Instruktori.Include("Korisnik").Where(x => x.InstruktorId == instruktorID).FirstOrDefault();
            }
        }

        public static Korisnici SelectKorisnikByInstruktorId(int instruktorID)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Korisnici.Include("Instruktor").Include("Grad").Where(x => x.Instruktor.InstruktorId == instruktorID).FirstOrDefault();
            }
        }

        public static Korisnici SelectById(int korisnikId)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Korisnici.Include("Instruktor").Where(x => x.KorisnikId == korisnikId).FirstOrDefault();
            }
        }
        public static List<Korisnici> SelectAllActive()
        {
            using (dataContext dt = new dataContext())
            {
                List < Korisnici > temp = dt.Korisnici.Where(x => x.Aktivan == 1 && x.Instruktor != null).ToList();

                Korisnici k = new Korisnici();
                k.Ime = "Odaberite instruktora";
                k.KorisnikId = 0;
                temp.Insert(0, k);

                return temp;
            }
        }

        public static int SelectInstruktorIdByKorisnikId(int id)
        {
            using (dataContext dt = new dataContext())
            {
                Instruktori temp = dt.Instruktori.Where(x => x.Korisnik.KorisnikId == id && x.Korisnik.Aktivan == 1).FirstOrDefault();

                return temp.InstruktorId;
            }
        }

        public static int CountPolozeneKategorije(int instruktorid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.InstruktorId == instruktorid && x.Status == 1)
                    .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp)
                    .Join(dt.PolaganjeTestova, kp => kp.KategorijaPrijavaId, p => p.KategorijaPrijavaId, (kp, p) => p).Where(p => p.Polozeno == 1).Count();
            }
        }

        public static int CountKandidate(int instruktorid)
        {
            using (dataContext dt = new dataContext())
            {
                List<int> lista = dt.Prijave.Where(x => x.InstruktorId == instruktorid && x.Status == 1).Select(x => x.KandidatId).ToList();

                lista = lista.Distinct().ToList();

                return lista.Count;
            }
        }

        public static int CountPrijavljeneKategorije(int instruktorid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.InstruktorId == instruktorid && x.Status == 1)
                    .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp).Count();
            }
        }
    }
}
