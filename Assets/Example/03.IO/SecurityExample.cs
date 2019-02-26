using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class SecurityExample : MonoBehaviour
{
    public string key = "123xxx";
    public string NormalText;
    public string EncryptText;
    public string DecryptText;
    // Start is called before the first frame update
    void Start()
    {
        string publickey;
        string privatekey;
        SecurityUtil.RSA.GenerateKey(out privatekey, out publickey);
        EncryptText = SecurityUtil.RSA.Encrypt(NormalText, publickey);
        DecryptText = SecurityUtil.RSA.Decrypt(EncryptText, privatekey);

        EncryptText = SecurityUtil.DES.Encrypt(NormalText, key);
        DecryptText = SecurityUtil.DES.Decrypt(EncryptText, key);
    }
}
