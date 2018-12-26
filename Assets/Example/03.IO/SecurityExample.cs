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
        SecurityFactory.RSA.GenerateKey(out privatekey, out publickey);
        EncryptText = SecurityFactory.RSA.Encrypt(NormalText, publickey);
        DecryptText = SecurityFactory.RSA.Decrypt(EncryptText, privatekey);
    }
}
