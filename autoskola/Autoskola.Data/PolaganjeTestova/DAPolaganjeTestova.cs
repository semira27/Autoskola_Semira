using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAPolaganjeTestova
    {
        public static List<PolaganjeTestova> SelectByKategorijaPrijavaId(int id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.PolaganjeTestova.Where(x => x.KategorijaPrijavaId == id).ToList();
            }
        }

        public static bool CheckPolozeno(int katprijavaid)
        {
            using (dataContext dt = new dataContext())
            {
                int rez = dt.PolaganjeTestova.Where(x => x.KategorijaIzPrijave.KategorijaPrijavaId == katprijavaid && x.Polozeno == 1).Select(x => x.Polozeno).FirstOrDefault();
                if (rez == 1)
                    return true;
                else
                    return false;
            }
        }

        public static void Insert(PolaganjeTestova p)
        {
            using (dataContext dt = new dataContext())
            {
                dt.PolaganjeTestova.Add(p);
                dt.SaveChanges();
            }
        }

        public static List<PolaganjeTestova> SelectByKandidatID(int kandidatID)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.PolaganjeTestova.Include("KategorijaIzPrijave.Kategorije").Where(x => x.KategorijaIzPrijave.Prijava.KandidatId == kandidatID).ToList();
            }
            
        }
    }
}
