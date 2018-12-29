## ResManager资源管理模块
* 资源下载  
	`资源下载支持断点续传`
	```
	string url ="www.test.com/test.bin";
	string path = Application.streamingAssetsPath;
	var load = new FileDownloader(url,path);
	load.Download();
	```
* AB包加载
## MessageManager消息管理模块
* 消息注册分发
## IO文件操作模块
* 支持AES、DES、MD5、RSA加密
	
