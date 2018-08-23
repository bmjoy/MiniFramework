using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniFramework;
using System;
using System.IO;

public class XmlExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //A xml = new A();
        //B b = new B() { b = new Vector3(1, 2, 3), a =new bool[] {false,true } };

        //xml.a = 111;
        //xml.b = "xxx";
        //xml.c = new float[] { 0.1f, 0.2f };
        //xml.blist = new List<B>();
        //xml.blist.Add(b);
        //if (SerializeUtil.SerializeToXml(xml, Application.streamingAssetsPath + "/A.xml"))
        //{
        //    Debug.Log("序列化成功");
        //}
        //xml = SerializeUtil.DeserializeFromXML<A>(Application.streamingAssetsPath + "/A.xml");
        File.Move(Application.streamingAssetsPath + "/A.xml",Application.streamingAssetsPath+"/XML");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[Serializable]
public class A
{
    public int a { get; set; }
    public string b { get; set; }
    public float[] c { get; set; }
    public List<B> blist;
}
[Serializable]
public class B
{
    public Vector3 b { get; set; }
    public bool[] a { get; set; }
}
