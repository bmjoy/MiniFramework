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
        public readonly Dictionary<string, UIPanel> UIPanelDict = new Dictionary<string, UIPanel>();
        public void Init()
        {
            UIRoot = GameObject.Find("UI Root");
            if (UIRoot != null)
            {
                UIRoot.transform.SetParent(transform);
            }
            else
            {
                UIRoot = Resources.Load("UI/UI Root") as GameObject;
                UIRoot = Instantiate(UIRoot, transform);
            }
            GetCanvas();
            GetCamera();
            GetEventSystem();
            GetUI();
        }
        void GetCanvas()
        {
            Canvas = UIRoot.GetComponentInChildren<Canvas>();
        }
        void GetCamera()
        {
            UICamera = UIRoot.GetComponentInChildren<Camera>();
        }

        void GetEventSystem()
        {
            EventSystem = UIRoot.GetComponentInChildren<EventSystem>();
        }

        void GetUI()
        {
            for (int i = 0; i < Canvas.transform.childCount; i++)
            {
                UIPanel panel = Canvas.transform.GetChild(i).GetComponent<UIPanel>();
                if (panel != null)
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }

        public void OpenUI(string name)
        {
            if (UIPanelDict.ContainsKey(name))
                UIPanelDict[name].Open();
        }
        public void CloseUI(string name)
        {
            if (UIPanelDict.ContainsKey(name))
                UIPanelDict[name].Close();
        }

        public void LoadUIFromResources(string path)
        {
            GameObject ui = Resources.Load(path) as GameObject;
            if (ui != null)
            {
                ui = Instantiate(ui, Canvas.transform);
                ui.SetActive(false);
                UIPanel panel = ui.GetComponent<UIPanel>();
                if (panel != null)
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }
        public void LoadUIFromAssetBundle(AssetBundle ab,string name)
        {
            GameObject ui = ab.LoadAsset(name) as GameObject;
            if(ui != null)
            {
                ui = Instantiate(ui, Canvas.transform);
                ui.SetActive(false);
                UIPanel panel = ui.GetComponent<UIPanel>();
                if (panel != null)
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }

        public void DestroyUI(string name)
        {
            if (UIPanelDict.ContainsKey(name))
            {
                Destroy(UIPanelDict[name].gameObject);
                UIPanelDict.Remove(name);
            }
               
        }
    }
}
