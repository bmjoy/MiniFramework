using System;
using UnityEngine;
using MiniFramework;
using System.Collections.Generic;
using ProtoBuf;
public class SerializeExample : MonoBehaviour
{
    public SerializeObj Obj;
    public string serializeContent;
    // Use this for initialization
    void Start()
    {
        SerializeObj obj = new SerializeObj();
        obj.id = 1;
        obj.name = "haha";
        obj.Age.Add(1.1415926d);
        byte[] data = SerializeUtil.Json.Serialize(obj);
        FileUtil.WriteToLocalAsync(data, Application.streamingAssetsPath + "/json",null);
        byte[] data2 = FileUtil.ReadFromLocal(Application.streamingAssetsPath + "/json");
       // serializeContent = BitConverter.ToString(data);
        Obj = SerializeUtil.Json.Deserialize<SerializeObj>(data2);
    }
}
[Serializable]
[ProtoContract]
public class SerializeObj
{
    [ProtoMember(1)]
    public int id;
    [ProtoMember(2)]
    public string name;
    [ProtoMember(3)]
    public List<double> Age = new List<double>();
}
