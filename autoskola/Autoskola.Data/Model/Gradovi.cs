using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class Gradovi
    {
        [Key]
        public int GradId { get; set; }
        public string Naziv { get; set; }

        public int DrzavaId { get; set; }
        public virtual Drzave Drzava { get; set; }

        public virtual List<Korisnici> Korisnici { get; set; }
        public virtual List<AutoSkole> AutoSkole { get; set; }

    }
}
