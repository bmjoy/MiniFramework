using UnityEngine;
using MiniFramework;
public class EncryptExample : MonoBehaviour
{
    public string Text;
    // Use this for initialization
    void Start()
    {
        Debug.Log("明文：" + Text);

        string md5 = MD5.Encrypt(Text);//md5加密
        string des = DES.Encrypt(Text);//des加密
        string aes = AES.Encrypt(Text);//aes加密

        string privateKey = "";
        string publicKey = "";

        RSA.RSAKey(out privateKey, out publicKey);//生成公钥和私钥
        string rsa = RSA.Encrypt(Text, publicKey);//rsa使用公钥加密

        Debug.Log("md5:" + md5);
        Debug.Log("des:" + des);
        Debug.Log("aes:" + aes);
        Debug.Log("rsa:" + rsa);

        Debug.Log("解密后-------------------------");

        des = DES.Decrypt(des);
        aes = AES.Decrypt(aes);
        rsa = RSA.Decrypt(rsa, privateKey);
        Debug.Log("md5:" + md5);
        Debug.Log("des:" + des);
        Debug.Log("aes:" + aes);
        Debug.Log("rsa:" + rsa);
    }
}
