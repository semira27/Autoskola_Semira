using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AllPitanjaTest
    {
        public class PitanjeInfo
        {
            public int PitanjeId { get; set; }
            public string Pitanje { get; set; }
            public string Slika { get; set; }
            public byte Multichoice { get; set; }
        }

        public int GrupaPitanjaId { get; set; }

        public int BrojPitanja { get; set; }

        public List<PitanjeInfo> Pitanja { get; set; }
    }
}