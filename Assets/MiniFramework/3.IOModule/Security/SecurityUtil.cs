namespace MiniFramework
{
    public abstract class SecurityUtil
    {
        public static DES DES { get { return new DES(); } }
        public static AES AES { get { return new AES(); } }
        public static RSA RSA { get { return new RSA(); } }
        public static MD5 MD5 { get { return new MD5(); } }
        public abstract string Encrypt(string normalText, string key = null);
        public virtual string Decrypt(string encryptText, string key) { return ""; }
    }
}