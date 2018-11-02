using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace MiniFramework { 
    public static class DES
    {
        public static byte[] KeyIV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08};
        public static string Key = "miniframework";
        public static string Encrypt(string normalTxt)
        {
            var bytes = Encoding.UTF8.GetBytes(normalTxt);
            var key = Encoding.UTF8.GetBytes(Key.PadRight(8, '0').Substring(0, 8));
            using (MemoryStream ms = new MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = key;
                des.IV = KeyIV;
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string Decrypt(string encryptTxt)   
        {
            try
            {
                var bytes= Convert.FromBase64String(encryptTxt);
                var key = Encoding.UTF8.GetBytes(Key.PadRight(8, '0').Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    des.Key = key;
                    des.IV = KeyIV;
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return "";
            }
        }

    }
}
