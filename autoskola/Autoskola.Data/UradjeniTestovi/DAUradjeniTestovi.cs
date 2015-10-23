using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAUradjeniTestovi
    {
        public static int Count()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.UradjeniTestovi.Count();

            }
        }

        public static int Insert(UradjeniTestovi u)
        {
            using (dataContext dt = new dataContext())
            {
                dt.UradjeniTestovi.Add(u);
                dt.SaveChanges();
                return u.UradjeniTestId;
            }
        }

        public static void Update(UradjeniTestovi u)
        {
            using (dataContext dt = new dataContext())
            {
                UradjeniTestovi ut = dt.UradjeniTestovi.Where(x => x.UradjeniTestId == u.UradjeniTestId).FirstOrDefault();
                ut.KrajTesta = u.KrajTesta;
                ut.MaxBodovi = u.MaxBodovi;
                ut.OsvojeniBodovi = u.OsvojeniBodovi;
                ut.Polozeno = u.Polozeno;
                ut.OsvojeniProcenat = u.OsvojeniProcenat;

                dt.SaveChanges();
            }
        }
        public static int CountUradjene(int katprijavaid)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.UradjeniTestovi.Where(x => x.KategorijaPrijava.KategorijaPrijavaId == katprijavaid).Count();
            }
        }

        public static List<UradjeniTestovi> SelectByKategorijePrijaveId(int id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.UradjeniTestovi.Where(x => x.KategorijaPrijavaId == id).OrderByDescending(x => x.UradjeniTestId).ToList();
            }
        }



        public static UradjeniTestovi SelectByIdAndKandidatID(int id, int kandidatID)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.Prijave.Where(x => x.KandidatId == kandidatID)
                        .Join(dt.KategorijePrijave, x => x.PrijavaId, kp => kp.PrijavaId, (x, kp) => kp)
                        .Join(dt.UradjeniTestovi, kp => kp.KategorijaPrijavaId, ut => ut.KategorijaPrijavaId, (kp, ut) => ut)
                        .Where(ut => ut.UradjeniTestId == id)
                        .Select(ut => ut).FirstOrDefault();
            }
        }
    }
}
