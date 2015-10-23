using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class Odgovori
    {
       [Key]
       public int OdgovorId { get; set; }
       public string Odgovor { get; set; }
       public byte Tacan { get; set; }

       public virtual List<OdabraniOdgovori> OdabraniOdgovori { get; set; }

       public int PitanjeId { get; set; }
       public virtual Pitanja OdredjenoPitanje { get; set; }

    }
}
