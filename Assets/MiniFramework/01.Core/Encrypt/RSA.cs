using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace MiniFramework
{
    public class RSA : IEncrypt, IDecrypt
    {
        public string XmlPublicKey { get; set; }
        public string XmlPrivateKey { get; set; }
        public RSA() { }
        public string Encrypt(string normalText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(normalText);
            CspParameters cp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(XmlPublicKey);
            byte[] encryptBytes = rsa.Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }
        public string Decrypt(string encryptText)
        {
            try
            {
                var bytes = Convert.FromBase64String(encryptText);
                CspParameters cp = new CspParameters();
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
                rsa.FromXmlString(XmlPrivateKey);
                var decryptBytes = rsa.Decrypt(bytes, false);
                return Encoding.UTF8.GetString(decryptBytes);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return "";
            }
        }

        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>

        public void GenerateKey(out string xmlPrivateKey, out string xmlPublicKey)
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

