using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class Korisnici
    {
        [Key]
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string JMBG { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public string Email { get; set; }
        public byte Aktivan { get; set; }

        public DateTime DatumRodjenja { get; set; }
        public DateTime DatumRegistracije { get; set; }

        public int GradId { get; set; }
        public virtual Gradovi Grad { get; set; }

        public Administratori Administrator { get; set; }
        public Instruktori Instruktor { get; set; }
        public Kandidati Kandidat { get; set; }


    }
}
