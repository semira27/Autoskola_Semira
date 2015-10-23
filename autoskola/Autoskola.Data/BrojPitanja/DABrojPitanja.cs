using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DABrojPitanja
    {
        public static void Insert(BrojPitanja brP)
        {
            using (dataContext dt = new dataContext())
            {
                dt.BrojPitanja.Add(brP);
                dt.SaveChanges();
            }
        }

        public static object Select_ByKategorija(int kategorija)
        {
            using (dataContext dt = new dataContext())
            {
                //return dt.BrojPitanja.Where(x => x.KategorijaPitanjeId == kategorija).ToList();

                return (from bp in dt.BrojPitanja 
                            join gp in dt.GrupePitanja
                            on bp.GrupaPitanjaId equals gp.GrupaPitanjaId
                            where bp.KategorijaId == kategorija
                            select new
                            {
                                bp.Broj,
                                gp.Naziv,
                                bp.BrojPitanjaId

                            }).ToList();
                
            }
        }

        public static List<BrojPitanja> Select_ByKategorija_Sample(int kategorija)
        {
            using (dataContext dt = new dataContext())
            {

                return (from bp in dt.BrojPitanja
                        join gp in dt.GrupePitanja
                        on bp.GrupaPitanjaId equals gp.GrupaPitanjaId
                        where bp.KategorijaId == kategorija
                        select bp).ToList();

            }
        }

        public static BrojPitanja SelectById(int p)
        {
            using (dataContext dt = new dataContext())
            {
                return (from bp in dt.BrojPitanja
                        where bp.BrojPitanjaId == p
                        select bp).FirstOrDefault();
            }
        }

        public static void DeleteBroj(int id)
        {
            using (dataContext dt = new dataContext())
            {
                BrojPitanja pp = dt.BrojPitanja.SingleOrDefault(x => x.BrojPitanjaId == id);
                
                if (pp != null)
                {
                    dt.BrojPitanja.Remove(pp);
                    dt.SaveChanges();
                }

              
            }
        }

        public static List<BrojPitanja> Select_BrojByKategorija(int kategorijaID)
        {
            using (dataContext dt = new dataContext())
            {
                //return dt.BrojPitanja.Where(x => x.KategorijaPitanjeId == kategorija).ToList();

                return (from bp in dt.BrojPitanja
                        where bp.KategorijaId == kategorijaID
                        select bp).ToList();

            }
        }
    }
}
