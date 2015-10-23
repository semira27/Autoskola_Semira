using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class Prijave
    {
       [Key]
       public int PrijavaId { get; set; }
       public DateTime DatumPrijave { get; set; }
       public string KategorijaPolaganja { get; set; }
       public string UkupnoUplatiti { get; set; }
       public byte Zavrseno { get; set; }
       public byte Status { get; set; }

       public int KandidatId { get; set; }
       public virtual Kandidati Kandidat { get; set; }

       public int InstruktorId { get; set; }
       public virtual Instruktori Instruktor { get; set; }

       public virtual List<PolaganjePrvePomoci> PolaganjePrvePomoci { get; set; }
       public virtual List<KategorijePrijave> KategorijeUPrijavi { get; set; }


       public static object Join(string p, List<KategorijePrijave> list)
       {
           String kats = null;
           for (int i = 0; i < list.Count; i++)
           {
               if(i == list.Count - 1)
                   kats += list[i].Kategorije.Naziv;
               else
                   kats += list[i].Kategorije.Naziv +", ";
           }

           return kats;
       }
    }
}
