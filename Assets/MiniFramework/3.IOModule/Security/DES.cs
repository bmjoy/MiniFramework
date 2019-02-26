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
        public override string Encrypt(string normalText,string key)
        {
            var bytes = Encoding.UTF8.GetBytes(normalText);
            using (MemoryStream ms = new MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key =Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                des.IV = KeyIV;
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
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
                using (MemoryStream ms = new MemoryStream())
                {
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                    des.IV = KeyIV;
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
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
