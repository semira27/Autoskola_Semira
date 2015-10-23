using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Autoskola.Infrastructure.Encryption
{
    public class EncryptionHelper
    {
        #region Private members
        private static string DEFAULT_HASH_ALGORITHM = "SHA1";
        private static int DEFAULT_KEY_SIZE = 256;
        private static int MAX_ALLOWED_SALT_LEN = 255;
        private static int MIN_ALLOWED_SALT_LEN = 4;
        private static int DEFAULT_MIN_SALT_LEN = MIN_ALLOWED_SALT_LEN;
        private static int DEFAULT_MAX_SALT_LEN = 8;
        private int minSaltLen = -1;
        private int maxSaltLen = -1;
        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;
        //private static string passPhrase = "com.tagba#";
        //private static string initVector = "#tagba2011";
        private static string passPhrase = "HelpDesk@ssolak##";
        private static string initVector = "#1500@seminarski";
        #endregion

        #region Constructors

        public EncryptionHelper() :
            this(-1)
        {
        }
        public EncryptionHelper(int minSaltLen) :
            this(minSaltLen, -1)
        {
        }
        public EncryptionHelper(int minSaltLen,
                                int maxSaltLen) :
            this(minSaltLen, maxSaltLen, -1)
        {
        }
        public EncryptionHelper(int minSaltLen,
                                int maxSaltLen,
                                int keySize) :
            this(minSaltLen, maxSaltLen, keySize, null)
        {
        }
        public EncryptionHelper(int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm) :
            this(minSaltLen, maxSaltLen, keySize,
                hashAlgorithm, null)
        {
        }
        public EncryptionHelper(int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm,
                                string saltValue) :
            this(minSaltLen, maxSaltLen, keySize,
                hashAlgorithm, saltValue, 1)
        {
        }
        public EncryptionHelper(int minSaltLen,
                                int maxSaltLen,
                                int keySize,
                                string hashAlgorithm,
                                string saltValue,
                                int passwordIterations)
        {

            if (minSaltLen < MIN_ALLOWED_SALT_LEN)
                this.minSaltLen = DEFAULT_MIN_SALT_LEN;
            else
                this.minSaltLen = minSaltLen;

            if (maxSaltLen < 0 || maxSaltLen > MAX_ALLOWED_SALT_LEN)
                this.maxSaltLen = DEFAULT_MAX_SALT_LEN;
            else
                this.maxSaltLen = maxSaltLen;

            if (keySize <= 0)
                keySize = DEFAULT_KEY_SIZE;

            if (hashAlgorithm == null)
                hashAlgorithm = DEFAULT_HASH_ALGORITHM;
            else
                hashAlgorithm = hashAlgorithm.ToUpper().Replace("-", "");

            byte[] initVectorBytes = null;

            byte[] saltValueBytes = null;

            if (initVector == null)
                initVectorBytes = new byte[0];
            else
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            if (saltValue == null)
                saltValueBytes = new byte[0];
            else
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                       passPhrase,
                                                       saltValueBytes,
                                                       hashAlgorithm,
                                                       passwordIterations);

            byte[] keyBytes = password.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            if (initVectorBytes.Length == 0)
                symmetricKey.Mode = CipherMode.ECB;
            else
                symmetricKey.Mode = CipherMode.CBC;

            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }
        #endregion

        #region Encryption routines
        public string Encrypt(string plainText)
        {
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }
        public string Encrypt(byte[] plainTextBytes)
        {
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }
        public byte[] EncryptToBytes(string plainText)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
        }
        public byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            byte[] plainTextBytesWithSalt = AddSalt(plainTextBytes);

            MemoryStream memoryStream = new MemoryStream();

            lock (this)
            {
                CryptoStream cryptoStream = new CryptoStream(
                                                   memoryStream,
                                                   encryptor,
                                                    CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytesWithSalt,
                                    0,
                                   plainTextBytesWithSalt.Length);

                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                return cipherTextBytes;
            }
        }
        #endregion

        #region Decryption routines
        public string Decrypt(string cipherText)
        {
            try
            {
                return Decrypt(Convert.FromBase64String(cipherText));
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public string Decrypt(byte[] cipherTextBytes)
        {
            return Encoding.UTF8.GetString(DecryptToBytes(cipherTextBytes));
        }
        public byte[] DecryptToBytes(string cipherText)
        {
            return DecryptToBytes(Convert.FromBase64String(cipherText));
        }
        public byte[] DecryptToBytes(byte[] cipherTextBytes)
        {
            byte[] decryptedBytes = null;
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            int saltLen = 0;

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            decryptedBytes = new byte[cipherTextBytes.Length];

            lock (this)
            {
                CryptoStream cryptoStream = new CryptoStream(
                                                   memoryStream,
                                                   decryptor,
                                                   CryptoStreamMode.Read);

                decryptedByteCount = cryptoStream.Read(decryptedBytes,
                                                        0,
                                                        decryptedBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
            }

            if (maxSaltLen > 0 && maxSaltLen >= minSaltLen)
            {
                saltLen = (decryptedBytes[0] & 0x03) |
                            (decryptedBytes[1] & 0x0c) |
                            (decryptedBytes[2] & 0x30) |
                            (decryptedBytes[3] & 0xc0);
            }

            plainTextBytes = new byte[decryptedByteCount - saltLen];

            Array.Copy(decryptedBytes, saltLen, plainTextBytes,
                        0, decryptedByteCount - saltLen);

            return plainTextBytes;
        }
        #endregion

        #region Helper functions
        private byte[] AddSalt(byte[] plainTextBytes)
        {
            if (maxSaltLen == 0 || maxSaltLen < minSaltLen)
                return plainTextBytes;

            byte[] saltBytes = GenerateSalt();

            byte[] plainTextBytesWithSalt = new byte[plainTextBytes.Length +
                                                     saltBytes.Length];
            Array.Copy(saltBytes, plainTextBytesWithSalt, saltBytes.Length);

            // Append plain text bytes to the salt value.
            Array.Copy(plainTextBytes, 0,
                        plainTextBytesWithSalt, saltBytes.Length,
                        plainTextBytes.Length);

            return plainTextBytesWithSalt;
        }
        /// <summary>
        /// Generates an array holding cryptographically strong bytes.
        /// </summary>
        /// <returns>
        /// Array of randomly generated bytes.
        /// </returns>
        /// <remarks>
        /// Salt size will be defined at random or exactly as specified by the
        /// minSlatLen and maxSaltLen parameters passed to the object constructor.
        /// The first four bytes of the salt array will contain the salt length
        /// split into four two-bit pieces.
        /// </remarks>
        private byte[] GenerateSalt()
        {
            // We don't have the length, yet.
            int saltLen = 0;

            // If min and max salt values are the same, it should not be random.
            if (minSaltLen == maxSaltLen)
                saltLen = minSaltLen;
            // Use random number generator to calculate salt length.
            else
                saltLen = GenerateRandomNumber(minSaltLen, maxSaltLen);

            // Allocate byte array to hold our salt.
            byte[] salt = new byte[saltLen];

            // Populate salt with cryptographically strong bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(salt);

            // Split salt length (always one byte) into four two-bit pieces and
            // store these pieces in the first four bytes of the salt array.
            salt[0] = (byte)((salt[0] & 0xfc) | (saltLen & 0x03));
            salt[1] = (byte)((salt[1] & 0xf3) | (saltLen & 0x0c));
            salt[2] = (byte)((salt[2] & 0xcf) | (saltLen & 0x30));
            salt[3] = (byte)((salt[3] & 0x3f) | (saltLen & 0xc0));

            return salt;
        }
        /// <summary>
        /// Generates random integer.
        /// </summary>
        /// <param name="minValue">
        /// Min value (inclusive).
        /// </param>
        /// <param name="maxValue">
        /// Max value (inclusive).
        /// </param>
        /// <returns>
        /// Random integer value between the min and max values (inclusive).
        /// </returns>
        /// <remarks>
        /// This methods overcomes the limitations of .NET Framework's Random
        /// class, which - when initialized multiple times within a very short
        /// period of time - can generate the same "random" number.
        /// </remarks>
        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            // We will make up an integer seed from 4 bytes of this array.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert four random bytes into a positive integer value.
            int seed = ((randomBytes[0] & 0x7f) << 24) |
                        (randomBytes[1] << 16) |
                        (randomBytes[2] << 8) |
                        (randomBytes[3]);

            // Now, this looks more like real randomization.
            Random random = new Random(seed);

            // Calculate a random number.
            return random.Next(minValue, maxValue + 1);
        }
        #endregion
    }
}
