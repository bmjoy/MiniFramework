## 支持模块
* 单例模板泛型实现，支持继承MonoBehaviour和非继承MonoBehaviour类
* 逻辑队列，支持链式调用
	```
	this.Delay(3f, () => { Debug.Log("3f后执行"); });
    this.Sequence().Delay(3f).Event(() => { Debug.Log("3f后执行"); }).Until(() => Input.GetKeyDown(KeyCode.Space)).Event(()=> { Debug.Log("按下空格键执行"); }).Execute();
	```
* 对象池，支持类对象和Prefab
* 简单有限状态机实现
* 支持AssetBundle多平台打包
## ResManager资源管理模块
* 资源下载 ，支持断点续传
	```
	string url ="www.test.com/test.bin";
	string path = Application.streamingAssetsPath;
	var load = new FileDownloader(url,path);
	load.Download();
	```
* AB包加载 支持差异化对比
>	>同一AB包需等待释放后，才能再次加载
## MsgManager消息管理模块
* 消息注册  
	`
	 MsgManager.Instance.RegisterMsg(this, "MsgID", ReceiveCallback);
	`
* 消息发送  
	`
	MsgManager.Instance.SendMsg("MsgID", "Hello");
	`
## IO文件操作模块
* 支持AES、DES、MD5、RSA加密  
	```
	//AES加密解密
	SecurityFactory.AES.Encrypt();
	SecurityFactory.AES.Decrypt();
	
	//DES加密解密
	SecurityFactory.DES.Encrypt();
	SecurityFactory.DES.Decrypt();
	
	//MD5加密
	SecurityFactory.MD5.Encrypt();
	```
	```
	//RSA加密解密
	string normalText = "Hello";
	string publickey;
    string privatekey;
    SecurityFactory.RSA.GenerateKey(out privatekey, out publickey);
    var EncryptText = SecurityFactory.RSA.Encrypt(normalText, publickey);
    var DecryptText = SecurityFactory.RSA.Decrypt(EncryptText, privatekey);
	```
* 支持对象序列化和反序列化	
	```
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
	```
## 网络通信模块
* 支持封包解包
>	>解决三种粘包现象
>	>1、接收到部分包A
>	>2、接收到完整包A+部分包B
>	>3、接收到完整包A+完整包B+.....+部分包X
* 支持TCP异步
* 支持UDP异步

	
