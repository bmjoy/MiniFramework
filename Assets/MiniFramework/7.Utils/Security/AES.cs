using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework{
     public static class AES{
        //初始化向量
        public static byte[]  KeyIV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        public static byte[] Encrypt(string content, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Encrypt(bytes,key);
        }
        public static byte[] Encrypt(byte[] normalData,string key)
        {
            var bytes = normalData;
            SymmetricAlgorithm aes = Rijndael.Create();
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            aes.IV = KeyIV;
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }
        public static byte[] Decrypt(string content,string key){
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Decrypt(bytes,key);
        }
        public static byte[] Decrypt(byte[] encryptData,string key)
        {
            try
            {
                var bytes = encryptData;
                SymmetricAlgorithm aes = Rijndael.Create();
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
                aes.IV = KeyIV;
                using (MemoryStream ms = new MemoryStream())
                {
                    CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}