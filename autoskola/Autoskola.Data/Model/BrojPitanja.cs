using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data.Model
{
    public class BrojPitanja
    {
        [Key]
        public int BrojPitanjaId { get; set; }
        public int Broj { get; set; }

        public int GrupaPitanjaId { get; set; }
        public virtual GrupePitanja GrupePitanja { get; set; }

        public int KategorijaId { get; set; }
        public virtual Kategorije Kategorije { get; set; }

    }
}
