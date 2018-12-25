using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework{
     public class AES:SecurityFactory{
        public byte[] KeyIV { get; set; }
        public AES()
        {
            KeyIV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        }
        public override string Encrypt(string normalText,string key)
        {
            var bytes = Encoding.UTF8.GetBytes(normalText);
            SymmetricAlgorithm aes = Rijndael.Create();
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            aes.IV = KeyIV;
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public override string Decrypt(string encryptText,string key)
        {
            try
            {
                var bytes = Convert.FromBase64String(encryptText);
                SymmetricAlgorithm aes = Rijndael.Create();
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
                aes.IV = KeyIV;
                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}