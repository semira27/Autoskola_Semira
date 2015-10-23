using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PitanjaKategorije
    {
        public class OdgovoriInfo
        {
            public int OdgovorId { get; set; }
            public string Odgovor { get; set; }
            public byte Tacan { get; set; }
        }

        public int PitanjeId { get; set; }
        public string Pitanje { get; set; }
        public string Slika { get; set; }
        public byte Multichoice { get; set; }


        public List<OdgovoriInfo> OdgovoriNaPitanja { get; set; }
    }
}