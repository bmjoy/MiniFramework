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
        private string AssetBundlePath;
        private readonly Dictionary<string, UIPanel> UIPanelDict = new Dictionary<string, UIPanel>();
        private readonly Stack<UIPanel> UIPanelStack = new Stack<UIPanel>();
        public void Init()
        {
            AssetBundlePath = Application.streamingAssetsPath + "/AssetBundle/StandaloneWindows/ui";
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

            ResLoader.Instance.LoadAssetBundle(this, AssetBundlePath, LoadUIFromAssetBundle);
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
                    UIPanelStack.Push(panel);
                }
            }
        }

        public void OpenUI(string name)
        {
            if (UIPanelDict.ContainsKey(name))
            {
                UIPanel panel = UIPanelDict[name];
                panel.SetLayerToTop();
                panel.Open();                
            }             
        }
        public void CloseUI(string name)
        {
            if (UIPanelDict.ContainsKey(name))
            {
                UIPanel panel = UIPanelDict[name];
                panel.SetLayerToButtom();
                panel.Close();
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
        void LoadUIFromAssetBundle(AssetBundle ab)
        {
            GameObject[] objects = ab.LoadAllAssets<GameObject>();
            for (int i = 0; i < objects.Length; i++)
            {
                GameObject ui = Instantiate(objects[i], Canvas.transform);
                UIPanel panel = ui.GetComponent<UIPanel>();
                if (panel != null)
                {
                    UIPanelDict.Add(panel.name, panel);
                }
            }
        }

    }
}
