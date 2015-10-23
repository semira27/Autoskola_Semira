using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class Kandidati
    {
        [Key, ForeignKey("Korisnik")]
        public int KandidatId { get; set; }

        public Korisnici Korisnik { get; set; }

        public int AutoSkolaId { get; set; }
        public virtual AutoSkole AutoSkola { get; set; }
        public virtual List<Prijave> Prijave { get; set; }
    }
}
