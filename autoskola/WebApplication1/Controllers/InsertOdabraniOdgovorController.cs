using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class InsertOdabraniOdgovorController : Controller
    {
        // GET: InsertOdabraniOdgovor
        public ActionResult Get(int odgovorId, int pitanjeId, int uradjeniTestId, int bodovi)
        {
            OdabraniOdgovori o = new OdabraniOdgovori();
            o.OdgovorId = odgovorId;
            o.PitanjeId = pitanjeId;
            o.UradjeniTestId = uradjeniTestId;
            o.Bodovi = bodovi;

            int odabraniOdgovor;

            using (dataContext dt = new dataContext())
            {
                dt.OdabraniOdgovori.Add(o);
                dt.SaveChanges();

                odabraniOdgovor = o.OdabraniOdgovorId;
            }

            var odabraniOdgovorID = odabraniOdgovor;
            return Json(odabraniOdgovorID, JsonRequestBehavior.AllowGet);

        }
    }
}