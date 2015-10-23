using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class PolaganjeVoznje
    {
       [Key]
       public int PolaganjeVoznjeId { get; set; }
       public DateTime DatumPolaganja { get; set; }
       public byte Polozeno { get; set; }

       public int PrijavaId { get; set; }
       public virtual Prijave Prijava { get; set; }
    }
}
