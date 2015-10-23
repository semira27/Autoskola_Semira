using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAGradovi
    {
        public static List<Gradovi> SelectAll()
        {
            using (dataContext dt = new dataContext())
            {
                List<Gradovi> temp = dt.Gradovi.OrderBy(x => x.Naziv).ToList();

                Gradovi g = new Gradovi();
                g.Naziv = "Odaberite grad";
                g.GradId = 0;

                temp.Insert(0, g);

                return temp;
            }
        }
    }
}
