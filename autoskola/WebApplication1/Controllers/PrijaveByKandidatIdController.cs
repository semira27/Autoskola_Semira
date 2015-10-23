using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autoskola.Data;

namespace WebApplication1.Controllers
{
    public class PrijaveByKandidatIdController : Controller
    {
        // GET: PrijaveByKandidatId
        public ActionResult Prijave(int kid)
        {
            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();

           // var rezultat = s.Prijave.Where(c => c.KandidatId == kid).Join(s.Instruktori, c => c.InstruktorId, p => p.InstruktorId, (c, p) => new { p.InstruktorId });

            var rezultat = from p in s.Prijave
                          join i in s.Instruktori on p.InstruktorId equals i.InstruktorId
                          join k in s.Korisnici on i.Korisnik.KorisnikId equals k.KorisnikId
                          where (p.KandidatId == kid) 
                          select new { p.PrijavaId, p.DatumPrijave, Instruktor = k.Ime + " " + k.Prezime, p.Zavrseno };

            return Json(rezultat, JsonRequestBehavior.AllowGet);

        }
    }
}