using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autoskola.Data;


namespace WebApplication1.Controllers
{
    public class PitanjaByKategorijaWithOdgController : Controller
    {
        Autoskola.Data.dataContext ctx = new Autoskola.Data.dataContext();
        // GET: PitanjaByKategorija
        public ActionResult Get(int katid)
        {
            var k = ctx.Pitanja.Where(x => x.GrupaPitanja.BrojPitanja.Any(b => b.KategorijaId == katid))
                .Select(p => new Models.PitanjaKategorije
                {
                    PitanjeId = p.PitanjeId,
                    Pitanje = p.Pitanje,
                    Slika = p.Slika,
                    Multichoice = p.Multichoice,

                    OdgovoriNaPitanja = p.Odgovori.Select(z => new Models.PitanjaKategorije.OdgovoriInfo
                    {
                        OdgovorId = z.OdgovorId,
                        Odgovor = z.Odgovor,
                        Tacan = z.Tacan
                    }).ToList(),
                }).ToList();

            //var k = ctx.BrojPitanja.Where(x => x.KategorijaPitanjeId == katid).Join(ctx.Pitanja, c=> c.GrupaPitanjaId, p => p.GrupaPitanjaId, (c, p) => new Models.PitanjaKategorije
            //{
            //    PitanjeId = p.PitanjeId,
            //    Pitanje = p.Pitanje,
            //    Slika = p.Slika,

            //    OdgovoriNaPitanja = p.Odgovori.Where(o => o.PitanjeId == p.PitanjeId).Select(z => new Models.PitanjaKategorije.OdgovoriInfo
            //    {
            //    OdgovorId = z.OdgovorId,
            //    Odgovor = z.Odgovor,
            //    Tacan = z.Tacan
            //    }).ToList(),
            //}).ToList();

            return Json(k, JsonRequestBehavior.AllowGet);
        }
    }
}