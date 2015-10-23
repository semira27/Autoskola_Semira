using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAPitanjaKategorije
    {
        public static void Insert(PitanjaKategorije p)
        {
            using (dataContext dt = new dataContext())
            {
                dt.PitanjaKategorije.Add(p);
                dt.SaveChanges();
            }
        }

        public static void Delete(int pkID)
        {
            using (dataContext dt = new dataContext())
            {
                PitanjaKategorije obj = dt.PitanjaKategorije.Where(x => x.PitanjaKategorijaId == pkID).FirstOrDefault();
                dt.PitanjaKategorije.Remove(obj);
                dt.SaveChanges();
            }
        }

    }
}
