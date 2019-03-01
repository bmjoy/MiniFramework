namespace MiniFramework
{
    public abstract class SecurityUtil
    {
        public static DES DES { get { return new DES(); } }
        public static AES AES { get { return new AES(); } }
        public static RSA RSA { get { return new RSA(); } }
        public static MD5 MD5 { get { return new MD5(); } }
        public abstract byte[] Encrypt(byte[] normalData, string key = null);
        public virtual byte[] Decrypt(byte[] encryptData, string key) { return null; }
    }
}