using MiniFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSingleton : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestMono.Instance.XX();
        Test.Instance.XX();
    }
}

public class TestMono : MonoSingleton<TestMono>
{
    public void XX()
    {
        Debug.Log("TestMono");
    }
}

public class Test: Singleton<Test>
{
    private Test() { }
    public void XX()
    {
        Debug.Log("Test");
    }
}