using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class PitanjaKategorije
    {
        [Key]
        public int PitanjaKategorijaId { get; set; }

        public int PitanjeId { get; set; }
        public virtual Pitanja Pitanje { get; set; }

        public int KategorijaId { get; set; }
        public virtual Kategorije Kategorije { get; set; }
    }
}
