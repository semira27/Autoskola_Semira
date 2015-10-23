using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Data
{
    public class DAKorisnici
    {
        public static Korisnici Login_Check(string username, string password)
        {
            using (dataContext dt = new dataContext())
            {

                Korisnici k = dt.Korisnici.Include("Administrator").Include("Instruktor").Include("Kandidat").Where(x => x.KorisnickoIme == username).FirstOrDefault();

                if (k != null)
                {
                    if (Infrastructure.Encryption.Helper.GenerateHash(password) == k.LozinkaHash)
                        return k;
                }

                return null;
            }
           
        }

        public static Korisnici selectById(int kandidatID)
        {
            using (dataContext dt = new dataContext())
            {
                return (from k in dt.Korisnici
                        where k.KorisnikId == kandidatID
                        select k).FirstOrDefault();
            }
        }
    }
}
