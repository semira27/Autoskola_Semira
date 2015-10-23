using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class GrupaPitanjaByGrupaPitanjaIdController : Controller
    {
        // GET: GrupaPitanjaByGrupaPitanjaId
         public ActionResult Get(int gid)
        {
            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var rezultat = from p in s.GrupePitanja
                           where (p.GrupaPitanjaId == gid && p.Status == 1)
                           select new { p.GrupaPitanjaId, p.Naziv, p.Opis, p.PitanjeBod};

            return Json(rezultat, JsonRequestBehavior.AllowGet);

        }
    }
}