using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class OdgovoriByPitanjeIdController : Controller
    {
        // GET: 
        public ActionResult Get(int pid)
        {
            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var rezultat = from p in s.Pitanja
                           join i in s.Odgovori on p.PitanjeId equals i.PitanjeId
                           where (p.PitanjeId == pid)
                           select new { i.OdgovorId, i.Odgovor, i.Tacan};

            return Json(rezultat, JsonRequestBehavior.AllowGet);

        }
    }
}