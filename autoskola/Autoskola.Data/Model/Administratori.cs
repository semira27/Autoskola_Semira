using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class Administratori
    {
        [Key, ForeignKey("Korisnik")]
        public int AdministratorId { get; set; }
        public Korisnici Korisnik { get; set; }
    }
}
