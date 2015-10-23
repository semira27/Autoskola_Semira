using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class UradjeniTestovi
    {
       [Key]
       public int UradjeniTestId { get; set; }
       public int MaxBodovi { get; set; }
       public int OsvojeniBodovi { get; set; }
       public double OsvojeniProcenat { get; set; }
       public DateTime PocetakTesta { get; set; }
       public DateTime KrajTesta { get; set; }
       public bool Polozeno { get; set; }

       public int KategorijaPrijavaId { get; set; }
       public virtual KategorijePrijave KategorijaPrijava { get; set; }
       public virtual List<OdabraniOdgovori> OdabraniOdgovori { get; set; }
    }
}
