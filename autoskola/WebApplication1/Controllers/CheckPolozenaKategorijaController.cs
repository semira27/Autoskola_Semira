using Autoskola.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class CheckPolozenaKategorijaController : Controller
    {
        // GET: CheckPolozenaKategorija
        public ActionResult Get(int KategorijaPrijavaId, int kandidatId)
        {
            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();

            var rezultat = from p in s.Prijave
                           join i in s.KategorijePrijave on p.PrijavaId equals i.PrijavaId
                           join k in s.PolaganjeTestova on i.KategorijaPrijavaId equals k.KategorijaPrijavaId
                           where (p.KandidatId == kandidatId && i.KategorijaPrijavaId == KategorijaPrijavaId)
                           select new { k.Polozeno };

            return Json(rezultat, JsonRequestBehavior.AllowGet);
        }
    }
}