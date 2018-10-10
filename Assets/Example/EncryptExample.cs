using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
public class EncryptExample : MonoBehaviour {
    public string Text;
	// Use this for initialization
	void Start () {
        Debug.Log("明文：" + Text);
        string md5 = MD5.Encrypt(Text);
        string des = DES.Encrypt(Text);
        string privateKey = "";
        string publicKey = "";
        RSA.RSAKey(out privateKey, out publicKey);
        string rsa = RSA.Encrypt(Text, publicKey);
        
        Debug.Log("md5:" + md5);
        Debug.Log("des:" + des);
        Debug.Log("rsa:" + rsa);
        Debug.Log("解密后-------------------------");
        des = DES.Decrypt(des);

        rsa = RSA.Decrypt(rsa, privateKey);
        Debug.Log("md5:" + md5);
        Debug.Log("des:" + des);
        Debug.Log("rsa:" + rsa);
    }
}
