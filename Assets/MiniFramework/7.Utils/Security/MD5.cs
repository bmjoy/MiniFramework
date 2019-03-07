using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public static class MD5
    {
        public static byte[] Encrypt(string content){
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Encrypt(bytes);
        }
        public static byte[] Encrypt(byte[] normalData)
        {
            byte[] bytes = normalData;
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return md5;
        }
    }
}