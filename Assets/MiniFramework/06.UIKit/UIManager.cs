using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniFramework
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public GameObject UIRoot;
        public Canvas Canvas;
        public Camera UICamera;
        public EventSystem EventSystem;
        public readonly Dictionary<int, GameObject> UIPanelDict = new Dictionary<int, GameObject>();
        public void Init()
        {
            GameObject obj = Resources.Load("UI/UI Root") as GameObject;
            UIRoot = Instantiate(obj, transform);
        }

        public void OpenUI(int id)
        {

        }
        public void OpenUI(string name)
        {

        }

        public void CloseUI(int id)
        {

        }
        public void CloseUI(string name)
        {

        }
    }
}
