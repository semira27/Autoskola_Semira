using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class UpdateUradjeniTestController : Controller
    {
        // GET: UpdateUradjeniTest
        public ActionResult Get(int testId, int maxbodovi, int osvojenibodovi, bool polozeno)
        {
            int uradjeni;

            using (dataContext dt = new dataContext())
            {
                UradjeniTestovi ut = dt.UradjeniTestovi.Where(x => x.UradjeniTestId == testId).FirstOrDefault();

                ut.KrajTesta = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                ut.MaxBodovi = maxbodovi;
                ut.OsvojeniBodovi = osvojenibodovi;
                ut.Polozeno = polozeno;
                ut.OsvojeniProcenat = (osvojenibodovi / maxbodovi) * 100;

                dt.SaveChanges();

                uradjeni = ut.UradjeniTestId;

                KategorijePrijave kp = DAKategorijePrijave.SelectById(ut.KategorijaPrijavaId);
                List<UradjeniTestovi> testovi = DAUradjeniTestovi.SelectByKategorijePrijaveId(ut.KategorijaPrijavaId);

                double brojnik = 0, nazivnik = 0;

                foreach (UradjeniTestovi u in testovi)
                {
                    brojnik = brojnik + (u.OsvojeniProcenat/100) * u.MaxBodovi;
                    nazivnik = nazivnik + u.MaxBodovi;
                }

                kp.Spremnost = brojnik / nazivnik;
                DAKategorijePrijave.UpdateSpremnost(kp);
            }

            return Json(uradjeni, JsonRequestBehavior.AllowGet);

        }
    }
}