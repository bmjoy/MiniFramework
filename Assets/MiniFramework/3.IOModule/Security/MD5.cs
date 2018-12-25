using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class MD5 : SecurityFactory
    {
        public MD5() { }
        public override string Encrypt(string normalText,string key =null)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(normalText);
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return Convert.ToBase64String(md5);
        }
    }
}