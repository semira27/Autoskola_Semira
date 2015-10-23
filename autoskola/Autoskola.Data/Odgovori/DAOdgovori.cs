using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAOdgovori
    {
        public static void Insert(Odgovori o)
        {
            using (dataContext dt = new dataContext())
            {
                dt.Odgovori.Add(o);
                dt.SaveChanges();
            }
        }

        public static void Update(Odgovori o)
        {
            using (dataContext dt = new dataContext())
            {
                Odgovori odgovor = dt.Odgovori.Where(x => x.OdgovorId == o.OdgovorId).First();
                odgovor.PitanjeId = o.PitanjeId;
                odgovor.Odgovor = o.Odgovor;
                odgovor.Tacan = o.Tacan;
                dt.SaveChanges();
            }
        }

        public static List<Odgovori> Select_ByPitanje(int pId)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Odgovori.Where(x => x.PitanjeId == pId).ToList();
            }
        }

        public static List<Odgovori> Select_ByPitanjeTacni(int pId)
        {
            using (dataContext dt = new dataContext())
            {
                return (from o in dt.Odgovori
                        where o.Tacan == 1 && o.PitanjeId == pId
                        select o).ToList();
            }
        }

        public static int insertUradjeni(UradjeniTestovi ut)
        {
            using (dataContext dt = new dataContext())
            {
                dt.UradjeniTestovi.Add(ut);
                dt.SaveChanges();

                return ut.UradjeniTestId;
            }
        }

        public static void insertOdgovor(OdabraniOdgovori oo)
        {
            using (dataContext dt = new dataContext())
            {
                dt.OdabraniOdgovori.Add(oo);
                dt.SaveChanges();
            }
        }

        public static void updateUradjeniTest(UradjeniTestovi ut)
        {
            using (dataContext dt = new dataContext())
            {
                UradjeniTestovi uradjeni = dt.UradjeniTestovi.Where(x => x.UradjeniTestId == ut.UradjeniTestId).First();
                uradjeni.KrajTesta = ut.KrajTesta;
                uradjeni.OsvojeniBodovi = ut.OsvojeniBodovi;
                ut.MaxBodovi = ut.MaxBodovi;

                dt.SaveChanges();
            }
        }

        public static List<Odgovori> Select_Netacne_Raskrsnice(int uradjeniID)
        {
            using (dataContext dt = new dataContext())
            {
                return (from ut in dt.UradjeniTestovi
                        join oo in dt.OdabraniOdgovori
                        on ut.UradjeniTestId equals oo.UradjeniTestId
                        join o in dt.Odgovori
                        on oo.OdgovorId equals o.OdgovorId
                        join p in dt.Pitanja
                        on o.PitanjeId equals p.PitanjeId
                        where o.Tacan == 0 && p.GrupaPitanjaId==29 && ut.UradjeniTestId == uradjeniID
                        select o).ToList();
            }
        }

        public static List<Odgovori> Select_Netacna_Pitanja(int uradjeniID)
        {
            using (dataContext dt = new dataContext())
            {
                return (from ut in dt.UradjeniTestovi
                        join oo in dt.OdabraniOdgovori
                        on ut.UradjeniTestId equals oo.UradjeniTestId
                        join o in dt.Odgovori
                        on oo.OdgovorId equals o.OdgovorId
                        join p in dt.Pitanja
                        on o.PitanjeId equals p.PitanjeId
                        where o.Tacan == 0 && p.Slika == null && ut.UradjeniTestId == uradjeniID
                        select o).ToList();
            }
        }

        public static List<Odgovori> Select_Netacna_Znakovi(int uradjeniID)
        {
            using (dataContext dt = new dataContext())
            {
                return (from ut in dt.UradjeniTestovi
                        join oo in dt.OdabraniOdgovori
                        on ut.UradjeniTestId equals oo.UradjeniTestId
                        join o in dt.Odgovori
                        on oo.OdgovorId equals o.OdgovorId
                        join p in dt.Pitanja
                        on o.PitanjeId equals p.PitanjeId
                        where o.Tacan == 0 && p.GrupaPitanjaId == 30 && ut.UradjeniTestId == uradjeniID
                        select o).ToList();
            }
        }

    }
}
