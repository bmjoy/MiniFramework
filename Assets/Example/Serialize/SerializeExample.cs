using System;
using UnityEngine;
using MiniFramework;
using System.Collections.Generic;
using ProtoBuf;
public class SerializeExample : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SerializeBinary();
        SerializeXml();
        SerializeJson();
        SerializeProtobuff();

        DeserializeFromBinary();
        DeserializeFromXml();
    }

    public void SerializeBinary()
    {
        ToBinary tb = new ToBinary();
        tb.id = 1;
        tb.name = "Binary";
        tb.Age = new List<float>();
        tb.Age.Add(1.1415926f);
        Binary.Serialize(tb, Application.streamingAssetsPath + "/binary");
        Debug.Log("binary序列化完成");
    }

    public void SerializeXml()
    {
        ToXml tb = new ToXml();
        tb.id = 1;
        tb.name = "Xml";
        tb.Age = new List<float>();
        tb.Age.Add(1.1415926f);
        Xml.Serialize(tb, Application.streamingAssetsPath + "/xml");
        Debug.Log("xml序列化完成");
    }

    public void SerializeJson()
    {
        ToJson tb = new ToJson();
        tb.id = 1;
        tb.name = "json";
        tb.Age = new List<float>();
        tb.Age.Add(1.1415926f);
        string value = Json.Serialize(tb);
        Debug.Log("Json序列化完成：" + value);
        tb = Json.Deserialize<ToJson>(value);
        Debug.Log("Json反序列化name：" + tb.name);
    }
    public void SerializeProtobuff()
    {
        ToProtoBuff tb = new ToProtoBuff();
        tb.id = 1;
        tb.name = "ProtoBuff";
        tb.Age = new List<float>();
        tb.Age.Add(1.1415926f);
        byte[] data = ProtoBuff.Serialize(tb);
        Debug.Log("Protobuff序列化完成：" + BitConverter.ToString(data));
        tb = ProtoBuff.Deserialize<ToProtoBuff>(data);
        Debug.Log("Protobuff反序列化name："+tb.name);
    }

    public void DeserializeFromBinary()
    {
        ToBinary tBinary = Binary.Deserialize<ToBinary>(Application.streamingAssetsPath + "/binary");
        Debug.Log("Binary反序列化name：" + tBinary.name);
    }
    public void DeserializeFromXml()
    {
        ToXml toXml = Xml.Deserialize<ToXml>(Application.streamingAssetsPath + "/xml");
        Debug.Log("Xml反序列化name：" + toXml.name);
    }
}
[Serializable]
public class ToBinary
{
    public int id;
    public string name;
    public List<float> Age;
}
[Serializable]
public class ToXml
{
    public int id;
    public string name;
    public List<float> Age;
}

[Serializable]
public class ToJson
{
    public int id;
    public string name;
    public List<float> Age;
}
[ProtoContract]
public class ToProtoBuff
{
    [ProtoMember(1)]
    public int id;
    [ProtoMember(2)]
    public string name;
    [ProtoMember(3)]
    public List<float> Age;
}
