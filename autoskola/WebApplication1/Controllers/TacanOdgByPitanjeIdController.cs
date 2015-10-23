using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TacanOdgByPitanjeIdController : Controller
    {
        // GET: TacanOdgByPitanjeId
        public ActionResult Get(int pid)
        {

            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var rez = from p in s.Odgovori
                      where (p.PitanjeId == pid & p.Tacan == 1)
                      select new { p.OdgovorId, p.Odgovor, p.Tacan };



            return Json(rez, JsonRequestBehavior.AllowGet);
        }
    }
}