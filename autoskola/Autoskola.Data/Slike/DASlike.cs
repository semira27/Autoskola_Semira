using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoskola.Data;
namespace Autoskola.Data
{
    public class DASlike
    {
        public static void Insert(Slike s)
        {

            using (dataContext dt = new dataContext())
            {

                dt.Slike.Add(s);
                dt.SaveChanges();
            }
        }

        public static int InsertGalerija(Galerija g)
        {

            using (dataContext dt = new dataContext())
            {

                dt.Galerija.Add(g);
                dt.SaveChanges();
                return g.GalerijaId;
            }
        }
        public static int SelectGalerijaMaxId()
        {

            using (dataContext dt = new dataContext())
            {

                return dt.Galerija.Max(x => x.GalerijaId);
            }
        }

        public static List<Galerija> SelectGalerije()
        {

            using (dataContext dt = new dataContext())
            {

                return dt.Galerija.Where(x => x.Aktivna == true).ToList();
            }
        }


        public static List<Slike> SelectByObavijest(int obavijestId)
        {

            using (dataContext dt = new dataContext())
            {

                return dt.Slike.Where(x => x.ObavijestId == obavijestId).ToList();
            }
        }

        public static void UpdateThumbnail(int obavijestId)
        {

            using (dataContext dt = new dataContext())
            {
                Obavijesti o = dt.Obavijesti.Where(x => x.ObavijestId == obavijestId).FirstOrDefault();
                o.Thumbnail = dt.Slike.Where(x => x.ObavijestId == obavijestId).FirstOrDefault().Slika;
                dt.SaveChanges();
            }
        }



        public static void InsertSlike(SlikeGalerija s)
        {

            using (dataContext dt = new dataContext())
            {

                dt.SlikeGalerija.Add(s);
                dt.SaveChanges();
            }
        }

        public static List<SlikeGalerija> SelectSlike(int id)
        {

            using (dataContext dt = new dataContext())
            {
               
                return dt.SlikeGalerija.Where(x => x.GalerijaId == id).ToList();
            }
        }


        public static void Delete(int slikaId)
        {
            using (dataContext dt = new dataContext())
            {
                SlikeGalerija d = dt.SlikeGalerija.Where(x => x.SlikaId == slikaId).First();
              //  d.Aktivna = false;
                dt.SaveChanges();
            }
        }



        public static Galerija SelectGalerijaById(int p)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Galerija.Where(x => x.GalerijaId == p).FirstOrDefault();
            }
        }
    }

}
