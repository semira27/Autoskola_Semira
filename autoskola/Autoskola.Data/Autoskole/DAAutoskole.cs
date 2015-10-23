using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autoskola.Data;

namespace Autoskola.Data
{
    public class DAAutoskole
    {
        public static AutoSkole Select()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.AutoSkole.First();
            }
        }

        public static AutoSkole SelectById(int autoskolaID)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.AutoSkole.Where(x => x.AutoSkolaId == autoskolaID).FirstOrDefault();
            }
        }
    }
}
