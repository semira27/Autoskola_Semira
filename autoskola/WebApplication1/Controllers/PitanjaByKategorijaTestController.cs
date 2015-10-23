using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autoskola.Data;
using Autoskola.Data.Model;
using System.Web.Script.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class PitanjaByKategorijaTestController : Controller
    {
        // GET: PitanjaByKategorijaTest
        Autoskola.Data.dataContext ctx = new Autoskola.Data.dataContext();

        public ActionResult Get(int katid)
        {

            List<BrojPitanja> k = ctx.BrojPitanja.Where(x => x.KategorijaId == katid).ToList();
            List<AllPitanjaTest> result = k.Select(b => new AllPitanjaTest
            {
                GrupaPitanjaId = b.GrupaPitanjaId,
                BrojPitanja = b.Broj,
                Pitanja = (ctx.PitanjaKategorije.Where(p => p.KategorijaId == katid)
                .OrderBy(p => Guid.NewGuid())
                .Select(p => new Models.AllPitanjaTest.PitanjeInfo 
                { 
                    PitanjeId = p.PitanjeId,
                    Pitanje = p.Pitanje.Pitanje,
                    Slika = p.Pitanje.Slika,
                    Multichoice = p.Pitanje.Multichoice
                })).Take(b.Broj).ToList(),
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}