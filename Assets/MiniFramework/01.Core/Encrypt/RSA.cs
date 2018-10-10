using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
namespace MiniFramework
{
    public static class RSA
    {
        public static string Encrypt(string normalText,string xmlPublicKey)
        {
            byte[] bytes = Encoding.Default.GetBytes(normalText);
            CspParameters cp = new CspParameters();
            //cp.KeyContainerName = "mini";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(xmlPublicKey);
            byte[] encryptBytes = rsa.Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }
        public static string Decrypt(string encryptText,string xmlPrivateKey)
        {
            try
            {
                var bytes = Convert.FromBase64String(encryptText);
                CspParameters cp = new CspParameters();
                //cp.KeyContainerName = "mini";
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
                rsa.FromXmlString(xmlPrivateKey);
                var decryptBytes = rsa.Decrypt(bytes, false);
                return Encoding.Default.GetString(decryptBytes);
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

        public static void RSAKey(out string xmlPrivateKey, out string xmlPublicKey)
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

