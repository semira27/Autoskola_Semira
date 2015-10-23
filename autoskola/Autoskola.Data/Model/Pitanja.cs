using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class Pitanja
    {
       [Key]
       public int PitanjeId { get; set; }
       public string Pitanje { get; set; }
       public string Slika { get; set; }

       public DateTime DatumDodavanja { get; set; }
       public byte Status { get; set; }
       public byte Multichoice { get; set; }


       public virtual List<OdabraniOdgovori> OdabraniOdgovori { get; set; }
       public virtual List<Odgovori> Odgovori { get; set; }
       public virtual List<PitanjaKategorije> PitanjaKategorije { get; set; }


       public int GrupaPitanjaId { get; set; }
       public virtual GrupePitanja GrupaPitanja { get; set; }

    }
}
