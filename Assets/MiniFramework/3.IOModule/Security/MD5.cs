using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class MD5 : SecurityUtil
    {
        public MD5() { }
        public override byte[] Encrypt(byte[] normalData,string key =null)
        {
            byte[] bytes = normalData;
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            return md5;
        }
    }
}