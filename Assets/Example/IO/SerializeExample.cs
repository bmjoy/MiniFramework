using UnityEngine;
using MiniFramework;
using System;
using ProtoBuf;

public class SerializeExample : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        FileUtil.CreateDirectoryIfNoExist(Application.streamingAssetsPath);

        TestClass tc = new TestClass() { age = 20, name = "xx", isBoy = true };
        SerializeUtil.SerializeToBinary(tc, Application.streamingAssetsPath + "/Binary.bin");
        SerializeUtil.SerializeToXml(tc, Application.streamingAssetsPath + "/Xml.xml");
        string json = SerializeUtil.SerializeToJson(tc);
        byte[] bytes = SerializeUtil.SerializeToProtoBuff(tc);
        MiniFramework.FileUtil.SaveBinaryToLocal(bytes, Application.streamingAssetsPath + "/Binary2.bin");
        bytes = FileUtil.ReadBinaryFromLocal(Application.streamingAssetsPath + "/Binary2.bin");
        tc = SerializeUtil.DeserializeFromBinary<TestClass>(Application.streamingAssetsPath + "/Binary.bin");
        tc = SerializeUtil.DeserializeFromXML<TestClass>(Application.streamingAssetsPath + "/Xml.xml");
        tc = SerializeUtil.DeserializeFromJson<TestClass>(json);
        tc = SerializeUtil.DeserializeFromProtoBuff<TestClass>(bytes);
    }
}
[Serializable]
[ProtoContract]
public class TestClass
{
    [ProtoMember(1)]
    public int age { get; set; }
    [ProtoMember(2)]
    public string name { get; set; }
    [ProtoMember(3)]
    public bool isBoy { get; set; }
}
