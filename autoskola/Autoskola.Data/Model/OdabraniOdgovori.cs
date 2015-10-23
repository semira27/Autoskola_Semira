using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class OdabraniOdgovori
    {
       [Key]
       public int OdabraniOdgovorId { get; set; }

       public int OdgovorId { get; set; }
       public virtual Odgovori Odgovor { get; set; }

       public int PitanjeId { get; set; }
       public virtual Pitanja Pitanje { get; set; }

       public int UradjeniTestId { get; set; }
       public virtual UradjeniTestovi UradjeniTest { get; set; }
       public float Bodovi { get; set; }
   }
}
