using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAVrstePitanja
    {

        public static void VrstePitanja_Insert(GrupePitanja gP)
        {
            using (dataContext dt = new dataContext())
            {
                dt.GrupePitanja.Add(gP);
                dt.SaveChanges();
            }
        }

        public static object Select_All()
        {
            using (dataContext dt = new dataContext())
            {
                List<Pitanja> temp =  (from p in dt.Pitanja
                            join gp in dt.GrupePitanja
                                on p.GrupaPitanjaId equals gp.GrupaPitanjaId
                            select p).ToList();

                List<GrupePitanja> tempGrupe = dt.GrupePitanja.ToList();


                for (int i = 0; i < tempGrupe.Count; i++)
                {
                    int brojac = 0;

                    foreach (Pitanja item2 in temp)
                    {
                        if (item2.GrupaPitanjaId == tempGrupe[i].GrupaPitanjaId)
                            brojac++;
                    }

                    tempGrupe[i].Naziv = tempGrupe[i].Naziv + " " + "(" +brojac.ToString() + ")";

                }

                return tempGrupe;
                    
            }
        }

        public static GrupePitanja Select_ById(int Id)
        {
            using (dataContext dt = new dataContext())
            {
                return dt.GrupePitanja.Where(x => x.GrupaPitanjaId == Id).FirstOrDefault();
            }
        }

        public static List<GrupePitanja> Select_All_Instruktor()
        {
            using (dataContext dt = new dataContext())
            {
                return dt.GrupePitanja.ToList();

            }
        }
    }
}
