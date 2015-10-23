using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAKategorije
    {

        public static void Insert(Kategorije k)
        {
            using (dataContext dt = new dataContext())
            {
                dt.Kategorije.Add(k);
                dt.SaveChanges();
            }
        }
        public static Kategorije Select_ById(int id)
        {
            using (dataContext dt = new dataContext())
            {

                return dt.Kategorije.Where(x => x.KategorijaId == id).FirstOrDefault();
            }
        }


        public static List<Kategorije> Select()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Kategorije.ToList();
            }
        }

        public static int getBrojPitanja(int kategorijaID)
        {
            using (dataContext dt = new dataContext())
            {
                int broj = (from k in dt.Kategorije
                        where k.KategorijaId == kategorijaID
                        select k.BrPitanjaTest).FirstOrDefault();
                return broj;
            }
        }

        public static List<Kategorije> SelectAll()
        {
            using (dataContext dt = new dataContext())
            {

                List<Kategorije> temp = dt.Kategorije.ToList();

                Kategorije k = new Kategorije();
                k.Naziv = "Odaberite kategoriju";
                k.KategorijaId = 0;

                temp.Insert(0, k);

                return temp;

            
            }
        }
    }
}
