using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public static class MD5
    {
        public static string Encrypt(string normalTxt)
        {
            byte[] bytes = Encoding.Default.GetBytes(normalTxt);
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return Convert.ToBase64String(md5);
        }
    }

}
