using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAOdabraniOdgovori
    {
        public static void Insert(OdabraniOdgovori o)
        {
            using (dataContext dt = new dataContext())
            {
                dt.OdabraniOdgovori.Add(o);
                dt.SaveChanges();
            }
        }

        public static List<OdabraniOdgovori> SelectByUradjeniTestId(int id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.OdabraniOdgovori.Include("Pitanje.Odgovori").Where(x => x.UradjeniTestId == id).OrderBy(x => x.OdabraniOdgovorId).ToList();
            }
        }
    }
}
