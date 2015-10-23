using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class GrupePitanja
    {
       [Key]
       public int GrupaPitanjaId { get; set; }
       public string Naziv { get; set; }
       public string Opis { get; set; }
       public int PitanjeBod { get; set; }
       public byte Status { get; set; }

       public virtual List<Pitanja> Pitanja { get; set; }
       public virtual List<BrojPitanja> BrojPitanja { get; set; }



    }
}
