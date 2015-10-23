using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAPitanja
    {
        public static int Insert(Pitanja p)
        {
            using (dataContext dt = new dataContext())
            {
                dt.Pitanja.Add(p);
                dt.SaveChanges();
                return p.PitanjeId;
            }
        }

        public static int Count()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Pitanja.Count();

            }
        }

        public static void Update(Pitanja p)
        {
            using (dataContext dt = new dataContext())
            {
                Pitanja pitanje = dt.Pitanja.Where(x => x.PitanjeId == p.PitanjeId).First();
                pitanje.Pitanje = p.Pitanje;
                pitanje.Slika = p.Slika;
                pitanje.Status = p.Status;
                pitanje.Multichoice = p.Multichoice;
                pitanje.GrupaPitanjaId = p.GrupaPitanjaId;
                dt.SaveChanges();
            }
        }

        public static Pitanja Select_ById(int pId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Pitanja.Include("PitanjaKategorije").Where(x => x.PitanjeId == pId).FirstOrDefault();
            }
        }

        public static List<Pitanja> Select(int vrstaPitanja)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Pitanja.Where(x => x.GrupaPitanjaId == vrstaPitanja && x.Status == 1).ToList();
            }
        }

        public static List<int> Select_AllPitanjaInCategory(int kategorijaid)
        {
            using (dataContext dt = new dataContext())
            {

                List<int> ids = (from i in dt.PitanjaKategorije
                                         join p in dt.Pitanja on i.PitanjeId equals p.PitanjeId
                                         where (i.KategorijaId == kategorijaid && p.Status == 1)
                                         orderby Guid.NewGuid()
                                         select p.PitanjeId).ToList();

                return ids;
            }
        }

        public static List<int> Select_PitanjaTestByCategory(int kategorijaid)
        {
            using (dataContext s = new dataContext())
            {

                List<int> trenutna_pitanja;
                List<int> finalna_pitanja = new List<int>();

                List<BrojPitanja> broj = (from b in s.BrojPitanja
                                          where (b.KategorijaId == kategorijaid)
                                          select b).ToList();

                for (int i = 0; i < broj.Count; i++)
                {
                    int br = broj[i].Broj;
                    int grupa = broj[i].GrupaPitanjaId;

                    trenutna_pitanja = (from bp in s.BrojPitanja
                                        join pk in s.PitanjaKategorije on bp.KategorijaId equals pk.KategorijaId
                                        join p in s.Pitanja on pk.PitanjeId equals p.PitanjeId
                                        where bp.KategorijaId == kategorijaid && p.GrupaPitanjaId == grupa && p.Status == 1
                                        orderby Guid.NewGuid()
                                        select p.PitanjeId).Take(br).ToList();

                    for (int j = 0; j < trenutna_pitanja.Count; j++)
                    {
                        finalna_pitanja.Add(trenutna_pitanja[j]);
                    }
                }

                return finalna_pitanja;
            }
        }


        public static Kategorije getKategorijaByID(int kategorijaID)
        {
            using (dataContext dt = new dataContext())
            {
                return (from k in dt.Kategorije
                        where k.KategorijaId == kategorijaID
                        select k).FirstOrDefault();

            }
        }

        public static List<Pitanja> getAll()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Pitanja.ToList();
            }
        }

        public static void updatePitanje(bool p,int id)
        {
            using (dataContext dt = new dataContext())
            {
                Pitanja kr = dt.Pitanja.First(i => i.PitanjeId == id);

                if(p)
                    kr.Multichoice = 1;
                else
                    kr.Multichoice = 0;
                    

                dt.SaveChanges();
            }
        }

        public static object getGrupePitanja()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.GrupePitanja.ToList();
            }
        }

        public static int GetBodove(int pitanjeID)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Pitanja.Where(x => x.Status == 1 && x.PitanjeId == pitanjeID)
                           .Join(dt.GrupePitanja, x => x.GrupaPitanjaId, gp => gp.GrupaPitanjaId, (x, gp) => gp).Select(gp => gp.PitanjeBod).FirstOrDefault();

            }
            
        }

        public static List<int> AllInts()
        {
            using (dataContext dt = new dataContext())
            {

                List<int> ids = (from p in dt.Pitanja
                                 select p.PitanjeId).ToList();

                return ids;
            }
        }

        public static void BigChange(int pitanjeid)
        {
            using (dataContext dt = new dataContext())
            {
                List<Odgovori> mojiOdg = dt.Odgovori.Where(x => x.Tacan == 1 && x.PitanjeId == pitanjeid).ToList();
                Pitanja pitanje = dt.Pitanja.Where(x => x.PitanjeId == pitanjeid).First();

                if(mojiOdg.Count > 1)
                {
                    pitanje.Multichoice = 1;
                    dt.SaveChanges();
                }
                else
                {
                    pitanje.Multichoice = 0;
                    dt.SaveChanges();
                }

            }
            
        }
    }
}
