## ResManager资源管理模块
* 资源下载  
	```
	//支持断点续传
	string url ="www.test.com/test.bin";
	string path = Application.streamingAssetsPath;
	var load = new FileDownloader(url,path);
	load.Download();
	```
* AB包加载
## MessageManager消息管理模块
* 消息注册
	·
	 MsgManager.Instance.RegisterMsg(this, "MsgID", ReceiveCallback);
	·
* 消息发送
	`
	MsgManager.Instance.SendMsg("MsgID", "Hello");
	`
## IO文件操作模块
* 支持AES、DES、MD5、RSA加密
	···
	//AES加密解密
	SecurityFactory.AES.Encrypt();
	SecurityFactory.AES.Decrypt();
	
	//DES加密解密
	SecurityFactory.DES.Encrypt();
	SecurityFactory.DES.Decrypt();
	
	//MD5加密解密
	SecurityFactory.MD5.Encrypt();
	···	
	···
	//RSA加密解密
	string normalText = "Hello";
	string publickey;
    string privatekey;
    SecurityFactory.RSA.GenerateKey(out privatekey, out publickey);
    var EncryptText = SecurityFactory.RSA.Encrypt(normalText, publickey);
    var DecryptText = SecurityFactory.RSA.Decrypt(EncryptText, privatekey);
	···
* 序列化和反序列化	
	···
	//Binary
	SerializeFactory.Binary.Serialize();
	SerializeFactory.Binary.Deserialize<T>();
	
	//Xml
	SerializeFactory.Xml.Serialize();
	SerializeFactory.Xml.Deserialize<T>();
	
	//Json
	SerializeFactory.Json.Serialize();
	SerializeFactory.Json.Deserialize<T>();
	
	//ProtoBuff
	SerializeFactory.ProtoBuff.Serialize();
	SerializeFactory.ProtoBuff.Deserialize<T>();
	···
## 网络通信模块
>	>支持封包解包
* TCP异步
* UDP异步

	
