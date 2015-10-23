using Autoskola.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class Kategorije
    {
        [Key]
        public int? KategorijaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int BrPitanjaTest { get; set; }



        public virtual List<KategorijePrijave> KategorijeUPrijavi { get; set; }

        public virtual List<PitanjaKategorije> PitanjaKategorije { get; set; }

        public virtual List<BrojPitanja> BrojPitanja { get; set; }

    }
}
