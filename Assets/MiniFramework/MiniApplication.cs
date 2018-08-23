using MiniFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiniFramework
{
    public class MiniApplication : MonoBehaviour
    {
        public string UIPath;
        // Use this for initialization
        void Start()
        {
            UIManager.Instance.Init();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}