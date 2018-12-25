using System;
using System.Security.Cryptography;
using System.Text;
namespace MiniFramework
{
    public class RSA : SecurityFactory
    {
        public RSA() { }
        public override string Encrypt(string normalText,string xmlPublicKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(normalText);
            CspParameters cp = new CspParameters();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(xmlPublicKey);
            byte[] encryptBytes = rsa.Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }
        public override string Decrypt(string encryptText,string xmlPrivateKey)
        {
            try
            {
                var bytes = Convert.FromBase64String(encryptText);
                CspParameters cp = new CspParameters();
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
                rsa.FromXmlString(xmlPrivateKey);
                var decryptBytes = rsa.Decrypt(bytes, false);
                return Encoding.UTF8.GetString(decryptBytes);
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

