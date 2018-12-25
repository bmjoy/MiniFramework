using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace MiniFramework
{
    public abstract class SecurityFactory
    {
        public static DES DES { get => new DES(); }
        public static AES AES { get => new AES(); }
        public static RSA RSA { get => new RSA(); }
        public static MD5 MD5 { get => new MD5(); }
        public virtual string Encrypt(string normalText) { return ""; }
        public virtual string Encrypt(string normalText, string key) { return ""; }
        public virtual string Decrypt(string encryptText, string key) { return ""; }
    }
}