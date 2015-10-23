using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAPrijave
    {
        public static List<Prijave> SelectAll()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }


        public static int CountPrijaveIzMjesecaIGodine(int month, int year)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.Status == 1 && x.DatumPrijave.Year == year && x.DatumPrijave.Month == month)
                       .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp).Count();
            }
        }


        public static List<Prijave> SelectByKandidatOrInstruktor(string imePrezime)
        {
            using (dataContext dt = new dataContext())
            {
                List<Prijave> firstResult, secondResult;
                firstResult = dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();

                secondResult = new List<Prijave>();
                foreach (Prijave p in firstResult)
                {
                    string iP_kandidat = p.Kandidat.Korisnik.Ime.ToLower() + " " + p.Kandidat.Korisnik.Prezime.ToLower();
                    string iP_instruktor = p.Instruktor.Korisnik.Ime.ToLower() + " " + p.Instruktor.Korisnik.Prezime.ToLower();

                    if (iP_kandidat.Contains(imePrezime) || iP_instruktor.Contains(imePrezime))
                        secondResult.Add(p);
                }

                return secondResult;
            
            }
        }

        public static List<Prijave> SelectActive()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Zavrseno == 0 && x.Status == 1).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();

            }
        }

        public static List<Prijave> SelectUnactive()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Zavrseno == 1 && x.Status == 1).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();

            }
        }

        public static List<Prijave> SelectDeleted()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 0).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }

        public static Prijave SelectById(int id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik.Grad").Where(x => x.PrijavaId == id).FirstOrDefault();
            }
        }

        public static List<Prijave> SelectByKandidatId(int kid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1 && x.Zavrseno == 0 && x.KandidatId == kid).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }

        public static List<Prijave> SelectByKandidatId_AktivneZavrsene(int kid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1 && x.KandidatId == kid).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }

        public static List<Prijave> SelectByKandidatId_Zavrsene(int kid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1 && x.Zavrseno == 1 && x.KandidatId == kid).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }

        public static List<Prijave> SelectByInstruktorId(int iid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Include("Kandidat.Korisnik").Include("Instruktor.Korisnik").Include("KategorijeUPrijavi.Kategorije").Where(x => x.Status == 1 && x.InstruktorId == iid).OrderByDescending(Prijave => Prijave.DatumPrijave).ToList();
            }
        }

        public static List<KategorijePrijave> SelectKategorije(int pid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.KategorijePrijave.Include("Kategorije").Where(x => x.PrijavaId == pid).ToList();
            }
        }
        public static void Update(Prijave p)
        {
            using (dataContext dt = new dataContext())
            {
                Prijave prijava = dt.Prijave.Where(x => x.PrijavaId == p.PrijavaId).First();
                prijava.InstruktorId = p.InstruktorId;
                prijava.KandidatId = p.KandidatId;
                prijava.Zavrseno = p.Zavrseno;
                prijava.Status = p.Status;
                dt.SaveChanges();
            }
        }

        public static int Insert(Prijave p)
        {
            using (dataContext dt = new dataContext())
            {
                dt.Prijave.Add(p);
                dt.SaveChanges();
                return p.PrijavaId;
            }
        }
    }
}
