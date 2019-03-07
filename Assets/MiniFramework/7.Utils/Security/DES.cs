using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public static class DES
    {
        //初始化向量
        public static byte[]  KeyIV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
         public static byte[] Encrypt(string content, string key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Encrypt(bytes,key);
        }
        public static byte[] Encrypt(byte[] normalData,string key)
        {
            var bytes = normalData;
            using (MemoryStream ms = new MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key =Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                des.IV = KeyIV;
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
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
                using (MemoryStream ms = new MemoryStream())
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                    des.IV = KeyIV;
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
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
