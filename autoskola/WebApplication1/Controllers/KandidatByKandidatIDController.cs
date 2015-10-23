﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class KandidatByKandidatIDController : Controller
    {
        // GET: KandidatByKandidatID
        public ActionResult Get(int kid)
        {

            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var rez = s.Kandidati.Include("Korisnik").Where(x => x.KandidatId == kid)
                .Select(x => new
                {
                    KandidatID = x.KandidatId,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    DatumRodjenja = (x.Korisnik.DatumRodjenja).ToString(),
                    DatumRegistracije = (x.Korisnik.DatumRegistracije).ToString(),
                    Email = x.Korisnik.Email,
                    JMBG = x.Korisnik.JMBG,
                    Adresa = x.Korisnik.Adresa,
                    Aktivan = x.Korisnik.Aktivan,
                    Grad = x.Korisnik.Grad.Naziv
                }).FirstOrDefault();



            return Json(rez, JsonRequestBehavior.AllowGet);
        }
    }
}