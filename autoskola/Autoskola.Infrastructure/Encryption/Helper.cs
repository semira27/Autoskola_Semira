using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Infrastructure.Encryption
{
    public class Helper
    {
        //public static string GenerateSalt()
        //{
        //    var buf = new byte[16];
        //    (new RNGCryptoServiceProvider()).GetBytes(buf);
        //    return Convert.ToBase64String(buf);
        //}

        //public static string GenerateHash(string lozinka, string salt)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(lozinka);
        //    byte[] src = Convert.FromBase64String(salt);
        //    byte[] dst = new byte[src.Length + bytes.Length];

        //    System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        //    System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
        //    HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
        //    byte[] inArray = algorithm.ComputeHash(dst);
        //    return Convert.ToBase64String(inArray);
        //}

        public static string GenerateHash(string lozinka)
        {
            return string.Join("", SHA1CryptoServiceProvider.Create().ComputeHash(Encoding.UTF8.GetBytes(lozinka)).Select(x => x.ToString("x2")));
        }
    }
}
