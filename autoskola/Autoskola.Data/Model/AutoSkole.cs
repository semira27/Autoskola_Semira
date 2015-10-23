using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
   public class AutoSkole
    {
       [Key]
       public int AutoSkolaId { get; set; }
       public string Naziv { get; set; }
       public string Adresa { get; set; }
       public string PostanskiBroj { get; set; }
       public string Telefon { get; set; }
       public string Fax { get; set; }
       public string Email { get; set; }
       public string Logo { get; set; }

       public virtual List<Instruktori> Instruktori { get; set; }
       public virtual List<Kandidati> Kandidati { get; set; }

       public int GradId { get; set; }
       public virtual Gradovi Grad { get; set; }

    }
}
