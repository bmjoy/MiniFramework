using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class DES:SecurityUtil
    {
        public byte[] KeyIV { get; set; }
        public DES()
        {
            KeyIV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        }
        public override byte[] Encrypt(byte[] normalData,string key)
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
        public override byte[] Decrypt(byte[] encryptData,string key)
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
