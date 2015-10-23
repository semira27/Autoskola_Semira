using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAKategorijePrijave
    {
        public static KategorijePrijave SelectById(int id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.KategorijePrijave.Where(x => x.KategorijaPrijavaId == id).FirstOrDefault();
            }
        }

        public static KategorijePrijave SelectById_Kandidat(int pid, int kpid, int kandidatid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == kandidatid && x.PrijavaId == pid)
                       .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp)
                       .Where(kp => kp.KategorijaPrijavaId == kpid).Select(kp => kp).FirstOrDefault();
            }
        }

        public static void Insert(KategorijePrijave kp)
        {
            using (dataContext dt = new dataContext())
            {
                dt.KategorijePrijave.Add(kp);
                dt.SaveChanges();
            }
        }

        public static List<KategorijePrijave> SelectByPrijavaId(int pid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.KategorijePrijave.Include("Kategorije").Include("PolaganjeTestova").Where(x => x.PrijavaId == pid).ToList();
            }
        }

        public static void UpdateSpremnost(KategorijePrijave kp)
        {
            using (dataContext dt = new dataContext())
            {
                KategorijePrijave katprijava = dt.KategorijePrijave.Where(x => x.KategorijaPrijavaId == kp.KategorijaPrijavaId).First();
                katprijava.Spremnost = kp.Spremnost;
                dt.SaveChanges();
            }
        }
    }
}
