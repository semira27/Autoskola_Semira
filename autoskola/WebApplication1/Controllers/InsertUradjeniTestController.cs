using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class InsertUradjeniTestController : Controller
    {
        // GET: InsertUradjeniTest
        public ActionResult Get(int kategorijaPrijavaId)
        {
            UradjeniTestovi ut = new UradjeniTestovi();
            ut.PocetakTesta = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            ut.KrajTesta = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            ut.KategorijaPrijavaId = kategorijaPrijavaId;
            ut.Polozeno = false;
            ut.MaxBodovi = 0;
            ut.OsvojeniBodovi = 0;
            ut.OsvojeniProcenat = 0;

            int uradjeni;

            using (dataContext dt = new dataContext())
            {
                dt.UradjeniTestovi.Add(ut);
                dt.SaveChanges();

                uradjeni = ut.UradjeniTestId;
            }

            var uradjeniID = uradjeni;
            return Json(uradjeniID, JsonRequestBehavior.AllowGet);

        }
    }
}