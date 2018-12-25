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
        obj.Age.Add(1.1415926f);
        byte[] data = SerializeFactory.ProtoBuff.Serialize(obj);
        FileUtil.SaveBinaryToLocal(data,Application.streamingAssetsPath+"/protobuff");
        serializeContent = BitConverter.ToString(data);

        Obj = SerializeFactory.ProtoBuff.Deserialize<SerializeObj>(data);
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
    public List<float> Age = new List<float>();
}
