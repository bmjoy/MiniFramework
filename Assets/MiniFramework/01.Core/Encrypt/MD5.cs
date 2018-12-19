using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class MD5 : IEncrypt
    {
        public MD5() { }
        public string Encrypt(string normalTxt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(normalTxt);
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return Convert.ToBase64String(md5);
        }
    }

}
