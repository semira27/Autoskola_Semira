using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class KategorijePrijave
    {
       [Key]
       public int KategorijaPrijavaId { get; set; }

       public int PrijavaId { get; set; }
       public virtual Prijave Prijava { get; set; }

       public int KategorijaId { get; set; }
       public virtual Kategorije Kategorije { get; set; }
       public double Spremnost { get; set; }

       public virtual List<UradjeniTestovi> UradjeniTestovi { get; set; }

       public virtual List<PolaganjeTestova> PolaganjeTestova { get; set; }
    }
}
