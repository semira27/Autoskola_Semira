using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class KategorijeUPrijaviController : Controller
    {
        // GET: KategorijeUPrijavi
        public ActionResult Get(int pid)
        {

            Autoskola.Data.dataContext s = new Autoskola.Data.dataContext();


            var rez = from p in s.Prijave
                    join i in s.KategorijePrijave on p.PrijavaId equals i.PrijavaId
                    join k in s.Kategorije on i.KategorijaId equals k.KategorijaId
                    where (p.PrijavaId == pid)
                    select new { p.PrijavaId, i.KategorijaPrijavaId, k.KategorijaId, k.Naziv, k.Opis, BrojPitanjaUTestu = k.BrPitanjaTest, i.Spremnost };



            return Json(rez, JsonRequestBehavior.AllowGet);
        }
    }
}