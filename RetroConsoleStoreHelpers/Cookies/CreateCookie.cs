using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RetroConsoleStoreHelpers.Cookies
{
    public static class CreateCookie
    {  // TODO
       // Replace the Verify functions with unit tests.
        private const string SaltData = "QADLz4qk3rVgBSGjDfAH3XWV" + "qKKagMXezBPv7TmXvwnXDDeR" +
                                        "pHaLBv4JnTGRwLg9tzbmV77g" + "8DUEAEa6JPv66hy7SwHBL4z4" +
                                        "FbGdh2MVs4kq9RcaZEAszuP5"
                                        + "ccLsEfqCpwdSvVVt479DCZrw" + "jSHrJVwaja9WQaWAmEY9NsPv" +
                                        "EHKnFwHTGAvPXpjpCxkbedYq" + "uEauLvZLphwmJLUteZ4QAXU6" +
                                        "Z4F3PDmh3wsQXvSctQBHvNWf";

        private static readonly byte[] Salt = ASCIIEncoding.ASCII.GetBytes(SaltData);
        private static bool VerifyClass()
        {

            try
            {

                string testPlainText = "TestValue123";
                string testSecretKey = "TestSecretKey456";

                string encrypted = EncryptAES(testPlainText, testSecretKey);

         
                string decrypted = DecryptAES(encrypted, testSecretKey);

               
                return testPlainText.Equals(decrypted);
            }
            catch (Exception)
            {
               
                return false;
            }
        }
        private static string EncryptAES(string PlainText, string SecretKey)
        {
            if (string.IsNullOrEmpty(PlainText))
                throw new ArgumentNullException(nameof(PlainText));
            if (string.IsNullOrEmpty(SecretKey))
                throw new ArgumentNullException(nameof(SecretKey));

            string outStr; 
            RijndaelManaged aesAlg = null;

            try
            {
                
                var key = new Rfc2898DeriveBytes(SecretKey, Salt);

               
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

               
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

              
                using (var msEncrypt = new MemoryStream())
                {
                  
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                          
                            swEncrypt.Write(PlainText);
                        }
                    }

                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                aesAlg?.Clear();
            }

      
            return outStr;
        }
        private static string DecryptAES(string CipherText, string SecretKey)
        {
            if (string.IsNullOrEmpty(CipherText))
                throw new ArgumentNullException(nameof(CipherText));
            if (string.IsNullOrEmpty(SecretKey))
                throw new ArgumentNullException(nameof(SecretKey));

          
            RijndaelManaged aesAlg = null;

            string plaintext;

            try
            {
               
                var key = new Rfc2898DeriveBytes(SecretKey, Salt);

                          
                var bytes = Convert.FromBase64String(CipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                 
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                 
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))


                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
        public static string Create(string value)
        {
            return EncryptAES(value,
                "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }

        public static string Validate(string value)
        {
            return DecryptAES(value,
                "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }
    }

}
