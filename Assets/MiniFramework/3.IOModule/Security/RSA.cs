using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class RSA : SecurityUtil
    {
        public RSA() { }
        public override byte[] Encrypt(byte[] normalData,string xmlPublicKey)
        {
            var bytes = normalData;
            CspParameters cp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(xmlPublicKey);
            byte[] encryptBytes = rsa.Encrypt(bytes, false);
            return encryptBytes;
        }
        public override byte[] Decrypt(byte[] encryptData,string xmlPrivateKey)
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

