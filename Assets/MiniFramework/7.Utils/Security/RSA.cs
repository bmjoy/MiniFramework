using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public static class RSA
    {
        public static byte[] Encrypt(string content,string xmlPublicKey){
            byte[] bytes= Encoding.UTF8.GetBytes(content);
            return Encrypt(bytes,xmlPublicKey);
        }
        public static byte[] Encrypt(byte[] normalData,string xmlPublicKey)
        {
            var bytes = normalData;
            CspParameters cp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(xmlPublicKey);
            byte[] encryptBytes = rsa.Encrypt(bytes, false);
            return encryptBytes;
        }
        public static byte[] Decrypt(string content,string xmlPrivateKey){
            byte[] bytes= Encoding.UTF8.GetBytes(content);
            return Decrypt(bytes,xmlPrivateKey);
        }
        public static byte[] Decrypt(byte[] encryptData,string xmlPrivateKey)
        {
            try
            {
                var bytes = encryptData;
                CspParameters cp = new CspParameters();
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
                rsa.FromXmlString(xmlPrivateKey);
                var decryptBytes = rsa.Decrypt(bytes, false);
                return decryptBytes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public static void GenerateKey(out string xmlPrivateKey, out string xmlPublicKey)
        {
            try
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlPrivateKey = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

