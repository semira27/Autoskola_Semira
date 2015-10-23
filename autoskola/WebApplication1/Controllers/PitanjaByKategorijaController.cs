using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PitanjaByKategorijaController : Controller
    {
        // GET: PitanjaByKategorija
        public ActionResult Get(int katid)
        {

            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var k = from p in s.Pitanja
                           join i in s.PitanjaKategorije on p.PitanjeId equals i.PitanjeId
                           where (i.KategorijaId == katid)
                    select new { p.PitanjeId, p.GrupaPitanjaId, p.Pitanje, p.Slika, p.Multichoice };


            return Json(k, JsonRequestBehavior.AllowGet);
        }
    }
}