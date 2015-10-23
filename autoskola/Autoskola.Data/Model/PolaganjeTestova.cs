using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class PolaganjeTestova
    {
        [Key]
        public int PolaganjeTestovaId { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public byte Polozeno { get; set; }
        public int KategorijaPrijavaId { get; set; }
        public virtual KategorijePrijave KategorijaIzPrijave { get; set; }
    }
}
