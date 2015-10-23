using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class KandidatByUsernamePassController : Controller
    {
        // GET: KandidatByUsernamePass
        public ActionResult Get(string username, string password)
        {
            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();

            var rez = s.Kandidati.Include("Korisnik").Include("Gradovi").Where(x => x.Korisnik.KorisnickoIme == username && x.Korisnik.LozinkaHash == password)
                .Select(x => new { KandidatID = x.KandidatId, Ime = x.Korisnik.Ime, 
                    Prezime = x.Korisnik.Prezime, DatumRodjenja = x.Korisnik.DatumRodjenja.ToString(), 
                DatumRegistracije = x.Korisnik.DatumRegistracije.ToString(), Email = x.Korisnik.Email, JMBG = x.Korisnik.JMBG, Adresa = x.Korisnik.Adresa, Aktivan = x.Korisnik.Aktivan, Grad = x.Korisnik.Grad.Naziv }).FirstOrDefault();

            return Json(rez, JsonRequestBehavior.AllowGet);
        }
    }
}